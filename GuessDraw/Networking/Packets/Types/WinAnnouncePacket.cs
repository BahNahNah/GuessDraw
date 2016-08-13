using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class WinAnnouncePacket : Packet
    {
        public WinAnnouncePacket(string s) : base(enPacket.WinAnnounce)
        {
            Write(s);
        }
    }
}
