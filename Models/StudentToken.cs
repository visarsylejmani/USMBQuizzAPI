using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USMBQuizzAPI.Models
{
    public class StudentToken
    {
        public int StudentTokenID { get; set; }
        public int StudentID { get; set; }
        public string Token { get; set; }
        public DateTime Creation { get; set; }
        public DateTime Expiration { get; set; }

    }
}
