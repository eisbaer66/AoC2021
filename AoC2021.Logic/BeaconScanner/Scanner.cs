using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AoC2021.Logic.BeaconScanner
{
    [DebuggerDisplay("{Id}: {Beacons.Count} Beacons")]
    public class Scanner
    {
        private readonly IList<Coordinate> _beacons     = new List<Coordinate>();
        private readonly IList<Conversion> _conversions = new List<Conversion>();

        public int Id { get; }

        public IList<Coordinate> Beacons     => new List<Coordinate>(_beacons);
        public IList<Conversion> Conversions => new List<Conversion>(_conversions);

        public Scanner(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));
            Id = id;
        }

        internal void AddBeacon(int x, int y, int z)
        {
            _beacons.Add(new Coordinate(x, y, z));
        }

        internal void AddConversion(Conversion conversion)
        {
            _conversions.Add(conversion);
        }
    }
}