namespace AoC2021.Logic.BeaconScanner
{
    public record Conversion(Scanner From, Scanner To, SensorConfiguration Configuration)
    {
        public Conversion Reverse()
        {
            return new Conversion(To, From, Configuration.Reverse());
        }
    }
}