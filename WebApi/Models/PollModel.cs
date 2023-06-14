using BLL.Dto;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class PollModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public IEnumerable<ChoiceDto> Choices { get; set; }
    }
}
