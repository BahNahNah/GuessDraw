using GuessDraw.Networking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessDraw.Controls
{
    class ListViewItemClient : ListViewItem
    {
        public int ID { get; private set; } 
        public ListViewItemClient(int _id, string name) : base(name)
        {
            ID = _id;
        }
    }
}
