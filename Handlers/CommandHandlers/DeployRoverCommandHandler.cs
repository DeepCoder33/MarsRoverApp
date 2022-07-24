using System.Threading;
using System.Threading.Tasks;
using Domain.Models.Aggregates;
using Domain.Models.Commands;
using Domain.ValueTypes;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace Handlers.CommandHandlers
{
    public class DeployRoverCommandHandler :
        CommandHandler<RoverAggregate, Identity, IExecutionResult, DeployRoverCommand>
    {
        public override async Task<IExecutionResult> ExecuteCommandAsync(
            RoverAggregate aggregate,
            DeployRoverCommand command,
            CancellationToken cancellationToken)
        {
            var executionResult = aggregate.DeployRover(command.RoverPositionInput, command.PlateauSurfaceId);

            return await Task.FromResult(executionResult);
        }
    }
}