using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cumulative1http5125.Models;
using MySql.Data.MySqlClient;

namespace cumulative1http5125.Controllers
{
    public class StudentDataController : ApiController
    {
        private SchoolDbContext SchoolDb = new SchoolDbContext();

        /// <summary>
        /// Access the students information in the database.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("api/studentsdata/liststudents")]

        public List<Student> ListStudents()
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from students";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Student> Students = new List<Student> { };

            while (ResultSet.Read())
            {
                //In the students tables,the studentid column had an issue with the attributes being "unassigned", I'm not sure if this was on purpose but it wasn't allowing me to run the list properly giving me the following error message while loading the page: "Specified cast is not valid". I changed the attributes to none to that column.

                int StudentId = (int)ResultSet["studentid"];
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];

                Student NewStudent = new Student();
                NewStudent.studentid = StudentId;
                NewStudent.studentfname = StudentFname;
                NewStudent.studentlname = StudentLname;
                NewStudent.studentnumber = StudentNumber;
                NewStudent.enroldate = EnrolDate;

                Students.Add(NewStudent);

            }

            Conn.Close();

            return Students;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("api/studentdata/findstudent/{id}")]

        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            MySqlConnection Conn = SchoolDb.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from students where studentid = " + id;

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int StudentId = (int)ResultSet["studentid"];
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];

                NewStudent.studentid = StudentId;
                NewStudent.studentfname = StudentFname;
                NewStudent.studentlname = StudentLname;
                NewStudent.studentnumber = StudentNumber;
                NewStudent.enroldate = EnrolDate;
            }

            Conn.Close();

            return NewStudent;
        }
    }

}
