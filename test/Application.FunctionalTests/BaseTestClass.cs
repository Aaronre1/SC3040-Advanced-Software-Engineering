namespace Application.FunctionalTests;

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