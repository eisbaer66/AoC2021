using System;
using System.Collections.Generic;
using System.Linq;
using AoC2021.Logic.BITS.Packets;

namespace AoC2021.Logic.BITS.Parsing
{
    public class Parser
    {
        private readonly char[]             _data;
        private          int                _position;
        private          ReadOnlySpan<char> Span => _data;

        public Parser(string hexData)
        {
            if (hexData == null) throw new ArgumentNullException(nameof(hexData));

            _data = hexData.Replace("\r", "")
                         .Replace("\n", "")
                         .SelectMany(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'))
                         .ToArray();
        }

        private Parser(char[] data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public PacketBase ReadPacket()
        {
            var startPosition = _position;
            var packetVersion = ReadInt(3);
            var packetTypeId  = ReadInt(3);
            if (packetTypeId == 4)
            {
                var literalValue = ReadLiteral();
                return new Literal
                       {
                           Version = packetVersion,
                           TypeId  = packetTypeId,
                           Value   = literalValue,
                           Size    = _position - startPosition,
                       };
            }

            var lengthTypeId = ReadInt(1);
            var subPackets   = new List<PacketBase>();
            if (lengthTypeId == 0)
            {
                var lengthInBits = ReadInt(15);
                var bits         = ReadBits(lengthInBits);
                var subParser    = new Parser(bits);

                var readBits = 0;
                do
                {
                    var @operator = subParser.ReadPacket();
                    readBits += @operator.Size;

                    subPackets.Add(@operator);
                } while (readBits < lengthInBits);
            }
            else
            {
                var lengthInPackets = ReadInt(11);

                do
                {
                    var @operator = ReadPacket();
                    subPackets.Add(@operator);
                } while (subPackets.Count < lengthInPackets);
            }

            return packetTypeId switch
                   {
                       0 => new Sum { Version         = packetVersion, TypeId = packetTypeId, SubPackets = subPackets, Size = _position - startPosition, },
                       1 => new Product { Version     = packetVersion, TypeId = packetTypeId, SubPackets = subPackets, Size = _position - startPosition, },
                       2 => new Minimum { Version     = packetVersion, TypeId = packetTypeId, SubPackets = subPackets, Size = _position - startPosition, },
                       3 => new Maximum { Version     = packetVersion, TypeId = packetTypeId, SubPackets = subPackets, Size = _position - startPosition, },
                       5 => new GreaterThen { Version = packetVersion, TypeId = packetTypeId, SubPackets = subPackets, Size = _position - startPosition, },
                       6 => new LessThen { Version    = packetVersion, TypeId = packetTypeId, SubPackets = subPackets, Size = _position - startPosition, },
                       7 => new EqualTo { Version     = packetVersion, TypeId = packetTypeId, SubPackets = subPackets, Size = _position - startPosition, },
                       _ => throw new InvalidOperationException("Packet-Type-ID " + packetTypeId + " is unknown")
                   };
        }

        public int ReadInt(int count)
        {
            var value = Convert.ToInt32(Span.Slice(_position, count).ToString(), 2);
            _position += count;

            return value;
        }

        public char[] ReadBits(int count)
        {
            var slice = Span.Slice(_position, count);
            _position += count;

            return slice.ToArray();
        }

        public long ReadLiteral()
        {
            var bits = new List<char>();
            bool       keepReading;
            do
            {
                keepReading = ReadInt(1) == 1;

                bits.AddRange(ReadBits(4));
            } while (keepReading);

            return Convert.ToInt64(new string(bits.ToArray()), 2);
        }
    }
}