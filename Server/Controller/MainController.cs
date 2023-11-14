namespace Server.Controller;

public class MainController
{
    private DatabaseController _databaseController;

    public MainController()
    {
        _databaseController = new DatabaseController();
    }
}