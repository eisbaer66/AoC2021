namespace AoC2021.Logic.Day2.Steps
{
    public interface IStep
    {
        Position Apply(Position position);
    }
}