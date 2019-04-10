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

        #region DB Connection String Information
        //Database information for the hosting website db
        private static string ReadOnlyConnectionString = "Server=MYSQL7003.site4now.net;Database=db_a458d6_shreelv;Uid=a458d6_shreelv;Pwd=elvish123;";

       
        private static string EditOnlyConnectionString = "Server=MYSQL7003.site4now.net;Database=db_a458d6_shreelv;Uid=a458d6_shreelv;Pwd=elvish123;";

        //private static string ReadOnlyConnectionString = "Server=localhost;Database=web_masters;Uid=root;Pwd=;";
        //private static string EditOnlyConnectionString = "Server=localhost;Database=web_masters;Uid=root;Pwd=;";
        #endregion
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
        /// Attempts to add user in the database
        /// Reference: PeerVal Project
        /// </summary>
        /// <remarks></remarks>

        internal static int AddUser(User obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_AddUser");
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
                comm.Parameters.AddWithValue("@" + User.db_DirectoryPath, obj.DirectoryPath);
                return AddObject(comm, "@" + User.db_ID);
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

        ///<summary>
        /// Check if username exists in the database
        /// </summary>
        /// <remarks></remarks>
        internal static int CheckUserExists(string username)
        {
            if (username == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_CheckUserName");
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_UserName, username);
                int dr = GetIntReader(comm);

                return dr;
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

        #region Assignment

        /// <summary>
        /// Attempts to add user in the database
        /// Reference: PeerVal Project
        /// </summary>
        /// <remarks></remarks>

        internal static int AddAssignment(Assignment obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_AssignmentAdd");
            try
            {
                comm.Parameters.AddWithValue("@" + Assignment.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Assignment.db_Feedback, obj.Feedback);
                comm.Parameters.AddWithValue("@" + Assignment.db_UserID, obj.UserID);
                return AddObject(comm, "@" + Assignment.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
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
                comm.Parameters.AddWithValue(User.db_ID, userID );
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
        /// Attempts to delete the database entry corresponding to the Role
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

        internal static Course GetCourse(int courseID)
        {
            MySqlCommand comm = new MySqlCommand("sproc_CourseGet");
            Course retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Course.db_ID, courseID);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new Course(dr);
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

        internal static User GetUser(int userID)
        {
            MySqlCommand comm = new MySqlCommand("sproc_UserGet");
            User retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_ID, userID);
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
        #endregion

        #region CourseSemester
        /// <summary>
        /// Gets the Classweb.CourseSemester corresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

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
        /// Attempts to the database entry corresponding to the given Section
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateCourseSemester(CourseSemester obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_CourseSectionObject");
            try
            {
                comm.Parameters.AddWithValue("@" + CourseSemester.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + CourseSemester.db_CourseID, obj.CourseID);
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


        #endregion

    }
}

