namespace Application.FunctionalTests;

public class Testing
{
    private static string? _userId;
    
    public static string? GetUserId()
    {
        return _userId;
    }

    public static void RunAsDefaultUser()
    {
        _userId = "test-user-id";
        
    }
}