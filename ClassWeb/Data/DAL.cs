using System;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ClassWeb.Data;

namespace ClassWeb.Data
{
    public class DAL
    {
        private static string ReadOnlyConnectionString = "Server=localhost;Database=webmasters;Uid=root;Pwd=;";
        private static string EditOnlyConnectionString = "Server=localhost;Database=webmasters;Uid=root;Pwd=;";
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
#endregion
}