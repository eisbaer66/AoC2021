using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.SyntaxScoring
{
    public class Chunks
    {
        private readonly string[] _lines;

        public Chunks(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            _lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public int FindCorruptionScore()
        {
            return _lines.Select(Check)
                         .Where(x => x.IllegalChar != null)
                         .Select(x => (char)x.IllegalChar)
                         .Select(GetCorruptionScore)
                         .Sum();
        }

        public long FindAutoCompleteScore()
        {
            var scores = _lines.Select(Check)
                               .Where(x => x.IllegalChar == null)
                               .Select(x => x.UnmatchedChunks)
                               .Select(GetAutoCompleteScore)
                               .OrderBy(s => s)
                               .ToArray();
            var index = scores.Length / 2;
            return scores[index];
        }

        private static SyntaxError Check(string line)
        {
            var stack = new Stack<char>();
            foreach (var c in line)
            {
                if (IsOpening(c))
                {
                    stack.Push(c);
                    continue;
                }

                var opening = MatchOpening(c);
                if (stack.Peek() != opening)
                    return new SyntaxError(c, stack.ToArray());

                stack.Pop();
            }

            return new SyntaxError(null, stack.ToArray());
        }

        private static bool IsOpening(char c)
        {
            return c is '(' or '[' or '{' or '<';
        }

        private static char MatchOpening(char c)
        {
            return c switch
                   {
                       ')' => '(',
                       ']' => '[',
                       '}' => '{',
                       '>' => '<',
                       _   => throw new InvalidOperationException(c + " is not closing")
                   };
        }

        private static int GetCorruptionScore(char c)
        {
            return c switch
                   {
                       ')' => 3,
                       ']' => 57,
                       '}' => 1197,
                       '>' => 25137,
                       _   => throw new InvalidOperationException(c + " is not closing")
                   };
        }

        private static long GetAutoCompleteScore(char[] chars)
        {
            return chars
                   .Select(GetAutoCompleteScore)
                   .Aggregate((long)0, (sum, score) => (sum * 5) + score);
        }

        private static int GetAutoCompleteScore(char c)
        {
            return c switch
                   {
                       '(' => 1,
                       '[' => 2,
                       '{' => 3,
                       '<' => 4,
                       _   => throw new InvalidOperationException(c + " is not a chunk")
                   };
        }
    }
}