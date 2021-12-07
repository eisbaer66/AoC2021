using System;

namespace AoC2021.Logic.CrabSubmarines.FuelConsumptions
{
    public class LinearFuelConsumption : IFuelConsumption
    {
        public int Between(int a, int b)
        {
            return Math.Abs(a - b);
        }
    }
}