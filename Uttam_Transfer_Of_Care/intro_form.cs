using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Uttam_Transfer_Of_Care
{
    public partial class intro_form : Form
    {
        public intro_form()
        {
            InitializeComponent();
        }

        //private void InitializeComponent()
        //{
        //  throw new NotImplementedException();
        //}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void intro_form_Load(object sender, EventArgs e)
        {

        }

        private void standardsimstartbutton_Click(object sender, EventArgs e)
        {

        }

        private void aisimstartbutton_Click(object sender, EventArgs e)
        {
            var standardSimclick = new AI_sim();
            standardSimclick.Show();

        }

        private void Experience_entry_TextChanged(object sender, EventArgs e)
        {

        }

        private void aisimstartbutton_Click_1(object sender, EventArgs e)
        {
            var standardSimclick = new AI_sim();
            standardSimclick.Show();
        }

        private void Role_entry_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
