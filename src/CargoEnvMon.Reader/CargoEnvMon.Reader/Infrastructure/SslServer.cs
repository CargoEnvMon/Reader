using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using CargoEnvMon.Reader.Infrastructure.Abstractions;

namespace CargoEnvMon.Reader.Infrastructure
{
    public abstract class SslServer : IDisposable
    {
        private const int PORT = 9021;
        private readonly TcpListener listener;
        private readonly X509Certificate certificate;
        private Thread thread;


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
            thread = new Thread(() =>
            {
                listener.Start();
                while (true)
                {
                    var client = listener.AcceptTcpClient();
                    ProcessClient(client);
                }
                // ReSharper disable once FunctionNeverReturns
            })
            {
                IsBackground = true
            };
            thread.Start();
        }

        public void Stop()
        {
            thread.Abort();
            listener.Stop();
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
                thread.Abort();
                listener.Stop();
            }
            catch
            {
                // ignored
            }
        }
    }
}