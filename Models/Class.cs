﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace USMBQuizzAPI.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public string Name { get; set; }
        public string Passkey { get; set; }
        public int ProfessorID { get; set; }

    }
}
