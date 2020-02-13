using System;
using System.Collections.Generic;
using System.Text;

namespace ZestHealthApp.Models
{
    public class Users
    {
        // All of the getters and setters that are stored inside of the Firebase database
        public string Email { get; set; }
        public string Password{ get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
    }
}
