using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class ClearCanvasPacket : Packet
    {
        public ClearCanvasPacket() : base(enPacket.ClearCanvas)
        { }
    }
}
