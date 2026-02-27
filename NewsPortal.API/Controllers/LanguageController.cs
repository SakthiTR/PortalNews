using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Application.Services;
using NewsPortal.Domain.Entities;

namespace NewsPortal.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly LanguageService _languageService;
        public LanguageController(LanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpPost("addLanguage")] // add language 
        public async Task<IActionResult> AddLanguage([FromBody] Language language)
        {
            var result = await _languageService.AddLanguage(language);

            if (result == "Language created successfully.")
                return Ok(new { success = true, message = result });
            else
                return BadRequest(new { success = false, message = result });
        }

        [HttpPost("UpdateLanguage")]
        public async Task<IActionResult> UpdateLanguage([FromBody] Language language)
        {
            var result = await _languageService.UpdateLanguage(language);
            return Ok(new { success = true, message = result });
           
        }


        [HttpGet("GetLanguages")] 
        public async Task<IActionResult> GetLanguages()
        {

            var result = await _languageService.GetLanguageAsync();
            return  Ok(new { success = true, objects = result, message = "Success" });
        }

        [HttpGet("GetLanguageById/{Code}")]
        public async Task<IActionResult> GetLanguageById(string Code)
        {
            return Ok(_languageService.GetLanguageById(Code));
        }

        [HttpDelete("DeleteLanguage/{Code}")]
        public async Task<IActionResult> DeleteLanguage(string Code)
        {
            var result = _languageService.DeleteLanguageAsync(Code);
            return Ok(new { success = true, objects = result, message = "Success" });
        }
    }
}
