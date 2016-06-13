using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.Infrastructure.Core
{
    public class GameSettings
    {
        public int wins { get; set; }
        public List<Move> moves { get; set; }
    }
}
