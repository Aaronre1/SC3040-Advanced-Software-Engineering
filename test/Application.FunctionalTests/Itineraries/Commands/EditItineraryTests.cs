using ASE3040.Application.Features.Itineraries.Commands.Edit;
using ASE3040.Domain.Entities;
using ASE3040.Application.Common.Models;
using ASE3040.Application.Features.Itineraries.Commands.Create;

namespace Application.FunctionalTests.Itineraries.Commands
{
    using static Testing;

    [TestClass]
    public class EditItineraryTests : BaseTestClass
    {
        [TestMethod]
        public async Task ShouldRequireUniqueTitle()
        {
            RunAsDefaultUser();
            var createdItinerary1 = await CreateItineraryForTest("Original Title 1");
            var createdItinerary2 = await CreateItineraryForTest("Original Title 2");

            var command = new EditItineraryCommand
            {
                Id = createdItinerary2.Id,
                Title = "Original Title 1" // Trying to set the title to the title of another itinerary
            };
            
            Result result = await SendAsync(command);
            
            Assert.IsFalse(result.Succeeded);
            Assert.IsTrue(result.Errors.Any());
            // due to validator method need add single quote in order to pass
            Assert.IsTrue(result.Errors.Contains("'Title' must be unique.")); 
        }

        [TestMethod]
        public async Task ShouldRequireTitle()
        {
            RunAsDefaultUser();
            var createdItinerary = await CreateItineraryForTest("Original Title");

            var command = new EditItineraryCommand
            {
                Id = createdItinerary.Id,
                Title = ""
            };
            
            Result result = await SendAsync(command);
            
            Assert.IsFalse(result.Succeeded);
            Assert.IsTrue(result.Errors.Any());
            Assert.IsTrue(result.Errors.Contains("'Title' must not be empty."));
        }

        [TestMethod]
        public async Task ShouldNotExceedMaximumTitleLength()
        {
            RunAsDefaultUser();
            var createdItinerary = await CreateItineraryForTest("Original Title");

            var command = new EditItineraryCommand
            {
                Id = createdItinerary.Id,
                Title = new string('A', 201) // 201 characters long, exceeding the limit
            };

            Result result = await SendAsync(command);
            
            Assert.IsFalse(result.Succeeded);
            Assert.IsTrue(result.Errors.Any());
            Assert.IsTrue(result.Errors.Contains("The length of 'Title' must be 200 characters or fewer. You entered 201 characters."));
        }

        [TestMethod]
        public async Task ShouldValidateBudget()
        {
            RunAsDefaultUser();
            var createdItinerary = await CreateItineraryForTest("Original Title");

            var command = new EditItineraryCommand
            {
                Id = createdItinerary.Id,
                Title = "Original Title",
                Budget = -1.0m // Negative budget
            };
            
            Result result = await SendAsync(command);
            
            Assert.IsFalse(result.Succeeded);
            Assert.IsTrue(result.Errors.Any());
            Assert.IsTrue(result.Errors.Contains("'Budget' must be greater than '0'."));
        }

        [TestMethod]
        public async Task ShouldUpdateItinerary()
        {
            RunAsDefaultUser();
            await AddAsync(new Itinerary()
            {
                Title = "Japan Trip",
                Budget = 1000M,
                Description = "A trip to Japan"
            });
            
            var command = new EditItineraryCommand()
            {
                Id = 1,
                Title = "Japan Trip 2023",
                Budget = 2000M,
                Description = "A trip to Japan 2030"
            };
            
            Result result = await SendAsync(command);
            var entity = await FindAsync<Itinerary>(x => x.Title == "Japan Trip 2023");
            
            Assert.IsTrue(result.Succeeded);
            entity.Should().NotBeNull();
            entity!.CreatedBy.Should().Be(GetUserId());
            entity.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
            entity.Budget.Should().Be(2000M);
            entity.Description.Should().Be("A trip to Japan 2030");
        }

        [TestMethod]
        public async Task ShouldFailIfNotFound()
        {
            RunAsDefaultUser();
            var command = new EditItineraryCommand()
            {
                Id = 1000,
                Title = "Japan Trip 2023",
            };
            
            Result result = await SendAsync(command);
            
            result.Succeeded.Should().BeFalse();
            result.Errors.Should().Contain("Itinerary not found.");
        }

        private async Task<Itinerary> CreateItineraryForTest(string title)
        {
            var command = new CreateItineraryCommand
            {
                Title = title,
            };
            await SendAsync(command);
            return await FindAsync<Itinerary>(i => i.Title == title);
        }
    }
}