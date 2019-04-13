using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Drawing.Drawing2D;
using Plasmoid.Extensions;

namespace Uttam_Transfer_Of_Care
{
    public partial class Output : Form
    {
        #region Variables defined in this form
        public static TimeSpan simtime;

        //public WindowSettings OwningWindowSettings { get; set; }

        //string conf_age = "age?";
        //string conf_hem = "bleed?";
        //string conf_airway = "air?";
        //string conf_breath = "breath?";
        //string conf_crit = "criticality?";
        //string conf_gender = "gender?";
        //string conf_consc = "conscious?";
        //string conf_heart = "circulation?";
        //string injury_location = "injuries to ";
        //string injury_type_f2 = "injury type?";
        string injury_type_and_location = "type and location?";
        //string criticality_description;
        public static int injury_count = 0;
        public static int crit_count = 0;
        public static int patient_condition_check;
        public static int total_transfer_seconds = 0;
        public static int hem_score = 0;
        public static int bre_score = 0;
        public static int cir_score = 0;
        public static int con_score = 0;
        public static int air_score = 0;
        public static int gen_score = 0;
        public static int age_score = 0;
        public static int loc_score = 0;
        public static int type_score = 0;
        public static int crit_score = 0;
        public static int time_score = 0;



        #endregion
        public Output()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.Text = String.Empty;
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void display_criticality_TextChanged(object sender, EventArgs e)
        {

        }

