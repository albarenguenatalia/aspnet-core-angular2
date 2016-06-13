using RockPaperScissors.Infrastructure.Core;
using RockPaperScissors.Models;

namespace RockPaperScissors.Infrastructure.Services.Abstract
{
    public interface IMembershipService
    {
        MembershipContext ValidatePlayer(string name);
        Player CreatePlayer(string name);
        Player GetPlayer(int playerId);
    }
}
