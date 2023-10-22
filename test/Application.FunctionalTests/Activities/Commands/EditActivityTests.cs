using ASE3040.Application.Common.Models;
using ASE3040.Application.Features.Activities.Commands.Edit;
using ASE3040.Domain.Entities;

namespace Application.FunctionalTests.Activities.Commands
{
    using static Testing;

    [TestClass]
    public class EditActivityTests : BaseTestClass
    {
        [TestMethod]
        public async Task ShouldRequireMinimumFields()
        {
            RunAsDefaultUser();
            var command = new EditActivityCommand();

            Result result = await SendAsync(command);

            result.Succeeded.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task ShouldHavePositiveCost()
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
                        Cost = 50.0m
                    }
                }
            });

            var command = new EditActivityCommand()
            {
                Id = 1,
                Title = "Activity Title",
                Description = "Activity Description",
                DateTime = new DateTime(2023, 01, 01),
                Cost = -50.0m
            };

            Result result = await SendAsync(command);

            result.Succeeded.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should().Contain("'Cost' must be greater than '0'.");
        }

        [TestMethod]
        public async Task ShouldRequireValidId()
        {
            RunAsDefaultUser();

            var command = new EditActivityCommand
            {
                Id = 0, // Invalid Id
                Title = "New Title",
                Description = "New Description",
                DateTime = DateTime.Now.AddDays(2),
                Cost = 100.50m,
                Done = true
            };

            Result result = await SendAsync(command);

            result.Succeeded.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should().Contain("Activity not found.");
        }

        [TestMethod]
        public async Task ShouldEditActivity()
        {
            RunAsDefaultUser();

            await AddAsync(new Itinerary
            {
                Title = "Title",
                Budget = 1000.0m,
                Description = "Description",
                Activities = new List<Activity>()
                {
                    new()
                    {
                        Id = 1,
                        Title = "Activity Title",
                        Description = "Activity Description",
                        DateTime = new DateTime(2023, 01, 01),
                        Cost = 50.0m
                    }
                }
            });

            var command = new EditActivityCommand
            {
                Id = 1,
                Title = "New Title",
                Description = "New Description",
                DateTime = new DateTime(2023, 01, 02),
                Cost = 100m,
                Done = true
            };

            Result result = await SendAsync(command);
            var activity = await FindAsync<Activity>(a => a.Id == 1);

            result.Succeeded.Should().BeTrue();
            activity!.Title.Should().Be("New Title");
            activity.Description.Should().Be("New Description");
            activity.DateTime.Should().Be(new DateTime(2023, 01, 02));
            activity.Cost.Should().Be(100m);
            activity.Done.Should().BeTrue();
            activity.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        }
    }
}