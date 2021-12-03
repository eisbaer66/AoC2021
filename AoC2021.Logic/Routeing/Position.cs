namespace AoC2021.Logic.Routeing
{
    public record Position
    {
        public int HorizontalPosition { get; init; } = 0;
        public int Aim                { get; init; } = 0;
        public int Depth              { get; init; } = 0;
    }
}