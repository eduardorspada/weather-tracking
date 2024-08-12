using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iVertion.Domain.Account
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(string email, string password);

        Task<bool> RegisterUser(
            string email,
            string password,
            bool isEnabled,
            int userProfileId,
            int personId
            );

        Task Logout();
    }
}