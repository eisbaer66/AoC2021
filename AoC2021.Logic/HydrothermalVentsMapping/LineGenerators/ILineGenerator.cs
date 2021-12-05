using System.Collections.Generic;

namespace AoC2021.Logic.HydrothermalVentsMapping.LineGenerators
{
    public interface ILineGenerator
    {
        IEnumerable<Coordinate> From(Coordinate startPoint, Coordinate endPoint);
    }
}