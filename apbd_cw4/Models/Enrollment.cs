using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apbd_cw4.Models
{
    public class Enrollment
    {
        public int IdEnrollment { get; set; }

        public int Semester { get; set; }

        public int IdStudy { get; set; }

        public string StartDate { get; set; }
    }
}
