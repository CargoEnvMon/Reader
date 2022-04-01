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
        public const int PORT = 9021;
        private readonly TcpListener listener;
        //private readonly X509Certificate certificate;
        private Thread thread;


        //public SslServer(IIpAddressProvider ipAddressProvider, ISslCertificateProvider certificateProvider)
        public SslServer(IIpAddressProvider ipAddressProvider)
        {
            listener = new TcpListener(
                new IPAddress(ipAddressProvider.GetIpAddressBytes()),
                PORT);
            //certificate = certificateProvider.GetCertificate();
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
            //using var networkStream = new SslStream(client.GetStream(), false);
            using var networkStream = client.GetStream();
            try
            {
                //networkStream.AuthenticateAsServer(certificate);
                var message = StreamHelper.ReadUntilEof(networkStream);
                var responseMessage = GetResponse(message);
                var responseData = Encoding.Default.GetBytes(responseMessage);
                networkStream.Write(responseData, 0, responseData.Length);
            }
            catch (Exception e)
            {
                ExceptionsHandler.Handle(e);
            }
            finally
            {
                networkStream.Close();
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