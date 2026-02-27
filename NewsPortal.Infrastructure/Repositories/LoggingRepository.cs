using NewsPortal.Domain.Interfaces;
using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Context;

namespace NewsPortal.Infrastructure.Repositories
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly ApplicationDbContext _context;
        public LoggingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogErrorAsync(ErrorLogs errorLog)
        {
            await _context.Set<ErrorLogs>().AddAsync(errorLog);
            await _context.SaveChangesAsync();
        }
    }
}
