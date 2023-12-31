using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using Telegram.Bot;

namespace project
{
    class Configuration
    {
        public TelegramConfiguration? telegramConfiguration;
        public DataBaseConfiguration? dataBaseConfiguration;
    }

    class TelegramConfiguration
    {
        public string? BotToken;
    }
    class DataBaseConfiguration
    {
        public string? host { get; init;}
        public string? dbusername { get; init;}
        public string? dataBaseName { get; init;}
        public string? password { get; init;}
        public string? port { get; init;}
    }
}