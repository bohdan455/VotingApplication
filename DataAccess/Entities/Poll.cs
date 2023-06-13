using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Poll
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string PollName { get; set; } = string.Empty;
        public ICollection<Choice> Choices { get; set; }
    }
}
