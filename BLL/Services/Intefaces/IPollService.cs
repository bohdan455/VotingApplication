using BLL.Dto;

namespace BLL.Services.Intefaces
{
    public interface IPollService
    {
        Task CreatePollAsync(PollDto pollDto);
        ResultsDto? GetResult(int pollId);
        Task<bool> VoteAsync(int pollId, int choiceId);
    }
}