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
    public class ClassesDataController : ApiController
    {
        private SchoolDbContext SchoolDb = new SchoolDbContext();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("api/classesdata/listclasses")]

        public List<Class> ListClasses()
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from classes";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Class> Classes = new List<Class> { };

            while (ResultSet.Read())
            {
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = ResultSet["classcode"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                string ClassName = ResultSet["classname"].ToString();

                Class NewClasses = new Class();
                NewClasses.classid = ClassId;
                NewClasses.classcode = ClassCode;
                NewClasses.teacherid = TeacherId;
                NewClasses.stardate = StartDate;
                NewClasses.finishdate = FinishDate;
                NewClasses.classname = ClassName;

                Classes.Add(NewClasses);
            }

            Conn.Close();

            return Classes;
        }

        [HttpGet]
        [Route ("api/classesdata/findclass/{id}")]

        public Class FindClass(int id)
        {
            Class NewClass = new Class();

            MySqlConnection Conn = SchoolDb.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand ();

            cmd.CommandText = "Select * from classes where classid = " + id;

            MySqlDataReader ResultSet = cmd.ExecuteReader ();

            while(ResultSet.Read())
            {
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = ResultSet["classcode"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                string ClassName = ResultSet["classname"].ToString ();

                NewClass.classid = ClassId;
                NewClass.classcode = ClassCode;
                NewClass.teacherid = TeacherId;
                NewClass.stardate = StartDate;
                NewClass.finishdate = FinishDate;
                NewClass.classname = ClassName;
            }

            Conn.Close();

            return NewClass;    
        }
    }
}
