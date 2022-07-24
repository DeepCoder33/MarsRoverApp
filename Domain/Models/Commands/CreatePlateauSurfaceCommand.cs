using Domain.Models.Aggregates;
using Domain.ValueTypes;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace Domain.Models.Commands
{
    public class CreatePlateauSurfaceCommand : Command<PlateauAggregate, Identity, IExecutionResult>
    {
        public CreatePlateauSurfaceCommand(
            Identity id,
            string surfaceSizeInput)
            : base(id)
        {
            SurfaceSizeInput = surfaceSizeInput;
        }

        public string SurfaceSizeInput { get; }
    }
}