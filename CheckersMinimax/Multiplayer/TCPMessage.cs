using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Multiplayer
{
    [Serializable]
    public class TCPMessage
    {
        private object data;

        public object Data
        {
            get { return data; }
            set { data = value; }
        }

        public TCPMessage(object data)
        {
            this.data = data;
        }
    }
}
