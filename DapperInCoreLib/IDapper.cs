using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DapperInCoreLib
{
    public interface IDapper
    {
        List<T> GetList<T>(string sqlString, object param = null, CommandType? commandType = CommandType.Text, int? commandTimeout = 100);

        bool Insert(string sqlString, object param = null, CommandType? commandType = CommandType.Text, int? commandTimeout = 100);
    }
}
