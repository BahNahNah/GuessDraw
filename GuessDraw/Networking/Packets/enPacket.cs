using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets
{
    enum enPacket : byte
    {
        NullPacket,
        NullPacket2,
        ConnectionRequest,
        ConnectionSuccess,
        PlayerConnection,
        DrawDot,
        SetColor,
        SetSize,
        ClearCanvas,
        ChatMessage,
        SetDrawStatus,
        Disconnect,
        UpdateStatus,
        WordToDraw,
        WordInfo,
        UpdateTimer,
        WinAnnounce
    }
}
