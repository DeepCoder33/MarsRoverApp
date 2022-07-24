using System.Threading;
using System.Threading.Tasks;
using Domain.Models.Aggregates;
using Domain.Models.Commands;
using Domain.ValueTypes;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace Handlers.CommandHandlers
{
    public class TurnLeftCommandHandler :
        CommandHandler<RoverAggregate, Identity, IExecutionResult, TurnLeftCommand>
    {
        public override async Task<IExecutionResult> ExecuteCommandAsync(
            RoverAggregate aggregate,
            TurnLeftCommand command,
            CancellationToken cancellationToken)
        {
            var executionResult = aggregate.TurnLeft();

            return await Task.FromResult(executionResult);
        }
    }
}