using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.Common
{
    public class SqlHelper
    {
        public static readonly string DbConnectionString = ConfigurationManager.ConnectionStrings["ProjectFileConnection"].ToString();

        private static void PrepareCommand(OleDbConnection conn, OleDbCommand cmd, CommandType ct, string commandString, List<OleDbParameter> param)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandText = commandString;
            cmd.CommandType = ct;

            if (param == null)
                return;

            foreach (OleDbParameter pa in param)
            {
                cmd.Parameters.Add(pa);
            }
        }

        public static int ExcuteNonQuery(CommandType ct, string commandString, List<OleDbParameter> param)
        {
            OleDbCommand cmd = new OleDbCommand();
            using (OleDbConnection conn = new OleDbConnection(DbConnectionString))
            {
                PrepareCommand(conn, cmd, ct, commandString, param);
                var result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return result;
            }
        }

        public static int ExcuteNonQuery(string commandString)
        {
            using (OleDbConnection conn = new OleDbConnection(DbConnectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(commandString, conn);
                var result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return result;
            }
        }

        public static DataSet ExcuteDataSet(string query)
        {
            using (OleDbConnection conn = new OleDbConnection(DbConnectionString))
            {
                conn.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        public static DataSet ExcuteDataSet(CommandType ct, string query, List<OleDbParameter> param)
        {
            OleDbCommand cmd = new OleDbCommand();
            using (OleDbConnection conn = new OleDbConnection(DbConnectionString))
            {
                PrepareCommand(conn, cmd, ct, query, param);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        public static DataTable ExcuteDataTable(string query)
        {
            using (OleDbConnection conn = new OleDbConnection(DbConnectionString))
            {
                conn.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                return dt;
            }
        }

        public static DataTable ExcuteDataTable(CommandType ct, string query, List<OleDbParameter> param)
        {
            OleDbCommand cmd = new OleDbCommand();
            using (OleDbConnection conn = new OleDbConnection(DbConnectionString))
            {
                PrepareCommand(conn, cmd, ct, query, param);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                return dt;
            }
        }

    }
}
