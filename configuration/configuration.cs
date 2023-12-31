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
        public string? BotToken { get; set; }
    }
    class DataBaseConfiguration
    {
        public string? host { get; set;}
        public string? dbusername { get; set;}
        public string? dataBaseName { get; set;}
        public string? password { get; set;}
        public string? port { get; set;}
    }
}