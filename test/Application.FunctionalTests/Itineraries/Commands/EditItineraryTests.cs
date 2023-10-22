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
            await AddAsync(new Itinerary()
            {
                Id = 1,
                Title = "Title 1",
                Budget = 1000M,
                Description = "A trip to Japan"
            });
            await AddAsync(new Itinerary()
            {
                Id = 2,
                Title = "Title 2",
                Budget = 1000M,
                Description = "A trip to Japan"
            });

            var command = new EditItineraryCommand
            {
                Id = 1,
                Title = "Title 2" 
            };

            Result result = await SendAsync(command);
            
            result.Succeeded.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should().Contain("'Title' must be unique.");
        }
        
        [TestMethod]
        public async Task ShouldRequireValidTitle()
        {
            RunAsDefaultUser();
            await AddAsync(new Itinerary
            {
                Id = 1,
                Title = "Title"
            });

            var command = new EditItineraryCommand
            {
                Id = 1,
                Title = new string('A', 201) // 201 characters long, exceeding the limit
            };

            Result result = await SendAsync(command);

            result.Succeeded.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should().Contain(
                "The length of 'Title' must be 200 characters or fewer. You entered 201 characters.");
        }
        
        [TestMethod]
        public async Task ShouldRequireMinimumFields()
        {
            RunAsDefaultUser();

            await AddAsync(new Itinerary()
            {
                Id = 1,
                Title = "Title"
            });

            var command = new EditItineraryCommand
            {
                Id = 1,
                Title = ""
            };
            
            Result result = await SendAsync(command);
            
            result.Succeeded.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should().Contain("'Title' must not be empty.");
        }
        
        [TestMethod]
        public async Task ShouldValidateBudget()
        {
            RunAsDefaultUser();
            await AddAsync(new Itinerary
            {
                Id = 1,
                Title = "Title"
            });

            var command = new EditItineraryCommand
            {
                Id = 1,
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
    }
}