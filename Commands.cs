using PRTelegramBot.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotMusic
{
    public class Commands
    {
        [ReplyMenuHandler(true, "hello", "Test")]
        static public async Task Example(ITelegramBotClient botClient, Update update)
        {
            var message = "hello";
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }

    }
}
