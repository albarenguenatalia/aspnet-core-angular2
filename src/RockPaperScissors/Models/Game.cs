using Newtonsoft.Json;
using RockPaperScissors.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.Models
{
    public class Game : IEntityBase
    {
        public Game() {
            Rounds = new List<Round>();
        }
        public int Id { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int WinnerId { get; set; }
        public int Result1 { get; set; }
        public int Result2 { get; set; }
        public string Settings { get; set; }
        public DateTime StartedAt { get; set; }

        // Navigation property
        public virtual Player Player1 {get; set; }
        public virtual Player Player2 { get; set; }
        public virtual Player Winner { get; set; }
        public virtual ICollection<Round> Rounds { get; set; }

        public GameSettings DeserializeSettings() {

            var settings = JsonConvert.DeserializeObject<GameSettings>(this.Settings);
            return settings;
        }

        public bool ValidateMove(string move) {

            GameSettings settings = this.DeserializeSettings();
            return settings.moves.Any(m => m.name == move);
        }

    }
}
