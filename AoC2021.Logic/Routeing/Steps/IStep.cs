namespace AoC2021.Logic.Routeing.Steps
{
    public interface IStep
    {
        Position Apply(Position position);
    }
}