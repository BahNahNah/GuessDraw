using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class SetColorPacket : Packet
    {
        public SetColorPacket(Color c) : base(enPacket.SetColor)
        {
            Write(c);
        }
    }
}
