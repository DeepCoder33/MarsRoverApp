using System.Threading;
using System.Threading.Tasks;
using Domain.Models.Aggregates;
using Domain.Models.Commands;
using Domain.ValueTypes;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace Handlers.CommandHandlers
{
    public class TurnRightCommandHandler :
        CommandHandler<RoverAggregate, Identity, IExecutionResult, TurnRightCommand>
    {
        public override async Task<IExecutionResult> ExecuteCommandAsync(
            RoverAggregate aggregate,
            TurnRightCommand command,
            CancellationToken cancellationToken)
        {
            var executionResult = aggregate.TurnRight();

            return await Task.FromResult(executionResult);
        }
    }
}