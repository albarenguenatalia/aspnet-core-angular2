using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.Models
{
    public class Player: IEntityBase
    {
        public Player()
        {
            Games = new List<Game>();
            Rounds = new List<Round>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastLoggedIn { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Round> Rounds { get; set; }

    }
}
