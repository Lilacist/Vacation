using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Security.Permissions;
using System.Data;
using System.Text.RegularExpressions;
namespace DistanceUpdateTool
{
    //打开、关闭。获取状态、执行更改
    class LilacSQLiteController
    {
        #region 私有变量
        private static string ItemDbName;
        private static string BasePath;
        private bool consuccess;
        private static SQLiteConnection m_SqlConnection;
        private bool ConnectionCLosed;
        #endregion
        #region 私有函数
        private void Lilac_CreateNewDatabase()
        {
            try
            {
                if (!Directory.Exists(BasePath))
                    Directory.CreateDirectory(BasePath);
                SQLiteConnection.CreateFile(BasePath + ItemDbName);
            }
            catch (Exception err)
            {
                _ = MessageBox.Show(err.ToString());
                return;
            }
        }
        private void Lilac_ConnectToDatabase()
        {
            try
            {
                m_SqlConnection = new SQLiteConnection("Data Source=" + BasePath + ItemDbName + ";Pooling=true;FailIfMissing=false;");
                m_SqlConnection.Open();
                return;
            }
            catch (Exception err)
            {
                consuccess = false;
                _ = MessageBox.Show(err.ToString());
                return;
            }
        }
        #endregion
        #region 构造函数
        public LilacSQLiteController(string dbName)
        {
            ItemDbName = dbName;
            consuccess = true;
            BasePath = Application.StartupPath+"\\DataBase\\";
            ConnectionCLosed = false;
            try
            {
                if (File.Exists(BasePath + ItemDbName))
                {
                    Lilac_ConnectToDatabase();
                }
                else 
                { 
                    Lilac_CreateNewDatabase(); 
                    Lilac_ConnectToDatabase(); 
                }
            }
            catch (Exception err)
            {
                _ = MessageBox.Show(err.ToString());
                return;
            }
        }
        ~LilacSQLiteController()
        {
            if (ConnectionCLosed != true)
            {
                m_SqlConnection = null;
                //throw new Exception("LilacSQLiteController控制器销毁时数据库连接没有关闭！");
            }
        }
        #endregion
        #region AttachParameters(SQLiteCommand,commandText,object[] paramList)
        /// <summary>
        /// 增加参数到命令（自动判断类型）
        /// </summary>
        /// <param name="commandText">命令语句</param>
        /// <param name="paramList">object参数列表</param>
        /// <returns>返回SQLiteParameterCollection参数列表</returns>
        /// <remarks>Status experimental. Regex appears to be handling most issues. Note that parameter object array must be in same ///order as parameter names appear in SQL statement.</remarks>
        private static SQLiteParameterCollection AttachParameters(SQLiteCommand cmd, string commandText, params object[] paramList)
        {
            if (paramList == null || paramList.Length == 0) return null;
            SQLiteParameterCollection coll = cmd.Parameters;
            string parmString = commandText.Substring(commandText.IndexOf("@"));
            // pre-process the string so always at least 1 space after a comma.
            parmString = parmString.Replace(",", " ,");
            // get the named parameters into a match collection
            string pattern = @"(@)\S*(.*?)\b";
            Regex ex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection mc = ex.Matches(parmString);
            string[] paramNames = new string[mc.Count];
            int i = 0;
            foreach (Match m in mc)
            {
                paramNames[i] = m.Value;
                i++;
            }
            // now let's type the parameters
            int j = 0;
            Type t = null;
            foreach (object o in paramList)
            {
                t = o.GetType();
                SQLiteParameter parm = new SQLiteParameter();
                switch (t.ToString())
                {
                    case ("DBNull"):
                    case ("Char"):
                    case ("SByte"):
                    case ("UInt16"):
                    case ("UInt32"):
                    case ("UInt64"):
                        throw new SystemException("Invalid data type");
                    case ("System.String"):
                        parm.DbType = DbType.String;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (string)paramList[j];
                        coll.Add(parm);
                        break;
                    case ("System.Byte[]"):
                        parm.DbType = DbType.Binary;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (byte[])paramList[j];
                        coll.Add(parm);
                        break;
                    case ("System.Int32"):
                        parm.DbType = DbType.Int32;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (int)paramList[j];
                        coll.Add(parm);
                        break;
                    case ("System.Boolean"):
                        parm.DbType = DbType.Boolean;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (bool)paramList[j];
                        coll.Add(parm);
                        break;
                    case ("System.DateTime"):
                        parm.DbType = DbType.DateTime;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDateTime(paramList[j]);
                        coll.Add(parm);
                        break;
                    case ("System.Double"):
                        parm.DbType = DbType.Double;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDouble(paramList[j]);
                        coll.Add(parm);
                        break;
                    case ("System.Decimal"):
                        parm.DbType = DbType.Decimal;
                        parm.ParameterName = paramNames[j];
                        parm.Value = Convert.ToDecimal(paramList[j]);
                        break;
                    case ("System.Guid"):
                        parm.DbType = DbType.Guid;
                        parm.ParameterName = paramNames[j];
                        parm.Value = (System.Guid)(paramList[j]);
                        break;
                    case ("System.Object"):
                        parm.DbType = DbType.Object;
                        parm.ParameterName = paramNames[j];
                        parm.Value = paramList[j];
                        coll.Add(parm);
                        break;
                    default:
                        throw new SystemException("Value is of unknown data type");
                } // end switch
                j++;
            }
            return coll;
        }
        #endregion
        #region 公有接口
        public string GetCurrentDbName()
        {
            try
            {
                return ItemDbName.ToString();
            }
            catch (Exception err)
            {
                _ = MessageBox.Show(err.Message);
                return null;
            }
        }
        public string GetCurrentDbPath()
        {
            try
            {
                return String.Format(BasePath + ItemDbName).ToString();
            }
            catch (Exception err)
            {
                _ = MessageBox.Show(err.Message);
                return null;
            }
        }
        public bool CloseController() 
        {
            try 
            { 
                m_SqlConnection.Close();
                m_SqlConnection = null;
                ConnectionCLosed = true;
            }
            catch(Exception err)
            {
                _ = MessageBox.Show(err.ToString());
                return false;
            }
            return true;
        }
        public bool IfConnectionSuccess()
        {
            return consuccess;
        }
        public void ExecuteWithoutReturnValue(string SQLrun)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(SQLrun, m_SqlConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                _ = MessageBox.Show(err.Message);
                return;
            }
        }
        public SQLiteDataReader ExecuteWithReturnDataReader(string SQLrun)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(SQLrun, m_SqlConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception err)
            {
                _ = MessageBox.Show(err.Message);
                return null;
            }
        }
        public void ExecuteWithRefTable(string SQLrun,ref DataTable table)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(SQLrun, m_SqlConnection);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                adapter.Fill(table);
                return;
            }
            catch (Exception err)
            {
                _ = MessageBox.Show(err.Message);
                return;
            }
        }
        public int ExecuteMutliQuery(string commandText, DataTable dtData)
        {
            int res = 0;
            if (m_SqlConnection.State == ConnectionState.Closed)
                m_SqlConnection.Open();
            using (SQLiteTransaction dbTrans = m_SqlConnection.BeginTransaction())
            {
                try
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        res += ExecuteNonQuery(dbTrans, commandText, row.ItemArray);
                    }
                    dbTrans.Commit();
                }
                catch (Exception ex)
                {
                    res = -1;
                    dbTrans.Rollback();
                    throw ex;
                }
            }
            return res;
        }
        ///调用方法
        public int ExecuteNonQuery(SQLiteTransaction transaction, string commandText, params object[] paramList)
        {
            if (transaction == null) throw new ArgumentNullException("transaction is null");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rolled back or committed,please provide an open transaction.", "transaction");
            using (IDbCommand cmd = transaction.Connection.CreateCommand())
            {
                cmd.CommandText = commandText;
                AttachParameters((SQLiteCommand)cmd, cmd.CommandText, paramList);
                if (transaction.Connection.State == ConnectionState.Closed)
                    transaction.Connection.Open();
                int result = cmd.ExecuteNonQuery();
                return result;
            }
        }
        #endregion
    }
}
