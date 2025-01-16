using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

namespace TCPServer
{
    /// <summary>
    /// Information about the client.
    /// </summary>
    [XmlRoot("ClientInfo")]
    public class ClientInfo
    {
        /// <summary>
        /// The version of the protocol used in the client.
        /// Must match the protocol version used on the server.
        /// </summary>
        [XmlElement("ProtocolVersion")]
        public uint ProtocolVersion;
        // More coming soon =3
    }

    /// <summary>
    /// A client, not necessarily registered.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// The socket used to communicate with the client.
        /// </summary>
        public Socket socket;
        /// <summary>
        /// Whether the client is registered or not.
        /// </summary>
        public bool registered = false;

        /// <summary>
        /// Whether the connection was accepted or not.
        /// </summary>
        public bool connectionAccepted = false;

        /// <summary>
        /// The server associated with this client.
        /// </summary>
        public ClientServer associatedServer;
    }

    /// <summary>
    /// A registered user.
    /// </summary>
    public class User : Client
    {
        public string UserName;
        public Guid ThreadGuid;

        public DateTime CreatedOn;
        public DateTime ConnectedOn;

        /// <summary>
        /// Users that are allowed to chat with this user.
        /// </summary>
        public List<User> Friends;

        /// <summary>
        /// Users that aren't allowed to chat with this user.
        /// Messages sent by anyone in this list won't be delivered to the client.
        /// </summary>
        public List<User> Blocked;
    }
}
