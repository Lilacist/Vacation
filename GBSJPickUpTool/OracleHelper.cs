using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Runtime.CompilerServices;
namespace GBSJPickUpTool
{
    class OracleHelper
    {
        static string connStr;
        public OracleHelper(string user,string pwd,string port,string sername,string seradd)
        {
            connStr = "User Id="+user+";Password="+pwd+ ";Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST="+seradd+ ")(PORT="+port+ ")))(CONNECT_DATA=(SERVICE_NAME="+sername+")))";
        }
        #region 执行SQL语句,返回受影响行数
        public int ExecuteNonQuery(string sql)
        {
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion
        #region 执行SQL语句,返回DataTable;只用来执行查询结果比较少的情况
        public void ExecuteDataTable(string sql,ref DataTable table)
        {
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    adapter.Fill(table);
                    return;
                }
            }
        }
        #endregion
        public bool TestTable()
        {
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                    return false;
                }
                return true;
            }
        }
    }
}
