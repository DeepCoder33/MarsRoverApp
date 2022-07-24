using Domain.Models.Aggregates;
using Domain.ValueTypes;
using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace Domain.Events
{
    [EventVersion("InitializePlateau", 1)]
    public class InitializePlateauEvent : AggregateEvent<PlateauAggregate, Identity>
    {
        public InitializePlateauEvent(SurfaceSize surfaceSize)
        {
            Size = surfaceSize;
        }

        public SurfaceSize Size { get; set; }
    }
}