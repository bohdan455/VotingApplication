using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Choice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RelativeId { get; set; }
        [Required]
        [MaxLength(255)]
        public string ChoiceText { get; set; } = string.Empty;
        [Required]
        public int NumberOfVoted { get; set; }
        [Required]
        public int PollId { get; set; }
        public Poll Poll { get; set; }
    }
}
