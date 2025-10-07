using System;

public class ServerView
{
    public void ShowMessage(string msg) => Console.WriteLine(msg);
    public string GetInput() => Console.ReadLine();
}
