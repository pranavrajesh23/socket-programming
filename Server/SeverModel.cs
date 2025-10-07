using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ServerModel
{
    private TcpListener server;
    private TcpClient client;
    private NetworkStream stream;

    public event Action<string> MessageReceived;
    public event Action<string> StatusUpdate;

    public void StartServer(int port)
    {
        server = new TcpListener(IPAddress.Any, port);
        server.Start();
        StatusUpdate?.Invoke("Server started. Waiting for client...");
        client = server.AcceptTcpClient();
        StatusUpdate?.Invoke("Client connected!");

        stream = client.GetStream();

        Thread receiveThread = new Thread(ReceiveMessages);
        receiveThread.Start();
    }

    public void SendMessage(string message)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }

    private void ReceiveMessages()
    {
        try
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                int bytes = stream.Read(buffer, 0, buffer.Length);
                string msg = Encoding.ASCII.GetString(buffer, 0, bytes);
                MessageReceived?.Invoke("Client: " + msg);
            }
        }
        catch
        {
            StatusUpdate?.Invoke("Client disconnected.");
        }
    }
}

