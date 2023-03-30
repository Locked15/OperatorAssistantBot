using OperatorAssistantBot.Domain.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Net.Mail;
using System.Net;

namespace OperatorAssistantBot.Domain.Commands
{
    public class ConnectOperatorCommand : TelegramCommand
    {
        public override string Name => "\U0001F4D6 Вызвать оператора";

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

            await botClient.SendTextMessageAsync(message.Chat.Id, "В скором времени оператор свяжется с вами.\nПожалуйста ожидайте.", replyMarkup: replyMarkup);

            MailAddress from = new MailAddress("diyarov.amir.r@gmail.com", "Amir");
            MailAddress to = new MailAddress("amir.diarov@yandex.ru");
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Новая заявка";
            m.Body = $"ID чата: {message.Chat.Id}";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("diyarov.amir.r@gmail.com", "ovcyepfiuskjuehf");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);

            Console.WriteLine("\nПисьмо отправлено\n");
        }
    }
}
