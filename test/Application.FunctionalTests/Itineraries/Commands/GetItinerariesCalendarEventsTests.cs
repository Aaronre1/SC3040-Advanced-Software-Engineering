using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Features.Itineraries.Queries.Calendar;
using ASE3040.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ASE3040.Infrastructure.Data;
using Moq;

namespace Application.FunctionalTests.Itineraries.Queries
{
    [TestClass]
    public class GetItinerariesCalendarEventsTests
    {
        private DbContextOptions<ApplicationDbContext> _options;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _userId = "user-123";
        }

        [TestMethod]
        public async Task ShouldRetrieveCalendarEventsOfUser()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var userMock = new Mock<IUser>();
                userMock.Setup(u => u.UserId).Returns(_userId);

                var createdItinerary1 = await CreateItineraryForTest(context, "Itinerary1");
                var createdItinerary2 = await CreateItineraryForTest(context, "Itinerary2");

                var createdActivity1 = await CreateActivityForTest(context, createdItinerary1.Id, "Activity1", DateTime.Now.AddDays(1).AddHours(3));
                var createdActivity2 = await CreateActivityForTest(context, createdItinerary2.Id, "Activity2", DateTime.Now.AddDays(3).AddHours(3));

                var query = new GetItinerariesCalendarEvents();
                var handler = new GetItinerariesCalendarEventsHandler(context, userMock.Object);
                
                var result = await handler.Handle(query, CancellationToken.None);
                
                Assert.AreEqual(4, result.Count());
                Assert.IsTrue(result.Any(e => e.Title == "Itinerary1" && e.Start == createdItinerary1.From.Value && e.End == createdItinerary1.To.Value));
                Assert.IsTrue(result.Any(e => e.Title == "Itinerary2" && e.Start == createdItinerary2.From.Value && e.End == createdItinerary2.To.Value));
                Assert.IsTrue(result.Any(e => e.Title == "Activity1" && e.Start == createdActivity1.DateTime));
                Assert.IsTrue(result.Any(e => e.Title == "Activity2" && e.Start == createdActivity2.DateTime));
            }
        }

        private async Task<Itinerary> CreateItineraryForTest(ApplicationDbContext context, string title)
        {
            var itinerary = new Itinerary
            {
                Title = title,
                CreatedBy = _userId,
            };

            context.Itineraries.Add(itinerary);
            await context.SaveChangesAsync(CancellationToken.None);

            return itinerary;
        }

        private async Task<Activity> CreateActivityForTest(ApplicationDbContext context, int itineraryId, string title, DateTime dateTime)
        {
            var activity = new Activity
            {
                ItineraryId = itineraryId,
                Title = title,
                DateTime = dateTime,
                CreatedBy = _userId,
            };

            context.Activities.Add(activity);
            await context.SaveChangesAsync(CancellationToken.None);

            return activity;
        }
    }
}
