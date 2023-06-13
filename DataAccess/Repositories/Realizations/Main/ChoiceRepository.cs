using DataAccess.Entities;
using DataAccess.Repositories.Intefaces;
using DataAccess.Repositories.Realisations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Realisations.Main
{
    public class ChoiceRepository : RepositoryBase<Choice>,IChoiceRepository
    {
        public ChoiceRepository(VotingApplicationContext context) : base(context)
        {
        }
    }
}
