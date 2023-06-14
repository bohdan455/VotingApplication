using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class ChoiceResultDto
    {
        public string Name { get; set; } = string.Empty;
        public int NumberOfVotes { get; set; }
        public decimal Percent { get; set; }
    }
}
