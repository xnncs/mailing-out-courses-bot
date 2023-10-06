using System.Net.Mail;
using System.Numerics;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;
using System.Data.Common;
using System.IO;
using Telegram.Bot.Requests;


namespace project
{
    class Database
    {
        //user data
        private readonly long Id;
        private string Username;
        private bool Admin;
        private bool Follow;


        private string[] lines = System.IO.File.ReadAllLines(path);
        private const string path = @"data\users.txt";

        //reutrns current user line 
        private int GetPosition(long id, string[] lines)
        {
            int temp = -1;

            for(int i = 0; i < lines.GetLength(0); i++)
            {
                if(lines[i].Contains(id.ToString()))
                {
                    temp = i;
                    break;
                }
            }

            return temp;
        
        }
        
        public void ChangeFollow(bool temp)
        {
            if(Follow != temp)
            {
                string[] str = System.IO.File.ReadAllLines(path);
                
                string thisLine = str[GetPosition(Id, str)];

                thisLine.Replace($"Follow:{!temp}",$"Follow:{temp}");


                using (FileStream file = new FileStream(path, FileMode.Open))
                {
                    
                    using (StreamWriter stream = new StreamWriter(file))
                    {
                        foreach(var line in str)
                        {
                            stream.WriteLine(line);
                        }
                    
                    }

                }
            }

            return;
        }

        private void CreateUser(Update update)
        {
            Console.WriteLine($"Created new user with id {Id}");
            Username = update.Message.Chat.Username;
            Admin = false;
            Follow = false;

            using (FileStream file = new FileStream(path, FileMode.Append))
            {
                using (StreamWriter stream = new StreamWriter(file))
                {
                    stream.Write(Id + " ");
                    stream.Write(Username + " ");
                    stream.Write($"Admin:{Admin} ");
                    stream.WriteLine($"Follow:{Follow}");
                }

            }
            
            return;
        }

        
        //parsing data from users.txt
        private User ParseData(string line)
        {
            string[] array = line.Trim().Split(' ');
            long id = Convert.ToInt64(array[0]);
            string username = array[1];
            bool admin = Convert.ToBoolean(array[2].Remove(0, "admin:".Length));
            bool follow = Convert.ToBoolean(array[3].Remove(0, "follow:".Length));

            return new User(id, username, admin:admin, follow:follow);


        }

        public Database(Update update)
        {
            Id = update.Message.Chat.Id;

            string temp = System.IO.File.ReadAllText(path);
            if(!temp.Contains(Id.ToString()))
            {
                CreateUser(update);
            }
            else
            {
                string line = lines[GetPosition(Id, lines)];
                User user = ParseData(line);
                Id = user.id;
                Username = user.username;
                Follow = user.follow;
                Admin = user.admin;

            }


            ReturnUser();
            return;
        }



        public List<User> ReturnAllUsers()
        {
            List<User> list = new List<User>();
            
            foreach(string line in lines)
            {
                User user = ParseData(line);
                list.Add(user);
            }

            return list;
        }
        public User ReturnUser()
        {
            return new User(Id, Username, Follow, Admin);
        }
    }
}
