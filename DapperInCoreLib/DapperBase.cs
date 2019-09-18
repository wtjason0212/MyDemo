using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DapperInCoreLib
{
    public class DapperBase : IDapper
    {
        private IDbConnection _conn;
        private string _connectionString;


        //public DapperBase(IDbConnection conn)
        public DapperBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection IDbConnection
        {
            get
            {
                return _conn = new SqlConnection(_connectionString);
                       
            }
        }

        public List<T> GetList<T>(string sqlString, object param = null, CommandType? commandType = CommandType.Text, int? commandTimeout = 100)
        {
            List<T> list = new List<T>();
            using (DbConnection db = _conn as DbConnection)
            {
                list = db.Query<T>(sqlString, param, null, true, commandTimeout, commandType).AsList();
            }

            return list;
        }

        public bool Insert(string sqlString, object param = null, CommandType? commandType = CommandType.Text, int? commandTimeout = 100)
        {
            throw new NotImplementedException();
        }
    }
}
