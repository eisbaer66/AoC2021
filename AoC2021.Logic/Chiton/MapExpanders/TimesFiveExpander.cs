using System.Collections.Generic;

namespace AoC2021.Logic.Chiton.MapExpanders
{
    public class TimesFiveExpander : IExpander
    {
        public IEnumerable<RiskReading> Expand(RiskReading node, int maxX, int maxY)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    var newRisk = (node.Risk + x + y);
                    while (newRisk > 9)
                    {
                        newRisk -= 9;
                    }

                    yield return node with
                                 {
                                     Coordinate = new Coordinate(node.Coordinate.X + ((maxX + 1) * x), 
                                                                 node.Coordinate.Y + ((maxY + 1) * y)),
                                     Neighbors = new List<RiskReading>(),
                                     Risk = newRisk
                                 };
                } 
            }
        }
    }
}