using System;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ClassWeb.Data;
using ClassWeb.Models;
using System.Data.SqlClient;
using System.Data;


namespace ClassWeb.Model
{
    public class DAL
    {
        /// <summary>
        /// created by: Ganesh Sapkota
        /// DAL for Classweb project. 
        /// </summary>
        
        //private static string EditOnlyConnectionString = "Server=localhost;Database=peerval;Uid=root;Pwd=;";
        //private static string ReadOnlyConnectionString = "Server=localhost;Database=peerval;Uid=root;Pwd=;";

        private static string ReadOnlyConnectionString = "Server=localhost;Database=web_masters;Uid=root;Pwd=;";
        private static string EditOnlyConnectionString = "Server=localhost;Database=web_masters;Uid=root;Pwd=;";
        private DAL()
        {
        }
        internal enum dbAction
        {
            Read,
            Edit
        }

        #region Database Connections
        internal static void ConnectToDatabase(MySqlCommand comm, dbAction action = dbAction.Read)
        {
            try
            {
                if (action == dbAction.Edit)
                    comm.Connection = new MySqlConnection(EditOnlyConnectionString);
                else
                    comm.Connection = new MySqlConnection(ReadOnlyConnectionString);

                comm.CommandType = System.Data.CommandType.StoredProcedure;
            }
            catch (Exception ex)
            {

            }
        }

        public static MySqlDataReader GetDataReader(MySqlCommand comm)
        {
            try
            {
                ConnectToDatabase(comm);
                comm.Connection.Open();
                return comm.ExecuteReader();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// reference: Proffesor's PeerEval Project. 
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        internal static int AddObject(MySqlCommand comm, string parameterName)
        {
            int retInt = 0;
            try
            {
                comm.Connection = new MySqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                MySqlParameter retParameter;
                retParameter = comm.Parameters.Add(parameterName, MySqlDbType.Int32);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.ExecuteNonQuery();
                retInt = (int)retParameter.Value;
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                if (comm.Connection != null)
                    comm.Connection.Close();

                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return retInt;
        }

        /// <summary>
        /// reference: Professor's DAL for PeerEval
        /// set connection and execute given command on the database
        /// </summary>
        /// <param name="comm">MySqlCommand to execute.</param>
        /// <returns>This will return the number of rows affected after execution. -1 on failure and positive number on success. </returns>
        internal static int UpdateObject(MySqlCommand comm)
        {
            int retInt = 0;
            try
            {
                comm.Connection = new MySqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                retInt = comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                if (comm.Connection != null)
                    comm.Connection.Close();

                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return retInt;
        }
        #endregion

        #region User
        /// public static User AddUser( User obj)
        /// {
        //if (obj == null)
        //    return -1;
        //MySqlCommand comm = new MySqlCommand();
        //try
        //{
        //    //sprocs here
        //    comm.Parameters.AddWithValue("@" + DatabaseObject._ID, obj.ID);
        //    return UpdateObject(comm);
        //}
        //catch(Exception ex)
        //{
        //    System.Diagnostics.Debug.WriteLine(ex.Message);
        //}
        //return -1;
        ////}

        #endregion

        #region Login

        ///<summary>
        /// Gets the User from the database corresponding to the Username
        /// Reference: Github, PeerEval Project
        /// </summary>
        /// <remarks></remarks>
        public static User GetUser(string userName, string password)
        {

            MySqlCommand comm = new MySqlCommand("sproc_GetUserByUserName");
            User retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_UserName, userName);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new User(dr);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            /// Verify password matches.
            //if (retObj != null)
            //{
            //    if (!Tools.Hasher.IsValid(passWord, retObj.Salt, _Pepper, _Stretches, retObj.Password))
            //    {
            //        retObj = null;
            //    }
            //}

            return retObj;
        }

            //return retObj;
            //User retObj = null;
            //MySqlCommand comm = new MySqlCommand("sproc_GetUserByUsername");
            //try
            //{
            //    comm.Parameters.AddWithValue("@" + User.db_UserName, username);
            //    MySqlDataReader dr = GetDataReader(comm);
            //    while (dr.Read())
            //    {
            //        retObj = new User(dr);
            //    }
            //    comm.Connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    comm.Connection.Close();
            //    System.Diagnostics.Debug.WriteLine(ex.Message);
            //}
            //return retObj;


            ///<summary>
            /// Get salt of the User from the database corresponding to the Username
            /// </summary>
            /// <remarks></remarks>

            public static string GetSaltForUser(string username)
        {
            String salt = "";
            MySqlCommand comm = new MySqlCommand("sproc_GetSaltForUser");
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_UserName, username);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    salt = dr.GetString(0);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return salt;
        }

        ///<summary>
        /// Set salt of the User from the database corresponding to the ID
        /// </summary>
        /// <remarks></remarks>
        internal static int SetSaltForUser(int userID, string salt)
        {
            if (userID == 0 || salt == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_SetSaltForUser");
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_ID, userID);
                comm.Parameters.AddWithValue("@" + User.db_Salt, salt);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        #endregion


        /// <summary>
        /// Gets a list of all PeerVal.Evaluation objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Evaluation> GetEvaluations()
        {
            MySqlCommand comm = new MySqlCommand("sprocEvaluationsGetAll");
            List<Evaluation> retList = new List<Evaluation>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retList.Add(new Evaluation(dr));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retList;
        }




    }

}