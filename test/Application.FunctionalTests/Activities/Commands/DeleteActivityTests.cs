using ASE3040.Application.Common.Models;
using ASE3040.Application.Features.Activities.Commands.Delete;
using ASE3040.Domain.Entities;

namespace Application.FunctionalTests.Activities.Commands;

using static Testing;

[TestClass]
public class DeleteActivityTests : BaseTestClass
{
    [TestMethod]
    public async Task ShouldDeleteActivity()
    {
        RunAsDefaultUser();

        await AddAsync(new Itinerary
        {
            Id = 1,
            Title = "Title",
            Budget = 1000.0m,
            Description = "Description",
            Activities = new List<Activity>
            {
                new()
                {
                    Id = 1,
                    Title = "Activity Title",
                    Description = "Activity Description",
                    DateTime = new DateTime(2023, 01, 01),
                    Cost = 50.0m,
                }
            }
        });

        var command = new DeleteActivityCommand
        {
            Id = 1,
        };

        Result result = await SendAsync(command);
        var activity = await FindAsync<Activity>(a => a.Id == 1);

        result.Succeeded.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        activity.Should().BeNull();
    }

    [TestMethod]
    public async Task ShouldRequireValidId()
    {
        RunAsDefaultUser();

        var command = new DeleteActivityCommand
        {
            Id = 0, // Invalid Id
        };

        Result result = await SendAsync(command);

        result.Succeeded.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Activity not found.");
    }
}