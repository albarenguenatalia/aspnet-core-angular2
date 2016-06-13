using RockPaperScissors.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.Models
{
    public class Round: IEntityBase
    {
        public int Id { get; set; }
        public string Move1 { get; set; }
        public string Move2 { get; set; }
        public int Result { get; set; }
        public int WinnerId { get; set; }
        public int GameId { get; set; }
        public int RoundNo { get; set; }

        public virtual Player Winner { get; set; }
        public virtual Game Game { get; set; }

        public Player ProcessMoves() {

            if (Move1 == Move2) {
                this.Result = 0;
                this.Winner = null;
            }
            else
            {
                GameSettings rules = Game.DeserializeSettings();
                foreach (Move move in rules.moves)
                {
                    if (move.name == Move1)
                    {
                        if (Move2 == move.beats) {
                            this.Result = 1;
                            this.Winner = Game.Player1;
                        }
                        break;

                    }
                    else if (move.name == Move2) {
                        if (Move1 == move.beats) {
                            this.Result = -1;
                            this.Winner = Game.Player2;
                        }
                        break;
                    }
                }
            }
            return this.Winner;
            
        }

    }
}
