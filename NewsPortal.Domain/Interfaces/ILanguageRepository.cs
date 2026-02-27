using NewsPortal.Domain.Entities;


namespace NewsPortal.Domain.Interfaces
{
    public interface ILanguageRepository
    {
        Task<string> CreateLanguageAsync(Language language);
        Task<string> UpdateLanguage(Language language);
        string DeleteLanguageAsync(string languageCode);
        Task<Language> GetLanguageByIdAsync(string languageCode);
        Task<IEnumerable<Language>> GetLanguageAsync();
    }
}
