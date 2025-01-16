using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TCPServer
{
    /// <summary>
    /// Events that can be triggered by the server.
    /// </summary>
    public class Events
    {
        /// <summary>
        /// Event handler for when a client connects to the server.
        /// </summary>
        /// <param name="client">The client that connected.</param>
        /// <param name="message">The message sent by the client. Usually an XML string containing user data</param>
        public delegate void OnConnectionRequest(Client client, byte[] message);
    }
}
