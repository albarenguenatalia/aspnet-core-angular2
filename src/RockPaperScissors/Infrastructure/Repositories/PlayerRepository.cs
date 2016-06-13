using RockPaperScissors.Infrastructure.Repositories.Abstract;
using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.Infrastructure.Repositories
{
    public class PlayerRepository : EntityBaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(RockPaperScissorsContext context)
            : base(context)
        { }

        public List<Game> GetGamesWon(string name)
        {
            List<Game> _gamesWon = null;
            Player _player = this.GetSingleByName(name);
            if (_player != null) {
                _gamesWon = new List<Game>();

                foreach (var game in _player.Games) {
                    if (game.WinnerId == _player.Id) {
                        _gamesWon.Add(game);
                    }
                }
            }
            return _gamesWon;
        }

        public List<Round> GetRoundsWon(int id, int gameId)
        {
            Player _player = this.GetSingle(id);
            List<Round> roundsWon = null;
            if (_player != null)
            {
                roundsWon = new List<Round>();

                foreach (Round round in _player.Rounds)
                {
                    if (round.WinnerId == _player.Id && round.GameId == gameId)
                    {
                        roundsWon.Add(round);
                    }
                }
            }
            return roundsWon;
        }

        public Player GetSingleByName(string name)
        {
            return this.GetSingle(x => x.Name == name);
        }
    }
}
