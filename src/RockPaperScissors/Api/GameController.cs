using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Infrastructure.Services.Abstract;
using RockPaperScissors.Infrastructure.Repositories.Abstract;
using RockPaperScissors.ViewModels;
using RockPaperScissors.Infrastructure.Core;
using RockPaperScissors.Models;
using RockPaperScissors.Utilities;
using Newtonsoft.Json;

namespace RockPaperScissors.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly IMembershipService _membershipService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IRoundRepository _roundRepository;
        private readonly ILoggingRepository _loggingRepository;

        public GameController(IMembershipService membershipService, IGameRepository gameRepository, 
            IRoundRepository roundRepository, ILoggingRepository _errorRepository)
        {
            _membershipService = membershipService;
            _gameRepository = gameRepository;
            _roundRepository = roundRepository;
            _loggingRepository = _errorRepository;
        }

        // POST: api/game
        [HttpPost]
        public async Task<IActionResult> New(GameViewModel game)
        {
            IActionResult _result = new ObjectResult(false);
            GenericResult _gameNewResult = null;

            try
            {
                if (ModelState.IsValid)
                {
                    MembershipContext _player1Context = _membershipService.ValidatePlayer(game.Player1Name);
                    MembershipContext _player2Context = _membershipService.ValidatePlayer(game.Player2Name);

                    Player _player1 = _player1Context.Player;
                    Player _player2 = _player2Context.Player;

                    if (_player1 == null)
                    {
                        _player1 = _membershipService.CreatePlayer(game.Player1Name);
                    }

                    if (_player2 == null)
                    {
                        _player2 = _membershipService.CreatePlayer(game.Player1Name);
                    }

                    Game newGame = new Game()
                    {
                        Player1 = _player1,
                        Player2 = _player2,
                        StartedAt = DateTime.Now
                    };

                    _gameRepository.Add(newGame);
                    _gameRepository.Commit();

                    _result = new ObjectResult(newGame);
                    return _result;
                }
                else
                {
                    var json = JsonConvert.SerializeObject(game);
                    _gameNewResult = new GenericResult()
                    {
                        Succeeded = false,
                        Message = "Invalid parameter fields " + json
                    };

                    _loggingRepository.Add(new Error() { Message = "Invalid Model POST api/game " + json, DateCreated = DateTime.Now });
                    _loggingRepository.Commit();
                    _result = new ObjectResult(_gameNewResult);
                    return _result;
                }

            }
            catch (Exception ex)
            {
                _gameNewResult = new GenericResult()
                {
                    Succeeded = false,
                    Message = ex.Message
                };

                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
                _result = new ObjectResult(_gameNewResult);
                return _result;
            }                                            
        }

        // POST api/game/round
        [HttpPost]
        [Route("/round")]
        public async Task<IActionResult> Round(RoundViewModel newRound)
        {
            IActionResult _result = new ObjectResult(false);
            NextRoundResult _nextRoundResult = null;
            GenericResult _genericResult = null;

            try
            {
                if (ModelState.IsValid)
                {
                    Game game = _gameRepository.GetSingle(newRound.GameId);
                    if (game != null) {

                        if (game.ValidateMove(newRound.Move1) && game.ValidateMove(newRound.Move2)) {

                            Round lastRound = _gameRepository.GetLastRound(game.Id);
                            int roundNo = lastRound != null ? lastRound.RoundNo + 1 : 1;

                            Round roundToAdd = new Round()
                            {
                                Move1 = newRound.Move1,
                                Move2 = newRound.Move2,
                                Game = game,
                                RoundNo = roundNo
                            };

                            bool gameOver = PlayRound(roundToAdd);

                            _roundRepository.Add(roundToAdd);
                            _roundRepository.Commit();

                            if (gameOver == true && game.WinnerId == game.Player1Id)
                            {
                                _nextRoundResult = new NextRoundResult()
                                {
                                    Succeded = true,
                                    GameOver = true,
                                    Winner = game.Player1,
                                    Round = roundToAdd
                                };
                            }
                            else if (gameOver == true && game.WinnerId == game.Player2Id)
                            {

                                _nextRoundResult = new NextRoundResult()
                                {
                                    Succeded = true,
                                    GameOver = true,
                                    Winner = game.Player2,
                                    Round = roundToAdd
                                };
                            }
                            else {
                                _nextRoundResult = new NextRoundResult()
                                {
                                    Succeded = true,
                                    GameOver = false,
                                    Winner = roundToAdd.Winner,
                                    Round = roundToAdd
                                };
                            }

                            _result = new ObjectResult(_nextRoundResult);
                            return _result;
                        }
                        else
                        {
                            var json = JsonConvert.SerializeObject(newRound);
                            _genericResult = new GenericResult()
                            {
                                Succeeded = false,
                                Message = "Move1 " + newRound.Move1 + " or Move2 " + newRound.Move2 + " are invalid for the game settings"
                            };

                            _loggingRepository.Add(new Error() { Message = "Invalid Move1 or Move2 POST api/game/round Move1 " + newRound.Move1 + " Move2 " + newRound.Move2, DateCreated = DateTime.Now });
                            _loggingRepository.Commit();
                            _result = new ObjectResult(_genericResult);
                            return _result;
                        }
                    }
                    else
                    {
                        var json = JsonConvert.SerializeObject(newRound);
                        _genericResult = new GenericResult()
                        {
                            Succeeded = false,
                            Message = "Game with id " + newRound.GameId + " was not found"
                        };

                        _loggingRepository.Add(new Error() { Message = "Invalid Model POST api/game/round " + json, DateCreated = DateTime.Now });
                        _loggingRepository.Commit();
                        _result = new ObjectResult(_genericResult);
                        return _result;
                    }
                   
                }
                else
                {
                    var json = JsonConvert.SerializeObject(newRound);
                    _genericResult = new GenericResult()
                    {
                        Succeeded = false,
                        Message = "Invalid parameter fields " + json
                    };

                    _loggingRepository.Add(new Error() { Message = "Invalid Model POST api/game/round " + json, DateCreated = DateTime.Now });
                    _loggingRepository.Commit();
                    _result = new ObjectResult(_genericResult);
                    return _result;
                }

            }
            catch (Exception ex)
            {
                _genericResult = new GenericResult()
                {
                    Succeeded = false,
                    Message = ex.Message
                };

                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
                _result = new ObjectResult(_genericResult);
                return _result;
            }
        }

        /*
         * It process the Move of the two players.
         * Returns true if the game is over and one player has effectively one the game.
         * Returns false if continue to the next round.
         */
        private bool PlayRound(Round round) {
            round.ProcessMoves();

            GameSettings settings = round.Game.DeserializeSettings();
            bool gameOver = false;

            if (round.Winner != null)
            {
                List<Round> p1RoundsWon = _playerRepository.GetRoundsWon(round.Game.Player1.Id, round.Game.Id);
                List<Round> p2RoundsWon = _playerRepository.GetRoundsWon(round.Game.Player2.Id, round.Game.Id);
                round.Game.Result1 = p1RoundsWon.Count;
                round.Game.Result2 = p2RoundsWon.Count;

                if (round.Game.Player1.Id == round.WinnerId)
                {
                    
                    if (p1RoundsWon.Count == settings.wins)
                    {
                        gameOver = true;                       
                        round.Game.Winner = round.Game.Player1;
                    }
                }
                else if (round.Game.Player2Id == round.WinnerId)
                {
                    if (p2RoundsWon.Count == settings.wins)
                    {
                        gameOver = true;
                        round.Game.Winner = round.Game.Player2;
                    }
                }
                _gameRepository.Edit(round.Game);
                _gameRepository.Commit();
            }

            return gameOver;
        }

    }
}
