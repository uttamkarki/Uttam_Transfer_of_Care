using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Plasmoid.Extensions;

namespace Uttam_Transfer_Of_Care
{
    using System.Runtime.InteropServices;

    public partial class StartSimButtonForm : Form
    {
        public StartSimButtonForm()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 200, 200));
        }

        private void StartSimButtonForm_Load(object sender, EventArgs e)
        {
            //Treatment_label_box.Text = AI_sim.MessageLabel;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
 

        //[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        //private static extern IntPtr CreateRoundRectRgn
        //(
        //    int nLeftRect,     // x-coordinate of upper-left corner
        //    int nTopRect,      // y-coordinate of upper-left corner
        //    int nRightRect,    // x-coordinate of lower-right corner
        //    int nBottomRect,   // y-coordinate of lower-right corner
        //    int nWidthEllipse, // height of ellipse
        //    int nHeightEllipse // width of ellipse
        //);

        #region old code
        private void Treatment_label_box_Click(object sender, EventArgs e)
        {

        }
        private void Message_form_Load(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
