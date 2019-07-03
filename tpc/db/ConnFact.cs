using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace tpc.db
{
    public class ConnFact : Chloe.Infrastructure.IDbConnectionFactory
    {
        string _connString = null;
        public ConnFact(string connString)
        {
            _connString = connString;
        }

        public IDbConnection CreateConnection()
        {
            var conn = new SqlConnection(_connString);
            return conn;
        }
    }
}
