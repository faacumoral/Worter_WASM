using System;
using System.Collections.Generic;

namespace Worter.DAO.Models
{
    public partial class Translation
    {
        public int IdTranslation { get; set; }
        public int IdWord { get; set; }
        public string Translate { get; set; }
        public int Score { get; set; }
        public string SearchValue { get; set; }

        public virtual Word IdWordNavigation { get; set; }
    }
}
