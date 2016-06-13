using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.Utilities
{
    public class NextRoundResult
    {
        public bool Succeded { get; set; }
        public bool GameOver { get; set; }
        public Player Winner { get; set; }
        public Round Round { get; set; }
    }
}