        private void Output_Load(object sender, EventArgs e)
        {
            TimeSpan simtime = inputform.endtime - AI_sim.starttime;

            Participant_experience_label.Text = intro_form.Participant_experience;
            Participatn_role_label.Text = intro_form.Participant_role;
            Participant_number_label.Text = intro_form.Participant_ID;

            #region calculate time taken in transfer of care 
            int simtime_seconds = simtime.Seconds;
            int simtime_minutes = simtime.Minutes;
            int simtime_minsecs = simtime_minutes * 60;
            int total_seconds = simtime_minsecs + simtime_seconds;
            string simtime_text = total_seconds.ToString();
            Output_transfer_time.Text = inputform.compound_time;
            total_transfer_seconds = inputform.input_counter;

            score_calc();

            #endregion


            #region injury location strings for description - front of body

            if (inputform.torso_on == true)
            {
                inputform.injury_location += "front of torso ";
                injury_count = injury_count + 1;
            }
            if (inputform.head_on == true)
            {
                inputform.injury_location += "front of head ";
                injury_count = injury_count + 1;
            }
            if (inputform.ms_on == true)
            {
                inputform.injury_location += "front midsection ";
                injury_count = injury_count + 1;
            }
            if (inputform.leftleg_on == true)
            {
                inputform.injury_location += "front of upper left leg ";
                injury_count = injury_count + 1;
            }
            if (inputform.rightleg_on == true)
            {
                inputform.injury_location += "front of upper right leg ";
                injury_count = injury_count + 1;
            }
            if (inputform.rightlowleg_on == true)
            {
                inputform.injury_location += "front of lower right leg ";
                injury_count = injury_count + 1;
            }
            if (inputform.leftlowleg_on == true)
            {
                inputform.injury_location += "front of lower left leg ";
                injury_count = injury_count + 1;
            }
            if (inputform.leftarm_on == true)
            {
                inputform.injury_location += "front of upper left arm ";
                injury_count = injury_count + 1;
            }
            if (inputform.rightarm_on == true)
            {
                inputform.injury_location += "front of upper right arm ";
                injury_count = injury_count + 1;
            }
            if (inputform.rightlowarm_on == true)
            {
                inputform.injury_location += "front of lower right arm ";
                injury_count = injury_count + 1;
            }
            if (inputform.leftlowarm_on == true)
            {
                inputform.injury_location += "front of lower left arm ";
                injury_count = injury_count + 1;
            }


            #endregion

            #region injury location strings for description - back of body

            if (inputform.backtorso_on == true)
            {
                inputform.injury_location += "back of torso ";
                injury_count = injury_count + 1;
            }
            if (inputform.backhead_on == true)
            {
                inputform.injury_location += "back of head ";
                injury_count = injury_count + 1;
            }
            if (inputform.backms_on == true)
            {
                inputform.injury_location += "back midsection ";
                injury_count = injury_count + 1;
            }
            if (inputform.backleftleg_on == true)
            {
                inputform.injury_location += "back of upper left leg ";
                injury_count = injury_count + 1;
            }
            if (inputform.backrightleg_on == true)
            {
                inputform.injury_location += "back of upper right leg ";
                injury_count = injury_count + 1;
            }
            if (inputform.backrightlowleg_on == true)
            {
                inputform.injury_location += "back of lower right leg ";
                injury_count = injury_count + 1;
            }
            if (inputform.backleftlowleg_on == true)
            {
                inputform.injury_location += "back of lower left leg ";
                injury_count = injury_count + 1;
            }
            if (inputform.backleftarm_on == true)
            {
                inputform.injury_location += "back of upper left arm ";
                injury_count = injury_count + 1;
            }
            if (inputform.backrightarm_on == true)
            {
                inputform.injury_location += "back of upper right arm ";
                injury_count = injury_count + 1;
            }
            if (inputform.backrightlowarm_on == true)
            {
                inputform.injury_location += "back of lower right arm ";
                injury_count = injury_count + 1;
            }
            if (inputform.backleftlowarm_on == true)
            {
                inputform.injury_location += "back of lower left arm ";
                injury_count = injury_count + 1;
            }


            #endregion



            //fill the output form 
            #region fill the actual patient status boxes with sim generated information

            #region set actual patient age
            Output_actual_patient_age.Text = Convert.ToString(AI_sim.age);
            #endregion

            Output_actual_patient_gender.Text = Convert.ToString(AI_sim.gender);
            Output_actual_patient_criticality.Text = AI_sim.criticality;
            Output_actual_patient_Injury_type.Text = Convert.ToString(AI_sim.wound_type);
            Output_actual_patient_Injury_location.Text = Convert.ToString(AI_sim.wound_location);

            #region set actual airway value
            if (AI_sim.airway == 0 && AI_sim.initial_airway == 0)
            {
                var actual_airway = 0;
                Output_actual_patient_airway.Text = Convert.ToString(AI_sim.airway_description);
            }
            else if (AI_sim.airway == 1 && AI_sim.initial_airway <= 1)
            {
                var actual_airway = 1;
                Output_actual_patient_airway.Text = Convert.ToString(AI_sim.airway_description);
            }
            else if (AI_sim.airway == 2 )
            {
                var actual_airway = 2;
                Output_actual_patient_airway.Text = Convert.ToString(AI_sim.airway_description);
            }
            else if (AI_sim.airway == 0 && AI_sim.initial_airway == 1)
            {
                var actual_airway = 3;
                Output_actual_patient_airway.Text = Convert.ToString(AI_sim.airway_description);
            }
            else if (AI_sim.airway == 1 && AI_sim.initial_airway == 2)
            {
                var actual_airway = 4;
                Output_actual_patient_airway.Text = Convert.ToString(AI_sim.airway_description);
            }
            else if (AI_sim.airway == 0 && AI_sim.initial_airway == 2)
            {
                var actual_airway = 5;
                Output_actual_patient_airway.Text = Convert.ToString(AI_sim.airway_description);
            }
            #endregion

            #region set actual circulation value
            if (AI_sim.circulation == 0 && AI_sim.initial_circulation == 0)
            {
                var actual_circulation = 0;
                Output_actual_patient_circulation.Text = Convert.ToString(AI_sim.circulation_description);
            }
            else if (AI_sim.circulation == 1 && AI_sim.initial_circulation <= 1)
            {
                var actual_circulation = 1;
                Output_actual_patient_circulation.Text = Convert.ToString(AI_sim.circulation_description);
            }
            else if (AI_sim.circulation == 2)
            {
                var actual_circulation = 2;
                Output_actual_patient_circulation.Text = Convert.ToString(AI_sim.circulation_description);
            }
            else if (AI_sim.circulation == 0 && AI_sim.initial_circulation == 1)
            {
                var actual_circulation = 3;
                Output_actual_patient_circulation.Text = Convert.ToString(AI_sim.circulation_description);
            }
            else if (AI_sim.circulation == 1 && AI_sim.initial_circulation == 2)
            {
                var actual_circulation = 4;
                Output_actual_patient_circulation.Text = Convert.ToString(AI_sim.circulation_description);
            }
            else if (AI_sim.circulation == 0 && AI_sim.initial_circulation == 2)
            {
                var actual_circulation = 5;
                Output_actual_patient_circulation.Text = Convert.ToString(AI_sim.circulation_description);
            }
            #endregion

            #region set actual breathing value
            if (AI_sim.breathing == 0 && AI_sim.initial_breathing == 0)
            {
                var actual_breathing = 0;
                Output_actual_patient_breathing.Text = Convert.ToString(AI_sim.breathing_description);
            }
            else if (AI_sim.breathing == 1 && AI_sim.initial_breathing <= 1)
            {
                var actual_breathing = 1;
                Output_actual_patient_breathing.Text = Convert.ToString(AI_sim.breathing_description);
            }
            else if (AI_sim.breathing == 2 )
            {
                var actual_breathing = 2;
                Output_actual_patient_breathing.Text = Convert.ToString(AI_sim.breathing_description);
            }
            else if (AI_sim.breathing == 0 && AI_sim.initial_breathing == 1)
            {
                var actual_breathing = 3;
                Output_actual_patient_breathing.Text = Convert.ToString(AI_sim.breathing_description);
            }
            else if (AI_sim.breathing == 1 && AI_sim.initial_breathing == 2)
            {
                var actual_breathing = 4;
                Output_actual_patient_breathing.Text = Convert.ToString(AI_sim.breathing_description);
            }
            else if (AI_sim.breathing == 0 && AI_sim.initial_breathing == 2)
            {
                var actual_breathing = 5;
                Output_actual_patient_breathing.Text = Convert.ToString(AI_sim.breathing_description);
            }
            #endregion

            #region set actual consciousness value
            if (AI_sim.consciousness == 0 && AI_sim.initial_consciousness == 0)
            {
                var actual_consciousness = 0;
                Output_actual_patient_Consciousness.Text = Convert.ToString(AI_sim.consciousness_description);
            }
            else if (AI_sim.consciousness == 1 && AI_sim.initial_consciousness <= 1)
            {
                var actual_consciousness = 1;
                Output_actual_patient_Consciousness.Text = Convert.ToString(AI_sim.consciousness_description);
            }
            else if (AI_sim.consciousness == 2)
            {
                var actual_consciousness = 2;
                Output_actual_patient_Consciousness.Text = Convert.ToString(AI_sim.consciousness_description);
            }
            else if (AI_sim.consciousness == 0 && AI_sim.initial_consciousness == 1)
            {
                var actual_consciousness = 3;
                Output_actual_patient_Consciousness.Text = Convert.ToString(AI_sim.consciousness_description);
            }
            else if (AI_sim.consciousness == 1 && AI_sim.initial_consciousness == 2)
            {
                var actual_consciousness = 4;
                Output_actual_patient_Consciousness.Text = Convert.ToString(AI_sim.consciousness_description);
            }
            else if (AI_sim.consciousness == 0 && AI_sim.initial_consciousness == 2)
            {
                var actual_consciousness = 5;
                Output_actual_patient_Consciousness.Text = Convert.ToString(AI_sim.consciousness_description);
            }
            #endregion

            #region set actual hemorrhage value
            if (AI_sim.hemorrhage == 0 && AI_sim.initial_hemorrhage == 0)
            {
                var actual_hemorrhage = 0;
                Output_actual_patient_haemorrhage.Text = Convert.ToString(AI_sim.hemorrhage_description);
            }
            else if (AI_sim.hemorrhage == 1 && AI_sim.initial_hemorrhage <= 1)
            {
                var actual_hemorrhage = 1;
                Output_actual_patient_haemorrhage.Text = Convert.ToString(AI_sim.hemorrhage_description);
            }
            else if (AI_sim.hemorrhage == 2)
            {
                var actual_hemorrhage = 2;
                Output_actual_patient_haemorrhage.Text = Convert.ToString(AI_sim.hemorrhage_description);
            }
            else if (AI_sim.hemorrhage == 0 && AI_sim.initial_hemorrhage == 1)
            {
                var actual_hemorrhage = 3;
                Output_actual_patient_haemorrhage.Text = Convert.ToString(AI_sim.hemorrhage_description);
            }
            else if (AI_sim.hemorrhage == 1 && AI_sim.initial_hemorrhage == 2)
            {
                var actual_hemorrhage = 4;
                Output_actual_patient_haemorrhage.Text = Convert.ToString(AI_sim.hemorrhage_description);
            }
            else if (AI_sim.hemorrhage == 0 && AI_sim.initial_hemorrhage == 2)
            {
                var actual_hemorrhage = 5;
                Output_actual_patient_haemorrhage.Text = Convert.ToString(AI_sim.hemorrhage_description);
            }
            #endregion

            #endregion

            #region fill out the transferred data boxes with info from ToC input form

            Output_transferred_patient_age.Text = inputform.conf_age;
            Output_transferred_patient_gender.Text = inputform.conf_gender;
            Output_transferred_patient_haemorrhage.Text = inputform.conf_hem;
            Output_transferred_patient_breathing.Text = inputform.conf_breath;
            Output_transferred_patient_consciousness.Text = inputform.conf_consc;
            Output_transferred_patient_circulation.Text = inputform.conf_heart;
            Output_transferred_patient_injury_type.Text = inputform.injury_type_f2;
            Output_transferred_patient_airway.Text = inputform.conf_airway;
            Output_transferred_patient_criticality.Text = inputform.conf_crit;
            Output_transferred_patient_injury_location.Text = inputform.var_injury_location_text;

            #endregion


            injury_type_and_location = inputform.injury_type_f2 + inputform.injury_location;

            draw_edge();

        }
        #region change color of the transferred criticaliyt rating
        private void Output_transferred_patient_criticality_TextChanged(object sender, EventArgs e)
        {
            if (inputform.conf_crit == "critical")
            {
                Output_transferred_patient_criticality.ForeColor = Color.Red;
            }
            if (inputform.conf_crit == "priority")
            {
                Output_transferred_patient_criticality.ForeColor = Color.Orange;
            }
            if (inputform.conf_crit == "routine")
            {
                Output_transferred_patient_criticality.ForeColor = Color.Green;
            }
        }
        #endregion
        void score_calc()
        {

            #region calculate hemorrhage scores
            if (AI_sim.initial_hemorrhage == 0 && AI_sim.hemorrhage == 0 && inputform.var_hemorrage == 0)
            {
                hem_score = 5;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else if (AI_sim.initial_hemorrhage <= 1 && AI_sim.hemorrhage == 1 && inputform.var_hemorrage == 1)
            {
                hem_score = 5;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else if (AI_sim.hemorrhage == 2 && inputform.var_hemorrage == 2)
            {
                hem_score = 5;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else if (AI_sim.initial_hemorrhage == 1 && AI_sim.hemorrhage == 0 && inputform.var_hemorrage == 3)
            {
                hem_score = 5;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else if (AI_sim.initial_hemorrhage == 2 && AI_sim.hemorrhage == 1 && inputform.var_hemorrage == 4)
            {
                hem_score = 5;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else if (AI_sim.initial_hemorrhage == 2 && AI_sim.hemorrhage == 0 && inputform.var_hemorrage == 5)
            {
                hem_score = 5;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }


            else if (AI_sim.hemorrhage == 0 && inputform.var_hemorrage > 0) // Reported_patient_status worse than actual
            {
                hem_score = 4;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else if (AI_sim.hemorrhage == 1 && inputform.var_hemorrage == 2) // Reported_patient_status worse than actual
            {
                hem_score = 4;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else if (AI_sim.hemorrhage == 1 && inputform.var_hemorrage == 4) // initial status reported worse but actual is ok 
            {
                hem_score = 4;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else if (AI_sim.hemorrhage == 1 && AI_sim.initial_hemorrhage == 2 && inputform.var_hemorrage == 1) // initial status reported better but actual is ok 
            {
                hem_score = 3;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else if (AI_sim.hemorrhage == 1 && AI_sim.initial_hemorrhage == 0 && inputform.var_hemorrage == 1) // initial status reported worse and actual is ok 
            {
                hem_score = 4;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else if (AI_sim.hemorrhage == 1 && inputform.var_hemorrage == 0 || inputform.var_hemorrage == 3 || inputform.var_hemorrage == 5) // reported less bad than low actual
            {
                hem_score = 2;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else if (AI_sim.hemorrhage == 2 && (inputform.var_hemorrage ==0 || inputform.var_hemorrage == 3 || inputform.var_hemorrage == 5)) // reported 2 less than actual
            {
                hem_score = 0;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else if ( AI_sim.hemorrhage == 2 && (inputform.var_hemorrage == 1 || inputform.var_hemorrage == 4)) // reported 1 less than high actual
            {
                hem_score =1;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else if (AI_sim.hemorrhage == 2 && AI_sim.initial_hemorrhage > 2 && inputform.var_hemorrage == 2 ) // actual ok but initial one less
            {
                hem_score = 4;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            else
            {
                hem_score = 3;
                Output_Haemorrhage_score.Text = Convert.ToString(hem_score);
            }
            #endregion
            
            #region calculate circulation scores
            if (AI_sim.initial_circulation == 0 && AI_sim.circulation == 0 && inputform.var_circulation == 0)
            {
                cir_score = 5;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else if (AI_sim.initial_circulation <= 1 && AI_sim.circulation == 1 && inputform.var_circulation == 1)
            {
                cir_score = 5;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else if (AI_sim.circulation == 2 && inputform.var_circulation == 2)
            {
                cir_score = 5;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else if (AI_sim.initial_circulation == 1 && AI_sim.circulation == 0 && inputform.var_circulation == 3)
            {
                cir_score = 5;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else if (AI_sim.initial_circulation == 2 && AI_sim.circulation == 1 && inputform.var_circulation == 4)
            {
                cir_score = 5;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else if (AI_sim.initial_circulation == 2 && AI_sim.circulation == 0 && inputform.var_circulation == 5)
            {
                cir_score = 5;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }


            else if (AI_sim.circulation == 0 && inputform.var_circulation > 0) // Reported_patient_status worse than actual
            {
                cir_score = 4;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else if (AI_sim.circulation == 1 && inputform.var_circulation == 2) // Reported_patient_status worse than actual
            {
                cir_score = 4;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else if (AI_sim.circulation == 1 && inputform.var_circulation == 4) // initial status reported worse but actual is ok 
            {
                cir_score = 4;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else if (AI_sim.circulation == 1 && AI_sim.initial_circulation == 2 && inputform.var_circulation == 1) // initial status reported better but actual is ok 
            {
                cir_score = 3;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else if (AI_sim.circulation == 1 && AI_sim.initial_circulation == 0 && inputform.var_circulation == 1) // initial status reported worse and actual is ok 
            {
                cir_score = 4;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else if (AI_sim.circulation == 1 && inputform.var_circulation == 0 || inputform.var_circulation == 3 || inputform.var_circulation == 5) // reported less bad than low actual
            {
                cir_score = 2;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else if (AI_sim.circulation == 2 && (inputform.var_circulation == 0 || inputform.var_circulation == 3 || inputform.var_circulation == 5)) // reported 2 less than actual
            {
                cir_score = 0;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else if (AI_sim.circulation == 2 && (inputform.var_circulation == 1 || inputform.var_circulation == 4)) // reported 1 less than high actual
            {
                cir_score = 1;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else if (AI_sim.circulation == 2 && AI_sim.initial_circulation > 2 && inputform.var_circulation == 2) // actual ok but initial one less
            {
                cir_score = 4;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            else
            {
                cir_score = 3;
                Output_circulation_score.Text = Convert.ToString(cir_score);
            }
            #endregion

            #region calculate airway scores
            if (AI_sim.initial_airway == 0 && AI_sim.airway == 0 && inputform.var_airways == 0)
            {
                air_score = 5;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else if (AI_sim.initial_airway <= 1 && AI_sim.airway == 1 && inputform.var_airways == 1)
            {
                air_score = 5;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else if (AI_sim.airway == 2 && inputform.var_airways == 2)
            {
                air_score = 5;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else if (AI_sim.initial_airway == 1 && AI_sim.airway == 0 && inputform.var_airways == 3)
            {
                air_score = 5;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else if (AI_sim.initial_airway == 2 && AI_sim.airway == 1 && inputform.var_airways == 4)
            {
                air_score = 5;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else if (AI_sim.initial_airway == 2 && AI_sim.airway == 0 && inputform.var_airways == 5)
            {
                air_score = 5;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
           
            else if (AI_sim.airway == 0 && inputform.var_airways > 0) // Reported_patient_status worse than actual
            {
                air_score = 4;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else if (AI_sim.airway == 1 && inputform.var_airways == 2) // Reported_patient_status worse than actual
            {
                air_score = 4;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else if (AI_sim.airway == 1 && inputform.var_airways == 4) // initial status reported worse but actual is ok 
            {
                air_score = 4;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else if (AI_sim.airway == 1 && AI_sim.initial_airway == 2 && inputform.var_airways == 1) // initial status reported better but actual is ok 
            {
                air_score = 3;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else if (AI_sim.airway == 1 && AI_sim.initial_airway == 0 && inputform.var_airways == 1) // initial status reported worse and actual is ok 
            {
                air_score = 4;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else if (AI_sim.airway == 1 && inputform.var_airways == 0 || inputform.var_airways == 3 || inputform.var_airways == 5) // reported less bad than low actual
            {
                air_score = 2;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else if (AI_sim.airway == 2 && (inputform.var_airways == 0 || inputform.var_airways == 3 || inputform.var_airways == 5)) // reported 2 less than actual
            {
                air_score = 0;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else if (AI_sim.airway == 2 && (inputform.var_airways == 1 || inputform.var_airways == 4)) // reported 1 less than high actual
            {
                air_score = 1;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else if (AI_sim.airway == 2 && AI_sim.initial_airway > 2 && inputform.var_airways == 2) // actual ok but initial one less
            {
                air_score = 4;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            else
            {
                air_score = 3;
                Output_airway_score.Text = Convert.ToString(air_score);
            }
            #endregion

            #region calculate breathing scores
            if (AI_sim.initial_breathing == 0 && AI_sim.breathing == 0 && inputform.var_breathing == 0)
            {
                bre_score = 5;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else if (AI_sim.initial_breathing <= 1 && AI_sim.breathing == 1 && inputform.var_breathing == 1)
            {
                bre_score = 5;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else if (AI_sim.breathing == 2 && inputform.var_breathing == 2)
            {
                bre_score = 5;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else if (AI_sim.initial_breathing == 1 && AI_sim.breathing == 0 && inputform.var_breathing == 3)
            {
                bre_score = 5;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else if (AI_sim.initial_breathing == 2 && AI_sim.breathing == 1 && inputform.var_breathing == 4)
            {
                bre_score = 5;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else if (AI_sim.initial_breathing == 2 && AI_sim.breathing == 0 && inputform.var_breathing == 5)
            {
                bre_score = 5;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }


            else if (AI_sim.breathing == 0 && inputform.var_breathing > 0) // Reported_patient_status worse than actual
            {
                bre_score = 4;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else if (AI_sim.breathing == 1 && inputform.var_breathing == 2) // Reported_patient_status worse than actual
            {
                bre_score = 4;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else if (AI_sim.breathing == 1 && inputform.var_breathing == 4) // initial status reported worse but actual is ok 
            {
                bre_score = 4;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else if (AI_sim.breathing == 1 && AI_sim.initial_breathing == 2 && inputform.var_breathing == 1) // initial status reported better but actual is ok 
            {
                bre_score = 3;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else if (AI_sim.breathing == 1 && AI_sim.initial_breathing == 0 && inputform.var_breathing == 1) // initial status reported worse and actual is ok 
            {
                bre_score = 4;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else if (AI_sim.breathing == 1 && inputform.var_breathing == 0 || inputform.var_breathing == 3 || inputform.var_breathing == 5) // reported less bad than low actual
            {
                bre_score = 2;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else if (AI_sim.breathing == 2 && (inputform.var_breathing == 0 || inputform.var_breathing == 3 || inputform.var_breathing == 5)) // reported 2 less than actual
            {
                bre_score = 0;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else if (AI_sim.breathing == 2 && (inputform.var_breathing == 1 || inputform.var_breathing == 4)) // reported 1 less than high actual
            {
                bre_score = 1;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else if (AI_sim.breathing == 2 && AI_sim.initial_breathing > 2 && inputform.var_breathing == 2) // actual ok but initial one less
            {
                bre_score = 4;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            else
            {
                bre_score = 3;
                Output_breathing_score.Text = Convert.ToString(bre_score);
            }
            #endregion

            #region calculate consciousness scores
            if (AI_sim.initial_consciousness == 0 && AI_sim.consciousness == 0 && inputform.var_consciousness == 0)
            {
                con_score = 5;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else if (AI_sim.initial_consciousness <= 1 && AI_sim.consciousness == 1 && inputform.var_consciousness == 1)
            {
                con_score = 5;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else if (AI_sim.consciousness == 2 && inputform.var_consciousness == 2)
            {
                con_score = 5;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else if (AI_sim.initial_consciousness == 1 && AI_sim.consciousness == 0 && inputform.var_consciousness == 3)
            {
                con_score = 5;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else if (AI_sim.initial_consciousness == 2 && AI_sim.consciousness == 1 && inputform.var_consciousness == 4)
            {
                con_score = 5;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else if (AI_sim.initial_consciousness == 2 && AI_sim.consciousness == 0 && inputform.var_consciousness == 5)
            {
                con_score = 5;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }


            else if (AI_sim.consciousness == 0 && inputform.var_consciousness > 0) // Reported_patient_status worse than actual
            {
                con_score = 4;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else if (AI_sim.consciousness == 1 && inputform.var_consciousness == 2) // Reported_patient_status worse than actual
            {
                con_score = 4;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else if (AI_sim.consciousness == 1 && inputform.var_consciousness == 4) // initial status reported worse but actual is ok 
            {
                con_score = 4;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else if (AI_sim.consciousness == 1 && AI_sim.initial_consciousness == 2 && inputform.var_consciousness == 1) // initial status reported better but actual is ok 
            {
                con_score = 3;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else if (AI_sim.consciousness == 1 && AI_sim.initial_consciousness == 0 && inputform.var_consciousness == 1) // initial status reported worse and actual is ok 
            {
                con_score = 4;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else if (AI_sim.consciousness == 1 && inputform.var_consciousness == 0 || inputform.var_consciousness == 3 || inputform.var_consciousness == 5) // reported less bad than low actual
            {
                con_score = 2;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else if (AI_sim.consciousness == 2 && (inputform.var_consciousness == 0 || inputform.var_consciousness == 3 || inputform.var_consciousness == 5)) // reported 2 less than actual
            {
                con_score = 0;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else if (AI_sim.consciousness == 2 && (inputform.var_consciousness == 1 || inputform.var_consciousness == 4)) // reported 1 less than high actual
            {
                con_score = 1;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else if (AI_sim.consciousness == 2 && AI_sim.initial_consciousness > 2 && inputform.var_consciousness == 2) // actual ok but initial one less
            {
                con_score = 4;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            else
            {
                con_score = 3;
                Output_consciousness_score.Text = Convert.ToString(con_score);
            }
            #endregion


            #region gender scores
            if (inputform.conf_gender == AI_sim.gender)
            {
                gen_score = 5;
                output_gender_score.Text = Convert.ToString(gen_score);
            }
            else
            {
                age_score = 0;
                output_gender_score.Text = Convert.ToString(gen_score);
            }
            #endregion


            #region location scores
            if (inputform.var_injury_location_text == AI_sim.wound_location)
            {
                loc_score = 5;
                Output_injury_location_score.Text = Convert.ToString(loc_score);
            }
            else
            {
                loc_score = 0;
                Output_injury_location_score.Text = Convert.ToString(loc_score);
            }
            #endregion

            #region type scores
            if (inputform.var_injury_type == AI_sim.wound_type)
            {
                type_score = 5;
                Output_injury_type_score.Text = Convert.ToString(type_score);
            }
            else
            {
                type_score = 0;
                Output_injury_type_score.Text = Convert.ToString(type_score);
            }
            #endregion

            #region criticality scores
            if (inputform.conf_crit == AI_sim.criticality)
            {
                crit_score = 5;
                Output_criticality_score.Text = Convert.ToString(crit_score);
            }
            else if (inputform.conf_crit == "routine" && AI_sim.criticality == "critical")
            {
                crit_score = 0;
                Output_criticality_score.Text = Convert.ToString(crit_score);
            }
            else if (inputform.conf_crit == "priority" && AI_sim.criticality == "critical")
            {
                crit_score = 1;
                Output_criticality_score.Text = Convert.ToString(crit_score);
            }
            else if (inputform.conf_crit == "routine" && AI_sim.criticality == "priority")
            {
                crit_score = 2;
                Output_criticality_score.Text = Convert.ToString(crit_score);
            }
            else if (inputform.conf_crit == "critical" && AI_sim.criticality == "routine")
            {
                crit_score = 2;
                Output_criticality_score.Text = Convert.ToString(crit_score);
            }
            else if (inputform.conf_crit == "critical" && AI_sim.criticality == "priority")
            {
                crit_score = 4;
                Output_criticality_score.Text = Convert.ToString(crit_score);
            }
            else if (inputform.conf_crit == "priority" && AI_sim.criticality == "routine")
            {
                crit_score = 3;
                Output_criticality_score.Text = Convert.ToString(crit_score);
            }
            #endregion

            #region time scores
            if (inputform.input_endtime < 120)
            {
                time_score = 5;
                Output_time_score.Text = Convert.ToString(time_score);
            }
            else if (inputform.input_endtime < 150)
            {
                time_score = 4;
                Output_time_score.Text = Convert.ToString(time_score);
            }
            else if (inputform.input_endtime < 180)
            {
                time_score = 3;
                Output_time_score.Text = Convert.ToString(time_score);
            }
            else if (inputform.input_endtime < 210)
            {
                time_score = 2;
                Output_time_score.Text = Convert.ToString(time_score);
            }
            else if (inputform.input_endtime < 240)
            {
                time_score = 1;
                Output_time_score.Text = Convert.ToString(time_score);
            }
            else
            {
                time_score = 0;
                Output_time_score.Text = Convert.ToString(time_score);

            }
            #endregion




            #region age scores
            if (inputform.conf_age == AI_sim.age_group)
            {
                age_score = 5;
                Output_age_score.Text = Convert.ToString(age_score);
            }
            else if ((inputform.conf_age == "adult" || inputform.conf_age == "over 70") && (AI_sim.age_group == "adult" || AI_sim.age_group == "over 70"))
            {
                age_score = 4;
                Output_age_score.Text = Convert.ToString(age_score);
            }
            else if ((inputform.conf_age == "adult" || inputform.conf_age == "child") && (AI_sim.age_group == "adult" || AI_sim.age_group == "child"))
            {
                age_score = 3;
                Output_age_score.Text = Convert.ToString(age_score);
            }
            else if ((inputform.conf_age == "infant" || inputform.conf_age == "child") && (AI_sim.age_group == "infant" || AI_sim.age_group == "child"))
            {
                age_score = 2;
                Output_age_score.Text = Convert.ToString(age_score);
            }
            else if ((inputform.conf_age == "over 70" || inputform.conf_age == "child") && (AI_sim.age_group == "over 70" || AI_sim.age_group == "child"))
            {
                age_score = 1;
                Output_age_score.Text = Convert.ToString(age_score);
            }
            else
            {
                age_score = 0;
                Output_age_score.Text = Convert.ToString(age_score);
            }
            #endregion





            #region weighted scores

            Output_age_ws.Text = Convert.ToString(age_score * 2);
            output_gender_ws.Text = Convert.ToString(gen_score * 1);
            Output_injury_type_ws.Text = Convert.ToString(type_score * 3);
            Output_injury_location_ws.Text = Convert.ToString(loc_score * 3);
            Output_criticality_ws.Text = Convert.ToString(crit_score * 7);
            Output_circulation_ws.Text = Convert.ToString(cir_score * 5);
            Output_airway_ws.Text = Convert.ToString(air_score * 4);
            Output_hemorrhage_ws.Text = Convert.ToString(hem_score * 5);
            Output_consciousness_ws.Text = Convert.ToString(con_score * 3);
            Output_breathing_ws.Text = Convert.ToString(bre_score * 4);
            Output_time_ws.Text = Convert.ToString(time_score * 5);

            float total_score = ((time_score * 5) + (bre_score * 4) + (con_score * 3) + (hem_score * 5) + (air_score * 4) + (cir_score * 5) + (crit_score * 7) +
                (loc_score * 3) + (type_score * 3) + (gen_score * 1) + (age_score * 2));
            float score_percent = ((100f / 210f) * total_score);
            string score_string = (Convert.ToString(score_percent));
            string total_score_string = (Convert.ToString(total_score));
            string round_score = score_string.Substring(0, 2);

            Output_total_ws.Text = (round_score + "%");
            #endregion










        }









        async void draw_edge()
        {
            System.Drawing.Graphics g = this.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            await Task.Delay(50);

            g.FillRoundedRectangle(new SolidBrush(Color.FromName("AliceBlue")), 500, 200, 2000, 200, 50);
            g.DrawRoundedRectangle(new Pen(Color.FromName("turquoise"), 15f), 500, 200, 2000, 200, 50);
            await Task.Delay(50);

            g.FillRoundedRectangle(new SolidBrush(Color.FromName("AliceBlue")), 500, 200, 2000, 200, 50);
            g.DrawRoundedRectangle(new Pen(Color.FromName("turquoise"), 15f), 500, 200, 2000, 200, 50);
            await Task.Delay(50);

            participantID_label.BringToFront();
            Experience_label.BringToFront();
            Participant_experience_label.BringToFront();
            Role_label.BringToFront();
            Participant_number_label.BringToFront();
            Participatn_role_label.BringToFront();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.OpenForms["AI_sim"].Close();
            Application.OpenForms["inputform"].Close();

            var standardSimclick = new AI_sim();
            standardSimclick.Show();

            this.Close();
        }


        // UNUSED EVENTS
        #region Unused click events
        private void Output_transferred_patient_injury_location_TextChanged(object sender, EventArgs e)
        {

        }

        private void Output_actual_patient_age_TextChanged(object sender, EventArgs e)
        {

        }

        private void Output_total_ws_TextChanged(object sender, EventArgs e)
        {

        }
        private void Actual_status_label_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        #endregion



        private void close_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void Output_consciousness_weight_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }

}
