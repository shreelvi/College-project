using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ClassWeb.Models;

namespace ClassWeb.Tools {
    /// <summary>
    /// This class is to make dealing with Session in ASP Core easier
    /// </summary>
    /// <remarks>
    /// Used: https://stackoverflow.com/questions/29841503/json-serialization-deserialization-in-asp-net-core
    /// https://stackoverflow.com/questions/38571032/how-to-get-httpcontext-current-in-asp-net-core
    /// https://visualstudiomagazine.com/articles/2018/12/01/working-with-session.aspx
    /// </remarks>
    public static class SessionHelper
    {
        private static HttpContext _context;
        public static object Get(HttpContext context, string key)
        {
            _context = context;
            string sObject = context.Session.GetString(key);
            return JsonConvert.DeserializeObject(sObject);
        }
        public static void Set(HttpContext context, string key, object obj)
        {
            //String jsonString = Microsoft.AspNetCore.Mvc.Formatters.Json;
            //String jsonString = System.Runtime.Serialization.Json;
            //String jsonString = Microsoft.Extensions.Configuration.Json ;
            String jsonString = JsonConvert.SerializeObject(obj);
            context.Session.SetString(key, jsonString);
        }

        internal static bool UserLoggedIn
        {
            get
            {
                return Get(_context, "CurrentUser") != null;
            }

        }

        internal static User CurrentUser
        {
            get
            {
                return (User)Get(_context, "CurrentUser");
            }
            set
            {
                Set(_context, "CurrentUser", value);
            }


        }
    }
}
