using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MySchoolAPI.Data.Repository
{
    /// <summary>
    /// Connection Database
    /// </summary>
    public class BaseRepository
    {

        private readonly string connectionString;


        public BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public IDbConnection GetConnection() 
        {
            return new SqlConnection(connectionString);
        }
    }
}
