namespace SharedObjects
{
    public enum StatusCode : byte
    {
        // General/Informational (0x0X)

        /// <summary>
        ///  No Operation (NOP)
        /// </summary>
        NoOperation = 0x00,

        /// <summary>
        ///  OK/Success
        /// </summary>
        Success = 0x01,

        // Connection Management (0x1X)
        /// <summary>
        ///  Connection Request
        /// </summary>
        ConnectionRequest = 0x10,

        /// <summary>
        ///  Connection Accepted
        /// </summary>
        ConnectionAccepted = 0x11,

        /// <summary>
        ///  Connection Rejected
        /// </summary>
        ConnectionRejected = 0x12,

        /// <summary>
        ///  Connection Closed by Peer
        /// </summary>
        ConnectionClosedByPeer = 0x13,

        /// <summary>
        /// Connection Close
        /// </summary>
        ConnectionClose = 0x14,

        /// <summary>
        ///  Ping Request
        /// </summary>
        PingRequest = 0x15,

        /// <summary>
        ///  Ping Response
        /// </summary>
        PingResponse = 0x16,

        // Messaging (0x2X)

        /// <summary>
        /// Message Send; Client-only
        /// </summary>
        MessageSend = 0x20,
        /// <summary>
        ///  Message Sent; Server-only
        /// </summary>
        MessageSent = 0x21,
        /// <summary>
        ///  Message Delivered; Server-only
        /// </summary>
        MessageDelivered = 0x22,
        /// <summary>
        ///  Message Read; Client-only
        /// </summary>
        MessageRead = 0x23,
        /// <summary>
        ///  Typing Indicator On; Client-only
        /// </summary>
        TypingIndicatorOn = 0x24,
        /// <summary>
        ///  Typing Indicator Off; Client-only
        /// </summary>
        TypingIndicatorOff = 0x25,
        /// <summary>
        ///  Message Undeliverable; Server-only
        /// </summary>
        MessageUndeliverable = 0x26,

        // Errors (0x3X); Server only

        /// <summary>
        ///  Unknown Error
        /// </summary>
        UnknownError = 0x30,
        /// <summary>
        ///  Invalid Format
        /// </summary>
        InvalidFormat = 0x31,
        /// <summary>
        ///  Unauthorized
        /// </summary>
        Unauthorized = 0x32,
        /// <summary>
        ///  Message Too Large
        /// </summary>
        MessageTooLarge = 0x33,
        /// <summary>
        ///  Rate Limit Exceeded
        /// </summary>
        RateLimitExceeded = 0x34,
        /// <summary>
        ///  Unknown Status Code
        /// </summary>
        UnknownStatusCode = 0x35,
        /// <summary>
        /// Protocol Version Mismatch
        /// </summary>
        VersionMismatch = 0x36,

        /// <summary>
        /// Invalid status code.
        /// </summary>
        Invalid = 0xFF
    }
}