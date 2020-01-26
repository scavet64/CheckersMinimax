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
    public class Server : TCPConnection
    {
        //TCP Listener
        private TcpListener tcpListener;

        /// <summary>
        /// Creates a message server (send messages)
        /// </summary>
        /// <param name="address">IP Address</param>
        /// <param name="port">Port number</param>
        public Server(IPAddress address, Int32 port)
        {
            this.address = address;
            this.port = port;
        }

        /// <summary>
        /// Listens for connections and sends data when ready
        /// </summary>
        public void StartListening()
        {
            //Check to see if already open
            if (connectionState == ConnectionStates.NotConnected)
            {
                //Create listener
                tcpListener = new TcpListener(address, port);
                tcpListener.Start();

                //Wait for connection
                //tcpListener.BeginAcceptTcpClient(new AsyncCallback(OnAcceptTcpClient), tcpListener);
                SetupNetworkStream();
            }
        }

        private void SetupNetworkStream()
        {
            // Enter the listening loop.
            Console.Write("Waiting for a connection... ");

            // Perform a blocking call to accept requests.
            // You could also user server.AcceptSocket() here.
            TcpClient client = tcpListener.AcceptTcpClient();
            Console.WriteLine("Connected!");

            //Set connection state
            Connected = true;

            // Get a stream object for reading and writing
            networkStream = client.GetStream();

            listenThread = new Thread(new ThreadStart(ListenLoop));
            listenThread.Start();
        }



        ///// <summary>
        ///// Accepts the connection request
        ///// </summary>
        ///// <param name="ar"></param>
        //public void OnAcceptTcpClient(IAsyncResult ar)
        //{
        //    try
        //    {
        //        //Accept pending connection
        //        using (TcpClient client = tcpListener.EndAcceptTcpClient(ar))
        //        {
        //            //Set connection state
        //            Connected = true;
        //            //Retrieve the network stream.  
        //            using (NetworkStream stream = client.GetStream())
        //            {
        //                IFormatter formatter = new BinaryFormatter();

        //                //Loop forever waiting for new messages, or until stream throws exception
        //                while (true)
        //                {
        //                    message = (TCPMessage)formatter.Deserialize(stream);
        //                    //Console.WriteLine("Message type: " + message.Type);

        //                    if (MessageReceived != null) MessageReceived(message);
        //                    MessageRecievedWaitHandle.Set();


        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //    finally
        //    {
        //        //Stop listening for new requests
        //        //listener.Stop();
        //        Connected = false;
        //        ConnectionState = ConnectionStates.NotConnected;
        //    }
        //}

        /// <summary>
        /// Request to stop the server
        /// </summary>
        public void StopListening()
        {
            //stopListening = true;
            //sendMessageSignal.Set();
            if (tcpListener != null) tcpListener.Stop();
        }
    }
}
