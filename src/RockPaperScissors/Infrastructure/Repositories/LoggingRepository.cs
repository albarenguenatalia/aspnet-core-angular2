using RockPaperScissors.Infrastructure.Repositories.Abstract;
using RockPaperScissors.Models;

namespace RockPaperScissors.Infrastructure.Repositories
{
    public class LoggingRepository : EntityBaseRepository<Error>, ILoggingRepository
    {
        public LoggingRepository(RockPaperScissorsContext context)
            : base(context)
        { }

        public override void Commit()
        {
            try
            {
                base.Commit();
            }
            catch { }
        }
    }
}
