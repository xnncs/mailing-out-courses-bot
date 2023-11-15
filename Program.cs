using Telegram.Bot;

namespace project
{
    class Program
    {
        static void Main()
        {
            string apiToken = "";

            var client = new TelegramBotClient(apiToken);

            client.StartReceiving(Bot.HandleUpdateAsync, Bot.HandlePollingErrorAsync, Bot.updateHandler, Bot.pollingErrorHandler);
            Console.WriteLine("the bot has launched");

            Console.ReadLine();
        }
    }
}