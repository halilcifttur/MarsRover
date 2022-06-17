using System.Threading;
using System.Threading.Tasks;
using MarsRovers.App.UseCases.Dtos.Enums;
using MarsRovers.App.UseCases.MarsRovers.Models;
using MediatR;

namespace MarsRovers.App.UseCases.MoveRover;

public class MoveRoverCommand : IRequest<Coordinate>
{
    public Coordinate Coordinate { get; set; }
    public MoveRoverCommand(Coordinate coordinate)
    {
        Coordinate = coordinate;
    }
}

public class MoveRoverCommandHandler : IRequestHandler<MoveRoverCommand, Coordinate>
{
    public Task<Coordinate> Handle(MoveRoverCommand request, CancellationToken cancellationToken)
    {
        if (request.Coordinate.Direction == Directions.N)
            request.Coordinate.Y++;
        else if (request.Coordinate.Direction == Directions.S)
            request.Coordinate.Y--;
        else if (request.Coordinate.Direction == Directions.E)
            request.Coordinate.X++;
        else if (request.Coordinate.Direction == Directions.W)
            request.Coordinate.X--;

        return Task.FromResult(request.Coordinate);
    }
}