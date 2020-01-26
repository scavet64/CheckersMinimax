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
    public class Client : TCPConnection
    {
        //Signal to send object
        private volatile Boolean stopListening = false;
        private AutoResetEvent sendMessageSignal = new AutoResetEvent(false);

        /// <summary>
        /// Creates a message client (receive messages)
        /// </summary>
        /// <param name="address">IP Address</param>
        /// <param name="port">Port number</param>
        public Client(IPAddress address, Int32 port)
        {
            this.address = address;
            this.port = port;
        }

        /// <summary>
        /// Starts a connection
        /// </summary>
        public void StartConnection()
        {
            try
            {
                //Close if exists
                if (tcpClient != null) tcpClient.Close();

                //Create client
                //Try to connect
                ConnectionState = ConnectionStates.Connecting;
                tcpClient = new TcpClient(address.ToString(), port);
                networkStream = tcpClient.GetStream();

                listenThread = new Thread(new ThreadStart(ListenLoop));
                listenThread.Start();
            }
            catch (SocketException ex)
            {
                throw new MultiplayerException(ex.Message);
            }

            //IAsyncResult ar = tcpClient.BeginConnect(address, port, new AsyncCallback(OnConnect), tcpClient);

            ////Check if timed out
            //Boolean success = ar.AsyncWaitHandle.WaitOne(2000, false);
            //if (!success)
            //{
            //    ConnectionState = ConnectionStates.NotConnected;
            //    throw new MultiplayerException("Timed out");
            //}
        }

        ///// <summary>
        ///// Runs when the client connects to the server
        ///// </summary>
        ///// <param name="ar">Connection object and state</param>
        //public void OnConnect(IAsyncResult ar)
        //{
        //    using (TcpClient client = (TcpClient)ar.AsyncState)
        //    {
        //        try
        //        {
        //            //Accept the connection
        //            client.EndConnect(ar);

        //            //Connected
        //            Connected = true;
        //            ConnectionState = ConnectionStates.Connected;

        //            //Retrieve the network stream.  
        //            using (NetworkStream stream = client.GetStream())
        //            {

        //                IFormatter formatter = new BinaryFormatter();
        //                while (true)
        //                {
        //                    //Wait for the signal to send
        //                    sendMessageSignal.WaitOne();

        //                    //Send the data
        //                    lock (message)
        //                    {
        //                        formatter.Serialize(stream, message);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.ToString());
        //        }
        //        finally
        //        {
        //            Connected = false;
        //            ConnectionState = ConnectionStates.NotConnected;
        //        }
        //    }
        //}

        /// <summary>
        /// Stops the connection
        /// </summary>
        public void StopConnection()
        {
            if (tcpClient != null) tcpClient.Close();
        }
    }
}
