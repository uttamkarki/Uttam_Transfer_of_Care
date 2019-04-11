using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;
using Plasmoid.Extensions;

namespace Uttam_Transfer_Of_Care
{
    public partial class intro_form : Form
    {
        public static bool ai_on = true;
        public static string Participant_role = "enter role";
        public static string Participant_experience = "enter experience (in years)";
        public static string Participant_ID = "null";

        public intro_form()
        {
            InitializeComponent();
        }

        private void intro_form_Load(object sender, EventArgs e)
        {
            Experience_entry.Text = Participant_experience;
            Role_entry.Text = Participant_role;
            Action_dummy.Enabled = false;
            draw_msgbox_inst();
        }


        async void draw_msgbox_inst()
        {


            System.Drawing.Graphics g = this.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            await Task.Delay(50);

            g.FillRoundedRectangle(new SolidBrush(Color.FromName("AliceBlue")), 200,1500, 400, 200, 100);
            g.DrawRoundedRectangle(new Pen(Color.FromName("turquoise"), 15f), 200, 1500, 400, 200, 100);

            await Task.Delay(50);

            message_label.BringToFront();

            g.FillRoundedRectangle(new SolidBrush(Color.FromName("AliceBlue")), 200, 1500, 400, 200, 100);
            g.DrawRoundedRectangle(new Pen(Color.FromName("turquoise"), 15f), 200, 1500, 400, 200, 100);

            await Task.Delay(50);

            message_label.BringToFront();


        }

        private void aisimstartbutton_Click_1(object sender, EventArgs e)
        {
            if (Role_entry.Text == "enter role" || Role_entry.Text == "")
            {
                MessageBox.Show("please enter a role");
            }
            else if (Experience_entry.Text == "enter experience (in years)" || Role_entry.Text == "")
            {
                MessageBox.Show("please enter experience");
            }
            else if (Participant_ID_box.Text == "")
            {
                MessageBox.Show("please enter ID");
            }
            else
            {
                var standardSimclick = new AI_sim();
                ai_on = true;
                standardSimclick.Show();
            }

        }

        private void standardsimstartbutton_Click_1(object sender, EventArgs e)
        {
            var standardSimclick = new AI_sim();
            ai_on = false;
            standardSimclick.Show();
        }

        private void Experience_entry_TextChanged_1(object sender, EventArgs e)
        {
            Participant_experience = Experience_entry.Text;

        }

        private void Role_entry_TextChanged(object sender, EventArgs e)
        {
            Participant_role = Role_entry.Text;
        }
        #region old button controls - not used 
        private void Experience_entry_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void Participant_ID_box_TextChanged(object sender, EventArgs e)
        {
            Participant_ID = Participant_ID_box.Text;
        }

        #region old events
        private void aisimstartbutton_Click(object sender, EventArgs e)
        {
            var standardSimclick = new AI_sim();
            ai_on = true;
            standardSimclick.Show();
        }
        #endregion

        private void Intro_label_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
