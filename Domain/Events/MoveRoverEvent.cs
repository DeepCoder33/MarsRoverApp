using Domain.Enums;
using Domain.Models.Aggregates;
using Domain.ValueTypes;
using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace Domain.Events
{
    [EventVersion("MoveRover", 1)]
    public class MoveRoverEvent : AggregateEvent<RoverAggregate, Identity>
    {
        public MoveRoverEvent(Movement movement)
        {
            MoveRover = movement;
        }

        public Movement MoveRover { get; set; }
    }
}