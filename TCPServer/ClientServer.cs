using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SharedObjects;
using Microsoft.FSharp.Control;

namespace TCPServer
{
    public class ClientServer
    {
        public Socket ClientSocket;
        private bool running;
        private Thread thread;
        public readonly Guid ClientGuid = Guid.NewGuid();
        public Client client { get; private set; }

        // Events
        public event Events.OnConnectionRequest OnConnectionRequest;

        public ClientServer(Socket clientSocket)
        {
            ClientSocket = clientSocket;
            thread = new Thread(Serve);
            client = new Client()
            {
                socket = clientSocket,
                registered = false,
                associatedServer = this
            };
        }

        public void Send(StatusCode code, string message, ushort replyingTo = 0)
        {
            try { SocketOperations.Send(ClientSocket, code, message, replyingTo); }
            catch (SocketException) { Stop(); }
        }

        public void Start() => thread.Start();

        private void Serve()
        {
            running = true;
            ClientSocket.ReceiveTimeout = 1000;
            try
            {
                while (running)
                {
                    Message message1 = SocketOperations.GetMessage(ClientSocket);
                    StatusCode errorCode = message1.Code;
                    byte[] message = message1.Data;
                    Console.WriteLine($"[Server] Message received: {errorCode}, {BigEndianBitConverter.ToString(message)}");

                    //if (errorCode == StatusCode.PingRequest)
                    //{
                    //    Console.WriteLine("[Server] Ping request received.");
                    //    Send(StatusCode.PingResponse, "Pong");
                    //    continue;
                    //}

                    if (((byte)errorCode) >> 4 == 0x2
                        && client.connectionAccepted == false)
                    {
                        Console.WriteLine("[Server] Connection not accepted yet.");
                        Send(StatusCode.Unauthorized, "Please, send your configuration first.");
                        continue;
                    }

                    switch (errorCode)
                    {
                        case StatusCode.ConnectionRequest:
                            //Console.WriteLine($"[Server] Connection request from {ClientSocket.RemoteEndPoint}");
                            OnConnectionRequest?.Invoke(client, message);
                            break;
                        case StatusCode.ConnectionClose:
                            Console.WriteLine("[Server] Connection closed by client.");
                            running = false;
                            break;
                        case StatusCode.PingRequest:
                            Console.WriteLine("[Server] Client is still alive!");
                            break;
                        default:
                            Console.WriteLine($"[Server] Unknown error code: {errorCode}");
                            break;
                    }
                }
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode == SocketError.TimedOut)
                    Console.WriteLine("[Server] Timeout");
                else
                    Console.WriteLine($"[Server] Socket exception: {e.Message} ({e.SocketErrorCode})");
            }
            ClientSocket.Close();
            running = false;
            Console.WriteLine($"[Server] Thread {ClientGuid} stopped.");
        }

        public void Stop()
        {
            running = false;
            if (thread != null && thread.IsAlive)
            {
                thread.Join();
            }
            ClientSocket.Close();
        }
    }
}
