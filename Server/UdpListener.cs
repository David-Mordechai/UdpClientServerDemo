using System.Net;
using System.Net.Sockets;

namespace Server;

public class UdpListener
{
    public Action<object, ReceivedDataArgs>? OnDataReceived;
 
    public void Listen()
    {
        var listener = new UdpClient(5555);
        var serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
        
        for (var i = 0; i < 100; i++)
        {
            var data = listener.Receive(ref serverEndPoint);
            OnDataReceived?.Invoke(this, new ReceivedDataArgs(serverEndPoint.Address, serverEndPoint.Port, data));
        }
    }

    public class ReceivedDataArgs
    {
        public IPAddress? IpAddress { get; }
        public int Port { get; }
        public byte[] ReceivedBytes { get; }

        public ReceivedDataArgs(IPAddress? ipAddress, int port, byte[] data)
        {
            ReceivedBytes = data;
            IpAddress = ipAddress;
            Port = port;
        }
    }
}