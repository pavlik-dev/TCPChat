using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedObjects;

namespace TCPClient
{
    public class Events
    {
        public delegate void OnConnectionAccept(Client client);
        public delegate void OnConnectionReject(Client client);

        public delegate void OnError(Client client, StatusCode code);

        public delegate void OnMessageReceived(Client client, byte[] message);
    }
}
