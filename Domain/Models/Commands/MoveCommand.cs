using Domain.Models.Aggregates;
using Domain.ValueTypes;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace Domain.Models.Commands
{
    public class MoveCommand : Command<RoverAggregate, Identity, IExecutionResult>
    {
        public MoveCommand(
            Identity id)
            : base(id)
        {
        }
    }
}