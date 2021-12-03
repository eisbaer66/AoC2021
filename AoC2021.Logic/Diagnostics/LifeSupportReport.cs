namespace AoC2021.Logic.Diagnostics
{
    public record LifeSupportReport
    {
        public int OxygenGeneratorRating { get; init; }
        public int Co2ScrubberRating     { get; init; }
        public int LifeSupportRating     => OxygenGeneratorRating * Co2ScrubberRating;
    }
}