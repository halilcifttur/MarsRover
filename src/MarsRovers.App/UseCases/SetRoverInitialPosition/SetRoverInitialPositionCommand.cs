using System.Threading;
using System.Threading.Tasks;
using MarsRovers.App.UseCases.Dtos.Models;
using MarsRovers.App.UseCases.MarsRovers.Models;
using MediatR;

namespace MarsRovers.App.UseCases.SetRoverInitialPosition;

public class SetRoverInitialPositionCommand : IRequest<Rover>
{
    public Coordinate Coordinate { get; set; }
    public string Moves { get; set; }
    public SetRoverInitialPositionCommand(Coordinate coordinate, string moves)
    {
        Coordinate = coordinate;
        Moves = moves;
    }
}

public class SetRoverInitialPositionCommandHandler : IRequestHandler<SetRoverInitialPositionCommand, Rover>
{
    public Task<Rover> Handle(SetRoverInitialPositionCommand request, CancellationToken cancellationToken)
    {
        var rover = new Rover
        {
            InitialPosition = request.Coordinate,
            Moves = request.Moves
        };

        return Task.FromResult(rover);
    }
}