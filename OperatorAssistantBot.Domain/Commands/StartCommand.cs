using OperatorAssistantBot.Domain.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace OperatorAssistantBot.Domain.Commands
{
    public class StartCommand : TelegramCommand
    {
        public override string Name => @"/start";

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
                    new KeyboardButton("\U0001F3E0 Главная"),
                    new KeyboardButton("\U0001F4D6 Вызвать оператора")
                }
            };
            var replyMarkup = new ReplyKeyboardMarkup(keyBoard) { ResizeKeyboard = true };

            await botClient.SendTextMessageAsync(message.Chat.Id, "Здравствуйте! Я умный ассистент." +
                                    "\nВыберите интересующий вас вопрос и я отвечу." +
                                    "\nТакже вы можете вызвать оператора нажав соответствующию кнопку.", replyMarkup: replyMarkup);
        }
    }
}
