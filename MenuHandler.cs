using PRTelegramBot.Attributes;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotMusic
{
    public class MenuHandler
    {
        #region Reply
        [ReplyMenuHandler("Меню")]
        public static async Task Menu(ITelegramBotClient botClient, Update update)
        {
            var message = "Меню";
            var menuList = new List<KeyboardButton>();
            var menuListString = new List<string>();

            var rnd = new Random().Next(0, 100);
            menuList.Add(new KeyboardButton($"Меню ({rnd})"));
            menuListString.Add("МЕНЮ");
            menuListString.Add($"Меню ({rnd})");

            var menu = MenuGenerator.ReplyKeyboard(2, menuList);
            var option = new OptionMessage();
            option.MenuReplyKeyboardMarkup = menu;

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message, option);

        }

        #endregion


        #region Slesh

        [ReplyMenuHandler("get")]
        public static async Task Get(ITelegramBotClient botClient, Update update)
        {
            var message = "/get, /get_1";
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }
        [SlashHandler("/get")]
        public static async Task GetSlash(ITelegramBotClient botClient, Update update)
        {
            string message;
            if (update.Message.Text.Contains('_'))
            {
                var parameter = update.Message.Text.Split('_')[1];
                message = $"Команда get с Параметром {parameter}";
            }
            else
            {
                message = "Команда get";
            }
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }
        [ReplyMenuHandler("load")]
        public static async Task Load(ITelegramBotClient botClient, Update update)
        {
            var message = "/load";

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }

        [SlashHandler("/load")]
        public static async Task DownloadMusic(ITelegramBotClient botClient, Update update)
        {
            var url = update.Message.Text.Split('_')[1];

            HttpWebClient webClient = new HttpWebClient();

            string message = "Скачиваю...";

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);

            webClient.DownloadMedia(url);

            
        }
        #endregion


        #region InLine
        [ReplyMenuHandler("inline")]
        public static async Task InLine(ITelegramBotClient botClient, Update update)
        {
            var message = "Пример inline";

            List<IInlineContent> menu = new List<IInlineContent>();

            var exampleOne = new InlineCallback("1", CustomTHeader.One);
            var url = new InlineURL("Google", "https://www.google.ru/?hl=ru");
            var exampleTwo = new InlineCallback<EntityTCommand<long>>("Кнопка 2", CustomTHeader.Two, new EntityTCommand<long>(2));
            var exampleThree = new InlineCallback<EntityTCommand<long>>("кнопка 3", CustomTHeader.Three, new EntityTCommand<long>(3));

            menu.Add(exampleOne);
            menu.Add(exampleTwo);
            menu.Add(exampleThree);
            menu.Add(url);

            var menuItems = MenuGenerator.InlineKeyboard(1, menu);
            var option = new OptionMessage();
            option.MenuInlineKeyboardMarkup = menuItems;

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message, option);
        }

        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.One)]
        public static async Task ExampleOne(ITelegramBotClient botClient, Update update)
        {
            var message = "Example One";

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }

        [InlineCallbackHandler<CustomTHeader>(CustomTHeader.Two, CustomTHeader.Three)]
        public static async Task ExampleTwoThree(ITelegramBotClient botClient, Update update)
        {
            var command = InlineCallback<EntityTCommand<long>>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);

            if(command != null)
            {
                var message = $"Данные {command.Data.EntityId}";
                var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
            }
        }

        #endregion

    }
}
