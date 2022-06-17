using MarsRovers.App.UseCases.MarsRovers.Models;

namespace MarsRovers.App.UseCases.Dtos.Models;

public class Rover
{
    public Coordinate InitialPosition { get; set; }
    public Coordinate FinalPosition { get; set; }
    public string Moves { get; set; }
}