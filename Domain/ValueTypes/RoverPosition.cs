using Domain.Enums;

namespace Domain.ValueTypes
{
    public class RoverPosition
    {
        public RoverPosition(
            Orientation orientation = Orientation.N,
            int x = 0,
            int y = 0)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Orientation Orientation { get; set; }
    }
}