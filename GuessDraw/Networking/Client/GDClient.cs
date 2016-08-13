using GuessDraw.Controls;
using GuessDraw.Networking.Packets;
using GuessDraw.Networking.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Client
{
    delegate void ClientRetrieveDelegate(GDClient sender, byte[] data);
    class GDClient
    {
        public Socket NetworkSocket { get; private set; }
        public event ClientRetrieveDelegate ClientDataRetrieve;


        public PacketManager Manager { get; private set; }


        public bool Connect(string IP, int port)
        {

            NetworkSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                NetworkSocket.Connect(IP, port);
            }
            catch
            {
                return false;
            }

            Manager = new PacketManager(NetworkSocket);
            Manager.OnDataRetrieve += Manager_OnDataRetrieve;
            return true;
        }

        private void Manager_OnDataRetrieve(byte[] data)
        {
            ClientDataRetrieve?.Invoke(this, data);
        }

        public void StartRetrieve()
        {
            Manager.StartRetrieve();
        }

        public void Send(Packet p)
        {
            Manager.Send(p.ToBytes());
        }
    }
}
