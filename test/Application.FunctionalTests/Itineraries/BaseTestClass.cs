namespace Application.FunctionalTests.Itineraries;

using static Testing;

[TestClass]
public class BaseTestClass
{
    [TestCleanup]
    public async Task CleanUp()
    {
        await ResetAsync();
    }
}