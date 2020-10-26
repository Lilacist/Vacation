using System;
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
using MaterialSkin;
using MaterialSkin.Controls;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Vacation
{
    public partial class MainForm : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private readonly ColorScheme LightColorScheme;
        private readonly ColorScheme DarkColorScheme;
        UiSwitchButtonMethods us;
        private Thread Th_progress = null;
        public MainForm()
        {
            InitializeComponent();
            SetProgressBar(0);
            us = new UiSwitchButtonMethods();
            us.InitializeSwitchEffect(this.tabPage6, new Point(65, 1), 1, 2, 2, Switch1_CheckedChanged);
            LightColorScheme = new ColorScheme(Primary.Blue700, Primary.Blue900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            DarkColorScheme = new ColorScheme(Primary.BlueGrey600, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Blue400, TextShade.WHITE);
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = LightColorScheme;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void SetProgressBar(int value)
        {
            try
            {
                Th_progress = new Thread(() =>
                {
                        this.CrossThreadCalls(() =>
                        {
                            progressBar1.Value = value;
                        });
                    return;
                });
                Th_progress.Start();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                return;
            }
        }
        private void Switch1_CheckedChanged(object sender, EventArgs e)
        {
            GC.Collect();
            materialSkinManager.Theme = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.LIGHT : MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = materialSkinManager.ColorScheme == DarkColorScheme ? LightColorScheme : DarkColorScheme;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            GC.Collect();
            PasswordForm passwordForm = new PasswordForm();
            passwordForm.ShowDialog();
            if (passwordForm.DialogResult == DialogResult.OK)
            {
                passwordForm.Dispose();
                GC.Collect();
                ChangePasswordForm changePassword = new ChangePasswordForm();
                changePassword.Show();
                return;
            }
            else if (passwordForm.DialogResult == DialogResult.No)
            {
                MessageBox.Show("用户名或密码错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (passwordForm.DialogResult == DialogResult.Cancel)
            {
                passwordForm.Dispose();
                GC.Collect();
                return;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SetProgressBar(60);
            Thread Temp = null;
            SetProgressBar(65);
            try
            {
                Temp = new Thread(() =>
                {
                    this.CrossThreadCalls(() =>
                    {
                        GC.Collect();
                        DistanceForm distanceform = new DistanceForm();
                        distanceform.Show();
                        this.CrossThreadCalls( () => { SetProgressBar(100); });
                        distanceform.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.materialTabControl1_SelectedIndexChanged);
                    });
                    return;
                });
                SetProgressBar(90);
                Temp.Start();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                return;
            }
        }

        private void materialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetProgressBar(0);
            GC.Collect();
        }

        private void button9_MouseDown(object sender, MouseEventArgs e)
        {
            SetProgressBar(50);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SetProgressBar(60);
            Thread Temp = null;
            SetProgressBar(65);
            try
            {
                Temp = new Thread(() =>
                {
                    this.CrossThreadCalls(() =>
                    {
                        GC.Collect();
                        VacationRegisterForm registerForm = new VacationRegisterForm();
                        this.CrossThreadCalls(() => { SetProgressBar(100); });
                        registerForm.Show();
                        registerForm.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.materialTabControl1_SelectedIndexChanged);
                    });
                    return;
                });
                SetProgressBar(90);
                Temp.Start();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                return;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            bool pass = false;
            string BaseDBPath = "";
            string savepath = Application.StartupPath + "\\DataBase";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "请选择文件";
            fileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fileDialog.Filter = "(*.sdb)|*.sdb";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                BaseDBPath = fileDialog.FileName;
                pass = true;
            }
            else
            {
                MessageBox.Show("没有选择文件", "未选择", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (pass)
            {
                DirectoryInfo destDirectory = new DirectoryInfo(savepath);
                string fileName = Path.GetFileName(BaseDBPath);
                if (fileName != "Distance.sdb") { MessageBox.Show("数据库文件不正确！"); return; }
                if (!File.Exists(BaseDBPath))
                {
                    return;
                }
                if (!destDirectory.Exists)
                {
                    destDirectory.Create();
                }
                string destPathname = destDirectory.FullName + @"\" + fileName;
                if (File.Exists(destPathname))
                {
                    if (MessageBox.Show("将会覆盖原本的文件", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        File.Copy(BaseDBPath, destPathname, true);
                    }
                    else
                    {
                        MessageBox.Show("拷贝已取消！"); return;
                    }
                }
                MessageBox.Show("文件已保存", "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            bool pass = false;
            string BaseDBPath = "";
            string savepath = Application.StartupPath + "\\DataBase";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "请选择文件";
            fileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fileDialog.Filter = "(*.sdb)|*.sdb";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                BaseDBPath = fileDialog.FileName;
                pass = true;
            }
            else
            {
                MessageBox.Show("没有选择文件", "未选择", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (pass)
            {
                DirectoryInfo destDirectory = new DirectoryInfo(savepath);
                string fileName = Path.GetFileName(BaseDBPath);
                if (fileName != "BaseInfo.sdb") { MessageBox.Show("数据库文件不正确！"); return; }
                if (!File.Exists(BaseDBPath))
                {
                    return;
                }
                if (!destDirectory.Exists)
                {
                    destDirectory.Create();
                }
                string destPathname = destDirectory.FullName + @"\" + fileName;
                if (File.Exists(destPathname))
                {
                    if (MessageBox.Show("将会覆盖原本的文件", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        File.Copy(BaseDBPath, destPathname, true);
                    }
                    else
                    {
                        MessageBox.Show("拷贝已取消！"); return;
                    }
                }
                MessageBox.Show("文件已保存", "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            bool pass = false;
            string BaseDBPath = "";
            string savepath = Application.StartupPath + "\\DataBase";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "请选择文件";
            fileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fileDialog.Filter = "(*.sdb)|*.sdb";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                BaseDBPath = fileDialog.FileName;
                pass = true;
            }
            else
            {
                MessageBox.Show("没有选择文件", "未选择", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (pass)
            {
                DirectoryInfo destDirectory = new DirectoryInfo(savepath);
                string fileName = Path.GetFileName(BaseDBPath);
                if (fileName != "BYBaseInfo.sdb") { MessageBox.Show("数据库文件不正确！"); return; }
                if (!File.Exists(BaseDBPath))
                {
                    return;
                }
                if (!destDirectory.Exists)
                {
                    destDirectory.Create();
                }
                string destPathname = destDirectory.FullName + @"\" + fileName;
                if (File.Exists(destPathname))
                {
                    if (MessageBox.Show("将会覆盖原本的文件", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        File.Copy(BaseDBPath, destPathname, true);
                    }
                    else
                    {
                        MessageBox.Show("拷贝已取消！"); return;
                    }
                }
                MessageBox.Show("文件已保存", "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
