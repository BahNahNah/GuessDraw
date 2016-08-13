using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class SetDrawStatusPacket : Packet 
    {
        public SetDrawStatusPacket(bool canDraw) : base(enPacket.SetDrawStatus)
        {
            Write(canDraw);
        }
    }
}
