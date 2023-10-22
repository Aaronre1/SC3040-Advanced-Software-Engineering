using ASE3040.Application.Features.Itineraries.Queries.Calendar;
using ASE3040.Domain.Entities;

namespace Application.FunctionalTests.Itineraries.Queries.Calendar;

using static Testing;

[TestClass]
public class GetItinerariesCalendarEventsTests : BaseTestClass
{
    [TestMethod]
    public async Task ShouldGetItinerariesCalendarEvents()
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

        var result = (await SendAsync(new GetItinerariesCalendarEvents())).ToList();
        var itinerary = result.FirstOrDefault(x => x.Title == "Japan Trip");
        var activity1 = result.FirstOrDefault(x => x.Title == "Departure from Oslo");
        var activity2 = result.FirstOrDefault(x => x.Title == "Departure from Tokyo");
        
        result.Should().HaveCount(3);
        
        itinerary.Should().NotBeNull();
        itinerary!.Start.Should().Be(new DateTime(2023, 01, 01));
        itinerary.End.Should().Be(new DateTime(2023, 02, 01));
        itinerary.Url.Should().Be("/Itinerary/Details?id=1");

        activity1.Should().NotBeNull();
        activity1!.Start.Should().Be(new DateTime(2023, 01, 01));
        activity1.End.Should().BeNull();
        activity1.Url.Should().Be("/Itinerary/Details?id=1");
        
        activity2.Should().NotBeNull();
        activity2!.Start.Should().Be(new DateTime(2023, 02, 01));
        activity2.End.Should().BeNull();
        activity2.Url.Should().Be("/Itinerary/Details?id=1");
    }
}