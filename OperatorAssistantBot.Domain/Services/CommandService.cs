﻿using System.Collections.Generic;
using OperatorAssistantBot.Domain.Commands;
using OperatorAssistantBot.Domain.Abstractions;

namespace OperatorAssistantBot.Domain.Services
{
    public class CommandService: ICommandService
    {
        private readonly List<TelegramCommand> _commands;

        public CommandService()
        {
            _commands = new List<TelegramCommand>
            {
                new StartCommand(),
                new ConnectOperatorCommand(),
                new QuestionsListCommand(),
            };
        }

        public List<TelegramCommand> Get() => _commands;
    }
}
