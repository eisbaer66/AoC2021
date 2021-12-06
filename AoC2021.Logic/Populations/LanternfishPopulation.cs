using System;
using System.Linq;

namespace AoC2021.Logic.Populations
{
    public class LanternfishPopulation
    {
        private readonly long[] _countsPerAge;

        public LanternfishPopulation(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            
            _countsPerAge = input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                               .Select(int.Parse)
                               .GroupBy(i => i)
                               .Aggregate(new long[9], (list, age) =>
                                                       {
                                                           list[age.Key] = age.Count();
                                                           return list;
                                                       });
        }

        public long Count => _countsPerAge.Sum();

        public void Tick(int ticks)
        {
            for (var tick = 0; tick < ticks; tick++)
            {
                var bornFishCount = _countsPerAge[0];
                _countsPerAge[0] = _countsPerAge[1];
                _countsPerAge[1] = _countsPerAge[2];
                _countsPerAge[2] = _countsPerAge[3];
                _countsPerAge[3] = _countsPerAge[4];
                _countsPerAge[4] = _countsPerAge[5];
                _countsPerAge[5] = _countsPerAge[6];
                _countsPerAge[6] = bornFishCount + _countsPerAge[7];
                _countsPerAge[7] = _countsPerAge[8];
                _countsPerAge[8] = bornFishCount;
            }
        }
    }
}