using AoC2021.Logic.Day2.Steps;

namespace AoC2021.Logic.Day2
{
    public interface IStepFactory
    {
        IStep From(string name, int value);
    }
}