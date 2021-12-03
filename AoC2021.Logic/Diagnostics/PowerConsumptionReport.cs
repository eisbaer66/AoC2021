namespace AoC2021.Logic.Diagnostics
{
    public record PowerConsumptionReport
    {
        public int GammaRate        { get; init; }
        public int EpsilonRate      { get; init; }
        public int PowerConsumption => GammaRate * EpsilonRate;
    }
}