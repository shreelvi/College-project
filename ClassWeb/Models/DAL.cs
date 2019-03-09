using System;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ClassWeb.Data;
using ClassWeb.Models;
using ClassWeb;

namespace ClassWeb.Model
{
    public class DAL
    {
        /// <summary>
        /// created by: Ganesh Sapkota
        /// DAL for Classweb project. 
        /// reference: Proffesor's PeerEval Project. 
        /// </summary>
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
        /// <summary>
        /// getting user based on their user ID.
        /// </summary>
        /// <param name="idstring"></param>
        /// <param name="retNewObject"></param>
        /// <returns></returns>
        public static User GetUser(String idstring, Boolean retNewObject)
        {
            User retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID))
            {
                if (ID == -1 && retNewObject)
                {
                    retObject = new User
                    {
                        ID = -1
                    };
                }
                else if (ID >= 0)
                {
                    retObject = GetUser(ID);
                }
            }
            return retObject;
        }
        /// <summary>
        /// getting user based on their user ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static User GetUser(int id)
        {
            MySqlCommand comm = new MySqlCommand("sprocUserGet");
            User retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_ID, id);
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
            return retObj;
        }
        /// <summary>
        /// get all the users from the database
        /// </summary>
        /// <returns></returns>
        public static List<User> GetUsers()
        {
            MySqlCommand comm = new MySqlCommand("sprocUsersGetAll");
            List<User> retList = new List<User>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retList.Add(new User(dr));
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
        /// <summary>
        /// adding database entry corresponding to the given user
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static int AddUser(User obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_UserAdd");
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + User.db_MiddleName, obj.MiddleName);
                comm.Parameters.AddWithValue("@" + User.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + User.db_EmailAddress, obj.EmailAddress);
                comm.Parameters.AddWithValue("@" + User.db_Address, obj.Address);
                comm.Parameters.AddWithValue("@" + User.db_UserName, obj.UserName);
                comm.Parameters.AddWithValue("@" + User.db_Password, obj.Password);
                comm.Parameters.AddWithValue("@" + User.db_PhoneNumber, obj.PhoneNumber);
                comm.Parameters.AddWithValue("@" + User.db_DateCreated, obj.DateCreated);
                comm.Parameters.AddWithValue("@" + User.db_DateModified, obj.DateModified);
                comm.Parameters.AddWithValue("@" + User.db_DateDeleted, obj.DateDeleted);
                comm.Parameters.AddWithValue("@" + User.db_AccountExpired, obj.AccountExpired);
                comm.Parameters.AddWithValue("@" + User.db_AccountLocked, obj.AccountLocked);
                comm.Parameters.AddWithValue("@" + User.db_Role, obj.RoleID);
                return AddObject(comm, "@" + User.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }
        #endregion
    }
}