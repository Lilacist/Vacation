using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;
using System.Threading;

namespace Vacation
{
    class BaseDBController
    {
        #region 私有
        private static LilacSQLiteController SQLiteController;
        private static string DBNameString;
        private static string DBTableString;
        private void CloseDB_ERR()
        {
            try
            {
                SQLiteController.CloseController();
                SQLiteController = null;
                return;
            }
            catch (Exception err)
            {
                _ = MessageBox.Show(err.Message);
                return;
            }
        }
        private void FInitDB()
        {
            try
            {
                SQLiteController = new LilacSQLiteController(DBNameString);
                if ((SQLiteController == null) || !SQLiteController.IfConnectionSuccess())
                {
                    CloseDB_ERR();
                    SQLiteController = null;
                    StackTrace trace = new StackTrace(1, true);
                    StackFrame frame = trace.GetFrame(0);
                    throw new Exception("LilacSQLiteController初始化失败！\r\n" + frame.GetFileName().ToString() + "\r\n" + frame.GetFileLineNumber().ToString() + "\r\n" + frame.GetMethod());
                }
                SQLiteController.ExecuteWithoutReturnValue(DBTableString);
            }
            catch (Exception err)
            {
                _ = MessageBox.Show(err.Message);
                return;
            }
        }
        private bool CloseDB()
        {
            if (SQLiteController == null) return true;
            try
            {
                bool _ = SQLiteController.CloseController();
                SQLiteController = null;
                return _;
            }
            catch (Exception err)
            {
                _ = MessageBox.Show(err.Message);
                return false;
            }
        }
        private void InitDB()
        {
            try
            {
                SQLiteController = new LilacSQLiteController(DBNameString);
                if ((SQLiteController == null) || !SQLiteController.IfConnectionSuccess())
                {
                    CloseDB_ERR();
                    SQLiteController = null;
                    StackTrace trace = new StackTrace(1, true);
                    StackFrame frame = trace.GetFrame(0);
                    throw new Exception("LilacSQLiteController初始化失败！\r\n" + frame.GetFileName().ToString() + "\r\n" + frame.GetFileLineNumber().ToString() + "\r\n" + frame.GetMethod());
                }
            }
            catch (Exception err)
            {
                _ = MessageBox.Show(err.Message);
                return;
            }
        }
        public BaseDBController(string inDBName,string inTableSQL)
        {
                DBNameString = inDBName;
                DBTableString = inTableSQL;
                FInitDB();
                CloseDB();
        }
        #endregion

        #region 保护接口
        
        protected void BaseExecuteWithoutReturnValue(string sql)
        {
            try
            {
                InitDB();
                SQLiteController.ExecuteWithoutReturnValue(sql);
                CloseDB();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                return;
            }
        }
        protected void BaseExecuteWithRefTable(string sql,ref DataTable table)
        {
            try
            {
                InitDB();
                SQLiteController.ExecuteWithRefTable(sql,ref table);
                CloseDB();
                return;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                return;
            }
        }
        protected bool BaseIfExist(string sql)
        {
            try
            {
                InitDB();
                SQLiteDataReader reader = SQLiteController.ExecuteWithReturnDataReader(sql);
                bool _ = reader.HasRows;
                CloseDB();
                return _;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                return false;
            }
        }
        #endregion
    }
}
