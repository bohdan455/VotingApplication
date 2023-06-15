using BLL.Dto;
using BLL.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApi.Tests.Controllers
{
    public class ApiControllerTests
    {
        private readonly ApiController _sut;
        private readonly Mock<IPollService> _pollService = new();
        public ApiControllerTests()
        {
            _sut = new(_pollService.Object);
        }
        [Fact]
        public async Task Try_To_CreatePoll_With_Invalid_Model()
        {
            // Arrange
            _sut.ModelState.AddModelError("test", "test");
            var pollModel = new PollModel();

            // Act
            var result = await _sut.CreatePollAsync(pollModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Try_To_CreatePoll()
        {
            // Arrange
            var pollModel = new PollModel();
            _pollService.Setup(ps => ps.CreatePollAsync(It.IsAny<PollDto>())).Verifiable();

            // Act
            var result = await _sut.CreatePollAsync(pollModel) as OkResult;

            // Assert
            _pollService.VerifyAll();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Try_To_Vote_With_Invalid_Model()
        {
            // Arrange
            _sut.ModelState.AddModelError("test", "test");
            var voteModel = new VoteModel();
            // Act
            var result = await _sut.VoteAsync(voteModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);

        }
        [Fact]
        public async Task Try_To_Vote_For_NonExisting_Poll_Choice()
        {
            // Arrange
            _pollService.Setup(ps => ps.VoteAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false).Verifiable();
            var voteModel = new VoteModel();

            // Act
            var result = await _sut.VoteAsync(voteModel) as BadRequestResult;

            // Assert
            _pollService.VerifyAll();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Try_To_Vote()
        {
            // Arrange
            _pollService.Setup(ps => ps.VoteAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true).Verifiable();
            var voteModel = new VoteModel();

            // Act
            var result = await _sut.VoteAsync(voteModel) as OkResult;

            // Assert
            _pollService.Verify();
            Assert.NotNull(result);
        }

        [Fact]
        public void Try_To_GetResult_With_Invalid_Model()
        {
            // Arrange
            _sut.ModelState.AddModelError("test", "test");

            // Act
            var result = _sut.GetResult(0) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Try_To_GetResult_From_NonExisting_Poll()
        {
            // Arrange
            _pollService.Setup(ps => ps.GetResult(It.IsAny<int>())).Returns<ResultsDto>(null).Verifiable();

            // Act
            var result = _sut.GetResult(0) as BadRequestResult;

            // Assert
            _pollService.VerifyAll();
            Assert.NotNull(result);
        }

        [Fact]
        public void Try_To_GetResult()
        {
            // Arrange
            var resultDto = new ResultsDto
            {
                PollName = "test",
                Choices = new List<ChoiceResultDto>
                {
                    new ChoiceResultDto
                    {
                        Name = "test",
                        NumberOfVotes = 1,
                        Percent = 20
                    },
                    new ChoiceResultDto
                    {
                        Name = "test",
                        NumberOfVotes = 1,
                        Percent = 20
                    },
                    new ChoiceResultDto
                    {
                        Name = "test",
                        NumberOfVotes = 1,
                        Percent = 20
                    },
                    new ChoiceResultDto
                    {
                        Name = "test",
                        NumberOfVotes = 1,
                        Percent = 20
                    },
                    new ChoiceResultDto
                    {
                        Name = "test",
                        NumberOfVotes = 1,
                        Percent = 20
                    },
                }
            };
            _pollService.Setup(ps => ps.GetResult(It.IsAny<int>())).Returns(resultDto).Verifiable();

            // Act
            var result = _sut.GetResult(0) as OkObjectResult;

            // Assert
            _pollService.VerifyAll();
            Assert.NotNull(result);
            Assert.Equal(resultDto, result.Value);
        }
    }
}
