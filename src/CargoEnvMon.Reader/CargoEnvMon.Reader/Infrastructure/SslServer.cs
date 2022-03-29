using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace CargoEnvMon.Reader.Infrastructure
{
    public abstract class SslServer : IDisposable
    {
        private const int PORT = 9021;
        private readonly TcpListener listener;
        private readonly X509Certificate certificate;


        public SslServer(IIpAddressProvider ipAddressProvider, ISslCertificateProvider certificateProvider)
        {
            listener = new TcpListener(
                new IPAddress(ipAddressProvider.GetIpAddressBytes()),
                PORT);
            certificate = certificateProvider.GetCertificate();
        }

        protected abstract string GetResponse(string request);

        public void Start()
        {
            new Thread(() =>
            {
                listener.Start();
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    var client = listener.AcceptTcpClient();
                    ProcessClient(client);
                }
                // ReSharper disable once FunctionNeverReturns
            }).Start();
        }

        private void ProcessClient(TcpClient client)
        {
            using var sslStream = new SslStream(client.GetStream(), false);
            try
            {
                sslStream.AuthenticateAsServer(certificate);
                var message = SslStreamHelper.ReadUntilEof(sslStream);
                var responseMessage = GetResponse(message);
                sslStream.Write(Encoding.Default.GetBytes(responseMessage));
            }
            catch (Exception e)
            {

            }
            finally
            {
                sslStream.Close();
                client.Close();
            }
        }

        public void Dispose()
        {
            try
            {
                listener.Stop();
            }
            catch
            {
                // ignored
            }
        }
    }
}