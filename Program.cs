using PRTelegramBot.Core;
using Telegram.Bot;

const string EXIT_COMMAND = "exit";

var telegram = new PRBot(option =>
{
    option.Token = "6435758351:AAESmTMBNtZ5qO7axxhen-LdvGAYHGjbkpY";
    option.ClearUpdatesOnStart = true;
    option.WhiteListUsers = new List<long>() { };
    option.Admins = new List<long>() { };
    option.BotId = 0;
});
telegram.OnLogError += Telegram_OnLogError;
telegram.OnLogCommon += Telegram_OnLogCommon;

void TelegramStart()
{
    telegram.Start();
}
TelegramStart();

void Telegram_OnLogError(Exception ex, long? id)
{
    Console.ForegroundColor = ConsoleColor.Red;
    string errorMessage = $"{DateTime.Now}:{ex}";
    Console.WriteLine(errorMessage);
    Console.ResetColor();
}

void Telegram_OnLogCommon(string msg, PRBot.TelegramEvents typeEvent, ConsoleColor color)
{
    Console.ForegroundColor = color;
    string message = $"{DateTime.Now}:{msg}";
    Console.WriteLine(message);
    Console.ResetColor();
}

while (true)
{
    var result = Console.ReadLine();
    if(result.ToLower() == EXIT_COMMAND)
    {
        Environment.Exit(0);
    }
}
