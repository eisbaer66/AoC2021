using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC2021.Logic.Utility;

namespace AoC2021.Logic.TransparentOrigami
{
    public class Sheet
    {
        private readonly HashSet<Coordinate> _dots;
        private readonly IList<Fold>         _folds;

        public Sheet(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var (dotLines, foldLines) = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                             .UnZip(line => line.Contains(','));

            _dots = dotLines
                    .Select(line =>
                            {
                                var segments = line.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                   .ToArray();
                                return new Coordinate(int.Parse(segments[0]), int.Parse(segments[1]));
                            })
                    .ToHashSet();
            _folds = foldLines
                     .Select(line =>
                             {
                                 var segments = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                    .Last()
                                                    .Split('=')
                                                    .ToArray();
                                 return new Fold(segments[0] == "x" ? Direction.X : Direction.Y, int.Parse(segments[1]));
                             })
                     .ToList();
        }

        public int DotCount => _dots.Count;

        public string GetDotPattern()
        {
            var maxX = _dots.Select(dot => dot.X).Max();
            var maxY = _dots.Select(dot => dot.Y).Max();

            var stringBuilder = new StringBuilder();
            for (var y = 0; y <= maxY; y++)
            {
                for (var x = 0; x <= maxX; x++)
                {
                    var c = _dots.Contains(new Coordinate(x, y)) ? '#' : '.';
                    stringBuilder.Append(c);
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        public void ExecuteFolds()
        {
            foreach (var fold in _folds)
            {
                ExecuteFold(fold);
            }

            _folds.Clear();
        }

        public void ExecuteOneFold()
        {
            var fold = _folds.First();

            ExecuteFold(fold);

            _folds.Remove(fold);
        }

        private void ExecuteFold(Fold fold)
        {
            switch (fold.Direction)
            {
                case Direction.Y:
                    ExecuteVerticalFold(fold.Position);
                    break;
                case Direction.X:
                    ExecuteHorizontalFold(fold.Position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Direction " + fold.Direction + " is unknown");
            }
        }

        private void ExecuteHorizontalFold(int position)
        {
            ExecuteFold(coord => coord.X > position,
                        dot => new Coordinate(position - (dot.X - position), dot.Y));
        }

        private void ExecuteVerticalFold(int position)
        {
            ExecuteFold(coord => coord.Y > position,
                        dot => new Coordinate(dot.X, position - (dot.Y - position)));
        }

        private void ExecuteFold(Func<Coordinate, bool> predicate, Func<Coordinate, Coordinate> func)
        {
            var dots = _dots.Where(predicate).ToArray();
            foreach (var dot in dots)
            {
                _dots.Remove(dot);
                _dots.Add(func(dot));
            }
        }
    }
}