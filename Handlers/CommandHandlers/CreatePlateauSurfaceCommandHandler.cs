using System.Threading;
using System.Threading.Tasks;
using Domain.Models.Aggregates;
using Domain.Models.Commands;
using Domain.ValueTypes;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace Handlers.CommandHandlers
{
    public class CreatePlateauSurfaceCommandHandler :
        CommandHandler<PlateauAggregate, Identity, IExecutionResult, CreatePlateauSurfaceCommand>
    {
        public override async Task<IExecutionResult> ExecuteCommandAsync(
            PlateauAggregate aggregate,
            CreatePlateauSurfaceCommand command,
            CancellationToken cancellationToken)
        {
            var executionResult = aggregate.Initialize(command.SurfaceSizeInput);

            return await Task.FromResult(executionResult);
        }
    }
}