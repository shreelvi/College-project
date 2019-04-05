using System;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
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
        /// </summary
        private static string ReadOnlyConnectionString = "Server=MYSQL5014.site4now.net;Database=db_a45fe7_classwe;Uid=a45fe7_classwe;Pwd=kish1029";
        private static string EditOnlyConnectionString = "Server=MYSQL5014.site4now.net;Database=db_a45fe7_classwe;Uid=a45fe7_classwe;Pwd=kish1029";
        public static string _Pepper = "gLj23Epo084ioAnRfgoaHyskjasf"; //HACK: set here for now, will move elsewhere later.
        public static int _Stretches = 10000;
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
            catch (Exception)
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
        public static int GetIntReader(MySqlCommand comm)
        {
            try
            {
                ConnectToDatabase(comm);
                comm.Connection.Open();
                int count = Convert.ToInt32(comm.ExecuteScalar());
                return count;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return 0;
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
        #region Assignment
        internal static int AddAssignment(Assignment obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_AssignmentAdd");
            try
            {
                comm.Parameters.AddWithValue("@" + Assignment.db_FileName, obj.FileName);
                comm.Parameters.AddWithValue("@" + Assignment.db_Location, obj.FileLocation);
                comm.Parameters.AddWithValue("@" + Assignment.db_DateStarted, obj.DateStarted);
                comm.Parameters.AddWithValue("@" + Assignment.db_DateSubmited, obj.DateSubmited);
                comm.Parameters.AddWithValue("@" + Assignment.db_Feedback, obj.Feedback);
                comm.Parameters.AddWithValue("@" + Assignment.db_FileSize, obj.FileSize);
                comm.Parameters.AddWithValue("@" + Assignment.db_Grade, obj.Grade);
                comm.Parameters.AddWithValue("@" + Assignment.db_DateDue, obj.DateDue);
                comm.Parameters.AddWithValue("@" + Assignment.db_IsEditable, obj.IsEditable);
                comm.Parameters.AddWithValue("@" + Assignment.db_DateModified, obj.DateModified);
                return AddObject(comm, "@" + Assignment.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        public static Assignment GetAssignmentByFileName(string fileName)
        {
            MySqlCommand comm = new MySqlCommand("sproc_AssignmentGetByFileName");
            Assignment retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Assignment.db_FileName, fileName);
                MySqlDataReader dr = GetDataReader(comm);

                while (dr.Read())
                {
                    retObj=new Assignment(dr);
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

        public static Assignment GetAllAssignment()
        {

            Assignment retObj = null;
            MySqlCommand comm = new MySqlCommand("sproc_GetAllAssignment");
            try
            {
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new Assignment(dr);
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
        internal static int DeleteAssignmentByID(int ID)
        {
            MySqlCommand comm = new MySqlCommand("sproc_AssignmentDeleteByID");
            int retInt = 0;
            try
            {
                comm.Parameters.AddWithValue("@" + Assignment.db_ID, ID);
                comm.Connection = new MySqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                MySqlParameter retParameter;
                retParameter = comm.Parameters.Add("@" + Assignment.db_ID, MySqlDbType.Int32);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.ExecuteNonQuery();
                retInt = (int)retParameter.Value;
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retInt;
        }
        #endregion
        #region Login

        ///<summary>
        /// Gets the User from the database corresponding to the Username
        /// Reference: Github, PeerEval Project
        /// </summary>
        /// <remarks></remarks>
        public static LoginModel GetUser(string userName, string password)
        {

            MySqlCommand comm = new MySqlCommand("sproc_UserGetByUserName");
            LoginModel retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + LoginModel.db_UserName, userName);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new LoginModel(dr);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            //Verify password matches.
            if (retObj != null)
            {
                if (!Tools.Hasher.IsValid(password, retObj.Salt, _Pepper, _Stretches, retObj.Password.TrimEnd('!')))
                {
                    retObj = null;
                }
            }

            return retObj;
        }

        /// <summary>
        /// Attempts to add user in the database
        /// Reference: PeerVal Project
        /// </summary>
        /// <remarks></remarks>

        internal static int AddUser(User obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_UserAdd");
            try
            {
                // generate new password first.
                obj.Salt = Tools.Hasher.GenerateSalt(50);
                string newPass = Tools.Hasher.Get(obj.Password, obj.Salt, _Pepper, _Stretches, 64);
                obj.Password = newPass;
                // now set object to Database.
                comm.Parameters.AddWithValue("@" + User.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + User.db_MiddleName, obj.MiddleName);
                comm.Parameters.AddWithValue("@" + User.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + User.db_EmailAddress, obj.EmailAddress);
                comm.Parameters.AddWithValue("@" + User.db_UserName, obj.UserName);
                comm.Parameters.AddWithValue("@" + User.db_Password, obj.Password);
                //comm.Parameters.AddWithValue("@" + User.db_Role, obj.RoleID);
                comm.Parameters.AddWithValue("@" + User.db_Salt, obj.Salt);
                return AddObject(comm, "@" + User.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }
        
        internal static int UpdateUser(User obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_UserUpdate");
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + User.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + User.db_MiddleName, obj.MiddleName);
                comm.Parameters.AddWithValue("@" + User.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + User.db_UserName, obj.UserName);
                comm.Parameters.AddWithValue("@" + User.db_Password, obj.Password);
                comm.Parameters.AddWithValue("@" + User.db_EmailAddress, obj.EmailAddress);
                comm.Parameters.AddWithValue("@" + User.db_Address, obj.Address);
                comm.Parameters.AddWithValue("@" + User.db_Salt, obj.Salt);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }




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
                //comm.Parameters.AddWithValue("@" + User.db_Salt, salt);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        #endregion

        #region GroupLogin

        ///<summary>
        /// Gets the User from the database corresponding to the Username
        /// Reference: Github, PeerEval Project
        /// </summary>
        /// <remarks></remarks>
        public static Group GetGroup(string userName, string password)
        {

            MySqlCommand comm = new MySqlCommand("get_GroupByUserName");
            Group retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Group.db_UserName, userName);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new Group(dr);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            //Verify password matches.
            if (retObj != null)
            {
                if (!Tools.Hasher.IsValid(password, retObj.Salt, _Pepper, _Stretches, retObj.Password.TrimEnd('!')))
                {
                    retObj = null;
                }
            }

            return retObj;
        }

        /// <summary>
        /// Attempts to add group in the database
        /// Reference: PeerVal Project + Login for User
        /// </summary>
        /// <remarks></remarks>

        internal static int AddGroup(Group obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("add_Group");
            try
            {
                // generate new password first.
                obj.Salt = Tools.Hasher.GenerateSalt(50);
                string newPass = Tools.Hasher.Get(obj.Password, obj.Salt, _Pepper, _Stretches, 64);
                obj.Password = newPass;
                // now set object to Database.

                comm.Parameters.AddWithValue("@" + Group.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Group.db_EmailAddress, obj.EmailAddress);
                comm.Parameters.AddWithValue("@" + Group.db_UserName, obj.Username);
                comm.Parameters.AddWithValue("@" + Group.db_Password, obj.Password);
                comm.Parameters.AddWithValue("@" + Group.db_Salt, obj.Salt);
                return AddObject(comm, "@" + Group.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        internal static int AddUserToGroup(Group obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("add_UserToGroup");
            try
            {
              
                comm.Parameters.AddWithValue("@" + User.db_EmailAddress, obj.EmailAddress);
               
                return AddObject(comm, "@" + Group.db_EmailAddress);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }


        internal static int UpdateGroup(Group obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("update_Group");
            try
            {
                comm.Parameters.AddWithValue("@" + Group.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Group.db_EmailAddress, obj.EmailAddress);
                comm.Parameters.AddWithValue("@" + Group.db_UserName, obj.Username);
                comm.Parameters.AddWithValue("@" + Group.db_Password, obj.Password);
                comm.Parameters.AddWithValue("@" + Group.db_Salt, obj.Salt);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        ///<summary>
        /// Check if username exists in the database
        /// </summary>
        /// <remarks></remarks>
        internal static int CheckGroupExists(string username)
        {
            if (username == null) return -1;
            MySqlCommand comm = new MySqlCommand("CheckUserName_Group");
            try
            {
                comm.Parameters.AddWithValue("@" + Group.db_UserName, username);
                int dr = GetIntReader(comm);

                return dr;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        internal static int RemoveUserFromGroup(Group obj)
        {
            if (obj == null)
                return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.Parameters.AddWithValue("@" + Group.db_ID, obj.ID);
                return UpdateObject(comm);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1; 
        }
        /// <summary>
        /// Gets List of usernames from the database to check for same names
        /// </summary>
        /// <returns>List of Usernames string</returns>
        internal static List<Group> GetAllGroups()
        {
            MySqlCommand comm = new MySqlCommand("get_Group");
            List<Group> groupList = new List<Group>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    Group group = new Group(dr);
                  
                    groupList.Add(group);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return groupList;
        }
        #endregion


        ///<summary>
        /// Get salt of the Group from the database corresponding to the Username
        /// </summary>
        /// <remarks></remarks>

        public static string GetSaltForGroup(string username)
        {
            String salt = "";
            MySqlCommand comm = new MySqlCommand("get_SaltForGroup");
            try
            {
                comm.Parameters.AddWithValue("@" + Group.db_UserName, username);
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
        /// Set salt of the Group from the database corresponding to the ID
        /// </summary>
        /// <remarks></remarks>
        internal static int SetSaltForGroup(int groupID, string salt)
        {
            if (groupID == 0 || salt == null) return -1;
            MySqlCommand comm = new MySqlCommand("set_SaltForGroup");
            try
            {
                comm.Parameters.AddWithValue("@" + Group.db_ID, groupID);
                //comm.Parameters.AddWithValue("@" + User.db_Salt, salt);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

       

    }

}