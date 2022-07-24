using Domain.Models.Aggregates;
using Domain.ValueTypes;
using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace Domain.Events
{
    [EventVersion("DeployRover", 1)]
    public class DeployRoverEvent : AggregateEvent<RoverAggregate, Identity>
    {
        public DeployRoverEvent(RoverPosition roverPosition, Identity plateauSurfaceId)
        {
            RoverPosition = roverPosition;
            PlateauSurfaceId = plateauSurfaceId;
        }

        public RoverPosition RoverPosition { get; }
        public Identity PlateauSurfaceId { get; }
    }
}