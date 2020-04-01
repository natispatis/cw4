using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using apbd_cw4.Models;
using apbd_cw4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apbd_cw4.Controllers
{
    
        [ApiController]
        [Route("api/students")]
        public class StudentsController : ControllerBase
        {

            private readonly IStudentsDal _dbService;

            [HttpGet("id")]
            public IActionResult GetStudents()
            {
                var list = new List<Student>();

                using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18444;Integrated Security=True"))
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "select * from student";

                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        var st = new Student();
                        st.IndexNumber = int.Parse(dr["IdStudent"].ToString());
                        st.FirstName = dr["FirstName"].ToString();
                        st.LastName = dr["LastName"].ToString();
                        st.BirthDate = dr["BirthDate"].ToString();
                        st.IdEnrollment = int.Parse(dr["IdEnrollment"].ToString());
                    
                        list.Add(st);
                    }
                }
                return Ok(list);
            }


            public IActionResult GetEnrollment(string idStudenta)
            {
                var list = new List<Enrollment>();

                using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18444;Integrated Security=True"))
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "select * from Enrollment where IdEnrollment = (select IdEnrollment from Student where Student.IndexNumber='@index')";

                    SqlParameter par = new SqlParameter();
                    par.Value = idStudenta;
                    par.ParameterName = "index";
                    com.Parameters.Add(par);

                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        var en = new Enrollment();
                        en.IdEnrollment = int.Parse(dr["IndexNumber"].ToString());
                        en.Semester = int.Parse(dr["Semesrt"].ToString());
                        en.StartDate = dr["StartDate"].ToString();
                        en.IdStudy = int.Parse(dr["IdStudy"].ToString());
                        list.Add(en);
                    }
                }
                return Ok(list);
            }      
        }
    }