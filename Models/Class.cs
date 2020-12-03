using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace USMBAPI.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        public string Name { get; set; }
        public string Passkey { get; set; }
        public int ProfessorID { get; set; }

    }
}
