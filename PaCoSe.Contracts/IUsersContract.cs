using PaCoSe.Models;
using System.Collections.Generic;

namespace PaCoSe.Contracts
{
    public interface IUsersContract
    {
        List<User> GetAllUsers();

        User GetUser(int id);

        bool DeleteUser(int id);

        User UpdateUser(int id, User user);

        bool DisableUser(int id);
    }
}
