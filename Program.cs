using Telegram.Bot;

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