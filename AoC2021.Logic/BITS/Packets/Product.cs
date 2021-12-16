using System.Linq;

namespace AoC2021.Logic.BITS.Packets
{
    public class Product : PacketBase
    {
        public override long Execute()
        {
            return SubPackets.Select(p => p.Execute())
                             .Aggregate((long)1, (product, l) => product * l);
        }
    }
}