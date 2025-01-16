using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;
using SharedObjects;

namespace TCPServer
{
    internal class Program
    {
        private static void ConnectionRequest(Client client, byte[] message)
        {
            Console.WriteLine($"[Server] Connection request from {client.socket.RemoteEndPoint}");
            Console.WriteLine($"[Server] Message: {Encoding.UTF8.GetString(message)}");

            XmlSerializer serializer = new XmlSerializer(typeof(ClientInfo));
            ClientInfo result;
            using (TextReader reader = new StringReader(Encoding.UTF8.GetString(message)))
            {
                result = (ClientInfo)serializer.Deserialize(reader);
            }
            Console.WriteLine($"[Server] Client version: {result.ProtocolVersion}");
            if (result.ProtocolVersion != ProtocolInfo.ProtocolVersion)
            {
                Console.WriteLine("[Server] Client version is not compatible with server version");
                client.associatedServer.Send(StatusCode.ConnectionRejected, "Client version is not compatible with server version");
                return;
            }
            client.associatedServer.Send(StatusCode.ConnectionAccepted, "Connection accepted");
        }

        private static void Main(string[] args)
        {
            List<Client> clients = new();
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                int port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(localAddr, port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();
                Console.WriteLine("Server started on port {0}", port);

                // Enter the listening loop.
                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    // Create a thread to handle the client connection.
                    ClientServer clientServer = new(client.Client);
                    clientServer.OnConnectionRequest += ConnectionRequest;
                    clientServer.Start();
                    //Thread clientThread = new(clientServer.);
                    //clientThread.Start(client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server?.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}
