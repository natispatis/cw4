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

            /*
            [Route("api/[controller]")]
            [ApiController]
            public class StudentsController : ControllerBase
            {
                private const string ConString = "Data Source=db-mssql;Initial Catalog = s18444; Integrated Security = True";

                private IStudentsDal _dbService;

                public StudentsController(IStudentsDal dbService)
                {
                    _dbService = dbService;
                }

                [HttpGet]
                public IActionResult GetStudents([FromServices]IStudentsDal dbService)
                { 
                    var list = new List<Student>();

                    using (SqlConnection con = new SqlConnection(ConString))
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.Connection = con;
                        com.CommandText = "select * from students";

                        con.Open();
                        SqlDataReader dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            var st = new Student();
                            st.IndexNumber = dr["IndexNumber"].ToString();
                            st.IndexNumber = dr["FirstName"].ToString();
                            st.IndexNumber = dr["LastName"].ToString();
                            list.Add(st);
                        }
                    }
                    return Ok();
                }

                [HttpGet("(indexNumber")]
                public IActionResult GetStudent(String indexNumber)
                {
                    using (SqlConnection con = new SqlConnection(ConString))
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.Connection = con;
                        com.CommandText = "select * from students where indexNumber=@index";

                        com.Parameters.AddWithValue("index", indexNumber);

                        con.Open();
                        var dr = com.ExecuteReader();

                        if (dr.Read())
                        {
                            var st = new Student();

                            st.IndexNumber = dr["IndexNumber"].ToString();
                            st.IndexNumber = dr["FirstName"].ToString();
                            st.IndexNumber = dr["LastName"].ToString();
                            return Ok(st);
                        }
                    }

                    return NotFound();
                }

                [HttpGet("ex2")]
                public IActionResult GetStudents2(String indexNumber)
                {
                    using (SqlConnection con = new SqlConnection(ConString))
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.Connection = con;
                        com.CommandText = "TestProc3";
                        com.CommandType = System.Data.CommandType.StoredProcedure;

                        com.Parameters.AddWithValue("LastName", "Kowalski");

                        var dr = com.ExecuteReader();
                        if (dr.Read())
                        {
                            var st = new Student();

                            st.IndexNumber = dr["IndexNumber"].ToString();
                            st.IndexNumber = dr["FirstName"].ToString();
                            st.IndexNumber = dr["LastName"].ToString();
                            return Ok(st);
                        }
                    }
                    return Ok();
                }

                [HttpGet("{indexNumber}")]
                public IActionResult GetEnrollment(string indexNumber)
                {
                    using (SqlConnection con = new SqlConnection(ConString))
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.Connection = con;
                        com.CommandText = "select Enrollment.IdEnrollment, Enrollment.IdStudy, Enrollment.Semester, Enrollment.StartDate FROM Enrollment, Student " +
                                            "where student.IndexNumber =@index AND Student.IdEnrollment = Enrollment.IdEnrollment";

                        com.Parameters.AddWithValue("index", indexNumber);

                        con.Open();
                        var reader = com.ExecuteReader();
                        if (reader.Read())
                        {
                            var enrollment = new Enrollment();
                            enrollment.idEnrollment = reader["IdEnrollment"].ToString();
                            enrollment.idStudy = reader["IdStudy"].ToString();
                            enrollment.semester = reader["Semester"].ToString();
                            enrollment.startDate = reader["StartDate"].ToString();
                            return Ok(enrollment);
                        }
                        con.Dispose();
                    }
                    return NotFound();
                }


                /*
                [HttpGet("ex3")]
                public IActionResult GetStudents3(String indexNumber)
                {
                    using (SqlConnection con = new SqlConnection(ConString))
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.Connection = con;
                        com.CommandText = "Insert into Student(FirstName) values (@FirstName)";



                        con.Open();
                        SqlTransaction transaction = con.BeginTransaction();

                        try
                        {
                            int affectedRows = com.ExecuteNonQuery();

                            com.CommandText = "update info...";
                            com.ExecuteNonQuery();
                            //...
                            transaction.Commit();
                        } catch(Exception exc)
                        {
                            transaction.Rollback();
                        }

                    }

                    return Ok();
                    */
        }
    }

