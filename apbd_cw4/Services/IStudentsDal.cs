using apbd_cw4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apbd_cw4.Services
{
    public interface IStudentsDal
    {
        public IEnumerable<Student> GetStudents();
        public Enrollment GetStudentEnrollment(string studentIndex);
    }
}
