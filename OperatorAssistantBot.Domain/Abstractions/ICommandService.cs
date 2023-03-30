using System.Collections.Generic;

namespace OperatorAssistantBot.Domain.Abstractions
{
    public interface ICommandService
    {
        List<TelegramCommand> Get();
    }
}
