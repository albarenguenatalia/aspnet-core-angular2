using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.Infrastructure
{
    public class DbInitializer
    {
        private static RockPaperScissorsContext context;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            context = (RockPaperScissorsContext)serviceProvider.GetService(typeof(RockPaperScissorsContext));

            InitializePlayers();
        }

        private static void InitializePlayers()
        {
            if (!context.Players.Any())
            {
                // create two initial players
                context.Players.Add(new Player()
                {
                    Name = "Jhon"
                });
                context.Players.Add(new Player()
                {
                    Name = "Melissa"
                });

                context.SaveChanges();
            }
        }
    }
}
