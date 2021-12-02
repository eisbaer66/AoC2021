﻿namespace AoC2021.Logic.Day2.Steps
{
    internal record Up : IStep
    {
        public int Value { get; set; }

        public Up(int value)
        {
            Value = value;
        }

        public Position Apply(Position position)
        {
            return position with { Aim = position.Aim - Value };
        }
    }
}