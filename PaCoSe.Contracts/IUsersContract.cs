using PaCoSe.Models;
using System.Collections.Generic;

namespace PaCoSe.Contracts
{
    public interface IUsersContract
    {
        List<User> GetAllUsers();

        User GetUser(int id);

        User GetUserByUsername(string username);

        User AddUser(User user);

        User AddUserWithoutProfile(User user);

        bool InviteUser(int userId);

        bool DeleteUser(int id);

        User UpdateUser(int id, User user);

        bool DisableUser(int id);
    }
}
