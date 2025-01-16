using System;
using System.Collections.Generic;
using System.Text;

namespace SharedObjects
{
    /// <summary>
    /// The message struct.
    /// </summary>
    public struct Message
    {
        /// <summary>
        /// The status code of the message.
        /// </summary>
        public StatusCode Code;

        /// <summary>
        /// The message sent by the client.
        /// </summary>
        public ushort ReplyingTo;

        /// <summary>
        /// The message sent by the client.
        /// </summary>
        public byte[] Data;
    }
}
