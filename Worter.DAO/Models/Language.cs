using System;
using System.Collections.Generic;

namespace Worter.DAO.Models
{
    public partial class Language
    {
        public Language()
        {
            Word = new HashSet<Word>();
        }

        public int IdLanguage { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Word> Word { get; set; }
    }
}
