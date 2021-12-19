using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AoC2021.Logic.Utility;
using AoC2021.Logic.Utility.PathFinding.Dijkstra;

namespace AoC2021.Logic.BeaconScanner
{
    public class Map
    {
        private readonly List<Scanner>             _scanners;
        private readonly IDictionary<int, Scanner> _dictionary;
        private readonly PathFinding<Scanner, int> _pathFinding;

        public Map(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            _scanners = new List<Scanner>();
            Scanner currentScanner = null;
            foreach (var line in input.ToLines())
            {
                var headerMatch = Regex.Match(line, @"--- scanner (\d+) ---");
                if (headerMatch.Success)
                {
                    currentScanner = new Scanner(int.Parse(headerMatch.Groups[1].Value));
                    _scanners.Add(currentScanner);
                    continue;
                }

                if (currentScanner == null)
                    throw new InvalidOperationException("unknown input format");

                var match = Regex.Match(line, @"(-?\d+),(-?\d+),(-?\d+)");
                if (!match.Success)
                    throw new InvalidOperationException("unknown input format");

                var x = int.Parse(match.Groups[1].Value);
                var y = int.Parse(match.Groups[2].Value);
                var z = int.Parse(match.Groups[3].Value);

                currentScanner.AddBeacon(x, y, z);
            }

            FillConfigurations();

            _dictionary = _scanners.ToDictionary(s => s.Id);

            _pathFinding = new PathFinding<Scanner, int>(_scanners,
                                                         scanner => new Node<Scanner>(scanner),
                                                         scanner => scanner.Id,
                                                         scanner => scanner.Conversions.Select(c => new Neighbor<int>(c.To.Id, 1)));
        }

        public HashSet<Coordinate> FindUniqueBeacons()
        {
            var beacons = _dictionary[0].Beacons.ToHashSet();

            for (var i = 1; i < _scanners.Count; i++)
            {
                var configurations = FindConfigurationPathFrom0(i);

                foreach (var beacon in _dictionary[i].Beacons)
                {
                    var translatedBeacon = configurations
                        .Aggregate(beacon,
                                   (current, configuration) => new Coordinate(configuration.Rotation.X.Apply(current) + configuration.X,
                                                                              configuration.Rotation.Y.Apply(current) + configuration.Y,
                                                                              configuration.Rotation.Z.Apply(current) + configuration.Z));

                    if (beacons.Contains(translatedBeacon))
                        continue;

                    beacons.Add(translatedBeacon);
                }
            }

            return beacons;
        }

        public IEnumerable<Coordinate> FindScannerCoordinates()
        {
            var beacons = new[] { new Coordinate(0, 0, 0) }.ToHashSet();

            for (var i = 1; i < _scanners.Count; i++)
            {
                var configurations = FindConfigurationPathFrom0(i);

                var translatedBeacon = configurations
                    .Aggregate(new Coordinate(0, 0, 0),
                               (current, configuration) => new Coordinate(configuration.Rotation.X.Apply(current) + configuration.X,
                                                                          configuration.Rotation.Y.Apply(current) + configuration.Y,
                                                                          configuration.Rotation.Z.Apply(current) + configuration.Z));

                if (beacons.Contains(translatedBeacon))
                    continue;

                beacons.Add(translatedBeacon);
            }

            return beacons;
        }

        private SensorConfiguration[] FindConfigurationPathFrom0(int i)
        {
            var lastScanner = _dictionary[0];
            var configurations = _pathFinding.FindPath(0, i)
                                             .Skip(1)
                                             .Select(scanner =>
                                                     {
                                                         var (_, _, sensorConfiguration) = lastScanner.Conversions.First(c => c.To.Id == scanner.Id);

                                                         lastScanner = scanner;
                                                         return sensorConfiguration;
                                                     })
                                             .Reverse()
                                             .ToArray();
            return configurations;
        }

        private void FillConfigurations()
        {
            var potentialScannerPairs = _scanners.CombinationsWithoutPermutation();
            var potentialAxisAdjustments = Matrix.GenerateAllRotations().ToArray();

            var matchingPairs = potentialScannerPairs.Select(pair => new Conversion(pair.a,
                                                                                    pair.b,
                                                                                    GetConfiguration(pair, potentialAxisAdjustments)))
                                                     .Where(x => x.Configuration != null)
                                                     .ToArray();

            foreach (var conversion in matchingPairs)
            {
                conversion.From.AddConversion(conversion);
                conversion.To.AddConversion(conversion.Reverse());
            }
        }

        private static SensorConfiguration GetConfiguration((Scanner a, Scanner b) pair,
                                                           IEnumerable<Matrix>    potentialAxisAdjustments)
        {
            var aBeacons = pair.a.Beacons;
            var bBeacons = pair.b.Beacons;

            return potentialAxisAdjustments
                   .Select(adj => new SensorConfigurationBuilder
                                  {
                                      Rotation = adj,
                                  })
                   .ForEach(c => c.X = GetPossibleTranslation(b => b.X, m => m.X, c.Rotation, aBeacons, bBeacons))
                   .Where(x => x.X != null)
                   .ForEach(c => c.Y = GetPossibleTranslation(b => b.Y, m => m.Y, c.Rotation, aBeacons, bBeacons))
                   .Where(c => c.Y != null)
                   .ForEach(c => c.Z = GetPossibleTranslation(b => b.Z, m => m.Z, c.Rotation, aBeacons, bBeacons))
                   .FirstOrDefault(c => c.Z != null)
                   ?.ToConfiguration();
        }

        private static int? GetPossibleTranslation(Func<Coordinate, int>   componentSelector,
                                                   Func<Matrix, MatrixRow> rowSelector,
                                                   Matrix                  potentialAxisAdjustment,
                                                   IEnumerable<Coordinate> aBeacons,
                                                   IEnumerable<Coordinate> bBeacons)
        {
            var aValues = aBeacons.Select(componentSelector).ToArray();
            return bBeacons.Select(b =>
                                   {
                                       var row = rowSelector(potentialAxisAdjustment);
                                       return row.Apply(b);
                                   })
                           .SelectMany(b => aValues.Select(a => a - b))
                           .GroupBy(x => x)
                           .Where(g => g.Count() >= 12)
                           .Select(g => g.Key)
                           .Cast<int?>()
                           .FirstOrDefault();
        }
    }
}