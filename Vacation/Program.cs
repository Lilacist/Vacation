using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Vacation
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            if (DateTime.Now.Date.Year != 2020 || DateTime.Now.Date.Month > 11)
            {
                MessageBox.Show("本软件是未完成版本，功能可能不完善!\r\n请联系开发者获取完整版本！", "软件过期", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PasswordForm passwordForm = new PasswordForm();
            passwordForm.ShowDialog();
            if (passwordForm.DialogResult == DialogResult.OK)
            {
                passwordForm.Dispose();
                GC.Collect();
                Application.Run(new MainForm());
            }
            else
            {
                passwordForm.Dispose();
                GC.Collect();
                Application.Exit();
            }
        }
    }
}
