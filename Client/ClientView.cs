using System;

public class ClientView
{
    public void ShowMessage(string message) => Console.WriteLine(message);
    public string GetInput() => Console.ReadLine();
}
