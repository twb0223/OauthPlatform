using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Service
{
    public class DapperHelper
    {
        public static List<T> Select<T>(string sql, Object param)
        {
            using (IDbConnection conn = DataBaseHelper.DepperSqlserver())
            {
                List<T> datas = conn.Query<T>(sql, param).ToList<T>();
                return datas;
            }
        }

        public static T Find<T>(string sql, Object param)
        {
            using (IDbConnection conn = DataBaseHelper.DepperSqlserver())
            {
                T datas = conn.Query<T>(sql, param).SingleOrDefault<T>();
                return datas;
            }
        }

        public static int Count(string sql, Object param)
        {
            using (IDbConnection conn = DataBaseHelper.DepperSqlserver())
            {
                List<dynamic> datas = conn.Query(sql, param).ToList();
                return datas.ToArray().Length;
            }
        }

        public static int Update(string sql, object param)
        {
            using (IDbConnection conn = DataBaseHelper.DepperSqlserver())
            {
                return conn.Execute(sql, param);
            }
        }

        public static int Add(string sql, Object param)
        {
            using (IDbConnection conn = DataBaseHelper.DepperSqlserver())
            {
                return conn.Execute(sql, param);
            }
        }

        public static int Execute(String sql, Object param)
        {
            using (IDbConnection conn = DataBaseHelper.DepperSqlserver())
            {
                return conn.Execute(sql, param);
            }
        }
    }
}
