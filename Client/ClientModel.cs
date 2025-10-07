using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ClientModel
{
    private TcpClient client;
    private NetworkStream stream;

    public event Action<string> MessageReceived;
    public event Action<string> StatusUpdate;

    public void ConnectToServer(string serverName, int port)
    {
        client = new TcpClient(serverName, port);
        StatusUpdate?.Invoke("Connected to server!");
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
                MessageReceived?.Invoke("Server: " + msg);
            }
        }
        catch
        {
            StatusUpdate?.Invoke("Server disconnected.");
        }
    }
}
