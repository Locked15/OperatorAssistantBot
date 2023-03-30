using OperatorAssistantBot.Domain.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace OperatorAssistantBot.Domain.Commands
{
    public class QuestionsListCommand : TelegramCommand
    {
        public override string Name => "\U0001F3E0 Часто задаваемые вопросы";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, ITelegramBotClient botClient)
        {
            var keyBoard = new[]
            {
                new[]
                {
                    new KeyboardButton("\U0001F3E0 Часто задаваемые вопросы"),
                    new KeyboardButton("\U0001F4D6 Вызвать оператора")
                }
            };
            var replyMarkup = new ReplyKeyboardMarkup(keyBoard) { ResizeKeyboard = true };

            await botClient.SendTextMessageAsync(message.Chat.Id, "Тут должен быть список вопросов и ответов на них", replyMarkup: replyMarkup);
        }
    }
}
