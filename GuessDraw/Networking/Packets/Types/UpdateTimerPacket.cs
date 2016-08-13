using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class UpdateTimerPacket : Packet
    {
        public UpdateTimerPacket(int progressValue) : base(enPacket.UpdateTimer)
        {
            Write(progressValue);
        }

    }
}
