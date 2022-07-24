using EventFlow.Core;

namespace Domain.ValueTypes
{
    public class Identity : Identity<Identity>
    {
        public Identity(string value)
            : base(value)
        {
        }
    }
}