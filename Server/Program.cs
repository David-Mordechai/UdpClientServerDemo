﻿using System.Text;
using Server;

var server = new UdpListener();

// task for listening incoming messages
var serverTask = new Task(() => server.Listen());
serverTask.Start();

// task for handling received messages
var dataHandlerTask = new Task(() => server.OnDataReceived += (_, args) =>
{
    Console.WriteLine($"Received message from [{args.IpAddress}:{args.Port}] - {Encoding.ASCII.GetString(args.ReceivedBytes)}");
});
dataHandlerTask.Start();

Console.ReadKey();