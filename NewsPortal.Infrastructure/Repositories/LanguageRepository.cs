using NewsPortal.Domain.Interfaces;
using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace NewsPortal.Infrastructure.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly ApplicationDbContext _context;
        public LanguageRepository(ApplicationDbContext dbContext) 
        {
            _context = dbContext;
        }

        public async Task<string> CreateLanguageAsync(Language language)
        {
            bool isExistLgCode = await _context.Language.AnyAsync(x => x.Code.ToLower() == language.Code.ToLower());
            if (!isExistLgCode)
            {
                await _context.Language.AddAsync(language);
                await _context.SaveChangesAsync();
                return "Language created successfully.";
            } else
            {
                return "Language code already exists.";
            }
        }

        public async Task<string> UpdateLanguage(Language language)
        {
            var lan = await _context.Language.FirstOrDefaultAsync(x => x.Id == language.Id);
            if (lan != null) { 
               lan.Name = language.Name;
               lan.IsActive = language.IsActive;
               lan.IsDefault = language.IsDefault;
                await _context.SaveChangesAsync();
            }
            return "Language updated successfully";
        }

        public async Task<IEnumerable<Language>> GetLanguageAsync()
        {
            IEnumerable<Language> languages = await _context.Language.ToListAsync();
            return languages;

        }

        public async Task<Language> GetLanguageByIdAsync(string languageCode)
        {
            return await _context.Language.Where(x => x.Name == languageCode).FirstOrDefaultAsync();
        }

        public string DeleteLanguageAsync(string languageCode)
        {
            var item  = _context.Language.FirstOrDefault(x => x.Code == languageCode);
            if (item != null)
            {
                _context.Language.Remove(item);
                _context.SaveChanges();
               
            }

            return "Deleted successfully";

        }
    }
}
