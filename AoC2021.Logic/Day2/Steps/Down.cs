namespace AoC2021.Logic.Day2.Steps
{
    internal record Down : IStep
    {
        public int Value { get; set; }

        public Down(int value)
        {
            Value = value;
        }

        public Position Apply(Position position)
        {
            return position with { Aim = position.Aim + Value };
        }
    }
}