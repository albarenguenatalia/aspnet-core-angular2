using RockPaperScissors.Infrastructure.Core;
using RockPaperScissors.Infrastructure.Repositories.Abstract;
using RockPaperScissors.Infrastructure.Services.Abstract;
using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace RockPaperScissors.Infrastructure.Services
{
    public class MembershipService: IMembershipService
    {
        #region Variables
        private readonly IPlayerRepository _playerRepository;

        #endregion
        public MembershipService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public MembershipContext ValidatePlayer(string name)
        {
            var membershipCtx = new MembershipContext();

            var player = _playerRepository.GetSingleByName(name);
            if (player != null )
            {
                /**
                 * Just in case later we want to extend and handle several types
                 * of users.
                 * Also to provide an easy way to get the player data
                 * (later we may want to store email, username, etc.)
                 **/
                membershipCtx.Player = player;

                var identity = new GenericIdentity(player.Name);
                membershipCtx.Principal = new GenericPrincipal(
                    identity, new string[] { });
            }

            return membershipCtx;
        }

        #region IMembershipService Implementation
        public Player CreatePlayer(string name)
        {
            var existingPlayer = _playerRepository.GetSingleByName(name);

            if (existingPlayer != null)
            {
                throw new Exception("Name is already in use");
            }

            var player = new Player()
            {
                Name = name,
                DateCreated = DateTime.Now
            };

            _playerRepository.Add(player);
            _playerRepository.Commit();

            return player;
        }

        public Player GetPlayer(int playerId)
        {
            return _playerRepository.GetSingle(playerId);
        }
        #endregion
    }
}
