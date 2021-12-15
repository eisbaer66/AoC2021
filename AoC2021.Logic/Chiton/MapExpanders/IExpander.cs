using System.Collections.Generic;

namespace AoC2021.Logic.Chiton.MapExpanders
{
    public interface IExpander
    {
        IEnumerable<RiskReading> Expand(RiskReading node, int maxX, int maxY);
    }
}