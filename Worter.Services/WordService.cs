using FMCW.Common;
using FMCW.Common.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Worter.DAO.Models;
using Worter.DTO.Language;
using Worter.Interfaces;
using Worter.Services.Shared;

namespace Worter.Services
{
    public class WordService : BaseService, IWordService
    {
        public WordService(WorterContext context, IConfiguration configuration) : base(context, configuration) { }

        public IntResult Add(TranslateDTO wordDto, int userId)
        {
            // check if word exists
            var word = context.Word.Where(w => w.Meaning == wordDto.OriginalMeaning && w.IdLanguage == wordDto.IdLanguage)
                .Include( w => w.Translation)
                .FirstOrDefault();

            if (word == null)
            {
                word = new Word
                {
                    IdLanguage = wordDto.IdLanguage,
                    Meaning = wordDto.OriginalMeaning,
                    IdStudent = userId,
                    SearchValue = Helpers.GetSearchValue(wordDto.OriginalMeaning)
                };
                context.Word.Add(word);
            }
            // check if translation already exists
            else if (word.Translation.Any(t => t.Translate == wordDto.TranslateMeaning))
            {
                return new IntResult 
                { 
                    ResultOperation = ResultOperation.RegisterAlreadyAdd,
                    Success = true
                };
            }

            var translation = new Translation
            {
                IdWordNavigation = word,
                Translate = wordDto.TranslateMeaning,
                SearchValue = Helpers.GetSearchValue(wordDto.TranslateMeaning),
                Score = 0
            };
            context.Translation.Add(translation);

            context.SaveChanges();
            return IntResult.Ok(word.IdWord);
        }

        public BoolResult DeleteTranslate(int idTranslate)
        {
            var translate = context.Translation
                .Include(t => t.IdWordNavigation)
                .ThenInclude(w => w.Translation)
                .FirstOrDefault(t => t.IdTranslation == idTranslate);

            if (translate == null)
            {
                return BoolResult.Error("Translate not found");
            }
            context.Entry(translate).State = EntityState.Deleted;

            if (translate.IdWordNavigation.Translation.Count() == 1)
            {
                context.Entry(translate.IdWordNavigation).State = EntityState.Deleted;
            }
            context.SaveChanges();
            return BoolResult.Ok(true);
        }

        public ListResult<TranslateDTO> Get(WordFilterDTO filters, int iduser)
        {
            var filterValue = Helpers.GetSearchValue(filters.Filter);
            
            var words = context
                .Word
                .Where(
                    w => w.IdStudent == iduser &&
                    w.IdLanguage == filters.IdLanguage &&
                    (w.Translation.Any(t => t.SearchValue.Contains(filterValue)) || w.SearchValue.Contains(filterValue)))
                .SelectMany(
                w => w
                    .Translation
                    .Select(t =>
                        new TranslateDTO
                        {
                            IdTranslate = t.IdTranslation,
                            IdLanguage = w.IdLanguage,
                            IdWord = w.IdWord,
                            OriginalMeaning = w.Meaning,
                            TranslateMeaning = t.Translate
                        }
                ))
                .AsNoTracking()
                .ToList();

            return ListResult<TranslateDTO>.Ok(words);
        }
    }
}
