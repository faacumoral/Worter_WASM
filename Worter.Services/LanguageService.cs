using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using Worter.DAO.Models;
using Worter.DTO.Language;
using Worter.Interfaces;
using Worter.Services.Shared;

namespace Worter.Services
{
    public class LanguageService : BaseService, ILanguageService
    {
        public LanguageService(WorterContext context, IConfiguration configuration) : base(context, configuration) { }

        public List<LanguageDTO> GetAll()
        {
            return context
                .Language
                .Select(l => new LanguageDTO
                {
                    IdLanguage = l.IdLanguage,
                    Name = l.Name
                }).ToList();
        }

    }
}
