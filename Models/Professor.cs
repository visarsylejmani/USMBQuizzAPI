﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USMBQuizzAPI.Models
{
    public class Professor
    {
        public int ProfessorID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}