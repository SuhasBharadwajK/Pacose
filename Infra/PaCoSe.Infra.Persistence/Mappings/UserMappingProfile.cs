using PaCoSe.Infra.Mappings;
using PaCoSe.Models;

namespace PaCoSe.Infra.Persistence.Mappings
{
    public class UserMappingProfile : MappingProfile
    {
        public UserMappingProfile()
        {
            this.CreateMap<User, Data.Model.User>();
            this.CreateMap<Data.Model.User, User>();

            this.CreateMap<UserProfile, Data.Model.UserProfile>();
            this.CreateMap<Data.Model.UserProfile, UserProfile>();

            this.CreateMap<Data.Model.UserProfileView, User>().AfterMap((src, dest) =>
            {
                dest.UserProfile = new UserProfile
                {
                    Id = (int) src.ProfileId,
                    Email = src.Email,
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    UserId = (int) src.Id,
                };
            });
        }
    }
}
