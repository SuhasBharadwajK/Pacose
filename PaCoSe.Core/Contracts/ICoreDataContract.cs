using PaCoSe.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaCoSe.Core.Contracts
{
    public interface ICoreDataContract
    {
        User GetUser(string userName);

        bool UpdateUserSub(string userName, string sub);

        Device GetDevice(string token);
    }
}
