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

        //private static string ReadOnlyConnectionString = "Server=localhost;Database=sapkgane;Uid=root;Pwd=";
        //private static string EditOnlyConnectionString = "Server=localhost;Database=sapkgane;Uid=root;Pwd=";

        private static string ReadOnlyConnectionString = "Server=localhost;Database=web_masters;Uid=root;Pwd=class";
        private static string EditOnlyConnectionString = "Server=localhost;Database=web_masters;Uid=root;Pwd=class";

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

        #region Role
        /// <summary>
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

        internal static List<Assignment> AssignmentsGetByID(int id)
        {
            MySqlCommand comm = new MySqlCommand("sproc_GetAssignmentsByUserID");
            List<Assignment> retList = new List<Assignment>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue(User.db_ID, id);
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

        internal static void GetAllAssignmentByUserName(string userName)
        {
            throw new NotImplementedException();
        }
        internal static Assignment GetAssignmentByNameLocationUserName(string name, string location, string userName)
        {
            Assignment retObj = null;
            MySqlCommand comm = new MySqlCommand("sproc_AssignmentGetByNameLocationUserName");
            try
            {
                comm.Parameters.AddWithValue("@" + Assignment.db_FileName, name);
                comm.Parameters.AddWithValue("@" + Assignment.db_Location, location);
                comm.Parameters.AddWithValue("@" + Assignment.db_UserName, userName);
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
        
        public static List<Assignment> GetAssignmentAllByFileName(string fileName)
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

      
        internal static int UpdateAssignment(Assignment obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_AssignmentResubmit");
            try
            {
                comm.Parameters.AddWithValue("@" + Assignment.db_Feedback, obj.Feedback);
                comm.Parameters.AddWithValue("@" + Assignment.db_ID, obj.ID);
                return UpdateObject(comm);
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

        #endregion

        #region user

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
        /// Attempts to delete the database entry corresponding to the User
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

        internal static int CheckUserExists(string userName)
        {
            throw new NotImplementedException();
        }


        #endregion

        //#region Courses
        /////<summary>
        ///// Create/Add Course in database
        ///// </summary>
        ///// <remarks></remarks>

        //internal static int CreateCourse(Course obj)
        //{
        //    if (obj == null) return -1;
        //    MySqlCommand comm = new MySqlCommand("sproc_CreateCourse");
        //    try
        //    {
        //        comm.Parameters.AddWithValue("@" + Course.db_CourseTitle, obj.CourseTitle);
        //        comm.Parameters.AddWithValue("@" + Course.db_CourseName, obj.CourseName);
        //        comm.Parameters.AddWithValue("@" + Course.db_ClassID, obj.ClassID);
        //        return AddObject(comm, "@" + Course.db_ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return -1;
        //}


        /////<summary>
        ///// Get All Courses from the database
        ///// Reference: PeerVal project by Professor
        ///// </summary>
        ///// <remarks></remarks>

        //internal static List<Course> GetAllCourses()
        //{
        //    List<Course> retObj = new List<Course>();
        //    MySqlCommand comm = new MySqlCommand("sproc_GetAllCourses");
        //    try
        //    {
        //        MySqlDataReader dr = GetDataReader(comm);
        //        while (dr.Read())
        //        {
        //            //
        //            retObj.Add(new Course(dr));
        //        }
        //        comm.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        comm.Connection.Close();
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return retObj;
        //}
        //internal static List<Course> GetCourse()
        //{

        //    MySqlCommand comm = new MySqlCommand("sproc_GetAllCourses");
        //    List<Course> retList = new List<Course>();
        //    try
        //    {
        //        comm.CommandType = System.Data.CommandType.StoredProcedure;
        //        MySqlDataReader dr = GetDataReader(comm);
        //        while (dr.Read())
        //        {
        //            retList.Add(new Course(dr));
        //        }
        //        comm.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        comm.Connection.Close();
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return retList;
        //}
        /////<summary>
        /////Edit Course 
        ///// Reference: PeerVal project by Professor
        ///// </summary>
        ///// <remarks></remarks>
        //internal static int UpdateCourse(Course obj)
        //{
        //    if (obj == null) return -1;
        //    MySqlCommand comm = new MySqlCommand("sproc_EditCourse");
        //    try
        //    {
        //        comm.Parameters.AddWithValue("@" + Course.db_ID, obj.ID);
        //        comm.Parameters.AddWithValue("@" + Course.db_CourseTitle, obj.CourseTitle);
        //        comm.Parameters.AddWithValue("@" + Course.db_CourseName, obj.CourseName);
        //        return UpdateObject(comm);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return -1;
        //}

        /////<summary>
        /////Delete Course by Id from the database
        ///// Reference: PeerVal project by Professor
        ///// </summary>
        ///// <remarks></remarks>

        //internal static int DeleteCourseByID(int ID)
        //{
        //    MySqlCommand comm = new MySqlCommand("sproc_DeleteCourseByID");
        //    int retInt = 0;
        //    try
        //    {
        //        comm.Parameters.AddWithValue("@" + Course.db_ID, ID);
        //        comm.Connection = new MySqlConnection(EditOnlyConnectionString);
        //        comm.CommandType = System.Data.CommandType.StoredProcedure;
        //        comm.Connection.Open();
        //        MySqlParameter retParameter;
        //        retParameter = comm.Parameters.Add("@" + Course.db_ID, MySqlDbType.Int32);
        //        retParameter.Direction = System.Data.ParameterDirection.Output;
        //        comm.ExecuteNonQuery();
        //        retInt = (int)retParameter.Value;
        //        comm.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        comm.Connection.Close();
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return retInt;
        //}
        //#endregion

        //#region Class
        //internal static List<Class> ClassGetAll()
        //{
        //    List<Class> retObj = new List<Class>();
        //    MySqlCommand comm = new MySqlCommand("sproc_ClassGetAll");
        //    try
        //    {
        //        MySqlDataReader dr = GetDataReader(comm);
        //        while (dr.Read())
        //        {
        //            //
        //            retObj.Add(new Class(dr));
        //        }
        //        comm.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        comm.Connection.Close();
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return retObj;
        //}
        //internal static List<Class> GetClass()
        //{

        //    MySqlCommand comm = new MySqlCommand("sproc_ClassesGetAll");
        //    List<Class> retList = new List<Class>();
        //    try
        //    {
        //        comm.CommandType = System.Data.CommandType.StoredProcedure;
        //        MySqlDataReader dr = GetDataReader(comm);
        //        while (dr.Read())
        //        {
        //            retList.Add(new Class(dr));
        //        }
        //        comm.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        comm.Connection.Close();
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return retList;
        //}

        //#endregion

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
            MySqlCommand comm = new MySqlCommand("add_Group");
            try
            {
                // generate new password first.
                obj.Salt = Tools.Hasher.GenerateSalt(50);
                string newPass = Tools.Hasher.Get(obj.Password, obj.Salt, _Pepper, _Stretches, 64);
                obj.Password = newPass;
                // now set object to Database.

                comm.Parameters.AddWithValue("g_" + Group.db_Name, obj.Name);
                comm.Parameters.AddWithValue("g_" + Group.db_EmailAddress, obj.EmailAddress);
                comm.Parameters.AddWithValue("g_" + Group.db_UserName, obj.UserName);
                comm.Parameters.AddWithValue("g_" + Group.db_Password, obj.Password);
                comm.Parameters.AddWithValue("g_" + Group.db_Salt, obj.Salt);
                return AddObject(comm, "g_ID");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        internal static int AddUserToGroup(User obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("add_UserToGroup");
            try
            {

                comm.Parameters.AddWithValue("@" + User.db_ID, obj.ID);

                return AddObject(comm, "@" + Group.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }
        internal static int RemoveUserFromGroup(int userID)
        {
            if (userID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "remove_UserFromGroup";
                comm.Parameters.AddWithValue("@" + Group.db_ID, userID);
                return UpdateObject(comm);
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
        internal static int UpdateGroupPassword(Group obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("update_GroupPassword");
            try
            {
                string newPass = Tools.Hasher.Get(obj.Password, obj.Salt, _Pepper, _Stretches, 64);
                comm.Parameters.AddWithValue("@" + Group.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Group.db_Password, newPass);
              
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

            MySqlCommand comm = new MySqlCommand("CheckUserName_Group");
            if (username == null)
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

        internal static int CheckUserExistsInGroup(int userID)
        {

            MySqlCommand comm = new MySqlCommand("CheckUserExistsInAGroup");
            if (userID == 0)
                try
                {
                    comm.Parameters.AddWithValue("@" + Group.db_ID, userID);
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
        //#region Class
        ///// <summary>
        ///// Attempts to add classes in the database
        ///// Reference: PeerVal Project
        ///// </summary>
        ///// <remarks></remarks>
        //internal static int AddClass(Class obj)
        //{
        //    if (obj == null) return -1;
        //    MySqlCommand comm = new MySqlCommand("sproc_ClassAdd");
        //    try
        //    {
        //        // now set object to Database.
        //        comm.Parameters.AddWithValue("@" + Class.db_ID, obj.ID);
        //        comm.Parameters.AddWithValue("@" + Class.db_Title, obj.Title);
        //        comm.Parameters.AddWithValue("@" + Class.db_IsAvailable, obj.IsAvailable);
        //        comm.Parameters.AddWithValue("@" + Class.db_DateStart, obj.DateStart);
        //        comm.Parameters.AddWithValue("@" + Class.db_DateEnd, obj.DateEnd);
        //        comm.Parameters.AddWithValue("@" + Class.db_SectionID, obj.SectionID);

        //        return AddObject(comm, "@" + Class.db_ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return -1;
        //}
        //internal static int DeleteClassByID(int ID)
        //{
        //    MySqlCommand comm = new MySqlCommand("sproc_ClassDeleteByID");
        //    int retInt = 0;
        //    try
        //    {
        //        comm.Parameters.AddWithValue("@" + Class.db_ID, ID);
        //        comm.Connection = new MySqlConnection(EditOnlyConnectionString);
        //        comm.CommandType = System.Data.CommandType.StoredProcedure;
        //        comm.Connection.Open();
        //        retInt = comm.ExecuteNonQuery();
        //        comm.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        comm.Connection.Close();
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return retInt;
        //}

        //internal static int UpdateClass(Class obj)
        //{
        //    if (obj == null) return -1;
        //    MySqlCommand comm = new MySqlCommand("sproc_ClassUpdate");
        //    try
        //    {
        //        comm.Parameters.AddWithValue("@" + Class.db_ID, obj.ID);
        //        comm.Parameters.AddWithValue("@" + Class.db_Title, obj.Title);
        //        comm.Parameters.AddWithValue("@" + Class.db_IsAvailable, obj.IsAvailable);
        //        comm.Parameters.AddWithValue("@" + Class.db_DateStart, obj.DateStart);
        //        comm.Parameters.AddWithValue("@" + Class.db_DateEnd, obj.DateEnd);
        //        comm.Parameters.AddWithValue("@" + Class.db_SectionID, obj.SectionID);
        //        return UpdateObject(comm);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return -1;
        //}
        ///// <summary>
        ///// Attempts to delete the database entry corresponding to the User
        ///// </summary>
        ///// <remarks></remarks>
        //internal static int RemoveClass(Class obj)
        //{
        //    if (obj == null) return -1;
        //    MySqlCommand comm = new MySqlCommand("sproc_ClassRemove");
        //    try
        //    {
        //        comm.Parameters.AddWithValue("@" + Class.db_ID, obj.ID);
        //        return UpdateObject(comm);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return -1;
        //}

        //internal static Class ClassGetByID(int? id)
        //{
        //    MySqlCommand comm = new MySqlCommand("sproc_ClassGetByID");
        //    Class retObj = null;
        //    try
        //    {
        //        comm.Parameters.AddWithValue("@" + Class.db_ID, id);
        //        MySqlDataReader dr = GetDataReader(comm);

        //        while (dr.Read())
        //        {
        //            retObj = new Class(dr);
        //        }
        //        comm.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        comm.Connection.Close();
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return retObj;
        //}
        /////<summary>
        ///// Get salt of the User from the database corresponding to the title
        ///// </summary>
        ///// <remarks></remarks>

        //public static string GetSaltForClass(string title)
        //{
        //    String salt = "";
        //    MySqlCommand comm = new MySqlCommand("sproc_GetSaltForClass");
        //    try
        //    {
        //        comm.Parameters.AddWithValue("@" + Class.db_Title, title);
        //        MySqlDataReader dr = GetDataReader(comm);
        //        while (dr.Read())
        //        {
        //            salt = dr.GetString(0);
        //        }
        //        comm.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        comm.Connection.Close();
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return salt;
        //}

        ///<summary>
        /// Set salt of the User from the database corresponding to the ID
        /// </summary>
        /// <remarks></remarks>
        //internal static int SetSaltForClass(int ClassID, string salt)
        //{
        //    if (ClassID == 0 || salt == null) return -1;
        //    MySqlCommand comm = new MySqlCommand("sproc_SetSaltForClass");
        //    try
        //    {
        //        comm.Parameters.AddWithValue("@" + User.db_ID, ClassID);
        //        //comm.Parameters.AddWithValue("@" + User.db_Salt, salt);
        //        return UpdateObject(comm);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    return -1;
        //}
        //#endregion
    }
}