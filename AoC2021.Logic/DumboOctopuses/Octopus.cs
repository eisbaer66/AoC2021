using System.Diagnostics;

namespace AoC2021.Logic.DumboOctopuses
{
    [DebuggerDisplay("{EnergyLevel} | {Flashes} | ({X} {Y})")]
    public class Octopus
    {
        public int  X           { get; }
        public int  Y           { get; }
        public int  EnergyLevel { get; set; }
        public bool Flashes     { get; set; }

        public Octopus(int x, int y, int energyLevel)
        {
            X           = x;
            Y           = y;
            EnergyLevel = energyLevel;
            Flashes     = false;
        }
    }
}