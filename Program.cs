using Telegram.Bot;

namespace project
{
    class Program
    {
        static void Main()
        {
            string apiToken = "";

            var client = new TelegramBotClient(apiToken);

            client.StartReceiving(Bot.Update, Bot.Error, Bot.updateHandler);
            Console.WriteLine("the bot has launched");

            Console.ReadLine();
        }
    }
}