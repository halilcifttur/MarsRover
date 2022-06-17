using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MarsRovers.App.UseCases.Dtos.Models;
using MarsRovers.App.UseCases.GetRoverFinalPosition;
using MarsRovers.App.UseCases.MoveRover;
using MarsRovers.App.UseCases.TurnRoverLeft;
using MarsRovers.App.UseCases.TurnRoverRight;
using MediatR;

namespace MarsRovers.App.UseCases.StartRoverSimulation;

public class StartRoverSimulationCommand : IRequest<List<Rover>>
{
    public List<Rover> Rovers { get; set; }
    public List<int> PlataeuSize { get; set; }

    public StartRoverSimulationCommand(List<Rover> rovers, List<int> plataeuSize)
    {
        Rovers = rovers;
        PlataeuSize = plataeuSize;
    }
}

public class StartRoverSimulationCommandHandler : IRequestHandler<StartRoverSimulationCommand, List<Rover>>
{
    private readonly IMediator mediator;
    public StartRoverSimulationCommandHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<List<Rover>> Handle(StartRoverSimulationCommand request, CancellationToken cancellationToken)
    {
        foreach (var rover in request.Rovers)
        {
            rover.FinalPosition = rover.InitialPosition;
            foreach (var move in rover.Moves)
            {
                switch (move)
                {
                    case 'M':
                        rover.FinalPosition = await mediator.Send(new MoveRoverCommand(rover.InitialPosition), cancellationToken);
                        break;
                    case 'L':
                        rover.FinalPosition.Direction = await mediator.Send(new TurnRoverLeftCommand(rover.InitialPosition.Direction), cancellationToken);
                        break;
                    case 'R':
                        rover.FinalPosition.Direction = await mediator.Send(new TurnRoverRightCommand(rover.InitialPosition.Direction), cancellationToken);
                        break;
                    default:
                        Console.WriteLine($"Invalid character : {move}");
                        break;
                }
                if (rover.FinalPosition.X < 0 || rover.FinalPosition.X > request.PlataeuSize[0]
                    || rover.FinalPosition.Y < 0 || rover.FinalPosition.Y > request.PlataeuSize[1])
                    throw new ArgumentException($" must be within the bounds of (0 , 0) and ({request.PlataeuSize[0]} , {request.PlataeuSize[1]})");
            }
            await mediator.Send(new GetRoverFinalPositionQuery(rover.FinalPosition), cancellationToken);
        }

        return request.Rovers;
    }
}