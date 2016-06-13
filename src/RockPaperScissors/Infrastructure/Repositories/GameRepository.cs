using System;
using System.Collections.Generic;
using RockPaperScissors.Infrastructure.Repositories.Abstract;
using RockPaperScissors.Models;
using RockPaperScissors.Utilities;
using System.Linq;

namespace RockPaperScissors.Infrastructure.Repositories
{
    public class GameRepository : EntityBaseRepository<Game>, IGameRepository
    {
        public GameRepository(RockPaperScissorsContext context)
            : base(context)
        { }

        public override void Add(Game entity)
        {
            if (entity.Settings == null || entity.Settings == "" || !ValidateType.IsValidJson(entity.Settings)) {
                entity.Settings = GlobalConstants.DefaultGameSettings;
            }
            base.Add(entity);
        }

        public Round GetLastRound(int gameId)
        {
            Game game = this.GetSingle(gameId);
            Round lastRound = null;

            if (game != null) {
                IEnumerable<Round> gameRounds = game.Rounds;
                lastRound = gameRounds.Count() > 0 ? gameRounds.Aggregate((r1, r2) => r1.RoundNo > r2.RoundNo ? r1 : r2) : null;
            }

            return lastRound;
        }
    }
}
