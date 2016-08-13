using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class SetSizePacket : Packet
    {
        public SetSizePacket(int size) : base(enPacket.SetSize)
        {
            Write(size);
        }
    }
}
