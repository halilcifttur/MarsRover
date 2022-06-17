using System;
using System.Threading;
using System.Threading.Tasks;
using MarsRovers.App.UseCases.MarsRovers.Models;
using MediatR;

namespace MarsRovers.App.UseCases.GetRoverFinalPosition;

public class GetRoverFinalPositionQuery : IRequest
{
    public Coordinate FinalPosition { get; set; }
    public GetRoverFinalPositionQuery(Coordinate finalPosition)
    {
        FinalPosition = finalPosition;

    }
}

public class GetRoverFinalPositionQueryHandler : AsyncRequestHandler<GetRoverFinalPositionQuery>
{
    protected override Task Handle(GetRoverFinalPositionQuery request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{request.FinalPosition.X} {request.FinalPosition.Y} {request.FinalPosition.Direction}");
        return Task.CompletedTask;
    }
}