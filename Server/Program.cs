using System.Text;
using Server;

var server = new UdpListener();

// thread for listening incoming messages
var serverThread = new Thread(() => server.Listen());
serverThread.Start();

// thread for handling received messages
var dataHandlerThread = new Thread(() => server.OnDataReceived += OnDataReceived);
dataHandlerThread.Start();

static void OnDataReceived(object sender, UdpListener.ReceivedDataArgs args)
{
    Console.WriteLine($"Received message from [{args.IpAddress}:{args.Port}] - {Encoding.ASCII.GetString(args.ReceivedBytes)}");
}

Console.ReadKey();