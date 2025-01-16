using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using SharedObjects;

namespace TCPClient
{
    /// <summary>
    /// The client class.
    /// </summary>
    public class Client
    {
        public bool Working { get; private set; }
        private TcpClient TcpClient = new();
        private DateTime lastMessageSent;

        private Thread receiveMessagesThread;
        private Thread pingThread;

        public IPEndPoint serverEndPoint { get; private set; }

        public Client(IPAddress ipAddress, int port)
        {
            receiveMessagesThread = new Thread(Receive);
            pingThread = new Thread(SendPingMessages);

            serverEndPoint = new IPEndPoint(ipAddress, port);

            TcpClient = new TcpClient();
            TcpClient.ReceiveTimeout = 10 * 1000; // 10 seconds
        }

        private void SendPingMessages(object? sender, ElapsedEventArgs e) => SendPingMessages();

        public void Start()
        {
            TcpClient.Connect(serverEndPoint);
            receiveMessagesThread.Start();

            // Building XML data
            var j = new XElement("ClientInfo",
                new XElement("ProtocolVersion", ProtocolInfo.ProtocolVersion));
            string xmlData = j.ToString();

            // Sending connection request
            Send(StatusCode.ConnectionRequest, xmlData);
            pingThread.Start();
            Working = true;
        }

        public void Stop()
        {
            Send(StatusCode.ConnectionClose, "Connection closed");
            Working = false;
            receiveMessagesThread.Join();
            pingThread.Join();
            TcpClient.Close();
        }

        public void SendPingMessages()
        {
            while(Working)
                if (DateTime.Now - lastMessageSent > TimeSpan.FromSeconds(0.9))
                    Send(StatusCode.PingRequest, "Ping");
        }

        public void Send(StatusCode code, string message, ushort replyingTo = 0)
        {
            SocketOperations.Send(TcpClient.Client, code, message, replyingTo);
            lastMessageSent = DateTime.Now;
        }

        public void Receive()
        {
            while (Working)
            {
                var message = SocketOperations.GetMessage(TcpClient.Client);
                Console.WriteLine($"[Client] Message received: {message.Code}, {BigEndianBitConverter.ToString(message.Data)}");
            }
            Debug.WriteLine("[Client] Client stopped.");
        }
    }
}
