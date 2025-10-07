public class ServerController
{
    private readonly ServerModel model;
    private readonly ServerView view;

    public ServerController(ServerModel model, ServerView view)
    {
        this.model = model;
        this.view = view;
        this.model.MessageReceived += view.ShowMessage;
        this.model.StatusUpdate += view.ShowMessage;
    }

    public void Run()
    {
        model.StartServer(5000);
        while (true)
        {
            string message = view.GetInput();
            model.SendMessage(message);
        }
    }
}
