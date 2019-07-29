using System;
using System.Data;
using System.Data.SQLite;
using Chloe.Infrastructure;


namespace tpc.db
{
    public class SQLiteConnectionFactory : IDbConnectionFactory
    {
        string _connString = null;
        public SQLiteConnectionFactory(string connString)
        {
            this._connString = connString;
        }
        public IDbConnection CreateConnection()
        {
            SQLiteConnection conn = new SQLiteConnection(this._connString);
            return conn;
        }
    }
}