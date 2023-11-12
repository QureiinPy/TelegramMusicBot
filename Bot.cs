using PRTelegramBot.Core;
using PRTelegramBot.Helpers;
using Telegram.Bot;

namespace TelegramBotMusic;

internal class Bot
{
    readonly PRBot bot;
    public Bot(PRBot bot)
    {
        this.bot = bot;
    }

    async void SendMessage(string text)
    {
        
    }
}
