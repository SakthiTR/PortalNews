using NewsPortal.Domain.Entities;

namespace NewsPortal.Domain.Interfaces
{
    public interface ILoggingRepository
    {
        Task LogErrorAsync(ErrorLogs errorLog);
    }
}
