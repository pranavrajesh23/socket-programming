using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ChatServer
{
    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 5000);
        server.Start();
        Console.WriteLine("Server started. Waiting for client...");

        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Client connected!");

        NetworkStream stream = client.GetStream();

        // Start a thread to receive messages
        Thread receiveThread = new Thread(() =>
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.ASCII.GetString(buffer, 0, bytes);
                    Console.WriteLine("Client: " + message);
                }
                catch
                {
                    Console.WriteLine("Client disconnected.");
                    break;
                }
            }
        });
        receiveThread.Start();

        // Send messages
        while (true)
        {
            string msg = Console.ReadLine();
            byte[] data = Encoding.ASCII.GetBytes(msg);
            stream.Write(data, 0, data.Length);
        }
    }
}
