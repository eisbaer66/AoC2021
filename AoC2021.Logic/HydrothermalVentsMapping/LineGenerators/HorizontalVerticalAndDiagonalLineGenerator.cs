using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.HydrothermalVentsMapping.LineGenerators
{
    public class HorizontalVerticalAndDiagonalLineGenerator : HorizontalAndVerticalLineGenerator
    {
        public override IEnumerable<Coordinate> From(Coordinate startPoint, Coordinate endPoint)
        {
            var list = base.From(startPoint, endPoint);
            if (list != null)
                return list;

            var xs = Range(startPoint.X, endPoint.X);
            var ys = Range(startPoint.Y, endPoint.Y);

            return xs.Zip(ys)
                     .Select(c => new Coordinate(c.First, c.Second));
        }
    }
}