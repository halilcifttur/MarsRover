using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MarsRovers.App.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MarsRovers.App.UseCases.SetRoverInitialPosition;
using MarsRovers.App.UseCases.StartRoverSimulation;
using MarsRovers.App.UseCases.Dtos.Enums;
using MarsRovers.App.UseCases.Dtos.Models;
using MarsRovers.App.UseCases.MarsRovers.Models;

namespace MarsRovers.App;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = ServiceProviderFactory.ServiceProvider;
        var mediator = serviceProvider.GetRequiredService<IMediator>();

        var rovers = new List<Rover>();
        var testInput = "5 5\r\n" +
                        "1 2 N\r\n" +
                        "LMLMLMLMM\r\n" +
                        "3 3 E\r\n" +
                        "MMRMMRMRRM";

        var stringReader = new StringReader(testInput);
        var plataeuSize = (await stringReader.ReadLineAsync()).Split(' ').Select(int.Parse).ToList();

        while (true)
        {
            string rowData = await stringReader.ReadLineAsync();
            if (string.IsNullOrEmpty(rowData))
                break;

            var initialCoordinate = rowData.Trim().Split(' ');
            var initialPosition = new Coordinate
            {
                X = int.Parse(initialCoordinate[0]),
                Y = int.Parse(initialCoordinate[1]),
                Direction = Enum.Parse<Directions>(initialCoordinate[2])
            };

            var moves = (await stringReader.ReadLineAsync()).Trim().ToUpper();
            rovers.Add(await mediator.Send(new SetRoverInitialPositionCommand(initialPosition, moves)));
        }
        stringReader.Dispose();

        await mediator.Send(new StartRoverSimulationCommand(rovers, plataeuSize));
    }
}