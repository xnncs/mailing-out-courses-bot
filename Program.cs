using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using Telegram.Bot;

namespace project
{
    class Program
    {
        static void Main()
        {
            Configuration configuration = JsonSerializer.Deserialize<Configuration>(@"configuration\configuration.json");

            string apiToken = configuration.telegramConfiguration.BotToken;

            var client = new TelegramBotClient(apiToken);

            client.StartReceiving(Bot.HandleUpdateAsync, Bot.HandlePollingErrorAsync, Bot.updateHandler, Bot.pollingErrorHandler);
            Console.WriteLine("the bot has launched");

            Console.ReadLine();
        }
    }
}