using ASE3040.Application.Common.Models;
using ASE3040.Application.Features.Activities.Commands.Create;
using ASE3040.Application.Features.Activities.Commands.Delete;
using ASE3040.Domain.Entities;
using ASE3040.Application.Features.Itineraries.Commands.Create;

namespace Application.FunctionalTests.Activities.Commands
{
    using static Testing;

    [TestClass]
    public class DeleteActivityTests
    {
        [TestMethod]
        public async Task ShouldDeleteActivity()
        {
            RunAsDefaultUser();
            var createdItinerary = await CreateItineraryForTest("Original Title");
            var createdActivity = await CreateActivityForTest(createdItinerary.Id, "Activity Title");

            var command = new DeleteActivityCommand
            {
                Id = createdActivity.Id,
            };
            
            Result result = await SendAsync(command);
            
            Assert.IsTrue(result.Succeeded);
            var activity = await FindAsync<Activity>(a => a.Id == createdActivity.Id);
            Assert.IsNull(activity);
        }

        [TestMethod]
        public async Task ShouldRequireValidId()
        {
            RunAsDefaultUser();

            var command = new DeleteActivityCommand
            {
                Id = 9999, // Invalid Id
            };
            
            Result result = await SendAsync(command);
            
            Assert.IsFalse(result.Succeeded);
            Assert.IsTrue(result.Errors.Any());
            Assert.IsTrue(result.Errors.Contains("Itinerary not found."));
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
