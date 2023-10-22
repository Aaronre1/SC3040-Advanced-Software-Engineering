using ASE3040.Application.Common.Models;
using ASE3040.Application.Features.Activities.Commands.Create;
using ASE3040.Domain.Entities;

namespace Application.FunctionalTests.Activities.Commands;

using static Testing;

[TestClass]
public class CreateActivityTests : BaseTestClass
{
    [TestMethod]
    public async Task ShouldRequireMinimumFields()
    {
        RunAsDefaultUser();
        var command = new CreateActivityCommand();

        Result result = await SendAsync(command);

        result.Succeeded.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [TestMethod]
    public async Task ShouldRequireValidItineraryId()
    {
        RunAsDefaultUser();
        var command = new CreateActivityCommand
        {
            ItineraryId = 0, // Invalid ItineraryId
            Title = "Activity Title",
            Description = "Activity Description",
            DateTime = DateTime.Now.AddDays(1),
            Cost = 50.0m
        };

        Result result = await SendAsync(command);

        result.Succeeded.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Itinerary is not found.");
    }

    [TestMethod]
    public async Task ShouldCreateActivity()
    {
        RunAsDefaultUser();
        await AddAsync(new Itinerary
        {
            Id = 1,
            Title = "Title",
            Budget = 1000.0m,
            Description = "Description"
        });

        var command = new CreateActivityCommand
        {
            ItineraryId = 1,
            Title = "Activity Title",
            Description = "Activity Description",
            DateTime = DateTime.Now.AddDays(1),
            Cost = 50.0m
        };

        Result result = await SendAsync(command);
        var activity = await FindAsync<Activity>(a => a.Title == "Activity Title");

        result.Succeeded.Should().BeTrue();
        activity!.Description.Should().Be("Activity Description");
        activity.DateTime.Should().Be(command.DateTime);
        activity.Cost.Should().Be(command.Cost);
        activity.ItineraryId.Should().Be(command.ItineraryId);
        activity.CreatedBy.Should().Be(GetUserId());
    }

    [TestMethod]
    public async Task ShouldNotHaveNegativeCost()
    {
        RunAsDefaultUser();
        await AddAsync(new Itinerary
        {
            Id = 1,
            Title = "Title",
            Budget = 1000.0m,
            Description = "Description"
        });

        var command = new CreateActivityCommand
        {
            ItineraryId = 1,
            Title = "Activity Title",
            DateTime = DateTime.Now.AddDays(1),
            Cost = -50.0m // Negative cost
        };

        Result result = await SendAsync(command);

        result.Succeeded.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("'Cost' must be greater than '0'.");
    }
}