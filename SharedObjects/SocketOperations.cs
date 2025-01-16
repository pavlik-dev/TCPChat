using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SharedObjects
{
    public class SocketOperations
    {
        public static void Send(Socket client, StatusCode code, string message, ushort replyingTo = 0)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] sizeBytes = BigEndianBitConverter.GetBytes((ushort)messageBytes.Length);
            byte[] replyingToBytes = BigEndianBitConverter.GetBytes(replyingTo);
            byte[] errorByte = { (byte)code };
            client.Send(errorByte);
            client.Send(replyingToBytes);
            client.Send(sizeBytes);
            client.Send(messageBytes);
        }

        public static Message GetMessage(Socket ClientSocket)
        {
            Message message1 = new Message();
            byte[] errorByte = new byte[1];
            ClientSocket.Receive(errorByte, 1, SocketFlags.None);
            StatusCode errorCode = Enum.IsDefined(typeof(StatusCode), errorByte[0])
                ? (StatusCode)errorByte[0] : StatusCode.Invalid;
            message1.Code = errorCode;

            byte[] replyingToBytes = new byte[2];
            ClientSocket.Receive(replyingToBytes, 2, SocketFlags.None);
            ushort replyingTo = BigEndianBitConverter.ToUInt16(replyingToBytes, 0);
            replyingToBytes = null;
            message1.ReplyingTo = replyingTo;

            byte[] sizeBytes = new byte[2];
            ClientSocket.Receive(sizeBytes, 2, SocketFlags.None);
            ushort size = BigEndianBitConverter.ToUInt16(sizeBytes, 0);
            sizeBytes = null;

            if (size == 0)
            {
                message1.Data = new byte[0];
                return message1;
            }

            byte[] message;
            message = new byte[size];
            ClientSocket.Receive(message, size, SocketFlags.None);

            message1.Data = message;

            return message1;
        }
    }
}
