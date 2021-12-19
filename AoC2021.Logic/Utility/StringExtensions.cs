using System;
using System.Collections.Generic;

namespace AoC2021.Logic.Utility
{
    public static class StringExtensions
    {
        public static IEnumerable<string> ToLines(this string input)
        {
            return input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}