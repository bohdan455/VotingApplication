using BLL.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions.Mappers;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IPollService _pollService;

        public ApiController(IPollService pollService)
        {
            _pollService = pollService;
        }
        [HttpPost("createPoll")]
        public async Task<IActionResult> CreatePollAsync(PollModel pollModel)
        {
            await _pollService.CreatePollAsync(pollModel.ToPollDto());
            return Ok();
        }
        [HttpPost("poll")]
        public async Task<IActionResult> VoteAsync(VoteModel voteModel)
        {
            var result = await _pollService.VoteAsync(voteModel.PollId,voteModel.ChoiceId);
            return result ? Ok() : BadRequest();
        }
        [HttpPost("getResult")]
        public IActionResult GetResult(int pollId)
        {
            var result = _pollService.GetResult(pollId);
            return result is null ? BadRequest() : Ok(result);
        }
    }
}
