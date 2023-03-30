using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Update = Telegram.Bot.Types.Update;
using OperatorAssistantBot.Domain.Abstractions;
using OperatorAssistantBot.Controllers;
using OperatorAssistantBot.Domain.Services;

namespace OperatorAssistantBot
{
    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("5821872816:AAHcJfhsl0NElp0tp7ugBI6rHZDV62cNaRw");
        static ICommandService command = new CommandService();

        static void Main(string[] args)
        {
            Console.WriteLine($"Бот {bot.GetMeAsync().Result.FirstName} успешно запущен!\n");

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // получить все типы обновлений
            };

            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            Console.ReadLine();
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            var bot = new BotController(command, botClient);
            bot.CommandProcessing(update);
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
    }
}