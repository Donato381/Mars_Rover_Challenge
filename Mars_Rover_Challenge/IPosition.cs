namespace Mars_Rover_Challenge
{
    public interface IPosition
    {
        Directions direction { get; set; }
        int X { get; set; }
        int Y { get; set; }
    }
}