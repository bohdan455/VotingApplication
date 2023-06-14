using BLL.Dto;
using WebApi.Models;

namespace WebApi.Extensions.Mappers
{
    public static class PollModelMapperExtension
    {
        public static PollDto ToPollDto(this PollModel pollModel)
        {
            return new PollDto
            {
                Choices = pollModel.Choices,
                Name = pollModel.Name,
            };
        }
    }
}
