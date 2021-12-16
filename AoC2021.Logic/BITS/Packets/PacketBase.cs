using System.Collections.Generic;

namespace AoC2021.Logic.BITS.Packets
{
    public abstract class PacketBase
    {
        public int               Version    { get; set; }
        public int               TypeId     { get; set; }
        public IList<PacketBase> SubPackets { get; set; } = new List<PacketBase>();
        public int               Size       { get; set; }

        public abstract long Execute();
    }
}