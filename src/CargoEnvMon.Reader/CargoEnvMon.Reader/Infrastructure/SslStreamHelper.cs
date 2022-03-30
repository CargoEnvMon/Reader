using System.Net.Security;
using System.Text;

namespace CargoEnvMon.Reader.Infrastructure
{
    public static class SslStreamHelper
    {
        public static string ReadUntilEof(SslStream stream)
        {
            var buffer = new byte[2048];
            var messageData = new StringBuilder();
            int bytes;

            do
            {
                bytes = stream.Read(buffer, 0, buffer.Length);

                var decoder = Encoding.UTF8.GetDecoder();
                var chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);

                if (messageData.ToString().Contains("<EOF>"))
                {
                    break;
                }
            } while (bytes != 0);

            return messageData
                .Replace("<EOF>", "")
                .ToString();
        }
    }
}