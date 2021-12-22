using System.IO;

namespace Vantage.Infra.Crosscutting.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            byte[] buffer = new byte[(int)stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
