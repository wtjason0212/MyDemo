using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DapperInCoreLib
{
    public class DapperFactory
    {
        private static DapperFactory _instance;
        private IDapper _dapper;
        private IConfiguration _configuration;
        private DapperFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static DapperFactory GetInstance(IConfiguration configuration)
        {
            if (null == _instance)
            {
                _instance = new DapperFactory(configuration);
            }

            return _instance;
        }

        public IDapper GetDapper()
        {
            if (null == _dapper)
            {
                var connectionString = string.Empty;
                var connectionTypeString = string.Empty;
                // ConnectionType connType = ConnectionType.SqlServer;
                //if (null != _configuration.GetSection(CONNECTION_STRING) && null != _configuration.GetSection(CONNECTION_STRING).GetSection(STUDENT_CONNECTION_STRING))
                //{
                //    connectionString = _configuration.GetSection(CONNECTION_STRING).GetSection(STUDENT_CONNECTION_STRING).Value;
                //}
                //if (null != _configuration.GetSection(CONNECTION_TYPE))
                //{
                //    connectionTypeString = _configuration.GetSection(CONNECTION_TYPE).Value;
                //}
                //if (!string.IsNullOrWhiteSpace(connectionTypeString))
                //{
                //    connType = connectionTypeString.ConvertFromString<ConnectionType>();
                //}
                connectionString = _configuration.GetConnectionString("DbConnection");
                IDbConnection conn = new SqlConnection(connectionString);
                //_dapper = new DapperBase(conn);
                _dapper = new DapperBase(connectionString);
            }

            return _dapper;
        }
    }
}
