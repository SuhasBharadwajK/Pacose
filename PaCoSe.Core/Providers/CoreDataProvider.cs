using PaCoSe.Core.Contracts;
using PaCoSe.Core.Extensions;
using PaCoSe.Models;
using PetaPoco;
using System.Linq;

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

        public User GetUser(string username)
        {
            var userDataModel = this.Database.FirstOrDefault<Data.Model.UserProfileView>("WHERE Username = @0 AND IsActivated = @1", username, true);
            var user = this.Mapper.MapTo<User>(userDataModel);

            var devices = this.Database.Fetch<Data.Model.DeviceOwnerView>("WHERE OwnerId = @0", user.Id);
            user.OwnedDevices = this.Mapper.MapCollectionTo<Data.Model.DeviceOwnerView, Device>(devices).ToList();

            return user;
        }

        public Device GetDevice(string token)
        {
            var deviceDataModel = this.Database.FirstOrDefault<Data.Model.DeviceTokenView>("WHERE TokenString = @0", token);
            return this.Mapper.MapTo<Device>(deviceDataModel);
        }

        public bool UpdateUserSub(string userName, string sub)
        {
            var result = this.Database.Update<Data.Model.User>("SET Sub = @0 WHERE Username = @1", sub, userName);
            return result > 0;
        }
    }
}
