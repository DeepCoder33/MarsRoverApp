using System.Threading;
using System.Threading.Tasks;
using Domain.Models.Aggregates;
using Domain.Models.Commands;
using Domain.ValueTypes;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace Handlers.CommandHandlers
{
    public class MoveCommandHandler :
        CommandHandler<RoverAggregate, Identity, IExecutionResult, MoveCommand>
    {
        public override async Task<IExecutionResult> ExecuteCommandAsync(
            RoverAggregate aggregate,
            MoveCommand command,
            CancellationToken cancellationToken)
        {
            var executionResult = aggregate.MoveAsync();

            return executionResult;
        }
    }
}