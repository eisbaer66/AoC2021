using System.Collections.Generic;

namespace AoC2021.Logic.Chiton.MapExpanders
{
    public class NullExpander : IExpander
    {
        public IEnumerable<RiskReading> Expand(RiskReading node, int maxX, int maxY)
        {
            return new []{ node };
        }
    }
}