using System;
using AoC2021.Logic.Day2.Steps;

namespace AoC2021.Logic.Day2
{
    public class StepFactory : IStepFactory
    {
        public IStep From(string name, int value)
        {
            return name switch
                   {
                       "forward" => new Forward(value),
                       "down"    => new Down(value),
                       "up"      => new Up(value),
                       _         => throw new NotSupportedException("command '" + name + "' is not supported")
                   };
        }
    }
}