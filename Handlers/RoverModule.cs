using Domain.Models.Commands;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;
using Handlers.CommandHandlers;

namespace Handlers
{
    public class RoverModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.AddDefaults(typeof(CreatePlateauSurfaceCommandHandler).Assembly);
            eventFlowOptions.AddDefaults(typeof(CreatePlateauSurfaceCommand).Assembly);
        }
    }
}