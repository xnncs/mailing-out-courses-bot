using Telegram.Bot;

namespace project
{
    class Program
    {
        static void Main()
        {
            string apiToken = "6470308128:AAG6ibAHfxsLwLy2o4Y-aw0WM0OS03Qqfbc";

            var client = new TelegramBotClient(apiToken);

            client.StartReceiving(Bot.HandleUpdateAsync, Bot.HandlePollingErrorAsync, Bot.updateHandler, Bot.pollingErrorHandler);
            Console.WriteLine("the bot has launched");

            Console.ReadLine();
        }
    }
}