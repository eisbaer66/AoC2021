using AoC2021.Logic.Day2.Steps;

namespace AoC2021.Logic.Day2.Part1.Steps
{
    internal record Up : IStep
    {
        public int Value { get; set; }

        public Up(int value)
        {
            Value = value;
        }

        public Position Apply(Position position)
        {
            return position with { Depth = position.Depth - Value };
        }
    }
}