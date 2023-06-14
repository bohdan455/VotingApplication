using BLL.Dto;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extensions.Mappers
{
    public static class PollMapperExtensions
    {
        public static Poll ToPoll(this PollDto dto)
        {
            return new Poll
            {
                PollName = dto.Name,
                Choices = dto.Choices.Select(c => new Choice
                {
                    ChoiceText = c.Text
                }).ToList()
            };
        }
    }
}
