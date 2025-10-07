public class ClientController
{
    private readonly ClientModel model;
    private readonly ClientView view;

    public ClientController(ClientModel model, ClientView view)
    {
        this.model = model;
        this.view = view;
        this.model.MessageReceived += view.ShowMessage;
        this.model.StatusUpdate += view.ShowMessage;
    }

    public void Run()
    {
        view.ShowMessage("Enter server IP or name:");
        string serverName = view.GetInput();
        model.ConnectToServer(serverName, 5000);

        while (true)
        {
            string msg = view.GetInput();
            model.SendMessage(msg);
        }
    }
}
