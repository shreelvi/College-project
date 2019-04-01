using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class ClassDBHandle
    {
        /// <summary>
        /// by: Ganesh
        /// reference: https://www.completecsharptutorial.com/mvc-articles/insert-update-delete-in-asp-net-mvc-5-without-entity-framework.php
        /// </summary>
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["classconn"].ToString();
            con = new SqlConnection(constring);
        }

        public bool AddStudent(Class cmodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewClass", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", cmodel.Title);
            cmd.Parameters.AddWithValue("@IsAvailable", cmodel.IsAvailable);
            cmd.Parameters.AddWithValue("@DateStart", cmodel.DateStart);
            cmd.Parameters.AddWithValue("@DateEnd", cmodel.DateEnd);
            cmd.Parameters.AddWithValue("@SectionID", cmodel.SectionID);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }


        public List<Class> GetClass()
        {
            connection();
            List<Class> classlist = new List<Class>();

            SqlCommand cmd = new SqlCommand("GetClassDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                classlist.Add(
                    new Class
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        Title = Convert.ToString(dr["Title"]),
                        IsAvailable = Convert.ToBoolean(dr["IsAvailable"]),
                        DateStart = Convert.ToDateTime(dr["StartDate"]),
                        DateEnd= Convert.ToDateTime(dr["DateEnd"]),
                        SectionID = Convert.ToInt32(dr["SectionID"]),
                    });
            }
            return classlist;
        }

        public bool UpdateDetails(Class cmodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateClassDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", cmodel.Title);
            cmd.Parameters.AddWithValue("@IsAvailable", cmodel.IsAvailable);
            cmd.Parameters.AddWithValue("@DateStart", cmodel.DateStart);
            cmd.Parameters.AddWithValue("@DateEnd", cmodel.DateEnd);
            cmd.Parameters.AddWithValue("@SectionID", cmodel.SectionID);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteClass(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteClass", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ClassID", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}
 
