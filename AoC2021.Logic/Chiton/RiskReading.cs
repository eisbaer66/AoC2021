using System.Collections.Generic;

namespace AoC2021.Logic.Chiton
{
    public record RiskReading
    {
        public RiskReading(Coordinate Coordinate, int Risk)
        {
            this.Coordinate = Coordinate;
            this.Risk       = Risk;
            this.Neighbors  = new List<RiskReading>();
        }

        public Coordinate        Coordinate { get; init; }
        public int               Risk       { get; init; }
        public List<RiskReading> Neighbors  { get; init; }

        public void Deconstruct(out Coordinate Coordinate, out int Risk)
        {
            Coordinate = this.Coordinate;
            Risk       = this.Risk;
        }
    }
}