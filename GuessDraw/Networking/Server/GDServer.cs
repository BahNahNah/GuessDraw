using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Server
{

    delegate void OnClientConnectDelegate(ConnectedClient client);
    class GDServer
    {
        public Socket NetworkSocket { get; private set; }
        public event OnClientConnectDelegate OnClientConnect;

        private AsyncCallback OnAcceptCallback;
        public void Start(int port)
        {
            OnAcceptCallback = new AsyncCallback(HandleAccept);

            NetworkSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            NetworkSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            NetworkSocket.Listen(50);
            NetworkSocket.BeginAccept(OnAcceptCallback, null);
        }

        private void HandleAccept(IAsyncResult AR)
        {
            Socket s = NetworkSocket.EndAccept(AR);
            ConnectedClient c = new ConnectedClient(s);
            OnClientConnect?.Invoke(c);

            NetworkSocket.BeginAccept(OnAcceptCallback, null);
        }
    }
}
