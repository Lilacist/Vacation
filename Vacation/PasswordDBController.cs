using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vacation
{
    partial class DBControllerSet
    {
        public class PasswordDBController : BaseDBController
        {
            #region 私有部分
            private static readonly string dbnamestring = "Password.sdb";
            private static readonly string tablestring = "CREATE TABLE IF NOT EXISTS USERANDPWD(USER VARCHAR(20), PWD VARCHAR(20))";
            public PasswordDBController() : base(dbnamestring, tablestring) { }
            private bool DbIfExist()
            {
                string sql = "SELECT * FROM USERANDPWD";
                return BaseIfExist(sql);
            }
            private bool DbIfExist(string username, string password)
            {
                string sql = "SELECT * FROM USERANDPWD WHERE USER = '" + username + "' AND PWD = '" + password + "'";
                return BaseIfExist(sql);
            }
            private void DbNewUser(string username, string password)
            {
                string sql = "INSERT INTO USERANDPWD (USER, PWD) values ('" + username + "', '" + password + "')";
                BaseExecuteWithoutReturnValue(sql);
            }
            private void DbChangePassword(string newusername, string newpassword)
            {
                string sql = "DELETE FROM USERANDPWD";
                BaseExecuteWithoutReturnValue(sql);
                NewUser(newusername,newpassword);
                return;
            }

            #endregion

            #region 公有接口
            public bool ChangePassword(string newusername,string newpassword)
            {
                if (newusername.Length >= 18 || newpassword.Length >= 18) { MessageBox.Show("用户名或密码太长！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                else DbChangePassword(newusername, newpassword);
                return true;
            }
            public bool NewUser(string username, string password)
            {
                if (DbIfExist(username, password)) { MessageBox.Show("用户名已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                else if (username.Length >= 18 || password.Length >= 18) { MessageBox.Show("用户名或密码太长！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
                else DbNewUser(username, password);
                return true;
            }
            public bool IfCurrent(string username, string password)
            {
                if (DbIfExist(username, password)) return true;
                else return false;
            }
            public bool IfEmpty()
            {
                if (DbIfExist()) return false;
                else return true;
            }
            #endregion
        }
    }
}
