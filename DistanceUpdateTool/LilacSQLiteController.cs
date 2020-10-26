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
        #endregion
    }
}
