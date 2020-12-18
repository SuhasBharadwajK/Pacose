using Microsoft.CSharp.RuntimeBinder;
using PaCoSe.Exceptions;
using PaCoSe.Infra.Context;
using PaCoSe.Infra.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaCoSe.Infra.Persistence
{
    public class AppDatabase : PetaPoco.Database, IAppDatabase
    {
        IRequestContext RequestContext { get; set; }

        public AppDatabase(IRequestContext requestContext, string connectionString, string provider) : base(connectionString, provider)
        {
            this.RequestContext = requestContext;
        }

        public T Get<T>(int id)
        {
            try
            {
                return base.First<T>("WHERE Id = @0 AND IsDeleted = @1", id, false);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException();
            }
        }

        public T GetDeleted<T>(int id)
        {
            try
            {
                return base.First<T>("WHERE Id = @0 AND IsDeleted = @1", id, true);
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException();
            }
        }

        public List<T> GetAll<T>()
        {
            return base.Fetch<T>("WHERE IsDeleted = @0", false);
        }

        public List<T> GetAllIncludingDeleted<T>()
        {
            return base.Fetch<T>();
        }

        public new int Insert(object poco)
        {
            try
            {
                dynamic entity = poco;
                entity.CreatedBy = this.RequestContext.EntityName;
                entity.DateCreated = DateTime.UtcNow;
                entity.ModifiedBy = this.RequestContext.EntityName;
                entity.DateModified = DateTime.UtcNow;
            }
            catch (RuntimeBinderException)
            {
            }

            return Convert.ToInt32(base.Insert(poco));
        }

        public new int Update(object poco, IEnumerable<string> columns)
        {
            if (columns == null)
            {
                columns = new List<string>();
            }

            try
            {
                dynamic entity = poco;
                entity.ModifiedBy = this.RequestContext.EntityName;
                entity.DateModified = DateTime.UtcNow;

                if (!columns.Contains("ModifiedBy"))
                {
                    columns = columns.Append("ModifiedBy");
                }

                if (!columns.Contains("DateModified"))
                {
                    columns = columns.Append("DateModified");
                }
            }
            catch (RuntimeBinderException)
            {
            }

            return Convert.ToInt32(base.Update(poco, columns));
        }

        public int Delete<T>(int id, bool isSoftDelete = true)
        {
            var poco = this.Get<T>(id);
            if (isSoftDelete)
            {
                var columns = new List<string> { "IsDeleted", "ModifiedBy", "DateModified" };
                try
                {
                    dynamic entity = poco;
                    entity.ModifiedBy = this.RequestContext.EntityName;
                    entity.DateModified = DateTime.UtcNow;
                    entity.IsDeleted = true;
                }
                catch (RuntimeBinderException)
                {
                }

                return base.Update(poco, columns);
            }

            return base.Delete(poco);
        }

        public void InsertBulk<T>(IEnumerable<T> records, bool useTransaction = false, bool includeAuditColumns = true, int batchSize = 100) where T : new()
        {
            if (records == null || records.Count() == 0)
            {
                return;
            }

            if (includeAuditColumns)
            {
                foreach (var record in records)
                {
                    record.SetProperty("CreatedBy", this.RequestContext.EntityName);
                    record.SetProperty("DateCreated", DateTime.UtcNow);
                    record.SetProperty("ModifiedBy", this.RequestContext.EntityName);
                    record.SetProperty("DateModified", DateTime.UtcNow);
                }
            }

            if (useTransaction)
            {
                using (var t = this.GetTransaction())
                {
                    this.Batch<T>(records, batchSize, item => this.GetInsertQuery(item));
                    t.Complete();
                }
            }
            else
            {
                Batch<T>(records, batchSize, item => this.GetInsertQuery(item));
            }
        }

        public void UpdateBulk<T>(IEnumerable<T> records, IEnumerable<string> columnsToUpdate, bool useTransaction = false, int batchSize = 100) where T : new()
        {
            if (columnsToUpdate == null)
            {
                columnsToUpdate = new List<string>();
            }

            if (records == null || records.Count() == 0)
            {
                return;
            }

            foreach (var record in records)
            {
                record.SetProperty("ModifiedBy", this.RequestContext.EntityName);
                record.SetProperty("DateModified", DateTime.UtcNow);
            }

            if (!columnsToUpdate.Contains("ModifiedBy"))
            {
                columnsToUpdate = columnsToUpdate.Append("ModifiedBy");
            }

            if (!columnsToUpdate.Contains("DateModified"))
            {
                columnsToUpdate = columnsToUpdate.Append("DateModified");
            }

            if (useTransaction)
            {
                using (var t = this.GetTransaction())
                {
                    this.Batch<T>(records, batchSize, item => this.GetUpdateQuery<T>(item, columnsToUpdate));
                    t.Complete();
                }
            }
            else
            {
                this.Batch<T>(records, batchSize, item => this.GetUpdateQuery<T>(item, columnsToUpdate));
            }
        }

        public void DeleteBulk<T>(IEnumerable<T> records, bool useTransaction = false, int batchSize = 100) where T : new()
        {
            if (records == null || records.Count() == 0)
            {
                return;
            }

            var softDeleteColumns = new string[] { "IsDeleted", "ModifiedBy", "DateModified" };

            foreach (var record in records)
            {
                record.SetProperty("IsDeleted", true);
                record.SetProperty("ModifiedBy", this.RequestContext.EntityName);
                record.SetProperty("DateModified", DateTime.UtcNow);
            }

            if (useTransaction)
            {
                using (var t = this.GetTransaction())
                {
                    this.Batch<T>(records, batchSize, item => this.GetUpdateQuery<T>(item, softDeleteColumns));
                    t.Complete();
                }
            }
            else
            {
                this.Batch<T>(records, batchSize, item => this.GetUpdateQuery<T>(item, softDeleteColumns));
            }
        }

        private void Batch<T>(IEnumerable<T> records, int batchSize, Func<T, string> buildBatchItem) where T : new()
        {
            int pageIndex = 1;
            IEnumerable<T> batch;
            while ((batch = records.Skip(batchSize * (pageIndex - 1)).Take(batchSize).ToList()).Count() > 0)
            {
                StringBuilder query = new StringBuilder();
                foreach (var item in batch)
                {
                    query.AppendLine(buildBatchItem(item));
                }

                if (!string.IsNullOrEmpty(query.ToString()))
                {
                    this.Execute(query.ToString());
                }

                pageIndex++;
            }
        }

        private string GetInsertQuery<T>(T obj) where T : new()
        {
            var objType = obj.GetType();
            var columns = new List<string>();
            var values = new List<string>();
            var primaryKey = string.Empty;
            var tableName = string.Empty;
            var attrs = Attribute.GetCustomAttributes(objType);

            foreach (var item in attrs)
            {
                var primaryKeyAttribute = item as PetaPoco.PrimaryKeyAttribute;
                var tableNameAttribute = item as PetaPoco.TableNameAttribute;
                if (primaryKeyAttribute != null)
                {
                    primaryKey = primaryKeyAttribute.Value;
                }

                if (tableNameAttribute != null)
                {
                    tableName = tableNameAttribute.Value;
                }
            }

            foreach (var item in objType.GetProperties())
            {
                if (item.Name != primaryKey)
                {
                    var value = item.GetValue(obj);
                    if (value != null)
                    {
                        columns.Add(item.Name);
                        var columnValue = value.ToString();
                        if (value is DateTime)
                        {
                            columnValue = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss:fff");
                        }

                        if (columnValue.Contains("@"))
                        {
                            columnValue = columnValue.Replace("@", "@@");
                        }

                        if (columnValue.Contains("'"))
                        {
                            columnValue = columnValue.ToString().Replace("'", "''");
                        }

                        values.Add(string.Format("'{0}'", columnValue));
                    }
                }
            }

            return string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, string.Join(", ", (from c in columns select string.Format("[{0}]", c)).ToArray()), string.Join(",", values));
        }

        private string GetUpdateQuery<T>(T obj, IEnumerable<string> columns) where T : new()
        {
            var objType = obj.GetType();
            var primaryKey = string.Empty;
            var primaryKeyValue = string.Empty;
            var tableName = string.Empty;
            var attrs = Attribute.GetCustomAttributes(objType);
            var sb = new StringBuilder();
            var index = 0;
            columns = columns.Select(c => c.ToLower());

            foreach (var item in attrs)
            {
                var primaryKeyAttribute = item as PetaPoco.PrimaryKeyAttribute;
                var tableNameAttribute = item as PetaPoco.TableNameAttribute;
                if (primaryKeyAttribute != null)
                {
                    primaryKey = primaryKeyAttribute.Value;
                }

                if (tableNameAttribute != null)
                {
                    tableName = tableNameAttribute.Value;
                }
            }

            foreach (var item in objType.GetProperties())
            {
                if (item.Name == primaryKey)
                {
                    primaryKeyValue = item.GetValue(obj).ToString();
                }

                if (columns.Contains(item.Name.ToLower()))
                {
                    var value = item.GetValue(obj);
                    if (value != null)
                    {
                        if (index > 0)
                        {
                            sb.Append(",");
                        }

                        var columnValue = value.ToString();
                        if (value is DateTime)
                        {
                            columnValue = ((DateTime)value).ToString("MM-dd-yyyy HH:mm:ss");
                        }

                        if (columnValue.Contains("@"))
                        {
                            columnValue = columnValue.Replace("@", "@@");
                        }

                        if (columnValue.Contains("'"))
                        {
                            columnValue = columnValue.ToString().Replace("'", "''");
                        }

                        sb.Append(string.Format("{0} = '{1}'", item.Name, columnValue));
                        index++;
                    }
                    else
                    {
                        if (item.PropertyType.Name == nameof(String))
                        {
                            if (index > 0)
                            {
                                sb.Append(",");
                            }

                            sb.Append(string.Format("{0} = NULL", item.Name));
                            index++;
                        }
                    }
                }
            }

            return string.Format("UPDATE {0} SET {1} where {2} = {3}", tableName, sb.ToString(), primaryKey, primaryKeyValue);
        }
    }
}
