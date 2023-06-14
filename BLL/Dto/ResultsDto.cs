using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class ResultsDto
    {
        public string PollName { get; set; }
        public IEnumerable<ChoiceResultDto> Choices { get; set; }
    }
}
