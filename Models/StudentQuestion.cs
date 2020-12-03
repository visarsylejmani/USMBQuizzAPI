using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USMBAPI.Models
{
    public class StudentQuestion
    {
        public int Student_QuestionID { get; set; }
        public int StudentID { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public int Mark { get; set; }
        public int MaxMark { get; set; }

    }
}
