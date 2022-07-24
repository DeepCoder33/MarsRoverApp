using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Events;
using Domain.HelperExtension;
using Domain.ValueTypes;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;

namespace Domain.Models.Aggregates
{
    public class RoverAggregate : AggregateRoot<RoverAggregate, Identity>
    {
        public RoverAggregate(Identity id) : base(id)
        {
        }

        public RoverPosition RoverPosition { get; private set; }
        public Identity PlateauSurfaceId { get; private set; }

        #region Aggregate methods

        public IExecutionResult DeployRover(string roverPositionInput, Identity plateauSurfaceId)
        {
            var roverPosition = ParsePosition(roverPositionInput);

            Emit(new DeployRoverEvent(roverPosition, plateauSurfaceId));

            return ExecutionResult.Success();
        }

        public IExecutionResult TurnLeft()
        {
            Emit(new MoveRoverEvent(Movement.L));

            return ExecutionResult.Success();
        }

        public IExecutionResult TurnRight()
        {
            Emit(new MoveRoverEvent(Movement.R));

            return ExecutionResult.Success();
        }

        public IExecutionResult MoveAsync()
        {
            MoveRoverAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            Emit(new MoveRoverEvent(Movement.M));

            return ExecutionResult.Success();
        }

        #endregion

        #region Apply methods

        public void Apply(DeployRoverEvent aggregateEvent)
        {
            RoverPosition = aggregateEvent.RoverPosition;
            PlateauSurfaceId = aggregateEvent.PlateauSurfaceId;
        }

        public void Apply(MoveRoverEvent aggregateEvent)
        {
            switch (aggregateEvent.MoveRover)
            {
                case Movement.L:
                    TurnLeftRover();
                    break;

                case Movement.R:
                    TurnRightRover();
                    break;

                case Movement.M:
                    MoveRoverAsync().GetAwaiter().GetResult();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(aggregateEvent.MoveRover), aggregateEvent.MoveRover,
                        null);
            }
        }

        #endregion

        #region Private methods

        private RoverPosition ParsePosition(string roverPositionInput)
        {
            var roverPositionArray = roverPositionInput.Split(' ');

            if (roverPositionArray.Length == 3)
            {
                var orientation = roverPositionArray[2].ToUpper();

                if (orientation.Equals("N", StringComparison.InvariantCultureIgnoreCase) ||
                    orientation.Equals("S", StringComparison.InvariantCultureIgnoreCase) ||
                    orientation.Equals("E", StringComparison.InvariantCultureIgnoreCase) ||
                    orientation.Equals("W", StringComparison.InvariantCultureIgnoreCase))
                {
                    var roverPosition = new RoverPosition
                    {
                        Orientation = (Orientation) Enum.Parse(typeof(Orientation), orientation),
                        X = int.Parse(roverPositionArray[0]),
                        Y = int.Parse(roverPositionArray[1])
                    };

                    return roverPosition;
                }
            }

            return null;
        }

        private async Task<bool> IsRoverInsideBoundariesAsync()
        {
            var aggregateStore = Helpers.RootResolver.Resolve<IAggregateStore>();
            var plateauAggregate =
                await aggregateStore.LoadAsync<PlateauAggregate, Identity>(PlateauSurfaceId, CancellationToken.None);

            if (RoverPosition.X > plateauAggregate.Size.Width ||
                RoverPosition.X < 0 ||
                RoverPosition.Y > plateauAggregate.Size.Height ||
                RoverPosition.Y < 0)
                return false;

            return true;
        }

        private void TurnRightRover()
        {
            RoverPosition.Orientation = RoverPosition.Orientation + 1 > Orientation.W
                ? Orientation.N
                : RoverPosition.Orientation + 1;
        }

        private void TurnLeftRover()
        {
            RoverPosition.Orientation = RoverPosition.Orientation - 1 < Orientation.N
                ? Orientation.W
                : RoverPosition.Orientation - 1;
        }

        private async Task MoveRoverAsync()
        {
            var roverX = RoverPosition.X;
            var roverY = RoverPosition.Y;

            switch (RoverPosition.Orientation)
            {
                case Orientation.N:
                    RoverPosition.Y++;
                    break;

                case Orientation.S:
                    RoverPosition.Y--;
                    break;
                case Orientation.W:
                    RoverPosition.X--;
                    break;

                case Orientation.E:
                    RoverPosition.X++;
                    break;

                default:
                    throw new InvalidOperationException();
            }

            if (!await IsRoverInsideBoundariesAsync())
            {
                RoverPosition.X = roverX;
                RoverPosition.Y = roverY;
                Console.WriteLine();
            }
        }

        #endregion
    }
}