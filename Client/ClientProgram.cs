class Program
{
    static void Main()
    {
        var model = new ClientModel();
        var view = new ClientView();
        var controller = new ClientController(model, view);
        controller.Run();
    }
}
