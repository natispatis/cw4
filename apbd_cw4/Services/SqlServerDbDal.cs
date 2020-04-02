using apbd_cw4.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace apbd_cw4.Services
{

        public class SqlServiceDbDal : IStudentsDal
        {
            public Enrollment GetEnrollment(string idStudenta)
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

        private Enrollment Ok(List<Enrollment> list)
        {
            throw new NotImplementedException();
        }

        public Enrollment GetStudentEnrollment(string studentIndex)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Student> GetStudents()
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
                        st.IndexNumber = int.Parse(dr["IndexNumber"].ToString());
                        st.FirstName = dr["FirstName"].ToString();
                        st.LastName = dr["LastName"].ToString();
                        st.BirthDate = dr["BirthDate"].ToString();
                        st.IdEnrollment = int.Parse(dr["IdEnrillment"].ToString());
                        list.Add(st);
                    }
                }
                return list;

            }
        }
    }
