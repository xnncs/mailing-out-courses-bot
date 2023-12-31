using Telegram.Bot.Types;
using Npgsql;
using System.ComponentModel;
using System.Text.Json;


namespace project
{
    class DataBase
    {
        public DataBase(Update update)
        {
            this.update = update;
            Id = update.Message.Chat.Id;

            bool containsUser = ContainsUser();

            if (!containsUser)
            {
                AddData();
            }
            GetData();

        }
        private Update update;


        private static string GetConnectionString()
        {
            const string jsonFilePath = @"configuration\configuration.json";

            static DataBaseConfiguration parseDataBaseConfiguration(string jsonFilePath)
            {
                Configuration configuration = JsonSerializer.Deserialize<Configuration>(jsonFilePath);

                return configuration.dataBaseConfiguration;
            }
            DataBaseConfiguration configuration = parseDataBaseConfiguration(jsonFilePath);


            return $"Server={configuration.host};Username={configuration.dbusername};Database={configuration.dataBaseName};Port={configuration.port};Password={configuration.password};SSLMode=Prefer";
        }

        private static string connectionString =  GetConnectionString(); 


        public long Id {get; private set;}
        public string Username {get; private set;}
        public bool Admin {get; private set;}
        public bool Follow {get; private set;}

        private void AddData()
        {
            Console.WriteLine("Adding data");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                Console.WriteLine("Opening connection");
                connection.Open();

                using (var command = new NpgsqlCommand("INSERT INTO users VALUES (@id, @username, @follow, @admin)", connection))
                {
                    command.Parameters.AddWithValue("id", update.Message.Chat.Id);
                    command.Parameters.AddWithValue("username", update.Message.Chat.Username);
                    command.Parameters.AddWithValue("follow", false);
                    command.Parameters.AddWithValue("admin", false);

                    Console.WriteLine($"Number of rows inserted = {command.ExecuteNonQuery()}");
                }
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> allUsers = new List<User>();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                Console.WriteLine("Opening connection");
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM users", connection))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        long id = reader.GetInt64(0);
                        string username = reader.GetString(1);
                        bool follow =  reader.GetBoolean(2);
                        bool admin = reader.GetBoolean(3);

                        allUsers.Add(new User() {
                            Id = id,
                            Username = username,
                            IsFollow = follow,
                            IsAdmin = admin
                        });
                    }
                }
            }

            return allUsers;
        }

        private void GetData()
        {
            Console.WriteLine("Geting data");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                Console.WriteLine("Opening connection");
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM users WHERE user_id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", Id);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Id = reader.GetInt64(0);
                        Username = reader.GetString(1);
                        Follow =  reader.GetBoolean(2);
                        Admin = reader.GetBoolean(3);
                    }
                }
            }
        }
        public void ChangeFollow(bool state)
        {
            Console.WriteLine($"Updating follow data to {state}");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                Console.WriteLine("Opening connection");
                connection.Open();

                using (var command = new NpgsqlCommand("UPDATE users SET isFollow = @state WHERE user_id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", Id);
                    command.Parameters.AddWithValue("state", state);

                    Console.WriteLine($"Number of rows updated = {command.ExecuteNonQuery()}");
                }
            }
        }
        private bool ContainsUser()
        {
            bool contains = false;
            using (var connection = new NpgsqlConnection(connectionString))
            {
                Console.WriteLine("Opening connection");
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE user_id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", Id);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int count = reader.GetInt32(0);
                        Console.WriteLine($"[test] count of users is {count}");
                        contains = count == 0 ? false : true;
                    }
                }
            }
            Console.WriteLine($"[test] contains = {contains}");
            return contains;
        }
        
        

    }
}