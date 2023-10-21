using ASE3040.Application.Common.Models;
using ASE3040.Application.Features.Itineraries.Commands.Create;
using ASE3040.Domain.Entities;

namespace Application.FunctionalTests.Itineraries.Commands;

using static Testing;

[TestClass]
public class CreateItineraryTests : BaseTestClass
{
    [TestMethod]
    public async Task ShouldRequireMinimumFields()
    {
        RunAsDefaultUser();

        var command = new CreateItineraryCommand();

        Result result = await SendAsync(command);

        Assert.IsFalse(result.Succeeded);
        Assert.IsTrue(result.Errors.Any());
    }

    [TestMethod]
    public async Task ShouldRequireUniqueTitle()
    {
        RunAsDefaultUser();

        await SendAsync(new CreateItineraryCommand()
        {
            Title = "Japan Trip"
        });

        var command = new CreateItineraryCommand()
        {
            Title = "Japan Trip"
        };

        Result result = await SendAsync(command);

        Assert.IsFalse(result.Succeeded);
        Assert.IsTrue(result.Errors.Any());
    }

    [TestMethod]
    public async Task ShouldCreateItinerary()
    {
        RunAsDefaultUser();

        var command = new CreateItineraryCommand()
        {
            Title = "Japan Trip",
            Budget = 1000M,
            Description = "A trip to Japan"
        };

        Result result = await SendAsync(command);

        var entity = await FindAsync<Itinerary>(x => x.Title == "Japan Trip");

        Assert.IsTrue(result.Succeeded);
        entity.Should().NotBeNull();
        entity!.CreatedBy.Should().Be(GetUserId());
        entity.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        entity.Budget.Should().Be(1000M);
        entity.Description.Should().Be("A trip to Japan");
    }
}