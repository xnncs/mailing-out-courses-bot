using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace project
{

    class Bot
    {
        private static async Task sendMessageNow(ITelegramBotClient botClient, Update update, CancellationToken token, List<User> users, Data data, InlineKeyboardMarkup CourseLink)
        {
            foreach(User temp in users)
            {
                if(temp.follow)
                {
                    Message courseMessage = await botClient.SendPhotoAsync(
                        chatId: temp.id,
                        photo: InputFile.FromUri("https://sun1-30.userapi.com/impg/fVaQqDIiS5T8tLabfYvGpfrk4Bca5fkxR7jPpw/2FTXDEQXg64.jpg?size=828x1076&quality=95&sign=ab963e3e4eda23b4bf720ae28ff169f7&type=album"),
                        caption: data.CourseText,
                        replyMarkup: CourseLink,
                        cancellationToken: token);
                }

            }

            return;
            
        }
        

        public static Telegram.Bot.Polling.ReceiverOptions? updateHandler = new Telegram.Bot.Polling.ReceiverOptions();

        public static Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        async public static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {   
            DataBase database = new DataBase(update);
            User user = database.GetUser();
            List<User> users = database.GetAllUsers();

            Data data = new Data();

            ReplyKeyboardMarkup adminReplyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "/start" },
                new KeyboardButton[] { "/sendnow" },
            })
            {
                ResizeKeyboard = true,
            };

            InlineKeyboardMarkup CourseLink = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithUrl(
                    text: Data.CourseButtonText,
                    url: data.CourseUrl)
            });

            ReplyKeyboardMarkup userReplyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "/subscribe" },
                new KeyboardButton[] { "/unsubscribe" },
                new KeyboardButton[] { "/course" },
            })
            {
                ResizeKeyboard = true,
            };


            // if(DateTime.Now.Hour == data.hour && DateTime.Now.Minute == data.minute)
            // {
            //     sendMessageNow(botClient, update, token, users, data, CourseLink);
            
            // }
            
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
                                text: Data.adminStartText,
                                cancellationToken: token);

                            return;
                        }
                        if (update.Message.Text.ToLower().Contains("/sendat"))
                        {
                            // data.UpdateDateTimeData(update);
                        }

                        if (update.Message.Text.ToLower().Contains("/sendnow"))
                        {
                            sendMessageNow(botClient, update, token, users, data, CourseLink);
                            
                        }
                    }
                    else
                    {
                        if (update.Message.Text.ToLower().Contains("/course"))
                        {
                            Message courseMessage = await botClient.SendTextMessageAsync(
                                chatId: user.id,
                                text: data.CourseText,
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
                                    text: Data.AlradySubscribeText,
                                    cancellationToken: token);

                                return;
                            }

                            database.ChangeFollow(true);

                            Message supscribeMessage = await botClient.SendTextMessageAsync(
                                chatId: user.id,
                                text: Data.SubscribeText,
                                cancellationToken: token);

                            return;
                        }

                        if (update.Message.Text.ToLower().Contains("/unsubscribe"))
                        {
                            if(user.follow == false)
                            {    
                                Message AlradySupscribeMessage = await botClient.SendTextMessageAsync(
                                    chatId: user.id,
                                    text: Data.AlradyUnsubscribeText,
                                    cancellationToken: token);

                                return;
                            }
                            database.ChangeFollow(false);
                            
                            Message unsupscribeMessage = await botClient.SendTextMessageAsync(
                                chatId: user.id,
                                text: Data.UnsubscribeText,
                                cancellationToken: token);

                            return;
                        }

                        if (update.Message.Text.ToLower().Contains("/start"))
                        {
                            Message startMessage = await botClient.SendTextMessageAsync(
                                chatId: user.id,
                                replyMarkup: userReplyKeyboardMarkup,
                                text: Data.StartText,
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