using NewsPortal.Domain.Entities;
using NewsPortal.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Application.Services
{
    public class LanguageService
    {
        private ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<string> AddLanguage(Language language)
        {
          return await  _languageRepository.CreateLanguageAsync(language);
        }

        public async Task<string> UpdateLanguage(Language language)
        {
            return await _languageRepository.UpdateLanguage(language);
        }
        public async Task<Language> GetLanguageById(string code)
        {
            return await _languageRepository.GetLanguageByIdAsync(code);
        }

        public async Task<IEnumerable<Language>> GetLanguageAsync() 
        {  
            return await _languageRepository.GetLanguageAsync();
        }

        public string DeleteLanguageAsync(string languageCode) 
        {
            return _languageRepository.DeleteLanguageAsync(languageCode);
        }
    }
}
