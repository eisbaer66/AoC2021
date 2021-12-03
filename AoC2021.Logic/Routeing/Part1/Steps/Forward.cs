using AoC2021.Logic.Routeing.Steps;

namespace AoC2021.Logic.Routeing.Part1.Steps
{
    internal record Forward : IStep
    {
        public int Value { get; }

        public Forward(int value)
        {
            Value = value;
        }

        public Position Apply(Position position)
        {
            return position with { HorizontalPosition = position.HorizontalPosition + Value };
        }
    }
}