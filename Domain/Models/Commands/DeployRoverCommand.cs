using Domain.Models.Aggregates;
using Domain.ValueTypes;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace Domain.Models.Commands
{
    public class DeployRoverCommand : Command<RoverAggregate, Identity, IExecutionResult>
    {
        public DeployRoverCommand(
            Identity id,
            Identity plateauSurfaceId,
            string roverPositionInput)
            : base(id)
        {
            PlateauSurfaceId = plateauSurfaceId;
            RoverPositionInput = roverPositionInput;
        }

        public string RoverPositionInput { get; }
        public Identity PlateauSurfaceId { get; }
    }
}