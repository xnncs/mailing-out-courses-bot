using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    internal class User
    {
        public User(long id, string username, bool follow, bool admin)
        {
            this.id = id;

            this.username = username;

            this.follow = follow;

            this.admin = admin;
        }
        public long id { get; private set; }
        public string username { get; private set; }
        public bool admin { get; set; }
        public bool follow { get; set; }

    }
}
