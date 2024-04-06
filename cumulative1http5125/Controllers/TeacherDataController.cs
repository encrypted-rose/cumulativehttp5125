using cumulative1http5125.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using Mysqlx.Connection;

namespace cumulative1http5125.Controllers
{
    public class TeacherDataController : ApiController

    {
        private SchoolDbContext SchoolDb = new SchoolDbContext();

        /// <summary>
        /// Access the teachers information in the database. Filters by teachers name, last name, hire date and salary matching a search key.
        /// </summary>
        /// <returns>
        /// Returns a list of teachers information in the database.
        /// </returns>
        /// <parameter name= "SearchKey"> The search key for the teacher's list. </parameter>
        /// <example>
        /// GET api/teachersdata/listteachers -> [Alexander Bennett, Caitlin Cummings]
        /// </example>

        [HttpGet]
        [Route("api/teachersdata/listteachers/{SearchKey}")]

        public List<Teacher> ListTeachers(string SearchKey)
        {
            //database access
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            //Opening connection between web server and database
            Conn.Open();

            //write sql cmd
            MySqlCommand cmd = Conn.CreateCommand();

            //create cmd
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key) or lower(salary) like lower(@key) or lower(hiredate) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //gathers query result
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //creates empty list of teachers
            List<Teacher> Teachers = new List<Teacher> { };

            while (ResultSet.Read())
            {
                //access column info in the db
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                Decimal Salary = (Decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.teacherid = TeacherId;
                NewTeacher.teacherfname = TeacherFname;
                NewTeacher.teacherlname = TeacherLname;
                NewTeacher.employeenumber = EmployeeNumber;
                NewTeacher.hiredate = HireDate;
                NewTeacher.salary = Salary;

                //Add teachers information to list
                Teachers.Add(NewTeacher);
            }
            //Closes connection
            Conn.Close();

            //Returns final list
            return Teachers;
        }


        /// <summary>
        /// Find a teacher from the database utilizing the ID.
        /// </summary>
        /// <param name="id">Teacher's table primary key</param>
        /// <returns>A teacher object containing the information aabout the teacher that matches the ID.
        /// </returns>
        /// <example>
        /// GET localhost:xx/api/teachersdata/findteacher/4 -> {"TeacherId": "4", "TeacherFname": "Lauren", "TeacherLname": "Smith", "EmployeeNumber":"T385","HireDate": "2014-06-22 00:00:00", "Salary": "74.20"}
        /// </example>

        [HttpGet]
        [Route("api/teachersdata/findteacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create connection instance
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            //Open connection
            Conn.Open();

            //Create query for database
            MySqlCommand cmd = Conn.CreateCommand();

            //Query
            cmd.CommandText = "Select * from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result set of query
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //loop to get data
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                Decimal Salary = (Decimal)ResultSet["salary"];

                NewTeacher.teacherid = TeacherId;
                NewTeacher.teacherfname = TeacherFname;
                NewTeacher.teacherlname = TeacherLname;
                NewTeacher.employeenumber = EmployeeNumber;
                NewTeacher.hiredate = HireDate;
                NewTeacher.salary = Salary;

            }
            Conn.Close();

            return NewTeacher;
        }

        //Add Teacher entry
        /// <summary>
        /// Receives a teacher information and adds it to the database.
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// POST: api/teachersdata/AddTeacher
        /// FORM DATA / REQUEST CONTENT
        /// {
        ///     "TeacherFname":"Mei",
        ///     "TeacherLname": "Raiden",
        ///     "EmployeeNumber": "T413",
        ///     "HireDate": "2024-05-12 00:00:00",
        ///     "Salary": "40.50"
        /// }
        /// </example>
        [HttpPost]
        [Route("api/teachersdata/AddTeacher")]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            Debug.WriteLine("API for adding a new teacher entry");
            Debug.WriteLine(NewTeacher.teacherfname);

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();


            cmd.CommandText = "insert into teachers (teacherfname,teacherlname,employeenumber,hiredate,salary) values (@teacherFname, @teacherLname, @EmployeeNum, @hiredate, @Salary)";

            cmd.Parameters.AddWithValue("@teacherFname", NewTeacher.teacherfname);
            cmd.Parameters.AddWithValue("@teacherLname", NewTeacher.teacherlname);
            cmd.Parameters.AddWithValue("@EmployeeNum", NewTeacher.employeenumber);
            cmd.Parameters.AddWithValue("@hiredate", NewTeacher.hiredate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.salary);
            cmd.Prepare();

            //To execute the insert statement
            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        //Delete teacher entry

        /// <summary>
        /// Deletes a teacher in the database/system.
        /// </summary>
        /// <param name="TeacherId">The Teacher Id</param>
        /// <returns>
        /// POST: api/teachersdata/DeleteTeacher/10
        /// </returns>
        [HttpPost]
        [Route("api/teachersdata/DeleteTeacher/{TeacherId}")]
        public void DeleteTeacher(int TeacherId)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText="Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", TeacherId);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close ();
        }
    }
}
