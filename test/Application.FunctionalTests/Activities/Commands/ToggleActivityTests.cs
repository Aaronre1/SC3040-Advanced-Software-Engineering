using ASE3040.Application.Common.Models;
using ASE3040.Application.Features.Activities.Commands.Edit;
using ASE3040.Domain.Entities;

namespace Application.FunctionalTests.Activities.Commands;

using static Testing;

[TestClass]
public class ToggleActivityTests : BaseTestClass
{
    [TestMethod]
    public async Task ShouldRequireValidId()
    {
        RunAsDefaultUser();

        var command = new ToggleActivityCommand()
        {
            Id = 1,
        };

        Result result = await SendAsync(command);

        result.Succeeded.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Activity not found.");
    }

    [TestMethod]
    public async Task ShouldToggleActivityDone()
    {
        RunAsDefaultUser();
        await AddAsync(new Itinerary()
        {
            Title = "Japan Trip",
            Budget = 1000M,
            Description = "A trip to Japan",
            Activities = new List<Activity>
            {
                new()
                {
                    Id = 1,
                    Title = "Activity Title",
                    Description = "Activity Description",
                    DateTime = new DateTime(2023, 01, 01),
                    Cost = 50.0m,
                    Done = false
                }
            }
        });

        var command = new ToggleActivityCommand()
        {
            Id = 1,
        };

        Result result = await SendAsync(command);
        var activity = await FindAsync<Activity>(x => x.Id == 1);

        result.Succeeded.Should().BeTrue();
        activity!.Done.Should().BeTrue();
    }

    [TestMethod]
    public async Task ShouldToggleActivityNotDone()
    {
        RunAsDefaultUser();
        await AddAsync(new Itinerary()
        {
            Title = "Japan Trip",
            Budget = 1000M,
            Description = "A trip to Japan",
            Activities = new List<Activity>
            {
                new()
                {
                    Id = 1,
                    Title = "Activity Title",
                    Description = "Activity Description",
                    DateTime = new DateTime(2023, 01, 01),
                    Cost = 50.0m,
                    Done = true
                }
            }
        });

        var command = new ToggleActivityCommand()
        {
            Id = 1,
        };

        Result result = await SendAsync(command);
        var activity = await FindAsync<Activity>(x => x.Id == 1);

        result.Succeeded.Should().BeTrue();
        activity!.Done.Should().BeFalse();
    }
}