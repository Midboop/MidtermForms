using System;
using System.Collections.Generic;
using System.Text;

namespace ZestHealthApp.Tables
{
    class RegUserTable
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
