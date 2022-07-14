using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToursimManagementSystem
{
    public static class AppSettings
    {
        public static string ConnectionString()
        {
            string constring = "Server=localhost;Database=tourism_management;Uid=root;Pwd=''";

            return constring;
        }
    }
}
