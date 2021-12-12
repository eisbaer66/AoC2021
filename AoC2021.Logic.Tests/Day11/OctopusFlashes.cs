using System;
using System.Collections.Generic;
using System.Linq;
using AoC2021.Logic.DumboOctopuses;

namespace AoC2021.Logic.Tests.Day11
{
    public class OctopusFlashes
    {
        private readonly IDictionary<(int X, int Y), Octopus> _octopuses;

        public int Tick { get; }

        public OctopusFlashes(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            _octopuses = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                              .SelectMany((line, y) => line.Select((c, x) => new Octopus(x, y, int.Parse((string)c.ToString()))).ToArray())
                              .ToDictionary(o => (o.X, o.Y));
            Tick = 0;
        }

        public OctopusFlashes(IDictionary<(int X, int Y), Octopus> octopuses, int tick)
        {
            if (tick < 0) throw new ArgumentOutOfRangeException(nameof(tick));

            _octopuses = octopuses ?? throw new ArgumentNullException(nameof(octopuses));
            Tick       = tick;
        }

        public int CountFlashes()
        {
            return _octopuses.Values.Count(octopus => octopus.Flashes);
        }

        private bool IsSynced()
        {
            return _octopuses.Values.All(o => o.Flashes);
        }

        public IEnumerable<OctopusFlashes> NextTicks(int count)
        {
            var flashes = this;
            yield return flashes;
            for (var i = 0; i < count; i++)
            {
                flashes = flashes.NextTick();
                yield return flashes;
            }
        }

        public OctopusFlashes TickUntilSync()
        {
            var levels = this;

            while (true)
            {
                levels = levels.NextTick();
                if (levels.IsSynced())
                    return levels;
            }
        }

        public OctopusFlashes NextTick()
        {
            var newOctopuses = _octopuses.Values
                                         .Select(o => new Octopus(o.X, o.Y, o.EnergyLevel))
                                         .ToDictionary(o => (o.X, o.Y));
            var flashes = new OctopusFlashes(newOctopuses, Tick + 1);
            flashes.Increment();
            flashes.FlashIfNeeded();
            flashes.ResetFlashedEnergies();

            return flashes;
        }

        private void Increment()
        {
            foreach (var octopus in _octopuses.Values)
            {
                octopus.EnergyLevel++;
            }
        }

        private void FlashIfNeeded()
        {
            foreach (var octopus in _octopuses.Values)
            {
                FlashIfNeeded(octopus);
            }
        }

        private void ResetFlashedEnergies()
        {
            foreach (var o in _octopuses.Values.Where(o => o.Flashes))
            {
                o.EnergyLevel = 0;
            }
        }

        private void FlashIfNeeded(Octopus octopus)
        {
            if (octopus.Flashes)
                return;

            var visited = new HashSet<(int, int)>();
            var queue   = new Queue<Octopus>();
            queue.Enqueue(octopus);

            while (queue.Count > 0)
            {
                var o = queue.Dequeue();
                if (o.EnergyLevel <= 9)
                    continue;
                if (o.Flashes)
                    continue;

                o.Flashes = true;
                visited.Add((o.X, o.Y));

                var neighbors = GetNeighbors(o.X, o.Y)
                    .Where(n => !visited.Contains((n.X, n.Y)));
                foreach (var neighbor in neighbors)
                {
                    neighbor.EnergyLevel++;
                    queue.Enqueue(neighbor);
                }
            }
        }

        private IEnumerable<Octopus> GetNeighbors(int x, int y)
        {
            var coords = new[]
                         {
                             //left
                             (x - 1, y - 1),
                             (x - 1, y + 0),
                             (x - 1, y + 1),

                             //middle
                             (x + 0, y - 1),
                             (x + 0, y + 1),

                             //right
                             (x + 1, y - 1),
                             (x + 1, y + 0),
                             (x + 1, y + 1),
                         };
            foreach (var coord in coords)
            {
                if (_octopuses.ContainsKey(coord))
                    yield return _octopuses[coord];
            }
        }
    }
}