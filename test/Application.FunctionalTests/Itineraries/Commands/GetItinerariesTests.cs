using ASE3040.Application.Features.Itineraries.Queries;
using ASE3040.Domain.Entities;
using ASE3040.Application.Features.Itineraries.Commands.Create;

namespace Application.FunctionalTests.Itineraries.Queries
{
    using static Testing;

    [TestClass]
    public class GetItinerariesTests
    {
        [TestMethod]
        public async Task ShouldRetrieveAllItinerariesForUser()
        {
            RunAsDefaultUser();
            await CreateItineraryForTest("Itinerary 1");
            await CreateItineraryForTest("Itinerary 2");
            
            var query = new GetItineraries();
            
            var result = await SendAsync(query);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(i => i.Title == "Itinerary 1"));
            Assert.IsTrue(result.Any(i => i.Title == "Itinerary 2"));
        }

        [TestMethod]
        public async Task ShouldNotRetrieveItinerariesFromOtherUsers()
        {

            RunAsDefaultUser();
            await CreateItineraryForTest("Itinerary 1");
            
            RunAsAnotherUser();
            await CreateItineraryForTest("Itinerary 2");
            
            var query = new GetItineraries();
            
            var result = await SendAsync(query);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsFalse(result.Any(i => i.Title == "Itinerary 1"));
            Assert.IsTrue(result.Any(i => i.Title == "Itinerary 2"));
        }

        private async Task<Itinerary> CreateItineraryForTest(string title)
        {
            var command = new CreateItineraryCommand { Title = title };
            await SendAsync(command);
            
            return await FindAsync<Itinerary>(i => i.Title == title);
        }
    }
}
