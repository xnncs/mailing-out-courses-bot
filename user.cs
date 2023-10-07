namespace project
{
    internal class User
    {
        public User(long id, string username, bool admin, bool follow)
        {
            this.id = id;

            this.username = username;

            this.follow = follow;

            this.admin = admin;
        }
        public long id { get; set; }
        public string username { get; set; }
        public bool admin { get; set; }
        public bool follow { get; set; }

    }
}
