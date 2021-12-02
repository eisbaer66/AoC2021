using System;
using AoC2021.Logic.Day2.Steps;

namespace AoC2021.Logic.Day2.Part1
{
    public class StepFactory : IStepFactory
    {
        public IStep From(string name, int value)
        {
            return name switch
                   {
                       "forward" => new Steps.Forward(value),
                       "down"    => new Steps.Down(value),
                       "up"      => new Steps.Up(value),
                       _         => throw new NotSupportedException("command '" + name + "' is not supported")
                   };
        }
    }
}
