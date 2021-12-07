using System;
using System.Linq;
using AoC2021.Logic.CrabSubmarines.FuelConsumptions;

namespace AoC2021.Logic.CrabSubmarines
{
    public class Alignment
    {
        private readonly IFuelConsumption _consumption;

        private readonly int[] _positions;

        public Alignment(string input, IFuelConsumption consumption)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            _consumption = consumption ?? throw new ArgumentNullException(nameof(consumption));

            _positions = input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                     .Select(int.Parse)
                                     .ToArray();
        }

        public int Align()
        {
            var max     = _positions.Max();
            var minFuel = int.MaxValue;
            for (var target = 0; target <= max; target++)
            {
                var fuel = _positions.Select(i => _consumption.Between(i, target)).Sum();
                if (fuel < minFuel)
                    minFuel = fuel;
            }

            return minFuel;
        }
    }
}