using MarsRovers.App.Infrastructure;
using MarsRovers.App.UseCases.Dtos.Enums;
using MarsRovers.App.UseCases.Dtos.Models;
using MarsRovers.App.UseCases.MarsRovers.Models;
using MarsRovers.App.UseCases.StartRoverSimulation;
using MediatR;

namespace MarsRovers.Test;

public class MarsRoverTest
{
    private readonly IMediator _mediator;
    private static IServiceProvider serviceProvider = ServiceProviderFactory.ServiceProvider;
    public MarsRoverTest()
    {
        _mediator = ((IMediator)serviceProvider.GetService(typeof(IMediator)));
    }
    [Fact]
    public async Task successfully_process()
    {
        var plataeuSize = new List<int>() { 5, 5 };
        var moves = "LMLMLMLMM";
        var cordinate = new Coordinate()
        {
            X = 1,
            Y = 2,
            Direction = Directions.N
        };
        var rover = new Rover
        {
            InitialPosition = cordinate,
            Moves = moves
        };

        var rovers = await _mediator.Send(new StartRoverSimulationCommand (new List<Rover> { rover }, plataeuSize));

        var actualOutput = $"{rovers.First().FinalPosition.X} {rovers.First().FinalPosition.Y} {rovers.First().FinalPosition.Direction.ToString()}";
        var expectedOutput = "1 3 N";

        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public async Task successfully_process_v2()
    {
        var plataeuSize = new List<int>() { 5, 5 };
        var moves = "MMRMMRMRRM";
        var cordinate = new Coordinate()
        {
            X = 3,
            Y = 3,
            Direction = Directions.E
        };
        var rover = new Rover
        {
            InitialPosition = cordinate,
            Moves = moves
        };

        var rovers = await _mediator.Send(new StartRoverSimulationCommand(new List<Rover> { rover }, plataeuSize ));

        var actualOutput = $"{rovers.First().FinalPosition.X} {rovers.First().FinalPosition.Y} {rovers.First().FinalPosition.Direction.ToString()}";
        var expectedOutput = "5 1 E";

        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public async Task unsuccessfully_process_v1()
    {
        var plataeuSize = new List<int>() { 5, 5 };
        var moves = "LMLMLMLLLMM";
        var cordinate = new Coordinate()
        {
            X = 1,
            Y = 2,
            Direction = Directions.S
        };
        var rover = new Rover
        {
            InitialPosition = cordinate,
            Moves = moves
        };

        var rovers = await _mediator.Send(new StartRoverSimulationCommand(new List<Rover> { rover }, plataeuSize));

        var actualOutput = $"{rovers.First().FinalPosition.X} {rovers.First().FinalPosition.Y} {rovers.First().FinalPosition.Direction.ToString()}";
        var expectedOutput = "1 3 N";

        Assert.NotEqual(expectedOutput, actualOutput);
    }

    [Fact]
    public async Task unsuccessfully_process_v2()
    {
        //Given
        var plataeuSize = new List<int>() { 5, 5 };
        var moves = "MMRMMRMLLM";
        var cordinate = new Coordinate()
        {
            X = 1,
            Y = 3,
            Direction = Directions.E
        };
        var rover = new Rover
        {
            InitialPosition = cordinate,
            Moves = moves
        };

        //When
        var rovers = await _mediator.Send(new StartRoverSimulationCommand(new List<Rover> { rover }, plataeuSize));

        //Then
        var actualOutput = $"{rovers.First().FinalPosition.X} {rovers.First().FinalPosition.Y} {rovers.First().FinalPosition.Direction.ToString()}";
        var expectedOutput = "4 1 W";

        Assert.NotEqual(expectedOutput, actualOutput);
    }

    [Fact]
    public void get_plataeuSize_exception()
    {
        var plataeuSize = new List<int>() { 5, 5 };
        var moves = "MMMMMMRMMRMLLM";
        var cordinate = new Coordinate()
        {
            X = 1,
            Y = 3,
            Direction = Directions.E
        };
        var rover = new Rover
        {
            InitialPosition = cordinate,
            Moves = moves
        };
        Assert.ThrowsAsync<ArgumentException>(async () => await _mediator.Send(new StartRoverSimulationCommand(new List<Rover> { rover }, plataeuSize)));
    }
}