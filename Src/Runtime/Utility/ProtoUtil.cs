

using System;
using System.IO;
using Google.Protobuf;

namespace UnityGameFramework.Runtime
{
    public static class ProtoUtil
    {

        /// <summary>
        /// 把envelope 编码为（包长+包内容）字节数组
        /// </summary>
        /// <param name="envelope">协议对象</param>
        /// <param name="packetHeaderLength">包头长度</param>
        public static byte[] EncodeToBytes(MelandGame3.Envelope envelope, int packetHeaderLength)
        {
            // 获取消息包envelope的长度
            int envelopLength = envelope.CalculateSize();
            // 根据长度转头字节数组
            byte[] headerBytes = BitConverter.GetBytes(envelopLength);
            if (packetHeaderLength < 0 || (packetHeaderLength > 0 && headerBytes.Length != packetHeaderLength))
            {
                Log.Fatal($"protobuf Serialize head size error,cur={headerBytes.Length} expected={packetHeaderLength}");
                return null;
            }

            // 创建（头长度+内容长度）的字节数组
            byte[] destination = new byte[packetHeaderLength + envelopLength];
            // 选取内容区域，写入数据
            Span<byte> envelopBytes = new(destination, packetHeaderLength, destination.Length - packetHeaderLength);
            envelope.WriteTo(envelopBytes);

            // 头部区域写入数据
            if (packetHeaderLength > 0)
            {
                for (int i = 0; i < packetHeaderLength; i++)
                {
                    destination[i] = headerBytes[i];
                }
            }
            return destination;
        }

        /// <summary>
        /// 解码协议数据 内容部分（不包括头长） 
        /// </summary>
        /// <param name="byteSpan"></param>
        /// <returns></returns>
        public static MelandGame3.Envelope Decode(Span<byte> byteSpan)
        {
            MelandGame3.Envelope envelope = MelandGame3.Envelope.Parser.ParseFrom(byteSpan);
            return envelope;
        }

        /// <summary>
        /// 解码协议数据 内容部分（不包括头长） 
        /// </summary>
        /// <param name="envelopBytes"></param>
        /// <returns></returns>
        public static MelandGame3.Envelope Decode(byte[] envelopBytes)
        {
            MelandGame3.Envelope envelope = MelandGame3.Envelope.Parser.ParseFrom(envelopBytes);
            return envelope;
        }

        /// <summary>
        /// 头长字节数组 转 内容长度
        /// </summary>
        /// <param name="headerBytes"></param>
        /// <returns></returns>
        public static int DecodeHeader(byte[] headerBytes)
        {
            return BitConverter.ToInt32(headerBytes, 0);
        }
    }
}