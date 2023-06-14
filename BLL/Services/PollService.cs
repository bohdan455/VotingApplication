using BLL.Dto;
using BLL.Extensions.Mappers;
using DataAccess.Repositories.Realisations.Main;

namespace BLL.Services
{
    public class PollService
    {
        private readonly PollRepository _pollRepository;
        private readonly ChoiceRepository _choiceRepository;

        public PollService(PollRepository pollRepository, ChoiceRepository choiceRepository)
        {
            _pollRepository = pollRepository;
            _choiceRepository = choiceRepository;
        }

        public async Task CreatePoll(PollDto pollDto)
        {
            await _pollRepository.AddAsync(pollDto.ToPoll());
        }
        /// <summary>
        /// Vote for choice
        /// </summary>
        /// <param name="pollId">Id of poll with choice</param>
        /// <param name="choiceId">RelativeId in db</param>
        /// <returns>Returns true if voted successfully either return false</returns>
        public async Task<bool> Vote(int pollId, int choiceId)
        {
            var choice = _choiceRepository.GetByCondition(c => c.PollId == pollId && c.RelativeId == choiceId).FirstOrDefault();
            if (choice is null) return false;
            choice.NumberOfVoted++;
            await _choiceRepository.UpdateAsync(choice);
            return true;
        }
        public ResultsDto GetResult(int pollId)
        {
            var poll = _pollRepository.GetByCondition(p => p.Id == pollId).FirstOrDefault();
            var choicesTotalNumberOfVoted = _choiceRepository.GetByCondition(c => c.PollId == pollId).Sum(c => c.NumberOfVoted);

            if (choicesTotalNumberOfVoted == 0)
            {
                var pollResult = poll.Select(p => new ResultsDto
                {
                    PollName = p.PollName,
                    Choices = p.Choices.Select(c => new ChoiceResultDto
                    {
                        Name = c.ChoiceText,
                        NumberOfVotes = c.NumberOfVoted,
                        Percent = 0
                    })
                });
            }
            else
            {
                var pollResult = poll .Select(p => new ResultsDto
                {
                    PollName = p.PollName,
                    Choices = p.Choices.Select(c => new ChoiceResultDto
                    {
                        Name = c.ChoiceText,
                        NumberOfVotes = c.NumberOfVoted,
                        Percent = c.NumberOfVoted / choicesTotalNumberOfVoted * 100
                    })
                });
            }
            return pollResult;
        }
    }
}
