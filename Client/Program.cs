using System.Net;
using System.Net.Sockets;
using System.Text;

var cts = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) => {
    e.Cancel = true; // prevent the process from terminating.
    cts.Cancel();
};

using var client = new UdpClient(4444);
var endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
client.Connect(endPoint);

while (cts.Token.IsCancellationRequested is false)
{
    const string message = "This is message from client";
    var data = Encoding.ASCII.GetBytes(message);
    client.Send(data, data.Length);
    Console.WriteLine($"message was sent to [{endPoint.Address}:{endPoint.Port}] - {message}");
    await Task.Delay(2000);
}