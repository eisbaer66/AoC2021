using AoC2021.Logic.Routeing.Steps;

namespace AoC2021.Logic.Routeing
{
    public interface IStepFactory
    {
        IStep From(string name, int value);
    }
}