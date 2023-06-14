using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class PollDto
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<ChoiceDto> Choices { get; set; }
    }
}
