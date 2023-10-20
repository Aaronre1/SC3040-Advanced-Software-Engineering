using ASE3040.Application.Common.Models;
using ASE3040.Application.Features.Activities.Commands.Create;
using ASE3040.Application.Features.Activities.Commands.Edit;
using ASE3040.Domain.Entities;
using ASE3040.Application.Features.Itineraries.Commands.Create;

namespace Application.FunctionalTests.Activities.Commands
{
    using static Testing;

    [TestClass]
    public class EditActivityTests
    {
        [TestMethod]
        public async Task ShouldEditActivity()
        {
            RunAsDefaultUser();
            var createdItinerary = await CreateItineraryForTest("Original Title");
            var createdActivity = await CreateActivityForTest(createdItinerary.Id, "Activity Title");

            var command = new EditActivityCommand
            {
                Id = createdActivity.Id,
                Title = "New Title",
                Description = "New Description",
                DateTime = DateTime.Now.AddDays(2),
                Cost = 100.50m,
                Done = true
            };

            Result result = await SendAsync(command);
            
            Assert.IsTrue(result.Succeeded);
            var activity = await FindAsync<Activity>(a => a.Id == createdActivity.Id);
            Assert.IsNotNull(activity);
            Assert.AreEqual("New Title", activity.Title);
            Assert.AreEqual("New Description", activity.Description);
            Assert.AreEqual(100.50m, activity.Cost);
            Assert.IsTrue(activity.Done);
        }

        [TestMethod]
        public async Task ShouldRequireValidId()
        {
            RunAsDefaultUser();

            var command = new EditActivityCommand
            {
                Id = 9999, // Invalid Id
                Title = "New Title",
                Description = "New Description",
                DateTime = DateTime.Now.AddDays(2),
                Cost = 100.50m,
                Done = true
            };
            
            Result result = await SendAsync(command);
            
            Assert.IsFalse(result.Succeeded);
            Assert.IsTrue(result.Errors.Any());
            Assert.IsTrue(result.Errors.Contains("Activity not found."));
        }

        private async Task<Itinerary> CreateItineraryForTest(string title)
        {
            var command = new CreateItineraryCommand { Title = title };
            await SendAsync(command);
            return await FindAsync<Itinerary>(i => i.Title == title);
        }

        private async Task<Activity> CreateActivityForTest(int itineraryId, string title)
        {
           var command = new CreateActivityCommand
            {
                ItineraryId = itineraryId,
                Title = title,
                DateTime = DateTime.Now.AddDays(1)
            };
            await SendAsync(command);
            return await FindAsync<Activity>(a => a.Title == title);
        }
    }
}
