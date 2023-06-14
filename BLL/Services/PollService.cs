using BLL.Dto;
using BLL.Extensions.Mappers;
using DataAccess.Repositories.Intefaces;
using DataAccess.Repositories.Realisations.Main;

namespace BLL.Services
{
    public class PollService
    {
        private readonly IPollRepository _pollRepository;
        private readonly IChoiceRepository _choiceRepository;

        public PollService(IPollRepository pollRepository, IChoiceRepository choiceRepository)
        {
            _pollRepository = pollRepository;
            _choiceRepository = choiceRepository;
        }

        public async Task CreatePollAsync(PollDto pollDto)
        {
            await _pollRepository.AddAsync(pollDto.ToPoll());
        }
        /// <summary>
        /// Vote for choice
        /// </summary>
        /// <param name="pollId">Id of poll with choice</param>
        /// <param name="choiceId">RelativeId in db</param>
        /// <returns>Returns true if voted successfully either return false</returns>
        public async Task<bool> VoteAsync(int pollId, int choiceId)
        {
            var choice = _choiceRepository.GetFirstByCondition(c => c.PollId == pollId && c.RelativeId == choiceId);
            if (choice is null) return false;
            choice.NumberOfVoted++;
            await _choiceRepository.UpdateAsync(choice);
            return true;
        }
        public ResultsDto? GetResult(int pollId)
        {
            var poll = _pollRepository.GetFirstByCondition(p => p.Id == pollId);
            if (poll is null) return null;

            var choicesTotalNumberOfVoted = _choiceRepository.GetSumByCondition(c => c.PollId == pollId, c => c.NumberOfVoted);
            ResultsDto pollResult = new ResultsDto
            {
                PollName = poll.PollName,
                Choices = poll.Choices.Select(c => new ChoiceResultDto
                {
                    Name = c.ChoiceText,
                    NumberOfVotes = c.NumberOfVoted,
                    Percent = choicesTotalNumberOfVoted == 0 ? 0 : c.NumberOfVoted / choicesTotalNumberOfVoted * 100
                })
            };
            return pollResult;
        }
    }
}
