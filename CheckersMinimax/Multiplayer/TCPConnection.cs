using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckersMinimax.Multiplayer
{
    public class TCPConnection
    {
        //Current TCP Client
        protected TcpClient tcpClient;

        //Current network stream to send data back and forth
        protected NetworkStream networkStream;

        //Address and port to serve on
        protected IPAddress address;
        protected Int32 port;

        protected ConnectionStates connectionState = ConnectionStates.NotConnected;
        public Boolean Connected = false;
        protected IFormatter formatter = new BinaryFormatter();

        protected AutoResetEvent MessageRecievedWaitHandle = new AutoResetEvent(false);

        //Events
        public event Action<TCPMessage> MessageReceived;
        public event Action<ConnectionStates> ConnectionStateChanged;

        /// <summary>
        /// Connection state
        /// </summary>
        public ConnectionStates ConnectionState
        {
            get { return connectionState; }
            set
            {
                connectionState = value;
                ConnectionStateChanged?.Invoke(connectionState);
            }
        }

        protected Thread listenThread;

        //Connection state
        public enum ConnectionStates
        {
            NotConnected,
            Listening,
            Connected,
            Connecting
        };

        /// <summary>
        /// Updates the message data and sets the signal to send
        /// </summary>
        /// <param name="m">New message data</param>
        public void SendMessage(TCPMessage message)
        {
            //Send a message
            lock (message)
            {
                formatter.Serialize(networkStream, message);
            }
        }

        protected TCPMessage message;

        public TCPMessage WaitForTCPMessage()
        {
            if (MessageRecievedWaitHandle.WaitOne())
            {
                return message;
            }
            else
            {
                throw new MultiplayerException("No client connected");
            }
        }

        protected void ListenLoop()
        {
            while (true)
            {
                if (networkStream != null)
                {
                    //Recieve the message
                    message = (TCPMessage)formatter.Deserialize(networkStream);
                    MessageReceived?.Invoke(message);
                    MessageRecievedWaitHandle.Set();
                }
            }
        }
    }
}
