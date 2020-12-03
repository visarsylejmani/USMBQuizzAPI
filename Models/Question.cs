﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace USMBAPI.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string Description { get; set; }
        public int QuizID { get; set; }

    }
}
