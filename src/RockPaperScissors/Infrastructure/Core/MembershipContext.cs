using RockPaperScissors.Models;
using System.Security.Principal;

namespace RockPaperScissors.Infrastructure.Core
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public Player Player { get; set; }
        public bool IsValid()
        {
            return Principal != null;
        }
    }
}
