using ASE3040.Application.Features.Itineraries.Commands.Delete;
using ASE3040.Domain.Entities;
using ASE3040.Application.Common.Models;
using ASE3040.Application.Features.Itineraries.Commands.Create;

namespace Application.FunctionalTests.Itineraries.Commands
{
    using static Testing;

    [TestClass]
    public class DeleteItineraryTests
    {
        [TestMethod]
        public async Task ShouldDeleteItineraryWhenOwnedByUser()
        {
            RunAsDefaultUser();
            var createdItinerary = await CreateItineraryForTest("Sample Itinerary");

            var command = new DeleteItineraryCommand
            {
                Id = createdItinerary.Id
            };
            
            Result result = await SendAsync(command);

            var entity = await FindAsync<Itinerary>(x => x.Id == createdItinerary.Id);
            
            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(entity);
        }

        [TestMethod]
        public async Task ShouldReturnErrorWhenItineraryDoesNotExist()
        {
            RunAsDefaultUser();
            var command = new DeleteItineraryCommand
            {
                Id = 9999 //non-existing ID
            };
            
            Result result = await SendAsync(command);

            Assert.IsFalse(result.Succeeded);
            Assert.IsTrue(result.Errors.Any());
            Assert.AreEqual(result.Errors.First(), "Itinerary not found.");
        }

        private async Task<Itinerary> CreateItineraryForTest(string title)
        {
            var command = new CreateItineraryCommand
            {
                Title = title
            };

            await SendAsync(command);
            return await FindAsync<Itinerary>(x => x.Title == title);
        }
    }
}