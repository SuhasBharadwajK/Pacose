using PaCoSe.Core.Contracts;
using PaCoSe.Models;
using PetaPoco;

namespace PaCoSe.Core.Providers
{
    public class CoreDataProvider : ICoreDataContract
    {
        IDatabase Database;

        AutoMapper.IMapper Mapper;

        public CoreDataProvider(IDatabase database, AutoMapper.IMapper mapper)
        {
            this.Database = database;
            this.Mapper = mapper;
        }

        public User GetUser(string userName)
        {
            //var userDataModel = this.Database.FirstOrDefault<Data.Model.UserProfileDetailsView>("WHERE UserName = @0 AND IsDeleted = @1", userName, false);
            //return this.Mapper.MapTo<User>(userDataModel);
            // TODO;
            return null;
        }

        public Device GetDevice(string identifier)
        {
            // TODO;
            return null;
        }

        public bool UpdateUserSub(string userName, string sub)
        {
            //var result = this.Database.Update<Data.Model.User>("SET Sub = @0 WHERE UserName = @1", sub, userName);
            //return result > 0;
            // TODO;
            return false;
        }
    }
}
