namespace AoC2021.Logic.Routeing.Steps
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
            return position with
                   {
                       HorizontalPosition = position.HorizontalPosition + Value,
                       Depth = position.Depth                           + position.Aim * Value
                   };
        }
    }
}