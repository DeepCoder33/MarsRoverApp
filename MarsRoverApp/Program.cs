using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Domain.HelperExtension;
using Domain.Models.Aggregates;
using Domain.Models.Commands;
using Domain.ValueTypes;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Commands;
using EventFlow.Extensions;
using Handlers;
using MarsRoverApp.Constants;

namespace MarsRoverApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var roverIdList = new List<Identity>();

            using var resolver = EventFlowOptions.New
                .RegisterModule<RoverModule>()
                .UseNullLog()
                .CreateResolver();
            Helpers.RootResolver = resolver;

            var commandBus = resolver.Resolve<ICommandBus>();

            Console.WriteLine(CliConstants.PlateauSurfaceAreaInput);

            var sizeInput = Console.ReadLine();

            var plateauAggregateId = Identity.New;
            var createPlateauSurfaceCommand = new CreatePlateauSurfaceCommand(plateauAggregateId, sizeInput);

            var commands = new List<ICommand>
            {
                createPlateauSurfaceCommand
            };

            while (true)
            {
                Console.WriteLine(CliConstants.RoverPositionInput);
                var roverPositionInput = Console.ReadLine();

                Console.WriteLine(CliConstants.RoverCommandInput);
                var roverCommandInput = Console.ReadLine();

                var roverId = Identity.New;
                roverIdList.Add(roverId);

                var deployRoverCommand = new DeployRoverCommand(roverId, plateauAggregateId, roverPositionInput);

                commands.Add(deployRoverCommand);
                commands.AddRange(roverCommandInput.ToRoverCommands(roverId));

                Console.WriteLine(CliConstants.RoverAddInput);
                var addRoverInput = Console.ReadLine();

                if (addRoverInput != null &&
                    !addRoverInput.Equals("Y", StringComparison.InvariantCultureIgnoreCase)) break;
            }

            commandBus
                .PublishMultipleAsync(commands.ToArray())
                .GetAwaiter()
                .GetResult();

            Console.WriteLine(CliConstants.ExpectedOutput);

            var aggregateStore = resolver.Resolve<IAggregateStore>();

            foreach (var rover in roverIdList.Select(roverId =>
                aggregateStore.LoadAsync<RoverAggregate, Identity>(roverId, CancellationToken.None).Result))
                Console.WriteLine($"{rover.RoverPosition.X} " +
                                  $"{rover.RoverPosition.Y} " +
                                  $"{rover.RoverPosition.Orientation.ToString()}");

            Console.Write("Press <enter> to exit...");
            Console.ReadLine();
        }
    }
}