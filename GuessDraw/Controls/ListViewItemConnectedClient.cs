using GuessDraw.Networking.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessDraw.Controls
{
    class ListViewItemConnectedClient : ListViewItem
    {
        public ConnectedClient ConnectedClient { get; private set; }
        public ListViewItemConnectedClient(ConnectedClient _client) : base(_client.Name)
        {
            ConnectedClient = _client;
            ConnectedClient.ListViewItem = this;
        }
    }
}
