using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ChatClient
{
    static void Main()
    {
        Console.Write("Enter server IP: ");
        string serverIp = Console.ReadLine();

        TcpClient client = new TcpClient(serverIp, 5000);
        Console.WriteLine("Connected to server!");

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
                    Console.WriteLine("Server: " + message);
                }
                catch
                {
                    Console.WriteLine("Server disconnected.");
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
