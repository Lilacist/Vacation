using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DistanceUpdateTool
{
    partial class DBControllerSet
    {
        public class DistanceDBController : BaseDBController
        {
            #region 私有部分
            private static readonly string dbnamestring = "Distance.sdb";
            private static readonly string tablestring = "CREATE TABLE IF NOT EXISTS DISTANCE(START VARCHAR(20), STOP VARCHAR(20), DIS VARCHAR(20))";
            public DistanceDBController() : base(dbnamestring, tablestring) { }
            private bool DbIfExist()
            {
                string sql = "SELECT * FROM DISTANCE";
                return BaseIfExist(sql);
            }
            private bool DbIfExist(string start, string stop)
            {
                string sql = "SELECT * FROM DISTANCE WHERE START = '" + start + "' AND STOP = '" + stop + "'";
                return BaseIfExist(sql);
            }
            private void DbNewDis(string start, string stop,string dis)
            {
                string sql = "INSERT INTO DISTANCE (START, STOP, DIS) values ('" + start + "', '" + stop + "', '" + dis + "')";
                BaseExecuteWithoutReturnValue(sql);
            }
            private string DbGetDis(string start, string stop)
            {
                string sql = "SELECT * FROM DISTANCE WHERE START = '" + start + "' AND STOP = '" + stop + "'";
                DataTable dt = new DataTable();
                BaseExecuteWithRefTable(sql,ref dt);
                return dt.Rows[0][2].ToString();
            }
            private int getsum()
            {
                string sql = "SELECT count(*) FROM DISTANCE";
                DataTable dt = new DataTable();
                BaseExecuteWithRefTable(sql, ref dt);
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            private void DbClear()
            {
                string sql = "DELETE FROM DISTANCE";
                BaseExecuteWithoutReturnValue(sql);
                return;
            }
            private void AllReturnDataTable(ref DataTable table)
            {
                string sql = "SELECT * FROM DISTANCE";
                BaseExecuteWithRefTable(sql, ref table);
            }
            #endregion
            #region 公有接口
            public bool NewDis(string start, string stop, string dis)
            {
                if (DbIfExist(start, stop)) { return true; }
                DbNewDis(start, stop, dis);
                return true;
            }
            public void GetTableAll(ref DataTable table)
            {
                AllReturnDataTable(ref table);
            }
            public bool Clear()
            {
                DbClear();
                return true;
            }
            public int Getsum()
            {
                return getsum();
            }
            public bool IfExist(string start, string stop)
            {
                if (DbIfExist(start, stop)) return true;
                else return false;
            }
            public bool IfEmpty()
            {
                if (DbIfExist()) return false;
                else return true;
            }
            public string GetDis(string start, string stop)
            {
                if (!DbIfExist(start, stop)) return "数据不存在！";
                else return DbGetDis(start, stop);
            }
            
            #endregion
        }
    }
}
