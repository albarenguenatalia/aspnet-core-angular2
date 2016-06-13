using RockPaperScissors.Models;
using System.Collections.Generic;

namespace RockPaperScissors.Infrastructure.Repositories.Abstract
{
    public interface IGameRepository : IEntityBaseRepository<Game> {

        Round GetLastRound(int gameId);

    }

    public interface ILoggingRepository : IEntityBaseRepository<Error> { }

    public interface IRoundRepository : IEntityBaseRepository<Round> { }

    public interface IPlayerRepository : IEntityBaseRepository<Player> {

        Player GetSingleByName(string name);
        List<Game> GetGamesWon(string name);
        List<Round> GetRoundsWon(int id, int gameId);
    }
}
