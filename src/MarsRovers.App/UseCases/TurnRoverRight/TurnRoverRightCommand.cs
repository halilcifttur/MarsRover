using System.Threading;
using System.Threading.Tasks;
using MarsRovers.App.UseCases.Dtos.Enums;
using MediatR;

namespace MarsRovers.App.UseCases.TurnRoverRight;

public class TurnRoverRightCommand : IRequest<Directions>
{
    public Directions Direction { get; set; }
    public TurnRoverRightCommand(Directions direction)
    {
        Direction = direction;

    }
}

public class TurnRoverRightCommandHandler : IRequestHandler<TurnRoverRightCommand, Directions>
{
    public Task<Directions> Handle(TurnRoverRightCommand request, CancellationToken cancellationToken)
    {
        if (request.Direction == Directions.N)
            request.Direction = Directions.E;
        else if (request.Direction == Directions.S)
            request.Direction = Directions.W;
        else if (request.Direction == Directions.E)
            request.Direction = Directions.S;
        else if (request.Direction == Directions.W)
            request.Direction = Directions.N;

        return Task.FromResult(request.Direction);
    }
}