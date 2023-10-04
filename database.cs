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


namespace project
{
    class Database
    {
        //user data
        private readonly long id;
        private string Username;
        private bool Admin;
        private bool Follow;


        private string[] lines = System.IO.File.ReadAllLines(path);
        private const string path = @"D:\code\codes\project\users.txt";

        //reutrns current user line 
        private int position
        {
            get
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
        }
        
        public void ChangeFollow(bool temp)
        {
            if(Follow != temp)
            {
                string line = lines[position];

                using (FileStream file = new FileStream(path, FileMode.Append))
                {
                    using (StreamWriter stream = new StreamWriter(file))
                    {
                        stream.Write(id + " ");
                        stream.Write(Username + " ");
                        stream.Write($"Admin:{Admin} ");
                        stream.WriteLine($"Follow:{Follow}");

                }

            }
            }

            return;
        }

        private void CreateUser(Update update)
        {
            Console.WriteLine($"Created new user with id {id}");
            Username = update.Message.Chat.Username;
            Admin = false;
            Follow = false;

            using (FileStream file = new FileStream(path, FileMode.Append))
            {
                using (StreamWriter stream = new StreamWriter(file))
                {
                    stream.Write(id + " ");
                    stream.Write(Username + " ");
                    stream.Write($"Admin:{Admin} ");
                    stream.WriteLine($"Follow:{Follow}");

                }

            }
            
            returnUser();
            return;
        }

        

        public Database(Update update)
        {
            id = update.Message.Chat.Id;
            
            string temp = System.IO.File.ReadAllText(path);
            if(!temp.Contains(id.ToString()))
            {
                CreateUser(update);
                return;
            }
            string line = lines[position];

            //parsing data from users.txt
            Username = line.Remove(0, id.ToString().Length).Trim().Split(' ')[0];
            Admin = Convert.ToBoolean(line.Remove(0, id.ToString().Length).Trim().Remove(0, Username.Length + "Admin:".Length).Trim().Split(' ')[0]);
            Follow = Convert.ToBoolean(line.Remove(0, id.ToString().Length).Trim().Remove(0, Username.Length + "Admin:".Length).Trim().Remove(0, Admin.ToString().Length + "Follow:".Length).Trim().Split(' ')[0]);;

            returnUser();
            return;
        }

        public User returnUser()
        {
            return new User(id, Username, Admin, Follow);
        }
    }
}
