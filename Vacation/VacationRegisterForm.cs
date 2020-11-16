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
    public partial class VacationRegisterForm : MaterialForm
    {
        ///GBSJPickUpTool.DBControllerSet.BaseInformationDBController dBController = new GBSJPickUpTool.DBControllerSet.BaseInformationDBController();
        public VacationRegisterForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
