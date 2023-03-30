using OperatorAssistantBot.Domain.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OperatorAssistantBot.Controllers
{
    public class BotController
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ICommandService _commandService;

        public BotController(ICommandService commandService, ITelegramBotClient telegramBotClient)
        {
            _commandService = commandService;
            _telegramBotClient = telegramBotClient;
        }

        /// <summary>
        /// Асинхронный метод реагирующий на поступающие команды
        /// </summary>
        /// <param name="update"></param>
        public async void CommandProcessing(Update update)
        {
            if (update == null) return;

            var message = update.Message;

            foreach (var command in _commandService.Get())
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, _telegramBotClient);
                    break;
                }
            }
            return;
        }
    }
}
