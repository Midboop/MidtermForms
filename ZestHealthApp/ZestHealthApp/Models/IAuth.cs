using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZestHealthApp.Models
{
    public interface IAuth
    {
        // Login with email and password is true
        Task<string> LoginWithEmailPassword(string email, string password);
    }
}
