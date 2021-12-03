using System;
using System.Linq;

namespace AoC2021.Logic.Routeing
{
    public class Route
    {
        private readonly IStepFactory _stepFactory;

        public Route(IStepFactory stepFactory)
        {
            _stepFactory = stepFactory;
        }

        public Position Follow(string input)
        {
            return input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(command =>
                                {
                                    var args  = command.Split(' ');
                                    var name  = args[0];
                                    var value = int.Parse(args[1]);

                                    return _stepFactory.From(name, value);
                                })
                        .Aggregate(new Position(), (position, step) => step.Apply(position));
        }
    }
}