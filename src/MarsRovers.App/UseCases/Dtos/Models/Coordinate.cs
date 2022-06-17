using MarsRovers.App.UseCases.Dtos.Enums;

namespace MarsRovers.App.UseCases.MarsRovers.Models;

public class Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }
    public Directions Direction { get; set; }
}