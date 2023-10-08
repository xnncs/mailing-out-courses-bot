using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace project
{

    class Bot
    {
        private static async Task sendMessage(ITelegramBotClient botClient, Update update, CancellationToken token, List<User> users, CourseData courseData, InlineKeyboardMarkup CourseLink)
        {
            Console.WriteLine("1");
            foreach(User user in users)
            {
                Console.WriteLine(user.id);
                if(user.follow)
                {
                    Message courseMessage = await botClient.SendPhotoAsync(
                        chatId: user.id,
                        photo: InputFile.FromUri(courseData.courseimg),
                        caption: courseData.coursetext,
                        replyMarkup: CourseLink,
                        cancellationToken: token);
                }

            }

            return;
        }

        public static Telegram.Bot.Polling.ReceiverOptions? updateHandler = new Telegram.Bot.Polling.ReceiverOptions();

        public static Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            Console.WriteLine(exception);
            throw new NotImplementedException();
        }

        async public static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {   
            DataBase database = new DataBase(update);
            User user = database.GetUser();
            List<User> users = database.GetAllUsers();

            Data data = new Data();
            TimeData timeData = data.GetTime();
            CourseData courseData = data.GetCourse();


            ReplyKeyboardMarkup adminReplyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "/start" },
                new KeyboardButton[] { "/sendnow" },
                new KeyboardButton[] { "/sendat + {time}" },
                new KeyboardButton[] { "/changelink + {link}" },
                new KeyboardButton[] { "/changetext + {text}" },
                new KeyboardButton[] { "/changeimg + {img url}" },
            })
            {
                ResizeKeyboard = true,
            };
            InlineKeyboardMarkup CourseLink = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithUrl(
                    text: "my courses",
                    url: courseData.courseurl)
            });
            ReplyKeyboardMarkup userReplyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "/start" },
                new KeyboardButton[] { "/subscribe" },
                new KeyboardButton[] { "/unsubscribe" },
                new KeyboardButton[] { "/course" },
            })
            {
                ResizeKeyboard = true,
            };
            

            if(DateTime.Now.Hour == timeData.hour && DateTime.Now.Minute == timeData.minute)
            {
                sendMessage(botClient, update, token, users, courseData, CourseLink);
            }
            
            if (update.Message != null)
            {

                if (update.Message.Text != null)
                {

                    Console.WriteLine($"{update.Message.Text} {user.id} {user.username} admin:{user.admin} follow:{user.follow} {update.Message.Date} ");

                    if (user.admin)
                    {
                        
                        if (update.Message.Text.ToLower().Contains("/start"))
                        {
                            Message startMessage = await botClient.SendTextMessageAsync(
                                chatId: user.id,
                                replyMarkup: adminReplyKeyboardMarkup,
                                text: "You are an administrator, there is a panel in front of you, you can use it to: upload videos for mailing, change the course description and change the link to the course."
                                        + "\n\nFor sent message use /sent {time} , for example /sent 13:48:25 ",
                                cancellationToken: token);

                            return;
                        }
                        if (update.Message.Text.ToLower().Contains("/sendat"))
                        {
                            string temp = update.Message.Text.Remove(0, "/sendat".Length).Trim();
                            string[] array = temp.Split(":");

                            try
                            {
                                bool isformated = Convert.ToInt32(array[0]) >= 0 || Convert.ToInt32(array[0]) <= 24 || Convert.ToInt32(array[1]) >= 0 || Convert.ToInt32(array[1]) <= 60 || Convert.ToInt32(array[2]) >= 0 || Convert.ToInt32(array[2]) <= 60;

                                if(!isformated)
                                {
                                    Message errorMessage = await botClient.SendTextMessageAsync(
                                        chatId: user.id,
                                        text: "wrong command format, try one more time",
                                        cancellationToken: token);
                                        return;
                                }
                                int hour = Convert.ToInt32(array[0]);
                                int minute = Convert.ToInt32(array[1]);
                                int second = Convert.ToInt32(array[2]);

                                data.ChangeTimeData(hour: hour, minute: minute, second: second);
                            }
                            catch
                            {
                                Message errorMessage = await botClient.SendTextMessageAsync(
                                    chatId: user.id,
                                    text: "wrong command format, try one more time",
                                    cancellationToken: token);
                                    return;
                            }
                            return;

                        }

                        if (update.Message.Text.ToLower().Contains("/sendnow"))
                        {
                            sendMessage(botClient, update, token, users, courseData, CourseLink);
                        }
                        if (update.Message.Text.ToLower().Contains("/changelink"))
                        {
                            string link = update.Message.Text.Remove(0, "/changelink".Length).Trim();
                            if(link == null)
                            {
                                return;
                            }
                            data.ChangeCourseData(courseurl: link);
                        }
                        if (update.Message.Text.ToLower().Contains("/changetext"))
                        {
                            string text = update.Message.Text.Remove(0, "/changetext".Length).Trim();
                            if(text == null)
                            {
                                return;
                            }
                            data.ChangeCourseData(coursetext: text);
                        }
                        if (update.Message.Text.ToLower().Contains("/changeimg"))
                        {
                            try
                            {
                                string imgUrl = update.Message.Text.Remove(0, "/changeimg".Length).Trim();
                                InputFile.FromUri(imgUrl);

                                if(imgUrl == null)
                                {
                                    return;
                                }
                                data.ChangeCourseData(courseimg: imgUrl);       
                            }
                            catch
                            {
                                Message errorMessage = await botClient.SendTextMessageAsync(
                                    chatId: user.id,
                                    text: "wrong command format, try one more time",
                                    cancellationToken: token);
                                    return;
                            }
                            
                        }
                    }
                    else
                    {
                        if (update.Message.Text.ToLower().Contains("/course"))
                        {
                            Message courseMessage = await botClient.SendPhotoAsync(
                                chatId: user.id,
                                photo: InputFile.FromUri(courseData.courseimg),
                                caption: courseData.coursetext,
                                replyMarkup: CourseLink,
                                cancellationToken: token);

                            return;
                        }

                        if (update.Message.Text.ToLower().Contains("/subscribe"))
                        {
                            if(user.follow == true)
                            {    
                                Message AlradySupscribeMessage = await botClient.SendTextMessageAsync(
                                    chatId: user.id,
                                    text: "You are alrady subscribed to the notifications",
                                    cancellationToken: token);

                                return;
                            }

                            database.ChangeFollow(true);

                            Message supscribeMessage = await botClient.SendTextMessageAsync(
                                chatId: user.id,
                                text: "Congratulations! You are now subscribed to the notifications",
                                cancellationToken: token);

                            return;
                        }

                        if (update.Message.Text.ToLower().Contains("/unsubscribe"))
                        {
                            if(user.follow == false)
                            {    
                                Message AlradySupscribeMessage = await botClient.SendTextMessageAsync(
                                    chatId: user.id,
                                    text: "You are alrady unsubscribed to notifications",
                                    cancellationToken: token);

                                return;
                            }
                            database.ChangeFollow(false);
                            
                            Message unsupscribeMessage = await botClient.SendTextMessageAsync(
                                chatId: user.id,
                                text: "Now you are not subscribed to notifications",
                                cancellationToken: token);

                            return;
                        }

                        if (update.Message.Text.ToLower().Contains("/start"))
                        {
                            Message startMessage = await botClient.SendTextMessageAsync(
                                chatId: user.id,
                                replyMarkup: userReplyKeyboardMarkup,
                                text: "I am a bot that will collect and send interesting videos and text materials related to blogger topics and invite users to sign " +
                            "up for a professional video blogging course.",
                                cancellationToken: token);

                            return;
                        }

                        return;
                    }
                }
            }
            return;
        }
    }
}