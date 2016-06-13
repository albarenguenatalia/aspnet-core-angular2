using System;
using RockPaperScissors.Infrastructure.Repositories.Abstract;
using RockPaperScissors.Models;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors.Infrastructure.Repositories
{
    public class RoundRepository : EntityBaseRepository<Round>, IRoundRepository
    {
        public RoundRepository(RockPaperScissorsContext context)
            : base(context)
        { }
    }
}
