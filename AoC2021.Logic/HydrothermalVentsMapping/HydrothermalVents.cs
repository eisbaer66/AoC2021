using System;
using System.Collections.Generic;
using System.Linq;
using AoC2021.Logic.HydrothermalVentsMapping.LineGenerators;

namespace AoC2021.Logic.HydrothermalVentsMapping
{
    public class HydrothermalVents
    {
        private readonly IDictionary<Coordinate, int> _coordinates;

        public HydrothermalVents(string input, ILineGenerator lineGenerator)
        {
            _coordinates = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                .SelectMany(line =>
                                            {
                                                var segments = line.Split(" -> ");
                                                var startPoints = segments[0]
                                                                  .Split(',')
                                                                  .Select(int.Parse)
                                                                  .ToArray();
                                                var endPoints = segments[1]
                                                                .Split(',')
                                                                .Select(int.Parse)
                                                                .ToArray();
                                                var startPoint = new Coordinate(startPoints[0], startPoints[1]);
                                                var endPoint   = new Coordinate(endPoints[0],   endPoints[1]);

                                                return lineGenerator.From(startPoint, endPoint) ?? Array.Empty<Coordinate>();
                                            })
                                .GroupBy(cs => cs)
                                .ToDictionary(g => g.Key, g => g.Count());
        }

        public int CountGreaterThen(int minLines)
        {
            return _coordinates.Count(c => c.Value >= minLines);
        }
    }
}