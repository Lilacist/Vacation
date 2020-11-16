using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GBSJPickUpTool
{
    public partial class OracleTest : Form
    {
        public string ServerAddress { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string ServerName { get; set; }
        public OracleTest()
        {
            InitializeComponent();
        }
        private void OracleTest_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text.ToString();
            string pwd = textBox2.Text.ToString();
            string port = textBox3.Text.ToString();
            string sername = textBox5.Text.ToString();
            string seradd = textBox4.Text.ToString();
            OracleHelper oracle = new OracleHelper(user, pwd, port, sername, seradd);
            if (oracle.TestTable())
            {
                User = user;
                Password = pwd;
                Port = port;
                ServerName = sername;
                ServerAddress = seradd;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                oracle = null;
                MessageBox.Show("连接失败！", "连接失败！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GC.Collect();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
