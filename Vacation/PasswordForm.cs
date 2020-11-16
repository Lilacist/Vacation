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
    public partial class PasswordForm : MaterialForm
    {
        DBControllerSet.PasswordDBController pwdctr;
        public PasswordForm()
        {
            InitializeComponent();
            pwdctr = new DBControllerSet.PasswordDBController();
            if (pwdctr.IfEmpty()) pwdctr.NewUser("123456", "123456");
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
            if (pwdctr.IfCurrent(textBox1.Text, textBox2.Text))
            {
                MessageBox.Show("验证成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.DialogResult = DialogResult.OK;
                return; 
            }
            else
            { 
                MessageBox.Show("用户名或密码错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
                return; 
            }
        }
        private void PasswordForm_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
#if DEBUG
            this.DialogResult = DialogResult.OK;
            return;
#endif
        }
    }
}
