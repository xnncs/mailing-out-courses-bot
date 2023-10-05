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

namespace project
{

    class Bot
    {
        public static Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        async public static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {

            if (update.Message.Text != null)
            {

                Database database = new Database(update);
                User user = database.returnUser();


                Console.WriteLine($"{update.Message.Text} {user.id} {user.username} {update.Message.Date}");

                if (user.admin)
                {
                    ReplyKeyboardMarkup adminReplyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                    {
                        new KeyboardButton[] { "/start" },
                        new KeyboardButton[] { "i am admin and what do you do me?" },
                    })
                    {
                        ResizeKeyboard = true,
                    };

                    if (update.Message.Text.ToLower().Contains("/start"))
                    {
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: user.id,
                            replyMarkup: adminReplyKeyboardMarkup,
                            text: "You are an administrator, there is a panel in front of you, you can use it to: upload videos for mailing, change the course description and change the link to the course",
                            cancellationToken: token);

                        return;


                    }
                }
                else
                {

                    InlineKeyboardMarkup courseLink = new InlineKeyboardMarkup(new[]
                    {
                        InlineKeyboardButton.WithUrl(
                            text: "Link to the course",
                            url: Data.courseUrl)
                    });

                    ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                    {
                        new KeyboardButton[] { "/start" },
                        new KeyboardButton[] { "/subscribe" },
                        new KeyboardButton[] { "/unsubscribe" },
                        new KeyboardButton[] { "/course" },
                    })
                    {
                        ResizeKeyboard = true,
                    };

                    if (update.Message.Text.ToLower().Contains("/course"))
                    {
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: user.id,
                            text: Data.courseText,
                            replyMarkup: courseLink,
                            cancellationToken: token);

                        return;
                    }

                    if (update.Message.Text.ToLower().Contains("/subscribe"))
                    {
                        

                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: user.id,
                            text: "Congratulations! You are now subscribed to the notifications",
                            cancellationToken: token);

                        return;
                    }

                    if (update.Message.Text.ToLower().Contains("/unbupscribe"))
                    {
                        

                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: user.id,
                            text: "Now you are not subscribed to notifications",
                            cancellationToken: token);

                        return;
                    }

                    if (update.Message.Text.ToLower().Contains("/start"))
                    {
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: user.id,
                            replyMarkup: replyKeyboardMarkup,
                            text: "I am a bot that will collect and send interesting videos and text materials related to blogger topics and invite users to sign " +
                            "up for a professional video blogging course.",
                            cancellationToken: token);

                        return;


                    }

                    return;
                }

            }
            return;
        }
    }
}