using System.Net;
using System.Net.Sockets;
using System.Text;
using CargoEnvMon.Reader.Infrastructure;

using var tcpClient = new TcpClient();

var input = "192.168.137.35";//Console.ReadLine();
IPAddress.TryParse(input, out var ip);
tcpClient.Connect(ip, SslServer.PORT);

var stream = tcpClient.GetStream();

var message = @"cargo-1|1648410051380|3
30|20.3|2.3
29|20.2|3.1
<EOF>";

var bytes = Encoding.Default.GetBytes(message);
stream.Write(bytes, 0, bytes.Length);