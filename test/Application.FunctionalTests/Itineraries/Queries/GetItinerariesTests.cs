using ASE3040.Application.Features.Itineraries.Queries;
using ASE3040.Domain.Entities;
using ASE3040.Application.Features.Itineraries.Commands.Create;

namespace Application.FunctionalTests.Itineraries.Queries
{
    using static Testing;

    [TestClass]
    public class GetItinerariesTests : BaseTestClass
    {
        [TestMethod]
        public async Task ShouldRetrieveItinerary()
        {
            RunAsDefaultUser();

            await AddAsync(new Itinerary
            {
                Title = "Japan Trip",
                Budget = 1000M,
                Description = "A trip to Japan",
                Activities =
                {
                    new Activity
                    {
                        Title = "Departure from Oslo",
                        Description = "Gate 1",
                        DateTime = new DateTime(2023, 01, 01),
                        Cost = 1000M
                    },
                    new Activity
                    {
                        Title = "Departure from Tokyo",
                        Description = "Gate 2",
                        DateTime = new DateTime(2023, 02, 01),
                        Cost = 1500M
                    }
                }
            });

            var result = await SendAsync(new GetItineraries());
            var itinerary = result.FirstOrDefault();

            result.Should().HaveCount(1);
            itinerary.Should().NotBeNull();
            itinerary!.Title.Should().Be("Japan Trip");
            itinerary.Budget.Should().Be(1000M);
            itinerary.Expenses.Should().Be(2500M);
            itinerary.Description.Should().Be("A trip to Japan");
            itinerary.From.Should().Be(new DateTime(2023, 01, 01));
            itinerary.To.Should().Be(new DateTime(2023, 02, 01));
            itinerary.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));

            itinerary.Activities.Should().HaveCount(2);
            var activity1 = itinerary.Activities.FirstOrDefault(x => x.Title == "Departure from Oslo");
            activity1!.Cost.Should().Be(1000M);
            activity1.Description.Should().Be("Gate 1");
            activity1.DateTime.Should().Be(new DateTime(2023, 01, 01));
            activity1.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
            activity1.ItineraryId.Should().Be(itinerary.Id);
            activity1.Done.Should().BeFalse();
            activity1.Id.Should().Be(1);
            
        }

        [TestMethod]
        public async Task ShouldRetrieveAllItinerariesForUser()
        {
            RunAsDefaultUser();
            await CreateItineraryForTest("Itinerary 1");
            await CreateItineraryForTest("Itinerary 2");

            var query = new GetItineraries();

            IEnumerable<ItineraryDto> result = await SendAsync(query);

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