using System.IO;

namespace Goal.Seedwork.Infra.Crosscutting.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            byte[] buffer = new byte[(int)stream.Length];
            _ = stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
