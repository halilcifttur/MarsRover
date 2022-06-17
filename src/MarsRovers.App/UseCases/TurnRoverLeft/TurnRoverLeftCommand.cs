using System.Threading;
using System.Threading.Tasks;
using MarsRovers.App.UseCases.Dtos.Enums;
using MediatR;

namespace MarsRovers.App.UseCases.TurnRoverLeft;

public class TurnRoverLeftCommand : IRequest<Directions>
{
    public Directions Direction { get; set; }
    public TurnRoverLeftCommand(Directions direction)
    {
        Direction = direction;

    }
}

public class TurnRoverLeftCommandHandler : IRequestHandler<TurnRoverLeftCommand, Directions>
{
    public Task<Directions> Handle(TurnRoverLeftCommand request, CancellationToken cancellationToken)
    {
        if (request.Direction == Directions.N)
            request.Direction = Directions.W;
        else if (request.Direction == Directions.S)
            request.Direction = Directions.E;
        else if (request.Direction == Directions.E)
            request.Direction = Directions.N;
        else if (request.Direction == Directions.W)
            request.Direction = Directions.S;

        return Task.FromResult(request.Direction);
    }        
}