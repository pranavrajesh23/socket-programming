class Program
{
    static void Main()
    {
        var model = new ServerModel();
        var view = new ServerView();
        var controller = new ServerController(model, view);
        controller.Run();
    }
}

