using FMCW.Common.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Worter.Common;
using Worter.Common.Constants;
using Worter.DAO.Models;
using Worter.DTO.Language;
using Worter.Interfaces;
using Worter.Services.Shared;

namespace Worter.Services
{
    public class LearnService : BaseService, ILearnService
    {
        public LearnService(WorterContext context, IConfiguration configuration) : base(context, configuration) { }

        /// <summary>
        /// returns 10 learns dto, 5 Word => Translations and 5 Translations => Word
        /// </summary>
        /// <param name="idLanguage"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ListResult<LearnDTO> GetLearns(int idLanguage, int userId, int wordsToReturn = CONSTANTS.WORDS_TO_RETURN)
        {
            var result = new List<LearnDTO>();
            // check if its at least 5 words
            var words = context
                .Word
                .Include(w => w.Translation)
                .Where(w => w.IdLanguage == idLanguage && w.IdStudent == userId)
                .AsNoTracking()
                .ToList();

            if (words.Count() < wordsToReturn)
                return ListResult<LearnDTO>.Error($"You must add at least {wordsToReturn} words");

            // TODO order by counter based on wrong or correct before answers
            words.Shuffle();

            // word to translations
            foreach (var word in words.Take(wordsToReturn / 2))
            {
                result.Add(new LearnDTO
                {
                    Word = word.Meaning,
                    Translations = word.Translation
                        .Select(t => new CorrectAnswerDTO
                        {
                            Answer = t.Translate,
                            IdTranslate = t.IdTranslation
                        })
                        .ToList(),
                });
            }

            // translation to word
            words.Shuffle();
            var translations = words.SelectMany(w => w.Translation).ToList();
            foreach (var t in translations.Take(wordsToReturn / 2))
            {
                result.Add(new LearnDTO
                {
                    Word = t.Translate,
                    Translations = new List<CorrectAnswerDTO> {
                        new CorrectAnswerDTO
                        {
                             IdTranslate = t.IdTranslation,
                             Answer = t.IdWordNavigation.Meaning
                        }}
                });
            }

            // random order
            result.Shuffle();
            return ListResult<LearnDTO>.Ok(result);
        }

        public ListResult<LearnDTO> SaveResultsGetNew(List<AnswerDTO> userAnswers, int userId)
        {
            var translationsIds = userAnswers.Select(a => a.IdTranslate);
            var translates = context.Translation.Where(t => translationsIds.Contains(t.IdTranslation)).ToList();

            // get language id by translate
            var idLanguage = context
                .Translation
                .Select(t => new { 
                    t.IdTranslation,
                    t.IdWordNavigation.IdLanguage
                })
                .First(t => userAnswers.First().IdTranslate == t.IdTranslation)
                .IdLanguage;

            foreach (var answer in userAnswers)
            {
                var translation = translates.First(t => t.IdTranslation == answer.IdTranslate);
                translation.Score +=
                    answer.IsCorrect ?
                    CONSTANTS.POINTS_CORRECT :
                    CONSTANTS.POINTS_WRONG;

                context.Entry(translation).State = EntityState.Modified;
            }
            context.SaveChanges();
            // get new ones
            return GetLearns(idLanguage, userId);
        }
    }
}
