using Domain.Models.Aggregates;
using Domain.ValueTypes;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace Domain.Models.Commands
{
    public class TurnRightCommand : Command<RoverAggregate, Identity, IExecutionResult>
    {
        public TurnRightCommand(
            Identity id)
            : base(id)
        {
        }
    }
}