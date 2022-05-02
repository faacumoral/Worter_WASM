using System;
using System.Collections.Generic;

namespace Worter.DAO.Models
{
    public partial class Student
    {
        public Student()
        {
            Word = new HashSet<Word>();
        }

        public int IdStudent { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Word> Word { get; set; }
    }
}
