using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Vacation
{
    public partial class ChangePasswordForm : MaterialForm
    {
        DBControllerSet.PasswordDBController pwdctr;
        public ChangePasswordForm()
        {
            InitializeComponent();
            pwdctr = new DBControllerSet.PasswordDBController();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("用户名或密码不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
                return;
            }
            if (textBox1.Text.Length >= 18 || textBox2.Text.Length >= 18) 
            { 
                MessageBox.Show("用户名或密码太长！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return; 
            }
            pwdctr.ChangePassword(textBox1.Text, textBox2.Text);
            MessageBox.Show("修改成功！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Close();
            this.Dispose();
        }
    }
}
