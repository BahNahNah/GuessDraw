using GuessDraw.Controls;
using GuessDraw.Networking.Packets;
using GuessDraw.Networking.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Server
{
    delegate void ClientDataRetrieveDelegate(ConnectedClient sender, byte[] data);
    delegate void ConnectedClientDisconnectDelegate(ConnectedClient sender);
    class ConnectedClient
    {
        public event ClientDataRetrieveDelegate ClientDataRetrieve;
        public event ConnectedClientDisconnectDelegate ClientDisconnect;
        public PacketManager Manager { get; private set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public bool Connected { get; set; }
        public PlayerStatus Status { get; set; }
        public ListViewItemConnectedClient ListViewItem {get;set;}
        public ConnectedClient(Socket s)
        {
            Connected = false;
            Status = PlayerStatus.Idle;

            Manager = new PacketManager(s);
            Manager.OnDataRetrieve += Manager_OnDataRetrieve;
            Manager.OnDisconnect += Manager_OnDisconnect;
        }

        private void Manager_OnDisconnect(PacketManager sender)
        {
            ClientDisconnect?.Invoke(this);
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
