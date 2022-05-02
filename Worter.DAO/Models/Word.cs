using System;
using System.Collections.Generic;

namespace Worter.DAO.Models
{
    public partial class Word
    {
        public Word()
        {
            Translation = new HashSet<Translation>();
        }

        public int IdWord { get; set; }
        public string Meaning { get; set; }
        public int IdLanguage { get; set; }
        public int IdStudent { get; set; }
        public string SearchValue { get; set; }

        public virtual Language IdLanguageNavigation { get; set; }
        public virtual Student IdStudentNavigation { get; set; }
        public virtual ICollection<Translation> Translation { get; set; }
    }
}
