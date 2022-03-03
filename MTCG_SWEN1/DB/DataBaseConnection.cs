using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_SWEN1.DB
{
    class DataBaseConnection
    {
        public static IDbConnection Connect()
        {
            return new NpgsqlConnection("Host=localhost;Username=postgre;Password=postgre;Database=mctg_db");
        }
    }
}
