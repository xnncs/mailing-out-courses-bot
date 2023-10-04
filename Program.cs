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

    class Program
    {
        public static string courseText = "Course";

        public static string courseUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

        static void Main()
        {

            string apiToken = "6346026713:AAFV6Ka8BUsFk2WsDr4bRzMsZ1HTg1OnzGE";

            var client = new TelegramBotClient(apiToken);

            client.StartReceiving(Bot.Update, Bot.Error);
            Console.WriteLine("the bot has launched");

            Console.ReadLine();
        }
    }
}