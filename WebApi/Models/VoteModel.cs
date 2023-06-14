using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class VoteModel
    {
        [Required]
        [JsonPropertyName("poll_id")]
        public int PollId { get; set; }
        [Required]
        [JsonPropertyName("choice_id")]
        public int ChoiceId { get; set; }
    }
}
