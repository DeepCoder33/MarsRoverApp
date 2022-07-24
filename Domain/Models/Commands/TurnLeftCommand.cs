using Domain.Models.Aggregates;
using Domain.ValueTypes;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace Domain.Models.Commands
{
    public class TurnLeftCommand : Command<RoverAggregate, Identity, IExecutionResult>
    {
        public TurnLeftCommand(
            Identity id)
            : base(id)
        {
        }
    }
}