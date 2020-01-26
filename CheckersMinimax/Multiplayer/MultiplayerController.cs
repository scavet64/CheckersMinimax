using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMinimax.Multiplayer
{
    public class MultiplayerController
    {
        private static readonly int Port = 35785;
        private static readonly string MultiplayerToken = "ce568ed2-946a-4eb9-8298-913a4f4a9895";

        private Client client;
        private Server server;

        public void StartClientConnection(string ipAddress)
        {
            if(server != null)
            {
                throw new MultiplayerException("Cannot have a client and server running at the same time");
            }
            else if (client != null)
            {
                client.StopConnection();
            }

            try
            {
                IPAddress ip = IPAddress.Parse(ipAddress);
                client = new Client(ip, Port);
                client.StartConnection();

                client.SendMessage(new TCPMessage(MultiplayerToken));
            }
            catch (MultiplayerException mpEx)
            {
                throw mpEx;
            }
        }

        public bool StartServerConnection()
        {
            if (client != null)
            {
                throw new MultiplayerException("Cannot have a client and server running at the same time");
            }
            else if (server != null)
            {
                server.StopListening();
            }

            server = new Server(IPAddress.Any, Port);
            server.StartListening();

            //wait for a client connection
            TCPMessage message = server.WaitForTCPMessage();
            if (message.Data is string dataString && dataString.Equals(MultiplayerToken))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CheckersMove GetMultiplayerMove()
        {
            if (client != null)
            {
                return (CheckersMove)client.WaitForTCPMessage().Data;
            }
            else
            {
                return (CheckersMove)server.WaitForTCPMessage().Data;
            }
        }

        public void SendMessage(TCPMessage message)
        {
            if(client != null)
            {
                client.SendMessage(message);
            }
            else
            {
                server.SendMessage(message);
            }
        }

    }
}
