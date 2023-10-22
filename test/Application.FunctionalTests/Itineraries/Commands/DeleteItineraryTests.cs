using ASE3040.Application.Features.Itineraries.Commands.Delete;
using ASE3040.Domain.Entities;
using ASE3040.Application.Common.Models;
using ASE3040.Application.Features.Itineraries.Commands.Create;

namespace Application.FunctionalTests.Itineraries.Commands;

using static Testing;

[TestClass]
public class DeleteItineraryTests : BaseTestClass
{
    [TestMethod]
    public async Task ShouldDeleteItinerary()
    {
        RunAsDefaultUser();
        await AddAsync(new Itinerary
        {
            Id = 1,
            Title = "Title",
            Budget = 1000M,
            Description = "Description"
        });
        var command = new DeleteItineraryCommand
        {
            Id = 1
        };
        
        Result result = await SendAsync(command);
        var entity = await FindAsync<Itinerary>(1);
        
        result.Succeeded.Should().BeTrue();
        entity.Should().BeNull();
    }

    [TestMethod]
    public async Task ShouldRequireValidId()
    {
        RunAsDefaultUser();
        
        var command = new DeleteItineraryCommand
        {
            Id = 0 //invalid ID
        };
        
        Result result = await SendAsync(command);
        
        result.Succeeded.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Itinerary not found.");
    }
}