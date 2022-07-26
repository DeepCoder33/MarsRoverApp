﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models.Commands;
using Domain.ValueTypes;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Configuration;

namespace Domain.HelperExtension
{
    public static class Helpers
    {
        public static IRootResolver RootResolver;

        public static async Task<IList<IExecutionResult>> PublishMultipleAsync(
            this ICommandBus commandBus,
            ICommand[] commands,
            bool continueOnError = false)
        {
            if (!commands.Any())
                return null;

            var result = new List<IExecutionResult>();

            foreach (dynamic command in commands)
            {
                IExecutionResult executionResult =
                    await commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

                result.Add(executionResult);

                if (!continueOnError && !executionResult.IsSuccess)
                    break;
            }

            return result;
        }

        public static IEnumerable<ICommand> ToRoverCommands(this string input, Identity roverId)
        {
            foreach (var letter in input)
                switch (char.ToUpper(letter))
                {
                    case 'L':
                        yield return new TurnLeftCommand(roverId);
                        break;

                    case 'R':
                        yield return new TurnRightCommand(roverId);
                        break;

                    case 'M':
                        yield return new MoveCommand(roverId);
                        break;

                    default:
                        throw new InvalidOperationException();
                }
        }
    }
}