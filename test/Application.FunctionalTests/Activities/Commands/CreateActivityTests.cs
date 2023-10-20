using ASE3040.Application.Features.Activities.Commands.Create;
using ASE3040.Application.Common.Models;
using ASE3040.Application.Features.Itineraries.Commands.Create;
using ASE3040.Domain.Entities;

namespace Application.FunctionalTests.Activities.Commands
{
    using static Testing;

    [TestClass]
    public class CreateActivityTests
    {
        [TestMethod]
        public async Task ShouldCreateActivity()
        {
            RunAsDefaultUser();
            var createdItinerary = await CreateItineraryForTest("Original Title");

            var command = new CreateActivityCommand
            {
                ItineraryId = createdItinerary.Id,
                Title = "Activity Title",
                Description = "Activity Description",
                DateTime = DateTime.Now.AddDays(1),
                Cost = 50.0m
            };
            
            Result result = await SendAsync(command);
            
            Assert.IsTrue(result.Succeeded);
            var activity = await FindAsync<Activity>(a => a.Title == "Activity Title");
            Assert.IsNotNull(activity);
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
            
            Assert.IsFalse(result.Succeeded);
            Assert.IsTrue(result.Errors.Any());
            Assert.IsTrue(result.Errors.Contains("Itinerary is not found."));
        }

        [TestMethod]
        public async Task ShouldValidateRequiredFields()
        {
            RunAsDefaultUser();
            var createdItinerary = await CreateItineraryForTest("Original Title");

            var command = new CreateActivityCommand
            {
                ItineraryId = createdItinerary.Id
                // Missing required fields: Title and DateTime
            };
            
            Result result = await SendAsync(command);
            
            Assert.IsFalse(result.Succeeded);
            Assert.IsTrue(result.Errors.Any());
            //Assert.IsTrue(result.Errors.Contains("The Title field is required."));
            //Assert.IsTrue(result.Errors.Contains("The DateTime field is required."));
        }

        [TestMethod]
        public async Task ShouldValidateCost()
        {
            RunAsDefaultUser();
            var createdItinerary = await CreateItineraryForTest("Original Title");

            var command = new CreateActivityCommand
            {
                ItineraryId = createdItinerary.Id,
                Title = "Activity Title",
                DateTime = DateTime.Now.AddDays(1),
                Cost = -50.0m // Negative cost
            };
            
            Result result = await SendAsync(command);
            
            Assert.IsFalse(result.Succeeded);
            Assert.IsTrue(result.Errors.Any());
            Assert.IsTrue(result.Errors.Contains("'Cost' must be greater than '0'."));
        }

        private async Task<Itinerary> CreateItineraryForTest(string title)
        {
            var command = new CreateItineraryCommand { Title = title };
            await SendAsync(command);
            return await FindAsync<Itinerary>(i => i.Title == title);
        }
    }
}
