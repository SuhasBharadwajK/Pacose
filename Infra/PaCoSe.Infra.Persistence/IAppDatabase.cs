using System.Collections.Generic;

namespace PaCoSe.Infra.Persistence
{
    public interface IAppDatabase : PetaPoco.IDatabase
    {
        T Get<T>(int id);

        T GetDeleted<T>(int id);

        List<T> GetAll<T>();

        List<T> GetAllIncludingDeleted<T>();

        new int Insert(object poco);

        new int Update(object poco, IEnumerable<string> columns);

        int Delete<T>(int id, bool isSoftDelete = true);

        void InsertBulk<T>(IEnumerable<T> records, bool useTransaction = false, bool includeAuditColumns = true, int batchSize = 100) where T : new();

        void UpdateBulk<T>(IEnumerable<T> records, IEnumerable<string> columnsToUpdate, bool useTransaction = false, int batchSize = 100) where T : new();

        void DeleteBulk<T>(IEnumerable<T> records, bool useTransaction = false, int batchSize = 100) where T : new();
    }
}
