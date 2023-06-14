using BLL.Dto;
using BLL.Services;
using DataAccess.Entities;
using DataAccess.Repositories.Intefaces;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.Services
{
    public class PollServiceTests
    {
        private readonly PollService _sut;
        private readonly Mock<IPollRepository> _pollRepository = new Mock<IPollRepository>();
        private readonly Mock<IChoiceRepository> _choiceRepository = new Mock<IChoiceRepository>();
        public PollServiceTests()
        {
            _sut = new PollService(_pollRepository.Object, _choiceRepository.Object);
        }

		[Fact]
		public async Task Try_To_Create_Poll()
		{
            // Arrange
            var poll = new Poll();
            var dto = new PollDto
            { 
                Name = "test",
                Choices = new List<ChoiceDto>
                {
                    new ChoiceDto
                    {
                        Text = "test"
                    }
                }
            };
            _pollRepository.Setup(pr => pr.AddAsync(It.IsAny<Poll>())).Verifiable();

            // Act
            await _sut.CreatePollAsync(dto);

            // Assert
            _pollRepository.VerifyAll();
		}

        [Fact]
        public async Task Try_To_Vote_With_NonExistent_PollId_Or_ChoiceId()
        {
            // Arrange
            _choiceRepository.Setup(cr => cr.GetFirstByCondition(It.IsAny<Expression<Func<Choice, bool>>>(),
                It.IsAny<Func<IQueryable<Choice>,IIncludableQueryable<Choice,object>>>())).Returns<Choice>(null);

            // Act
            var result = await _sut.VoteAsync(0, 0);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Try_To_Vote_With_Existing_PollId_And_ChoiceId()
        {
            // Arrange
            var initialNumberOfVoted = 2;
            var endingNumberOfVoted = initialNumberOfVoted + 1;

            var choice = new Choice
            {
                NumberOfVoted = initialNumberOfVoted
            };

            _choiceRepository.Setup(cr => cr.GetFirstByCondition(It.IsAny<Expression<Func<Choice, bool>>>(),
                It.IsAny<Func<IQueryable<Choice>, IIncludableQueryable<Choice, object>>>()))
                .Returns(choice);
            _choiceRepository.Setup(cr => cr.UpdateAsync(It.IsAny<Choice>())).Verifiable();

            // Act
            var result = await _sut.VoteAsync(0, 0);

            // Assert
            _choiceRepository.VerifyAll()
;            Assert.True(result);
            Assert.Equal(endingNumberOfVoted, choice.NumberOfVoted);
        }

        [Fact]
        public void Get_Result_With_NonExisting_PollId()
        {
            // Arrange
            _pollRepository.Setup(pr => pr.GetFirstByCondition(It.IsAny<Expression<Func<Poll, bool>>>(),
                It.IsAny<Func<IQueryable<Poll>, IIncludableQueryable<Poll, object>>>())).Returns<Poll>(null);

            // Act
            var results = _sut.GetResult(0);

            // Assert
            Assert.Null(results);
        }
        [Fact]
        public void Get_Result_With_Existing_PullId_And_With_Zero_NumberOfVoted_In_All_Choices()
        {
            // Arrange
            var poll = new Poll
            { 
                PollName = "1",
                Choices = new List<Choice>
                {
                    new Choice
                    {
                        ChoiceText = "1",
                        NumberOfVoted = 0
                    }
                }
            };
            _pollRepository.Setup(pr => pr.GetFirstByCondition(It.IsAny<Expression<Func<Poll, bool>>>(), It.IsAny<Func<IQueryable<Poll>,
                IIncludableQueryable<Poll, object>>>())).Returns(poll);
            _choiceRepository
                .Setup(cr => cr.GetSumByCondition(It.IsAny<Expression<Func<Choice, bool>>>(),It.IsAny<Expression<Func<Choice, decimal>>>(),
               It.IsAny<Func<IQueryable<Choice>, IIncludableQueryable<Choice, object>>>())).Returns(0);

            // Act
            var results = _sut.GetResult(0);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(results.PollName, poll.PollName);
        }

        [Fact]
        public void Get_Result_With_Existing_PullId()
        {
            // Arrange
            var poll = new Poll
            {
                PollName = "1",
                Choices = new List<Choice>
                {
                    new Choice
                    {
                        ChoiceText = "1",
                        NumberOfVoted = 2
                    },
                    new Choice
                    {
                        ChoiceText = "2",
                        NumberOfVoted = 2
                    }
                }
            };
            _pollRepository.Setup(pr => pr.GetFirstByCondition(It.IsAny<Expression<Func<Poll, bool>>>(),
                It.IsAny<Func<IQueryable<Poll>, IIncludableQueryable<Poll, object>>>())).Returns(poll);
            _choiceRepository
                .Setup(cr => cr.GetSumByCondition(It.IsAny<Expression<Func<Choice, bool>>>(), It.IsAny<Expression<Func<Choice, decimal>>>(),
                It.IsAny<Func<IQueryable<Choice>, IIncludableQueryable<Choice, object>>>())).Returns(4);

            // Act
            var results = _sut.GetResult(0);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(poll.PollName, results.PollName);
            Assert.Equal(poll.Choices.First().NumberOfVoted, results.Choices.First().NumberOfVotes);


        }
    }
}
