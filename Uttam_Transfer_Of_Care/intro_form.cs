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
        }

        private void aisimstartbutton_Click_1(object sender, EventArgs e)
        {
            var standardSimclick = new AI_sim();
            ai_on = true;
            standardSimclick.Show();
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
    }
}
