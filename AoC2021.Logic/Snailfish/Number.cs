using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.Snailfish
{
    public class Number
    {
        private Number _leftValue;
        private Number _rightValue;

        public Number LeftValue
        {
            get => _leftValue;
            set
            {
                _leftValue = value;
                if (_leftValue != null)
                    _leftValue.Parent = this;
            }
        }

        public Number RightValue
        {
            get => _rightValue;
            set
            {
                _rightValue = value;
                if (_rightValue != null)
                    _rightValue.Parent = this;
            }
        }

        public int?            Value  { get; set; }
        public Number Parent { get; private set; }
        public bool            IsPair => Value == null;

        private Number()
        {
        }

        private Number(int? value)
        {
            Value = value;
        }

        public int GetMagnitude()
        {
            if (!IsPair)
                return Value.Value;

            return 3 * LeftValue.GetMagnitude() + 2 * RightValue.GetMagnitude();
        }

        public Number Add(Number other)
        {
            var number = new Number
                         {
                             LeftValue  = this,
                             RightValue = other
                         };
            number.Reduce();
            return number;
        }

        private void Reduce()
        {
            bool reduced;
            do
            {
                reduced = ReduceOnce();
            } while (reduced);
        }

        public bool ReduceOnce()
        {
            return Explode(0) || Split();
        }

        private bool Explode(int depth)
        {
            if (!IsPair)
                return false;

            if (depth < 4)
                return LeftValue.Explode(depth + 1) || RightValue.Explode(depth + 1);

            Parent.AddToLeftNeighbor(this, LeftValue.Value);
            Parent.AddRightNeighbor(this, RightValue.Value);

            LeftValue  = null;
            RightValue = null;
            Value      = 0;
            return true;
        }

        private bool Split()
        {
            if (IsPair)
                return LeftValue.Split() || RightValue.Split();

            if (Value < 10)
                return false;

            LeftValue  = new Number(Value / 2);
            RightValue = new Number((int)Math.Round((float)Value / 2, MidpointRounding.AwayFromZero));
            Value      = null;
            return true;
        }

        private void AddToLeftNeighbor(Number source, int? value)
        {
            if (LeftValue == source)
            {
                Parent?.AddToLeftNeighbor(this, value);
                return;
            }

            if (RightValue != source)
                throw new InvalidOperationException("source is not a child");

            if (LeftValue.IsPair)
            {
                LeftValue.AddRightChild(value);
                return;
            }

            LeftValue.Value += value;
        }

        private void AddRightNeighbor(Number source, int? value)
        {
            if (RightValue == source)
            {
                Parent?.AddRightNeighbor(this, value);
                return;
            }

            if (LeftValue != source)
                throw new InvalidOperationException("source is not a child");

            if (RightValue.IsPair)
            {
                RightValue.AddLeftChild(value);
                return;
            }

            RightValue.Value += value;
        }

        private void AddLeftChild(int? value)
        {
            if (LeftValue != null)
            {
                LeftValue.AddLeftChild(value);
                return;
            }

            Value += value;
        }

        private void AddRightChild(int? value)
        {
            if (RightValue != null)
            {
                RightValue.AddRightChild(value);
                return;
            }

            Value += value;
        }

        public Number Copy()
        {
            return new Number(Value)
                   {
                       LeftValue  = LeftValue?.Copy(),
                       RightValue = RightValue?.Copy()
                   };
        }

        public static Number Parse(string input)
        {
            var numbers  = new Stack<Number>();
            var span     = new ReadOnlySpan<char>(input.ToArray());
            var position = 0;

            numbers.Push(new Number());
            while (position < span.Length)
            {
                var c = span[position];
                position++;


                switch (c)
                {
                    case ',':
                        var number = numbers.Pop();
                        var parent = numbers.Peek();
                        parent.LeftValue = number;
                        break;
                    case ']':
                        var rightNumber = numbers.Pop();
                        var rightParent = numbers.Peek();
                        rightParent.RightValue = rightNumber;
                        break;
                    case '[':
                        numbers.Push(new Number());
                        break;
                    default:
                        
                        string s;
                        var    cNext = span[position];
                        if (cNext != ',' &&
                            cNext != '[' &&
                            cNext != ']')
                        {
                            s = new string(new[] { c, cNext });
                            position++;
                        }
                        else
                        {
                            s = c.ToString();
                        }

                        numbers.Push(new Number(int.Parse(s)));

                        break;
                }
            }

            return numbers.Pop();
        }

        public override string ToString()
        {
            if (!IsPair)
                return Value.ToString();
            return "[" + LeftValue + "," + RightValue + "]";
        }
    }
}