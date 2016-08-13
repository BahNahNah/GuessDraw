using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class DrawDotPacket : Packet
    {
        public DrawDotPacket(Point p) : base(enPacket.DrawDot)
        {
            Write(p);
        }
    }
}
