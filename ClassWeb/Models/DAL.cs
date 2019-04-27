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
        /// DAL for Classweb project which provides simplified access to stored data
        /// by creating a class of data access methods that directly reference a corresponding set of database store procedures. 
        /// </summary

         //private static string ReadOnlyConnectionString = "Server=localhost;Database=classweb;Port=3307;Uid=root;Pwd=kish1029;Convert Zero Datetime=True;Allow Zero Datetime=True";
         //private static string EditOnlyConnectionString = "Server=localhost;Database=classweb; Port=3307;Uid=root;Pwd=kish1029;Convert Zero Datetime=True;Allow Zero Datetime=True";
        //private static string ReadOnlyConnectionString = "Server=MYSQL7003.site4now.net;Database=db_a458d6_shreelv;Uid=a458d6_shreelv;Pwd=x129y190;";
        //private static string EditOnlyConnectionString = "Server=MYSQL7003.site4now.net;Database=db_a458d6_shreelv;Uid=a458d6_shreelv;Pwd=x129y190;";
        private static string ReadOnlyConnectionString = "Server=localhost;Database=sapkgane;Uid=root;Pwd= ;";
        private static string EditOnlyConnectionString = "Server=localhost;Database=sapkgane;Uid=root;Pwd= ;";
        //private static string ReadOnlyConnectionString = "Server=MYSQL7003.site4now.net;Database=db_a458d6_shreelv;Uid=a458d6_shreelv;Pwd=elvish123;";
        // private static string EditOnlyConnectionString = "Server=MYSQL7003.site4now.net;Database=db_a458d6_shreelv;Uid=a458d6_shreelv;Pwd=elvish123;";
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

        internal static User GetUser(int userID)
        {
            throw new NotImplementedException();
        }
        internal static Course GetCourse(int courseID)
        {
            throw new NotImplementedException();
        }


        internal static int UpdateUserRole(User obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_UserRoleUpdate");
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + User.db_Role, obj.RoleID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }
        internal static int UpdateUserPassword(User obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_UserPasswordUpdate");
            try
            {
                string newPass = Tools.Hasher.Get(obj.Password, obj.Salt, _Pepper, _Stretches, 64);
                comm.Parameters.AddWithValue("@" + User.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + User.db_Password, newPass);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
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

        internal static Role GetRoleByID(int id)
        {
            MySqlCommand comm = new MySqlCommand("sproc_RoleGetByID");
            Role retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Role.db_ID, id);
                MySqlDataReader dr = GetDataReader(comm);

                while (dr.Read())
                {
                    retObj = new Role(dr);
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

        internal static int RemoveRole(Role role)
        {
            throw new NotImplementedException();
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
        #region user

        /// <summary>
        /// Created by: Mohan 
        /// add user in the database
        /// Reference: PeerVal Project by Professor from github.
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

        /// <summary>
        /// Created by: Mohan 
        /// get all users from the database
        /// Reference: PeerVal Project by Professor from github.
        /// </summary>
        /// <remarks></remarks>
        internal static List<User> UserGetAll()
        {
            List<User> retObj = new List<User>();
            MySqlCommand comm = new MySqlCommand("sproc_UserGetAll");
            try
            {
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    //
                    retObj.Add(new User(dr));
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
        /// Get list of all Role CLassweb.objects from the database
        /// Reference: Taken code from the peerval project
        /// </summary>
        /// <returns></returns>
        public static List<Role> GetRoles()
        {
            MySqlCommand comm = new MySqlCommand("sproc_RolesGetAll");
            List<Role> retList = new List<Role>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retList.Add(new Role(dr));
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
        /// Created by: Mohan 
        /// Delete user from the database
        /// Reference: PeerVal Project by Professor from github.
        /// </summary>
        /// <remarks></remarks>
        internal static int DeleteUserByID(int ID)
        {
            MySqlCommand comm = new MySqlCommand("sproc_UserDeleteByID");
            int retInt = 0;
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_ID, ID);
                comm.Connection = new MySqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                retInt = comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retInt;
        }


        /// <summary>
        /// Created by: Mohan 
        /// Update user from the database
        /// Reference: PeerVal Project by Professor from github.
        /// </summary>
        /// <remarks></remarks>
        internal static int UpdateUser(User obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_UserUpdate");
            try
            {
                string newPass = Tools.Hasher.Get(obj.Password, obj.Salt, _Pepper, _Stretches, 64);
                comm.Parameters.AddWithValue("@" + User.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + User.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + User.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + User.db_UserName, obj.UserName);
                comm.Parameters.AddWithValue("@" + User.db_Password, newPass);
                comm.Parameters.AddWithValue("@" + User.db_ResetCode, obj.ResetCode);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }
        /// <summary>
        /// Created by: Mohan 
        /// Remove user from the database
        /// Reference: PeerVal Project by Professor from github.
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveUser(User obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_UserRemove");
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_ID, obj.ID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Created by: Mohan 
        /// get a user from the database
        /// Reference: PeerVal Project by Professor from github.
        /// </summary>
        /// <remarks></remarks>
        internal static User UserGetByID(int? id)
        {
            MySqlCommand comm = new MySqlCommand("sproc_UserGetByID");
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
        ///<summary>
        ///  Created by: Mohan 
        /// Get salt of the User from the database corresponding to the Username
        /// Reference: PeerVal Project by Professor from github.
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
        ///  Created by: Mohan 
        /// Set salt for the User from the database corresponding to the Username
        /// Reference: PeerVal Project by Professor from github.
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

        internal static Assignment GetAssignmentByNameLocationUserName(string name, string location, string userName)
        {
            throw new NotImplementedException();
        }

        internal static int CheckUserExists(string userName)
        {
            if (userName == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_CheckUserName");
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_UserName, userName);
                int dr = GetIntReader(comm);

                return dr;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }


        #endregion

        #region Login


        ///<summary>
        /// Gets the User from the database corresponding to the Username
        /// Reference: Github, PeerEval Project
        /// </summary>
        /// <remarks></remarks>
        public static User GetUser(string userName, string password)
        {

            MySqlCommand comm = new MySqlCommand("sproc_UserGetByUserName");
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

            //Verify password matches.
            if (retObj != null)
            {
                if (!Tools.Hasher.IsValid(password, retObj.Salt, _Pepper, _Stretches, retObj.Password))
                {
                    retObj = null;
                }
            }

            return retObj;

        }

        /// <summary>
        /// Gets List of usernames from the database to check for same names
        /// </summary>
        /// <returns>List of Usernames string</returns>
        internal static List<User> GetAllUsers()
        {
            MySqlCommand comm = new MySqlCommand("sproc_GetAllUsers");
            List<User> retList = new List<User>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    User user = new User(dr);
                    //a.User = new User(dr);
                    retList.Add(user);
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

        internal static List<Assignment> GetAllAssignmentByUserNameAndLocation(string userName, string v)
        {
            throw new NotImplementedException();
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
                comm.Parameters.AddWithValue("@" + Assignment.db_UserName, obj.UserName);
                return AddObject(comm, "@" + Assignment.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        internal static Assignment AssignmentGetByID(int id)
        {
            MySqlCommand comm = new MySqlCommand("sproc_GetAssignmentByUserID");
            Assignment retList = new Assignment();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue(User.db_ID, id);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    Assignment a = new Assignment(dr);
                    //a.User = new User(dr);
                    retList=a;
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



        internal static int UpdateAssignment(Assignment obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_AssignmentResubmit");
            try
            {
                comm.Parameters.AddWithValue("@" + Assignment.db_Feedback, obj.Feedback);
                comm.Parameters.AddWithValue("@" + Assignment.db_ID, obj.ID);
                UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        internal static int ResubmitAssignment(Assignment obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_ReSubmitAssignment");
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

        /// <summary>
        /// Gets a list of all ClassWeb.Assignment objects for the user from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Assignment> GetUserAssignments(int userID)
        {
            MySqlCommand comm = new MySqlCommand("sproc_GetAssignmentsByUserID");
            List<Assignment> retList = new List<Assignment>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue(User.db_ID, userID);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    Assignment a = new Assignment(dr);
                    //a.User = new User(dr);
                    retList.Add(a);
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

        internal static List<Assignment> GetAllAssignmentByUserName(string UserName)
        {
            List<Assignment> retObj = new List<Assignment>();
            MySqlCommand comm = new MySqlCommand("sproc_AssignmentGetAllByUserName");
            try
            {
                comm.Parameters.AddWithValue("@" + Assignment.db_UserName, UserName);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj.Add(new Assignment(dr));
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

        public static List<Assignment> GetAllAssignmentByFileName(string fileName)
        {
            List<Assignment> retObj = new List<Assignment>();
            MySqlCommand comm = new MySqlCommand("sproc_AssignmentAllGetByFileName");
            try
            {
                comm.Parameters.AddWithValue("@" + Assignment.db_FileName, fileName);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj.Add(new Assignment(dr));
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

        #endregion

        #region Role
        internal static int CheckUserExistsByEmailAddress(string v)
        {
            throw new NotImplementedException();
        }

        // <summary>
        /// Attempts to add a database entry corresponding to the given Role
        /// Reference: Taken code from the peerval project
        /// </summary>
        internal static int AddRole(Role obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_RoleAdd");
            try
            {
                comm.Parameters.AddWithValue("@" + Role.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Role.db_IsAdmin, obj.IsAdmin);
                comm.Parameters.AddWithValue("@" + Role.db_Users, obj.Users.DAVESet);
                comm.Parameters.AddWithValue("@" + Role.db_Role, obj.Roles.DAVESet);
                comm.Parameters.AddWithValue("@" + Role.db_Assignment, obj.Assignment.DAVESet);
                return AddObject(comm, "@" + Role.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Gets the Classweb.Role corresponding with the given ID
        /// Reference: Code taken from the peerval project
        /// </summary>
        /// <remarks></remarks>

        public static Role GetRole(int id)
        {
            return Roles.Get(id);
        }

        internal static int RoleRemoveByID(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Attempts to the database entry corresponding to the given Role
        /// Reference: Taken code from the peerval project
        /// </summary>
        internal static int UpdateRole(Role obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_RoleUpdate");
            try
            {
                comm.Parameters.AddWithValue("@" + Role.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Role.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Role.db_IsAdmin, obj.IsAdmin);
                comm.Parameters.AddWithValue("@" + Role.db_Users, obj.Users.DAVESet);
                comm.Parameters.AddWithValue("@" + Role.db_Role, obj.Roles.DAVESet);
                comm.Parameters.AddWithValue("@" + Role.db_Assignment, obj.Assignment.DAVESet);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        internal static Assignment GetAssignmentByFileName(string fileName)
        {
            throw new NotImplementedException();
        }

        internal static User UserGetByUserName(string userName, string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Attempts to delete the database entry corresponding to the Role
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveRole(int roleID)
        {
            if (roleID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "sproc_RoleRemove";
                comm.Parameters.AddWithValue("@" + Role.db_ID, roleID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }


        #endregion

        #region Courses
        ///<summary>
        ///Created by: Mohan
        ///Create/Add Course in database
        /// Reference: Professor peerVal project from github. 
        /// </summary>
        /// <remarks></remarks>

        internal static int CreateCourse(Course obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_CreateCourse");
            try
            {
                comm.Parameters.AddWithValue("@" + Course.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Course.db_Title, obj.Title);
                comm.Parameters.AddWithValue("@" + Course.db_Description, obj.Description);
                return AddObject(comm, "@" + Course.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }


        ///<summary>
        /// Created BY: Mohan
        /// Get All Courses from the database
        /// Reference: PeerVal project by Professor
        /// </summary>
        /// <remarks></remarks>

        internal static List<Course> GetAllCourses()
        {
            List<Course> retObj = new List<Course>();
            MySqlCommand comm = new MySqlCommand("sproc_GetAllCourses");
            try
            {
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    //
                    retObj.Add(new Course(dr));
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

        ///<summary>
        /// Created By: Mohan 
        /// Get Course 
        /// Reference: PeerVal project by Professor
        /// </summary>
        /// <remarks></remarks>
        internal static List<Course> GetCourse()
        {

            MySqlCommand comm = new MySqlCommand("sproc_GetAllCourses");
            List<Course> retList = new List<Course>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retList.Add(new Course(dr));
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
        ///<summary>
        /// Created By: Mohan 
        /// Edit Course 
        /// Reference: PeerVal project by Professor
        /// </summary>
        /// <remarks></remarks>
        internal static int UpdateCourse(Course obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_EditCourse");
            try
            {
                comm.Parameters.AddWithValue("@" + Course.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Course.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Course.db_Title, obj.Title);
                comm.Parameters.AddWithValue("@" + Course.db_Description, obj.Description);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        ///<summary>
        /// Created By: Mohan
        /// Delete Course by Id from the database
        /// Reference: PeerVal project by Professor
        /// </summary>
        /// <remarks></remarks>

        internal static int DeleteCourseByID(int ID)
        {
            MySqlCommand comm = new MySqlCommand("sproc_DeleteCourseByID");
            int retInt = 0;
            try
            {
                comm.Parameters.AddWithValue("@" + Course.db_ID, ID);
                comm.Connection = new MySqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                MySqlParameter retParameter;
                retParameter = comm.Parameters.Add("@" + Course.db_ID, MySqlDbType.Int32);
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

        #region Section
        ///Created on: 04/01/2019
        ///Created by: Meshari
        ///CRUD methods for Section object in ClassWeb Database
        ///Reference: Prof. Holmes PeerVal Project
        ///Copied code for Roles CRUD and modified to use for the section
        /// <summary>
        /// Gets a list of all Sections objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Section> GetSections()
        {
            MySqlCommand comm = new MySqlCommand("sproc_SectionsGetAll");
            List<Section> retList = new List<Section>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retList.Add(new Section(dr));
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
        /// Gets the Classweb.Section corresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Section GetSection(int id)
        {
            MySqlCommand comm = new MySqlCommand("sproc_SectionGet");
            Section retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Section.db_ID, id);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new Section(dr);
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
        /// Attempts to add a database entry corresponding to the given Section
        /// </summary>
        /// <remarks></remarks>

        internal static int AddSection(Section obj)
        {

            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_SectionAdd");
            try
            {
                comm.Parameters.AddWithValue("@" + Section.db_CRN, obj.CRN);
                comm.Parameters.AddWithValue("@" + Section.db_Number, obj.SectionNumber);
                return AddObject(comm, "@" + Section.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given Section
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateSection(Section obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_SectionUpdate");
            try
            {
                comm.Parameters.AddWithValue("@" + Section.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Section.db_Number, obj.SectionNumber);
                comm.Parameters.AddWithValue("@" + Section.db_CRN, obj.CRN);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the Section
        /// </summary>
        /// <remarks></remarks
        internal static int RemoveSection(Section obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "sproc_SectionRemove";
                comm.Parameters.AddWithValue("@" + Section.db_ID, obj.ID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }
        /// <summary>
        /// Attempts to delete the database entry corresponding to the Section
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveSection(int sectionID)
        {
            if (sectionID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "sproc_SectionRemoveByID";
                comm.Parameters.AddWithValue("@" + Section.db_ID, sectionID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        #endregion

        #region CourseSemester

        /// <summary>
        /// Created by Elvis
        /// Modified By Ganesh
        /// Get list of all CourseSemesters CLassweb.objects from the database
        /// Reference: Taken code from the peerval project
        /// </summary>
        /// <returns></returns>
        public static List<CourseSemester> GetCourseSemesters()
        {
            MySqlCommand comm = new MySqlCommand("sproc_CourseSemesterGetAll");
            List<CourseSemester> retList = new List<CourseSemester>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retList.Add(new CourseSemester(dr));
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

        //<summary>
        //Created on: 04/09/2019
        //Created by: Elvis
        //CRUD methods for CourseSemester object in ClassWeb Database
        //Reference: Prof. Holmes PeerVal Project
        //Copied code for Roles CRUD and modified to use for the section
        //</summary>


        public static CourseSemester GetCourseSemester(int id)
        {
            MySqlCommand comm = new MySqlCommand("sproc_CourseSemesterGet");
            CourseSemester retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + CourseSemester.db_ID, id);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new CourseSemester(dr);
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
        /// Attempts to add a database entry corresponding to the given CourseSemester
        /// </summary>
        /// <remarks></remarks>

        internal static int AddCourseSemester(CourseSemester obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_CourseSemesterAdd");
            try
            {
                comm.Parameters.AddWithValue("@" + CourseSemester.db_CourseID, obj.CourseID);
                comm.Parameters.AddWithValue("@" + CourseSemester.db_SemesterID, obj.SemesterID);
                comm.Parameters.AddWithValue("@" + CourseSemester.db_YearID, obj.YearID);
                comm.Parameters.AddWithValue("@" + CourseSemester.db_SectionID, obj.SectionID);
                comm.Parameters.AddWithValue("@" + CourseSemester.db_UserID, obj.UserID);
                return AddObject(comm, "@" + CourseSemester.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Attempts to edit the database entry corresponding to the given CourseSemester
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateCourseSemester(CourseSemester obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_CourseSemesterEdit");
            try
            {
                comm.Parameters.AddWithValue("@" + CourseSemester.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + CourseSemester.db_CourseID, obj.CourseID);
                comm.Parameters.AddWithValue("@" + CourseSemester.db_SemesterID, obj.SemesterID);
                comm.Parameters.AddWithValue("@" + CourseSemester.db_YearID, obj.YearID);
                comm.Parameters.AddWithValue("@" + CourseSemester.db_SectionID, obj.SectionID);
                comm.Parameters.AddWithValue("@" + CourseSemester.db_UserID, obj.UserID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }
        /// <summary>
        /// Attempts to delete the database entry corresponding to the CourseSemester
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveCourseSemester(int CourseSemesterID)
        {
            if (CourseSemesterID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "sproc_CourseSemesterRemove";
                comm.Parameters.AddWithValue("@" + CourseSemester.db_ID, CourseSemesterID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        #endregion

        #region Semester
        /// <summary>
        /// Created By Elvis
        /// Modified By Ganesh
        /// Gets the Classweb.Semester corresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Semester GetSemester(int id)
        {
            MySqlCommand comm = new MySqlCommand("sproc_SemesterGet");
            Semester retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Semester.db_ID, id);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new Semester(dr);
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
        /// Gets a list of all semester objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Semester> GetSemesters()
        {
            MySqlCommand comm = new MySqlCommand("sproc_SemesterGetAll");
            List<Semester> retList = new List<Semester>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retList.Add(new Semester(dr));
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
        /// Attempts to add a database entry corresponding to the given Semester
        /// </summary>
        /// <remarks></remarks>

        internal static int AddSemester(Semester obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_SemesterAdd");
            try
            {
                comm.Parameters.AddWithValue("@" + Semester.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Semester.db_Name, obj.Name);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Attempts to edit the database entry corresponding to the given Semester
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateSemester(Semester obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_SemesterEdit");
            try
            {
                comm.Parameters.AddWithValue("@" + Semester.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Semester.db_Name, obj.Name);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Attempts to delete the database entry corresponding to the Semester
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveSemester(int semesterID)
        {
            if (semesterID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "sproc_SemesterRemove";
                comm.Parameters.AddWithValue("@" + Semester.db_ID, semesterID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        #endregion

        #region Year
        /// <summary>
        /// Elvis
        /// Gets the Classweb.Semester corresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Year GetYear(int id)
        {
            MySqlCommand comm = new MySqlCommand("sproc_YearGet");
            Year retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Year.db_ID, id);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new Year(dr);
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
        /// Gets a list of all semester objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Year> GetYears()
        {
            MySqlCommand comm = new MySqlCommand("sproc_YearGetAll");
            List<Year> retList = new List<Year>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retList.Add(new Year(dr));
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
        /// Attempts to add a database entry corresponding to the given Year
        /// </summary>
        /// <remarks></remarks>

        internal static int AddYear(Year obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_YearAdd");
            try
            {
                comm.Parameters.AddWithValue("@" + Year.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Year.db_Year, obj.Year1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Attempts to edit the database entry corresponding to the given Semester
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateYear(Year obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_YearEdit");
            try
            {
                comm.Parameters.AddWithValue("@" + Year.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Year.db_Year, obj.Year1);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// Attempts to delete the database entry corresponding to the Year
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveYear(int YearID)
        {
            if (YearID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "sproc_YearRemove";
                comm.Parameters.AddWithValue("@" + Year.db_ID, YearID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        #endregion

        #region Group
        internal static List<User> GetGroupUsers(int id)
        {
            MySqlCommand comm = new MySqlCommand("sproc_GetUsersFromGroup");
            List<User> retList = new List<User>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@" + "GroupID", id);
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
        #endregion

        #region GroupLogin

        ///<summary>
        /// Gets the User from the database corresponding to the Username
        /// Reference: Github, PeerEval Project
        /// </summary>
        /// Created by Sakshi
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
        internal static Group GroupGetByID(int? id)
        {
            MySqlCommand comm = new MySqlCommand("get_GroupByID");
            Group retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Group.db_ID, id);
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
            MySqlCommand comm = new MySqlCommand("Sproc_AddGroup");
            try
            {
                // generate new password first.
                obj.Salt = Tools.Hasher.GenerateSalt(50);
                string newPass = Tools.Hasher.Get(obj.Password, obj.Salt, _Pepper, _Stretches, 64);
                obj.Password = newPass;
                // now set object to Database.

                comm.Parameters.AddWithValue("@" + Group.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Group.db_EmailAddress, obj.EmailAddress);
                comm.Parameters.AddWithValue("@" + Group.db_UserName, obj.UserName);
                comm.Parameters.AddWithValue("@" + Group.db_Password, obj.Password);
                comm.Parameters.AddWithValue("@" + Group.db_Salt, obj.Salt);
                return AddObject(comm, "@" + "GroupID");

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        internal static int AddUserToGroup(int GroupID, int UserID)
        {
            if (GroupID == 0 || UserID == 0) return -1;
            MySqlCommand comm = new MySqlCommand("Sproc_AddUserToGroup");
            try
            {
                comm.Parameters.AddWithValue("@" + "GroupID", GroupID);
                comm.Parameters.AddWithValue("@" + User.db_ID, UserID);

                return AddObject(comm, "@" + GroupUser.db_ID);
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
                comm.Parameters.AddWithValue("@" + Group.db_UserName, obj.UserName);
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

        internal static int DeleteGroupByID(int ID)
        {
            MySqlCommand comm = new MySqlCommand("delete_GroupByID");
            int retInt = 0;
            try
            {
                comm.Parameters.AddWithValue("@" + Group.db_ID, ID);
                comm.Connection = new MySqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                retInt = comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retInt;
        }
        ///<summary>
        /// Check if username exists in the database
        /// </summary>
        /// <remarks></remarks>
        internal static int CheckGroupExists(string username)
        {

            MySqlCommand comm = new MySqlCommand("CheckGroupUsernameExists");
            if (username != null)
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

        ///<summary>
        /// Checks if user exists in the database
        /// Before adding user to the group
        /// </summary>
        /// <remarks></remarks>
        internal static int CheckUserExistsByEmail(string email)
        {

            MySqlCommand comm = new MySqlCommand("sproc_CheckUserByEmail");
            if (email != null)
                try
                {
                    comm.Parameters.AddWithValue("@" + User.db_EmailAddress, email);
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
            catch (Exception ex)
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


        #endregion


    }
}
