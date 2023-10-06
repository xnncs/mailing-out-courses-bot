using System.ComponentModel;
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
using Telegram.Bot.Polling;
using System.Runtime.CompilerServices;

namespace project
{

    class Program
    {
        static void Main()
        {
            string apiToken = "6346026713:AAFV6Ka8BUsFk2WsDr4bRzMsZ1HTg1OnzGE";

            var client = new TelegramBotClient(apiToken);

            client.StartReceiving(Bot.Update, Bot.Error, Bot.updateHandler);
            Console.WriteLine("the bot has launched");

            Console.ReadLine();
        }
    }
}