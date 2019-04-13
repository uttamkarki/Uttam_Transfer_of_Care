using System;
using System.Diagnostics;
using System.Drawing;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using Plasmoid.Extensions;

namespace Uttam_Transfer_Of_Care
{
    delegate void message_fPtoEMS(Message msg);
    delegate void message_fEMStoMax(Message msg);
    delegate void message_fMaxtoEMS(Message msg);
    delegate void message_fEMStoP(Message msg);

    public partial class AI_sim : Form
    {
        #region define public variables here

        public static DateTime starttime;
        public delegate void DELEGATE();
        public string from { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string state { get; set; }
        public string action { get; set; }
        public string treatment_info_to_transfer { get; set; }
        public static Boolean AI_assist = true;
        public static int hem = 0;
        public static int air = 0;
        public static int con = 0;
        public static int cir = 0;
        public static int hem1_count = 0;
        public static int hem2_count = 0;
        public static int bre = 0;
        public static int conc2_count = 0;
        public static int conc1_count = 0;
        public static int air1_count = 0;
        public static int air2_count = 0;
        public static int airway1_count = 0;
        public static int airway2_count = 0;
        public static int breath1_count = 0;
        public static int breath2_count = 0;
        public static int circ1_count = 0;
        public static int circ2_count = 0;
        public static int cpr2_count = 0;
        public static int total_success_count = 0;
        public static int total_partial_success_count = 0;
        public static int total_unsuccess_count = 0;
        public static string age_group = "unknown";
        public static string wound_location;
        public static int wound_loc_ID;
        public static int wound_type_ID;
        public static string wound_type;
        public static string breathing_description = "no breathing problem";
        public static string airway_description = "airway clear";
        public static string hemorrhage_description = "no bleeding";
        public static string consciousness_description = "fully conscious";
        public static string circulation_description = "no heart or circulation problem";
        public static string MessageLabel = "null";
        public string predicted_hem;
        public string predicted_con;
        public string predicted_air;
        public string predicted_bre;
        public string predicted_cir;
        public string predicted_time;
        public string hemorrhage_count;
        public string consciousness_count;
        public string airway_count;
        public string breathing_count;
        public string circulation_count;
        public static string var_age = "25";
        public static string var_gender = "male";
        public static string var_hem = "2";
        public static string var_air = "0";
        public static string var_conc = "1";
        public static string var_breath = "0";
        public static string var_circ = "0";
        public static string var_injury = "Gunshot wound";
        public static string var_injury_location = "lower left leg";
        public static string criticality = "critical";

        // treatment improvement probabilities
        public static int breath1to0_cure_probability = 50;
        public static int breath1_successprob_reduction = 5;
        public static int breath2to0_cure_probability = 20;
        public static int breath2to1_cure_probability = 50;
        public static int hem1to0_cure_probability = 90;
        public static int hem2to0_cure_probability = 10;
        public static int hem2to1_cure_probability = 60;
        public static int conc1to0_cure_probability = 50;
        public static int conc1to1_cure_probability = 50;
        public static int conc2to0_cure_probability = 20;
        public static int conc2to1_cure_probability = 30;
        public static int conc2to2_cure_probability = 50;
        public static int circ1to0_cure_probability = 50; // the probability that circulation treatment from level 1 to 0 is successful
        public static int circ2to0_cure_probability = 10; // the probability that circulation treatment from level 2 to 0 is successful
        public static int circ2to1_cure_probability = 20; // the probability that circulation treatment from level 2 to 1 is successful
        public static int cpr2to0_cure_probability = 5; // the probability that circulation treatment from level 2 to 0 is successful
        public static int cpr2to1_cure_probability = 10; // the probability that circulation treatment from level 2 to 1 is successful
        public static int airway_1to0_cure_probability = 50; // the probability that airway treatment from level 1 to 0 is successful
        public static int airway_2to0_cure_probability = 33; // the probability that airway treatment from level 2 to 0 is successful
        public static int airway_2to1_cure_probability = 33; // the probability that airway treatment from level 2 to 1 is successful

        //probabilty that the state will not change
        public static int hem_deg_prob_longtime = 80;
        public static int hem_deg_prob_shorttime = 95;
        public static int hem_deg_prob;
        public static int hem_deg_prob0to1;
        public static int hem_deg_prob0to2;
        public static int hem_deg_prob1to2;
        public static int hem_deg_prob0to0;
        public static int hem_deg_prob1to1;
        //probability that the state will change

        public static int circ0to1if1_deg_prob_longtime = 50;
        public static int circ0to1if1_deg_prob_shorttime = 40;
        public static int circ0to2if1_deg_prob_longtime = 25;
        public static int circ0to2if1_deg_prob_shorttime = 15;
        public static int circ1to2if1_deg_prob_longtime = 40;
        public static int circ1to2if1_deg_prob_shorttime = 25;
        public static int circ0to1if2_deg_prob_longtime = 60;
        public static int circ0to1if2_deg_prob_shorttime = 50;
        public static int circ0to2if2_deg_prob_longtime = 35;
        public static int circ0to2if2_deg_prob_shorttime = 25;
        public static int circ1to2if2_deg_prob_longtime = 60;
        public static int circ1to2if2_deg_prob_shorttime = 40;

        public static int air_deg_prob_longtime = 80;
        public static int air_deg_prob_shorttime = 95;
        public static int conc_deg_prob_longtime = 50;
        public static int conc_deg_prob_shorttime = 75;
        public static int conc_imp_prob_longtime = 50;
        public static int conc_imp_prob_shorttime = 25;
        public static int breath_deg_prob = 0;
        public static int breath_deg_prob0to0 = 0;
        public static int breath_deg_prob0to1 = 0;
        public static int breath_deg_prob0to2 = 0;
        public static int breath_deg_prob1to2 = 0;
        public static int breath_deg_prob1to1 = 0;
        public static int breath_deg_prob_shorttime = 95;
        public static int patient_instability_score = 0;
        // probability of changing state
        public static int hemcon1to2_deg_prob_shorttime = 25;
        public static int hemcon1to2_deg_prob_longtime = 95;
        public static int hemcon0to2_deg_prob_shorttime = 25;
        public static int hemcon0to1_deg_prob_shorttime = 25;
        public static int hemcon0to1_deg_prob_longtime = 40;
        public static int hemcon0to2_deg_prob_longtime = 40;
        public static int hemcirc1to2_deg_prob_shorttime = 25;
        public static int hemcirc1to2_deg_prob_longtime = 95;
        public static int hemcirc0to2_deg_prob_shorttime = 25;
        public static int hemcirc0to1_deg_prob_shorttime = 25;
        public static int hemcirc0to1_deg_prob_longtime = 40;
        public static int hemcirc0to2_deg_prob_longtime = 40;
        public static int brecon1to2_deg_prob_shorttime = 25;
        public static int brecon1to2_deg_prob_longtime = 95;
        public static int brecon0to2_deg_prob_shorttime = 25;
        public static int brecon0to1_deg_prob_shorttime = 25;
        public static int brecon0to1_deg_prob_longtime = 40;
        public static int brecon0to2_deg_prob_longtime = 40;

        public static int airway_close_prob1to2 = 10;
        public static int airway_close_prob0to1 = 10;
        public static int patient_air_instability_score = 0;
        public static int breair_prob0to2 = 45;
        public static int breair_prob1to2 = 90;
        public static int breair_prob0to1 = 45;
        public static int bre_prob0to2if0 = 0;
        public static int bre_prob1to2if0 = 0;
        public static int bre_prob0to1if0 = 0;
        public static int bre_prob0to2if1 = 5;
        public static int bre_prob1to2if1 = 10;
        public static int bre_prob0to1if1 = 10;
        public static int bre_prob0to2if2 = 15;
        public static int bre_prob1to2if2 = 25;
        public static int bre_prob0to1if2 = 25;

        public static int patient_bre_instability_score = 0;

        public static int major_heart_attack_probability;

        //Degradation instability variables
        public static int patient_hem_instability_score;
        public static int patient_con_instability_score;
        public static int patient_circ_instability_score;

        public static int mistake_count = 0;
        // define variables for filling the patient condition text
        public static int UICounter = 0;
        public static string HemLevel = "null";
        public static string BreLevel = "null";
        public static string ConLevel = "null";
        public static string CirLevel = "null";
        public static string AirLevel = "null";
        public static string linktxt1 = "null";
        public static string linktxt2 = "null";
        public static string Woundtxt = "null";

        //success probability
        public static float overall_success_prob = 100;


        #endregion


        #region timing variables - define the thread pause variables here
        int Patient_Assign_to_Run_Pause_time = 5;
        Stopwatch stopwatch_hem = new Stopwatch();
        Stopwatch stopwatch_con = new Stopwatch();
        Stopwatch stopwatch_air = new Stopwatch();
        Stopwatch stopwatch_bre = new Stopwatch();
        Stopwatch stopwatch_cir = new Stopwatch();
        Stopwatch stopwatch_sim = new Stopwatch();
        public static System.Windows.Forms.Timer patient_deg_timer;
        public static System.Windows.Forms.Timer hemorrhage_Timer;
        public static double hem_seconds = 0;
        public static double air_seconds = 0;
        public static double breath_seconds = 0;
        public static double conc_seconds = 0;
        public static double circ_seconds = 0;
        public static int Patient_deg_interval;
        //initiate timer here 
        System.Windows.Forms.Timer timer1;
        public static int counter = 120;
        public static int main_minutes = 2;
        public static int main_seconds = 00;
        string string_seconds;
        string string_minutes = "2";
        string string_seconds1 = "0";
        string string_seconds2 = "0";

        #endregion

        public AI_sim()
        {
            InitializeComponent();
            start_button.Location = new Point(1250, 200);
            start_button.Visible = true;
            this.ControlBox = false;
            this.Text = String.Empty;

            Application.OpenForms["intro_form"].Hide();
        }

        #region On load - Set AI assist value, clear the patient transfer button and make disabled!
        private void AI_sim_Load(object sender, EventArgs e)
        {
            if (intro_form.ai_on == true)
            {
                AI_assist = true;
            }
            else
            {
                AI_assist = false;
            }
            AI_patient_transfer_button.Enabled = false;
            AI_patient_transfer_button.Visible = false;
            AI_patient_transfer_button.Location = new Point(1250, 200);
        }
        #endregion

        // Click start Simulation button - Initiates environmental controller and subsequently the agents - starts a Patient Agent thread and EMS agent thread
        #region start simulation button click
        private void button1_Click_2(object sender, EventArgs e)
        {
            start_button.Enabled = false;
            start_button.Visible = false;

            #region initiate characteristics stopwatches and start sim stopwatch
            stopwatch_sim.Start();
            stopwatch_hem.Start();
            stopwatch_con.Start();
            stopwatch_cir.Start();
            stopwatch_air.Start();
            stopwatch_bre.Start();
            #endregion
            subject = "start environment";
            Environment_control_function();

        }
        #endregion

        public void Closeform()
        {
            this.Close();
        }

        //timers and agent start/stop controllers
        public void Environment_control_function()
        {
            if (subject == "start environment")
            {
                #region Start patient and EMS agents
                from = "called by main function";
                patient();
                subject = "start degradation check reminder";
                AI();
                timer();
                set_prediction_metrics();
                #endregion
            }
            else if (subject == "highlight buttons")
            {
                ButtonHighlighter();
            }
            if (subject == "update UI")
            {
                UIController();
                set_prediction_metrics();
            }
            if (subject == "update patient initialization info")
            {
                UIController_initialize();
                set_prediction_metrics();
            }


            void timer()
            {
                Starttimer();

                void Starttimer()
                {
                    timer1 = new System.Windows.Forms.Timer();
                    timer1.Tick += new EventHandler(timer1_Tick);
                    timer1.Interval = 1000; // 1 second
                    timer1.Start();
                    string compound_time_start = String.Format("{0}:{1}{2}", main_minutes, string_seconds1, string_seconds2);
                    Timerlbl.Text = compound_time_start.ToString();
                }

                void timer1_Tick(object sender, EventArgs e)
                {
                    counter--;
                    if (counter == 0)
                    {
                        timer1.Stop();

                        // add form load here 
                    }
                    if (counter == 120)
                    {
                        main_seconds = 0;
                        main_minutes = 2;
                        string_seconds = Convert.ToString(main_seconds);
                        string_minutes = Convert.ToString(main_minutes);
                        string_seconds1 = string_seconds.Substring(0, 1);
                        string_seconds2 = string_seconds.Substring(1, 1);
                    }
                    else if (counter >= 70 && counter < 120)
                    {
                        main_seconds = counter - 60;
                        main_minutes = 1;
                        string_seconds = Convert.ToString(main_seconds);
                        string_minutes = Convert.ToString(main_minutes);
                        string_seconds1 = string_seconds.Substring(0, 1);
                        string_seconds2 = string_seconds.Substring(1, 1);
                    }
                    else if (counter > 60 && counter < 70)
                    {
                        main_seconds = counter - 60;
                        main_minutes = 1;
                        string_seconds = Convert.ToString(main_seconds);
                        string_minutes = Convert.ToString(main_minutes);
                        string_seconds1 = "0";
                        string_seconds2 = string_seconds.Substring(0, 1);
                    }
                    else if (counter == 60)
                    {
                        main_seconds = 0;
                        main_minutes = 1;
                        string_seconds = Convert.ToString(main_seconds);
                        string_minutes = Convert.ToString(main_minutes);
                        string_seconds1 = "0";
                        string_seconds2 = "0";
                    }
                    else if (counter > 30 && counter < 60)
                    {
                        main_seconds = counter;
                        main_minutes = 0;
                        string_seconds = Convert.ToString(main_seconds);
                        string_minutes = Convert.ToString(main_minutes);
                        string_seconds1 = string_seconds.Substring(0, 1);
                        string_seconds2 = string_seconds.Substring(1, 1);
                        //Timerlbl.ForeColor = Color.FromName("Blue");
                    }
                    else if (counter >= 10 && counter <= 30)
                    {
                        main_seconds = counter;
                        main_minutes = 0;
                        string_seconds = Convert.ToString(main_seconds);
                        string_minutes = Convert.ToString(main_minutes);
                        string_seconds1 = string_seconds.Substring(0, 1);
                        string_seconds2 = string_seconds.Substring(1, 1);
                        Timerlbl.ForeColor = Color.FromName("Blue");
                    }

                    else if (counter > 0 && counter < 10)
                    {
                        main_seconds = counter;
                        main_minutes = 0;
                        string_seconds = Convert.ToString(main_seconds);
                        string_minutes = Convert.ToString(main_minutes);
                        string_seconds1 = "0";
                        string_seconds2 = string_seconds.Substring(0, 1);
                        Timerlbl.ForeColor = Color.FromName("Green");
                    }
                    else if (counter == 0)
                    {
                        string_minutes = "0";
                        string_seconds1 = "0";
                        string_seconds2 = "0";
                        Injury_level_description.Visible = false;
                        Patient_description_title.Visible = false;
                        blocker.Visible = true;
                        blocker.BringToFront();

                        AI_patient_transfer_button.Enabled = true;
                        AI_patient_transfer_button.Visible = true;
                        AI_patient_transfer_button.BringToFront();

                        Button_air_Intubate.Enabled = false;
                        Button_air_clearair.Enabled = false;
                        Button_breath_aspirate.Enabled = false;
                        Button_breath_Oxygen.Enabled = false;
                        //Button_breath_rescuebreath_Click.Enabled = false;
                        Button_circ_chest.Enabled = false;
                        Button_circ_drugs.Enabled = false;
                        Button_conc_drugs.Enabled = false;
                        Button_hemm_torniquet.Enabled = false;
                        Button_hemm_treat.Enabled = false;
                        Button_CPR.Enabled = false;
                    }

                    string compound_time = String.Format("{0}:{1}{2}", main_minutes, string_seconds1, string_seconds2);

                    Timerlbl.Text = compound_time;
                }
            }

            void UIController_initialize()
            {
                if (UICounter == 0)
                {
                    UICounter += 1;

                    #region generate the natural language patient status text
                    //MessageBox.Show("ui initialize");

                    if (wound_type == "Gunshot" && hemorrhage == 2) { Woundtxt = String.Format("and has a serious gunshot wound to the {0}.", wound_location); }
                    else if (wound_type == "Gunshot" && hemorrhage == 1) { Woundtxt = String.Format("and has a gunshot wound to the {0}.", wound_location); }
                    if (wound_type == "Blunt Force Trauma" && hemorrhage == 2) { Woundtxt = String.Format("and has serious impact trauma to the {0}.", wound_location); }
                    else if (wound_type == "Blunt Force Trauma" && hemorrhage < 2) { Woundtxt = String.Format("and has impact trauma to the {0}.", wound_location); }
                    if (wound_type == "Heart Attack" && circulation == 2) { Woundtxt = String.Format("and has suffered a cardiac arrest."); }
                    else if (wound_type == "Heart Attack" && circulation == 1) { Woundtxt = String.Format("and has suffered a cardiac event."); }
                    if (wound_type == "Drowning") { Woundtxt = String.Format("and has suffered respiratory impairment due to submersion."); }
                    if (wound_type == "Allergic Reaction" && breathing == 2)
                    {
                        //MessageBox.Show("all react - bre prob");
                        Woundtxt = String.Format("and has suffered a severe allergic reaction.");
                    }
                    else if (wound_type == "Allergic Reaction" && breathing < 2)
                    {
                        //MessageBox.Show("all react - no bre prob");
                        Woundtxt = String.Format("and has suffered an allergic reaction.");
                    }

                    if (hemorrhage == 2)
                    {
                        HemLevel = "The patient is bleeding heavily";
                    }
                    else if (hemorrhage == 1)
                    {
                        HemLevel = "The patient is bleeding lightly";
                    }
                    else if (hemorrhage == 0)
                    {
                        HemLevel = "The patient is not bleeding";
                    }

                    #region generate the first link text
                    if (hemorrhage == 2 || hemorrhage == 1)
                    {
                        if (circulation == 2 || circulation == 1)
                        {
                            linktxt1 = "and";
                        }
                        else linktxt1 = "but";
                    }
                    else
                    {
                        if (circulation == 2 || circulation == 1)
                        {
                            linktxt1 = "but";
                        }
                        else linktxt1 = "and";
                    }
                    #endregion

                    if (circulation == 2) { CirLevel = "has suffered a heart attack."; }
                    else if (circulation == 1) { CirLevel = "is experiencing heart arrythmia."; }
                    else if (circulation == 0) { CirLevel = "heart rate is normal."; }

                    if (breathing == 2) { BreLevel = "The patient is not breathing"; }
                    else if (breathing == 1) { BreLevel = "The patient is having trouble breathing"; }
                    else if (breathing == 0) { BreLevel = "The patient is breathing normally"; }

                    #region generate the second link text
                    if (breathing == 2 && airway == 2)
                    {
                        linktxt2 = "and has";
                    }
                    else if (breathing == 2 && airway == 1)
                    {
                        linktxt2 = "and has";
                    }
                    else if (breathing == 2 && airway == 0)
                    {
                        linktxt2 = "despite";
                    }
                    else if (breathing == 1 && airway == 2)
                    {
                        linktxt2 = "and has";
                    }
                    else if (breathing == 1 && airway == 1)
                    {
                        linktxt2 = "and has";
                    }
                    else if (breathing == 1 && airway == 0)
                    {
                        linktxt2 = "despite";
                    }
                    else if (breathing == 0 && airway == 2)
                    {
                        linktxt2 = "despite";
                    }
                    else if (breathing == 0 && airway == 1)
                    {
                        linktxt2 = "despite";
                    }
                    else if (breathing == 0 && airway == 0)
                    {
                        linktxt2 = "and has";
                    }

                    #endregion

                    if (airway == 2) { AirLevel = "a blocked airway."; }
                    else if (airway == 1) { AirLevel = "a partially blocked airway."; }
                    else if (airway == 0) { AirLevel = "a clear airway."; }

                    if (consciousness == 2) { ConLevel = "The patient is unconscious."; }
                    else if (consciousness == 1) { ConLevel = "The patient is disoriented."; }
                    else if (consciousness == 0) { ConLevel = "The patient is fully conscious."; }
                    string wound_level_description = String.Format("The patient is a {0} year old {1} {2} {3} {4} {5} {6} {7} {8} {9}. ", //  /n is new line if needed
                        age, gender, Woundtxt, HemLevel, linktxt1, CirLevel, BreLevel, linktxt2, AirLevel, ConLevel);

                    if (Woundtxt == "null")
                    {
                        string bre_str = Convert.ToString(breathing);

                        MessageBox.Show(wound_location);
                        MessageBox.Show(wound_type);
                        MessageBox.Show(bre_str);

                    }

                    System.Drawing.Graphics g = this.CreateGraphics();
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    g.FillRoundedRectangle(new SolidBrush(Color.FromName("AliceBlue")), 700, 150, 1600, 300, 50);
                    g.DrawRoundedRectangle(new Pen(Color.FromName("turquoise"), 10f), 700, 150, 1590, 290, 50);
                    Show_incoming_message();

                    async void Show_incoming_message()
                    {
                        Patient_description_title.Visible = true;
                        Injury_level_description.Visible = true;
                        await Task.Delay(500);
                        Injury_level_description.Text = wound_level_description;
                        //MessageBox.Show(wound_level_description);
                        if (AI_assist == true)
                        {
                            // Initialize a new instance of the SpeechSynthesizer.
                            SpeechSynthesizer synth = new SpeechSynthesizer();

                            // Configure the audio output. 
                            synth.SetOutputToDefaultAudioDevice();

                            synth.SpeakAsync(wound_level_description);
                        }


                    }
                    #endregion

                    hem_ind_panel.BackgroundImage = DrawIndicator(hemorrhage);
                    air_ind_panel.BackgroundImage = DrawIndicator(airway);
                    breath_ind_panel.BackgroundImage = DrawIndicator(breathing);
                    conc_ind_panel.BackgroundImage = DrawIndicator(consciousness);
                    circ_ind_panel.BackgroundImage = DrawIndicator(circulation);
                }


                // add the indicator drawing part here
                hem_ind_panel.BackgroundImage = DrawIndicator(hemorrhage);
                air_ind_panel.BackgroundImage = DrawIndicator(airway);
                breath_ind_panel.BackgroundImage = DrawIndicator(breathing);
                conc_ind_panel.BackgroundImage = DrawIndicator(consciousness);
                circ_ind_panel.BackgroundImage = DrawIndicator(circulation);

                #region draw the indicator
                Bitmap DrawIndicator(int pat_char_stat)
                {
                    #region assign variables
                    int wid = 80; //indicator sise sizing
                    int hgt = 160;
                    Color bg_color = Color.FromName("White");
                    Color outline_color = Color.FromName("Black");
                    Color uncharged_color = Color.FromName("White");
                    Color charged_color = Color.FromName("Green");
                    PowerStatus batstatus = SystemInformation.PowerStatus;

                    if (pat_char_stat == 0)
                    {
                        charged_color = Color.FromName("Green");
                    }
                    else if (pat_char_stat == 1)
                    {
                        charged_color = Color.FromName("Orange");
                    }
                    else
                    {
                        charged_color = Color.FromName("Red");
                    }

                    float indicator_level = 1f - (0.333f * pat_char_stat); //determine the indicator level depending on the specific patient statistic;
                    bool striped = true; // add the stripes across the UI
                    #endregion

                    #region draw the indicator and return to calling class/object
                    Bitmap bm = new Bitmap(wid, hgt);
                    using (Graphics gr = Graphics.FromImage(bm))
                    {
                        // OLD CODE FROM EXAMPLE - to rotate indicator if needed - kept in for future reference - DELETE LATER
                        #region old code
                        // If the indicator has a horizontal orientation,
                        // rotate so we can draw it vertically.
                        //if (wid > hgt)
                        //{
                        //    gr.RotateTransform(90, MatrixOrder.Append);
                        //    gr.TranslateTransform(wid, 0, MatrixOrder.Append);
                        //    int temp = wid;
                        //    wid = hgt;
                        //    hgt = temp;
                        //}
                        #endregion

                        // Draw the battery.
                        DrawVerticalIndicator(gr, indicator_level, wid, hgt, bg_color,
                            outline_color, charged_color, uncharged_color,
                            striped);
                    }
                    return bm;
                    #endregion
                }
                #endregion

                #region update the treatment counts

                int total_circ_count = circ1_count + circ2_count;
                int total_conc_count = conc1_count + conc2_count;
                int total_airway_count = airway1_count + airway2_count;
                int total_breathing_count = breath1_count + breath2_count;
                int total_hemorrhage_count = hem1_count + hem2_count;
                int total_cpr_count = cpr2_count;

                string str_total_circ = Convert.ToString(total_circ_count);
                string str_total_conc = Convert.ToString(total_conc_count);
                string str_total_air = Convert.ToString(total_airway_count);
                string str_total_bre = Convert.ToString(total_breathing_count);
                string str_total_hem = Convert.ToString(total_hemorrhage_count);
                string str_total_cpr = Convert.ToString(total_cpr_count);
                string str_mistake = Convert.ToString(mistake_count);
                string str_success = Convert.ToString(total_success_count + total_partial_success_count);
                string str_unsuccess = Convert.ToString(total_unsuccess_count);

                hem_treat_count_box.Text = str_total_circ;
                cpr_treat_count_box.Text = str_total_cpr;
                air_treat_count_box.Text = str_total_air;
                con_treat_count_box.Text = str_total_conc;
                bre_treat_count_box.Text = str_total_bre;
                cir_treat_count_box.Text = str_total_circ;
                treatment_NR_text.Text = str_mistake;
                treatment_succ_text.Text = str_success;
                treatment_uns_text.Text = str_unsuccess;



                #endregion

                #region code to actually construct rectangles for indicators
                // Draw a vertically oriented battery with
                // the indicated percentage filled in.
                void DrawVerticalIndicator(Graphics gr, float indicator_level, int wid, int hgt, Color bg_color, Color outline_color, Color charged_color, Color uncharged_color, bool striped)
                {
                    gr.Clear(bg_color);
                    gr.SmoothingMode = SmoothingMode.AntiAlias;

                    // Make a rectangle for the main body.
                    float thickness = hgt / 20f;
                    RectangleF body_rect = new RectangleF(
                        thickness * 0.5f, thickness * 1.5f,
                        wid - thickness, hgt - thickness * 2f);

                    using (Pen pen = new Pen(outline_color, thickness))
                    {
                        // Fill the body
                        using (Brush brush = new SolidBrush(uncharged_color))
                        {
                            gr.FillRectangle(brush, body_rect);
                        }

                        // Fill the required area with color.
                        float indicator_hgt = body_rect.Height * indicator_level;
                        RectangleF charged_rect = new RectangleF(
                            body_rect.Left, body_rect.Bottom - indicator_hgt,
                            body_rect.Width, indicator_hgt);
                        using (Brush brush = new SolidBrush(charged_color))
                        {
                            gr.FillRectangle(brush, charged_rect);
                        }

                        // Optionally stripe multiples of 25%
                        if (striped)
                            for (int i = 1; i <= 3; i++)
                            {
                                float y = body_rect.Bottom -
                                    i * 0.33f * body_rect.Height;
                                gr.DrawLine(pen, body_rect.Left, y,
                                    body_rect.Right, y);
                            }

                        // Draw the main body.
                        gr.DrawPath(pen, MakeRoundedRect(
                            body_rect, thickness, thickness,
                            true, true, true, true));

                        // Draw the positive terminal. //
                        //RectangleF terminal_rect = new RectangleF(
                        //    wid / 2f - thickness, 0,
                        //    2 * thickness, thickness);
                        //gr.DrawPath(pen, MakeRoundedRect(
                        //    terminal_rect, thickness / 2f, thickness / 2f,
                        //    true, true, false, false));
                    }
                }

                // Draw a rectangle in the indicated Rectangle
                // rounding the indicated corners.
                #region code to make rounded rectangle
                GraphicsPath MakeRoundedRect(
                    RectangleF rect, float xradius, float yradius,
                    bool round_ul, bool round_ur, bool round_lr, bool round_ll)
                {
                    // Make a GraphicsPath to draw the rectangle.
                    PointF point1, point2;
                    GraphicsPath path = new GraphicsPath();

                    // Upper left corner.
                    if (round_ul)
                    {
                        RectangleF corner = new RectangleF(
                            rect.X, rect.Y,
                            2 * xradius, 2 * yradius);
                        path.AddArc(corner, 180, 90);
                        point1 = new PointF(rect.X + xradius, rect.Y);
                    }
                    else point1 = new PointF(rect.X, rect.Y);

                    // Top side.
                    if (round_ur)
                        point2 = new PointF(rect.Right - xradius, rect.Y);
                    else
                        point2 = new PointF(rect.Right, rect.Y);
                    path.AddLine(point1, point2);

                    // Upper right corner.
                    if (round_ur)
                    {
                        RectangleF corner = new RectangleF(
                            rect.Right - 2 * xradius, rect.Y,
                            2 * xradius, 2 * yradius);
                        path.AddArc(corner, 270, 90);
                        point1 = new PointF(rect.Right, rect.Y + yradius);
                    }
                    else point1 = new PointF(rect.Right, rect.Y);

                    // Right side.
                    if (round_lr)
                        point2 = new PointF(rect.Right, rect.Bottom - yradius);
                    else
                        point2 = new PointF(rect.Right, rect.Bottom);
                    path.AddLine(point1, point2);

                    // Lower right corner.
                    if (round_lr)
                    {
                        RectangleF corner = new RectangleF(
                            rect.Right - 2 * xradius,
                            rect.Bottom - 2 * yradius,
                            2 * xradius, 2 * yradius);
                        path.AddArc(corner, 0, 90);
                        point1 = new PointF(rect.Right - xradius, rect.Bottom);
                    }
                    else point1 = new PointF(rect.Right, rect.Bottom);

                    // Bottom side.
                    if (round_ll)
                        point2 = new PointF(rect.X + xradius, rect.Bottom);
                    else
                        point2 = new PointF(rect.X, rect.Bottom);
                    path.AddLine(point1, point2);

                    // Lower left corner.
                    if (round_ll)
                    {
                        RectangleF corner = new RectangleF(
                            rect.X, rect.Bottom - 2 * yradius,
                            2 * xradius, 2 * yradius);
                        path.AddArc(corner, 90, 90);
                        point1 = new PointF(rect.X, rect.Bottom - yradius);
                    }
                    else point1 = new PointF(rect.X, rect.Bottom);

                    // Left side.
                    if (round_ul)
                        point2 = new PointF(rect.X, rect.Y + yradius);
                    else
                        point2 = new PointF(rect.X, rect.Y);
                    path.AddLine(point1, point2);

                    // Join with the start point.
                    path.CloseFigure();

                    return path;
                }
                #endregion

                #endregion
            }

            void UIController()
            {
                #region old initializer - commented out
                //if (UICounter == 0)
                //{
                //    UICounter += 1;

                //    #region generate the natural language patient status text


                //    if (wound_type == "gunshot" && hemorrhage == 2) { Woundtxt = String.Format("and has a serious gunshot wound to the {0}.", wound_location); }
                //    else if (wound_type == "gunshot" && hemorrhage == 1) { Woundtxt = String.Format("and has a gunshot wound to the {0}.", wound_location); }
                //    if (wound_type == "blunt force trauma" && hemorrhage == 2) { Woundtxt = String.Format("and has serious impact trauma to the {0}.", wound_location); }
                //    else if (wound_type == "blunt force trauma" && hemorrhage < 2) { Woundtxt = String.Format("and has impact trauma to the {0}.", wound_location); }
                //    if (wound_type == "heart attack" && circulation == 2) { Woundtxt = String.Format("and has suffered a cardiac arrest."); }
                //    else if (wound_type == "heart attack" && circulation == 1) { Woundtxt = String.Format("and has suffered a cardiac event."); }
                //    if (wound_type == "drowning") { Woundtxt = String.Format("and has suffered respiratory impairment due to submersion."); }
                //    if (wound_type == "allergic reaction" && breathing == 2)
                //    {
                //        //MessageBox.Show("all react - bre prob");
                //        Woundtxt = String.Format("and has suffered a severe allergic reaction.");
                //    }
                //    else if (wound_type == "allergic reaction" && breathing < 2)
                //    {
                //        //MessageBox.Show("all react - no bre prob");
                //        Woundtxt = String.Format("and has suffered an allergic reaction.");
                //    }

                //    if (hemorrhage == 2)
                //    {
                //        HemLevel = "The patient is bleeding heavily";
                //    }
                //    else if (hemorrhage == 1)
                //    {
                //        HemLevel = "The patient is bleeding lightly";
                //    }
                //    else if (hemorrhage == 0)
                //    {
                //        HemLevel = "The patient is not bleeding";
                //    }

                //    #region generate the first link text
                //    if (hemorrhage == 2 || hemorrhage == 1)
                //    {
                //        if (circulation == 2 || circulation == 1)
                //        {
                //            linktxt1 = "and";
                //        }
                //        else linktxt1 = "but";
                //    }
                //    else
                //    {
                //        if (circulation == 2 || circulation == 1)
                //        {
                //            linktxt1 = "but";
                //        }
                //        else linktxt1 = "and";
                //    }
                //    #endregion

                //    if (circulation == 2) { CirLevel = "has suffered a heart attack."; }
                //    else if (circulation == 1) { CirLevel = "is experiencing heart arrythmia."; }
                //    else if (circulation == 0) { CirLevel = "heart rate is normal."; }

                //    if (breathing == 2) { BreLevel = "The patient is not breathing"; }
                //    else if (breathing == 1) { BreLevel = "The patient is haveing trouble breathnig"; }
                //    else if (breathing == 0) { BreLevel = "The patient is breathing normally"; }

                //    #region generate the second link text
                //    if (breathing == 2 && airway == 2)
                //    {
                //        linktxt2 = "and has";
                //    }
                //    else if (breathing == 2 && airway == 1)
                //    {
                //        linktxt2 = "and has";
                //    }
                //    else if (breathing == 2 && airway == 0)
                //    {
                //        linktxt2 = "despite";
                //    }
                //    else if (breathing == 1 && airway == 2)
                //    {
                //        linktxt2 = "and has";
                //    }
                //    else if (breathing == 1 && airway == 1)
                //    {
                //        linktxt2 = "and has";
                //    }
                //    else if (breathing == 1 && airway == 0)
                //    {
                //        linktxt2 = "despite";
                //    }
                //    else if (breathing == 0 && airway == 2)
                //    {
                //        linktxt2 = "despite";
                //    }
                //    else if (breathing == 0 && airway == 1)
                //    {
                //        linktxt2 = "despite";
                //    }
                //    else if (breathing == 0 && airway == 0)
                //    {
                //        linktxt2 = "and has";
                //    }

                //    #endregion

                //    if (airway == 2) { AirLevel = "a blocked airway."; }
                //    else if (airway == 1) { AirLevel = "a partially blocked airway."; }
                //    else if (airway == 0) { AirLevel = "a clear airway."; }

                //    if (consciousness == 2) { ConLevel = "The patient is unconscious."; }
                //    else if (consciousness == 1) { ConLevel = "The patient is disoriented."; }
                //    else if (consciousness == 0) { ConLevel = "The patient is fully conscious."; }
                //    string wound_level_description = String.Format("The patient is a {0} year old {1} {2} {3} {4} {5} {6} {7} {8} {9}. ", //  /n is new line if needed
                //        age, gender, Woundtxt, HemLevel, linktxt1, CirLevel, BreLevel, linktxt2, AirLevel, ConLevel);

                //    if (Woundtxt == "null")
                //    {
                //        string bre_str = Convert.ToString(breathing);

                //        MessageBox.Show(wound_location);
                //        MessageBox.Show(wound_type);
                //        MessageBox.Show(bre_str);

                //    }

                //    System.Drawing.Graphics g = this.CreateGraphics();
                //    g.SmoothingMode = SmoothingMode.AntiAlias;

                //    g.FillRoundedRectangle(new SolidBrush(Color.FromName("AliceBlue")), 700, 150, 1600, 300, 50);
                //    g.DrawRoundedRectangle(new Pen(Color.FromName("turquoise"), 10f), 700, 150, 1590, 290, 50);
                //    Show_incoming_message();

                //    async void Show_incoming_message()
                //    {
                //        Patient_description_title.Visible = true;
                //        Injury_level_description.Visible = true;
                //        await Task.Delay(2000);
                //        Injury_level_description.Text = wound_level_description;
                //        //MessageBox.Show(wound_level_description);
                //    }
                //    #endregion

                //    hem_ind_panel.BackgroundImage = DrawIndicator(hemorrhage);
                //    air_ind_panel.BackgroundImage = DrawIndicator(airway);
                //    breath_ind_panel.BackgroundImage = DrawIndicator(breathing);
                //    conc_ind_panel.BackgroundImage = DrawIndicator(consciousness);
                //    circ_ind_panel.BackgroundImage = DrawIndicator(circulation);
                //}
                #endregion

                // add the indicator drawing part here
                hem_ind_panel.BackgroundImage = DrawIndicator(hemorrhage);
                air_ind_panel.BackgroundImage = DrawIndicator(airway);
                breath_ind_panel.BackgroundImage = DrawIndicator(breathing);
                conc_ind_panel.BackgroundImage = DrawIndicator(consciousness);
                circ_ind_panel.BackgroundImage = DrawIndicator(circulation);

                #region draw the indicator
                Bitmap DrawIndicator(int pat_char_stat)
                {
                    #region assign variables
                    int wid = 80; //indicator sise sizing
                    int hgt = 160;
                    Color bg_color = Color.FromName("White");
                    Color outline_color = Color.FromName("Black");
                    Color uncharged_color = Color.FromName("White");
                    Color charged_color = Color.FromName("Green");
                    PowerStatus batstatus = SystemInformation.PowerStatus;

                    if (pat_char_stat == 0)
                    {
                        charged_color = Color.FromName("Green");
                    }
                    else if (pat_char_stat == 1)
                    {
                        charged_color = Color.FromName("Orange");
                    }
                    else
                    {
                        charged_color = Color.FromName("Red");
                    }

                    float indicator_level = 1f - (0.333f * pat_char_stat); //determine the indicator level depending on the specific patient statistic;
                    bool striped = true; // add the stripes across the UI
                    #endregion

                    #region draw the indicator and return to calling class/object
                    Bitmap bm = new Bitmap(wid, hgt);
                    using (Graphics gr = Graphics.FromImage(bm))
                    {
                        // OLD CODE FROM EXAMPLE - to rotate indicator if needed - kept in for future reference - DELETE LATER
                        #region old code
                        // If the indicator has a horizontal orientation,
                        // rotate so we can draw it vertically.
                        //if (wid > hgt)
                        //{
                        //    gr.RotateTransform(90, MatrixOrder.Append);
                        //    gr.TranslateTransform(wid, 0, MatrixOrder.Append);
                        //    int temp = wid;
                        //    wid = hgt;
                        //    hgt = temp;
                        //}
                        #endregion

                        // Draw the battery.
                        DrawVerticalIndicator(gr, indicator_level, wid, hgt, bg_color,
                            outline_color, charged_color, uncharged_color,
                            striped);
                    }
                    return bm;
                    #endregion
                }
                #endregion

                #region update the treatment counts

                int total_circ_count = circ1_count + circ2_count;
                int total_conc_count = conc1_count + conc2_count;
                int total_airway_count = airway1_count + airway2_count;
                int total_breathing_count = breath1_count + breath2_count;
                int total_hemorrhage_count = hem1_count + hem2_count;
                int total_cpr_count = cpr2_count;

                string str_total_circ = Convert.ToString(total_circ_count);
                string str_total_conc = Convert.ToString(total_conc_count);
                string str_total_air = Convert.ToString(total_airway_count);
                string str_total_bre = Convert.ToString(total_breathing_count);
                string str_total_hem = Convert.ToString(total_hemorrhage_count);
                string str_total_cpr = Convert.ToString(total_cpr_count);
                string str_mistake = Convert.ToString(mistake_count);
                string str_success = Convert.ToString(total_success_count + total_partial_success_count);
                string str_unsuccess = Convert.ToString(total_unsuccess_count);

                hem_treat_count_box.Text = str_total_circ;
                cpr_treat_count_box.Text = str_total_cpr;
                air_treat_count_box.Text = str_total_air;
                con_treat_count_box.Text = str_total_conc;
                bre_treat_count_box.Text = str_total_bre;
                cir_treat_count_box.Text = str_total_circ;
                treatment_NR_text.Text = str_mistake;
                treatment_succ_text.Text = str_success;
                treatment_uns_text.Text = str_unsuccess;



                #endregion

                #region code to actually construct rectangles for indicators
                // Draw a vertically oriented battery with
                // the indicated percentage filled in.
                void DrawVerticalIndicator(Graphics gr, float indicator_level, int wid, int hgt, Color bg_color, Color outline_color, Color charged_color, Color uncharged_color, bool striped)
                {
                    gr.Clear(bg_color);
                    gr.SmoothingMode = SmoothingMode.AntiAlias;

                    // Make a rectangle for the main body.
                    float thickness = hgt / 20f;
                    RectangleF body_rect = new RectangleF(
                        thickness * 0.5f, thickness * 1.5f,
                        wid - thickness, hgt - thickness * 2f);

                    using (Pen pen = new Pen(outline_color, thickness))
                    {
                        // Fill the body
                        using (Brush brush = new SolidBrush(uncharged_color))
                        {
                            gr.FillRectangle(brush, body_rect);
                        }

                        // Fill the required area with color.
                        float indicator_hgt = body_rect.Height * indicator_level;
                        RectangleF charged_rect = new RectangleF(
                            body_rect.Left, body_rect.Bottom - indicator_hgt,
                            body_rect.Width, indicator_hgt);
                        using (Brush brush = new SolidBrush(charged_color))
                        {
                            gr.FillRectangle(brush, charged_rect);
                        }

                        // Optionally stripe multiples of 25%
                        if (striped)
                            for (int i = 1; i <= 3; i++)
                            {
                                float y = body_rect.Bottom -
                                    i * 0.33f * body_rect.Height;
                                gr.DrawLine(pen, body_rect.Left, y,
                                    body_rect.Right, y);
                            }

                        // Draw the main body.
                        gr.DrawPath(pen, MakeRoundedRect(
                            body_rect, thickness, thickness,
                            true, true, true, true));

                        // Draw the positive terminal. //
                        //RectangleF terminal_rect = new RectangleF(
                        //    wid / 2f - thickness, 0,
                        //    2 * thickness, thickness);
                        //gr.DrawPath(pen, MakeRoundedRect(
                        //    terminal_rect, thickness / 2f, thickness / 2f,
                        //    true, true, false, false));
                    }
                }

                // Draw a rectangle in the indicated Rectangle
                // rounding the indicated corners.
                #region code to make rounded rectangle
                GraphicsPath MakeRoundedRect(
                    RectangleF rect, float xradius, float yradius,
                    bool round_ul, bool round_ur, bool round_lr, bool round_ll)
                {
                    // Make a GraphicsPath to draw the rectangle.
                    PointF point1, point2;
                    GraphicsPath path = new GraphicsPath();

                    // Upper left corner.
                    if (round_ul)
                    {
                        RectangleF corner = new RectangleF(
                            rect.X, rect.Y,
                            2 * xradius, 2 * yradius);
                        path.AddArc(corner, 180, 90);
                        point1 = new PointF(rect.X + xradius, rect.Y);
                    }
                    else point1 = new PointF(rect.X, rect.Y);

                    // Top side.
                    if (round_ur)
                        point2 = new PointF(rect.Right - xradius, rect.Y);
                    else
                        point2 = new PointF(rect.Right, rect.Y);
                    path.AddLine(point1, point2);

                    // Upper right corner.
                    if (round_ur)
                    {
                        RectangleF corner = new RectangleF(
                            rect.Right - 2 * xradius, rect.Y,
                            2 * xradius, 2 * yradius);
                        path.AddArc(corner, 270, 90);
                        point1 = new PointF(rect.Right, rect.Y + yradius);
                    }
                    else point1 = new PointF(rect.Right, rect.Y);

                    // Right side.
                    if (round_lr)
                        point2 = new PointF(rect.Right, rect.Bottom - yradius);
                    else
                        point2 = new PointF(rect.Right, rect.Bottom);
                    path.AddLine(point1, point2);

                    // Lower right corner.
                    if (round_lr)
                    {
                        RectangleF corner = new RectangleF(
                            rect.Right - 2 * xradius,
                            rect.Bottom - 2 * yradius,
                            2 * xradius, 2 * yradius);
                        path.AddArc(corner, 0, 90);
                        point1 = new PointF(rect.Right - xradius, rect.Bottom);
                    }
                    else point1 = new PointF(rect.Right, rect.Bottom);

                    // Bottom side.
                    if (round_ll)
                        point2 = new PointF(rect.X + xradius, rect.Bottom);
                    else
                        point2 = new PointF(rect.X, rect.Bottom);
                    path.AddLine(point1, point2);

                    // Lower left corner.
                    if (round_ll)
                    {
                        RectangleF corner = new RectangleF(
                            rect.X, rect.Bottom - 2 * yradius,
                            2 * xradius, 2 * yradius);
                        path.AddArc(corner, 90, 90);
                        point1 = new PointF(rect.X, rect.Bottom - yradius);
                    }
                    else point1 = new PointF(rect.X, rect.Bottom);

                    // Left side.
                    if (round_ul)
                        point2 = new PointF(rect.X, rect.Y + yradius);
                    else
                        point2 = new PointF(rect.X, rect.Y);
                    path.AddLine(point1, point2);

                    // Join with the start point.
                    path.CloseFigure();

                    return path;
                }
                #endregion

                #endregion
            }

            #region highlight appropriate buttons
            void ButtonHighlighter()
            {

                Button_hemm_torniquet.BackColor = Color.FromName("Menu");
                Button_hemm_treat.BackColor = Color.FromName("Menu");
                Button_conc_drugs.BackColor = Color.FromName("Menu");
                Button_air_Intubate.BackColor = Color.FromName("Menu");
                Button_air_clearair.BackColor = Color.FromName("Menu");
                Button_circ_drugs.BackColor = Color.FromName("Menu");
                Button_circ_chest.BackColor = Color.FromName("Menu");
                Button_breath_Oxygen.BackColor = Color.FromName("Menu");
                Button_breath_aspirate.BackColor = Color.FromName("Menu");
                Button_CPR.BackColor = Color.FromName("Menu");

                //CHECK -REMOVE IF  NO USE
                //treatment_timeline.Items.Add($"Display Action (EMS): Thread Running {action}.....");
                if (action == "check hemorrhage")
                {
                    if (hem == 2)
                    {
                        Button_hemm_torniquet.BackColor = Color.Turquoise;
                    }
                    else
                    {
                        Button_hemm_treat.BackColor = Color.Turquoise;
                    }
                }
                if (action == "check consciousness")
                {
                    Button_conc_drugs.BackColor = Color.Turquoise;
                }
                if (action == "check airway")
                {
                    if (air == 2)
                    {
                        Button_air_Intubate.BackColor = Color.Turquoise;
                    }
                    else
                    {
                        Button_air_clearair.BackColor = Color.Turquoise;
                    }
                }
                if (action == "check circulation")
                {
                    if (cir == 2)
                    {
                        Button_circ_chest.BackColor = Color.Turquoise;
                    }
                    else
                    {
                        Button_circ_drugs.BackColor = Color.Turquoise;
                    }
                }
                if (action == "check breathing")
                {
                    if (bre == 2)
                    {
                        Button_breath_Oxygen.BackColor = Color.Turquoise;
                    }
                    else
                    {
                        Button_breath_aspirate.BackColor = Color.Turquoise;
                    }
                }
                if (action == "check CPR")
                {
                    Button_CPR.BackColor = Color.Turquoise;
                }
            }
            #endregion
        }

        #region Create Patient Agent 
        public void patient()
        {
            if (from == "called by main function")
            #region If the message is from the main start function, initialize patient variables (according to NEMSIS distributions)
            {
                #region assigning initial patient attributes in line with NEMSIS data where available or random from normal distribution.
                Random random = new Random();
                int a;

                #region set wound type
                //currently set to 1 injury type 'gunshot' for ease of simulation
                wound_type_ID = random.Next(0, 5);
                if (wound_type_ID == 0) wound_type = "Gunshot";
                else if (wound_type_ID == 1) wound_type = "Blunt Force Trauma";
                else if (wound_type_ID == 2) wound_type = "Drowning";
                else if (wound_type_ID == 3) wound_type = "Heart Attack";
                else wound_type = "Allergic Reaction";
                #endregion

                #region set wound location strings

                if (wound_type_ID > 1)
                {
                    wound_location = "internal/global";
                }
                else
                {
                    wound_loc_ID = random.Next(1, 23);
                    if (wound_loc_ID == 1) wound_location = "front head";
                    else if (wound_loc_ID == 2) wound_location = "front torso";
                    else if (wound_loc_ID == 3) wound_location = "front mid-section";
                    else if (wound_loc_ID == 4) wound_location = "front right arm";
                    else if (wound_loc_ID == 5) wound_location = "front left arm";
                    else if (wound_loc_ID == 6) wound_location = "front right low arm";
                    else if (wound_loc_ID == 7) wound_location = "front left low arm";
                    else if (wound_loc_ID == 8) wound_location = "front right leg";
                    else if (wound_loc_ID == 9) wound_location = "front left leg";
                    else if (wound_loc_ID == 10) wound_location = "front right low leg";
                    else if (wound_loc_ID == 11) wound_location = "front left low leg";
                    else if (wound_loc_ID == 12) wound_location = "back head";
                    else if (wound_loc_ID == 13) wound_location = "back torso";
                    else if (wound_loc_ID == 14) wound_location = "back mid-section";
                    else if (wound_loc_ID == 15) wound_location = "back right arm";
                    else if (wound_loc_ID == 16) wound_location = "back left arm";
                    else if (wound_loc_ID == 17) wound_location = "back right low arm";
                    else if (wound_loc_ID == 18) wound_location = "back left low arm";
                    else if (wound_loc_ID == 19) wound_location = "back right leg";
                    else if (wound_loc_ID == 20) wound_location = "back left leg";
                    else if (wound_loc_ID == 21) wound_location = "back right low leg";
                    else wound_location = "back left low leg";
                }
                #endregion

                #region Define age category probabilities by injury type
                //coarse probability distributiuons for each of the four age categories
                int[] Ageprob_gunshot = { 2, 4, 90, 100 };
                int[] Ageprob_bft = { 5, 10, 80, 100 };
                int[] Ageprob_drowning = { 15, 60, 85, 100 };
                int[] Ageprob_heart = { 1, 3, 60, 100 };// cumulative probabilities as age group increases
                int[] Ageprob_allergy = { 10, 40, 75, 100 };
                #endregion

                #region assign injury type age group and age for display depending on type of injury
                #region assign age - weighted probability commented out as this is done based on injury now
                age_p = random.Next(0, 101);
                //if (a < 5)
                //{
                //    age_p = random.Next(0, 3); //set random seed for probability of any given age
                //}
                //else if (a >= 5 && a < 25)
                //{
                //    age_p = random.Next(3, 18); //set random seed for probability of any given age
                //}
                //else if (a >= 25 && a < 90 )
                //{
                //    age_p = random.Next(19, 69); //set random seed for probability of any given age
                //}
                //else 
                //{
                //    age_p = random.Next(70, 100); //set random seed for probability of any given age
                //}
                #endregion 

                #region gunshot wounds
                if (wound_type_ID == 0)
                    if (age_p <= Ageprob_gunshot[0])
                    {
                        age_group = "infant";
                        age = random.Next(1, 3);
                    }
                    else if (age_p > Ageprob_gunshot[0] && age_p <= Ageprob_gunshot[1])
                    {
                        age_group = "child";
                        age = random.Next(4, 17);
                    }
                    else if (age_p > Ageprob_gunshot[1] && age_p <= Ageprob_gunshot[2])
                    {
                        age_group = "adult";
                        age = random.Next(18, 70);
                    }
                    else
                    {
                        age_group = "over 70";
                        age = random.Next(71, 90);
                    }
                #endregion
                #region blunt force trauma wounds
                else if (wound_type_ID == 1)
                    if (age_p <= Ageprob_bft[0])
                    {
                        age_group = "infant";
                        age = random.Next(1, 3);
                    }
                    else if (age_p > Ageprob_bft[0] && age_p <= Ageprob_bft[1])
                    {
                        age_group = "child";
                        age = random.Next(4, 17);
                    }
                    else if (age_p > Ageprob_bft[1] && age_p <= Ageprob_bft[2])
                    {
                        age_group = "adult";
                        age = random.Next(18, 70);
                    }
                    else
                    {
                        age_group = "over 70";
                        age = random.Next(71, 90);
                    }
                #endregion
                #region drowning
                else if (wound_type_ID == 2)
                    if (age_p <= Ageprob_drowning[0])
                    {
                        age_group = "infant";
                        age = random.Next(1, 3);
                    }
                    else if (age_p > Ageprob_drowning[0] && age_p <= Ageprob_drowning[1])
                    {
                        age_group = "child";
                        age = random.Next(4, 17);
                    }
                    else if (age_p > Ageprob_drowning[1] && age_p <= Ageprob_drowning[2])
                    {
                        age_group = "adult";
                        age = random.Next(18, 70);
                    }
                    else
                    {
                        age_group = "over 70";
                        age = random.Next(71, 90);
                    }
                #endregion
                #region heart attack
                else if (wound_type_ID == 3)
                    if (age_p <= Ageprob_heart[0])
                    {
                        age_group = "infant";
                        age = random.Next(1, 3);
                    }
                    else if (age_p > Ageprob_heart[0] && age_p <= Ageprob_heart[1])
                    {
                        age_group = "child";
                        age = random.Next(4, 17);
                    }
                    else if (age_p > Ageprob_heart[1] && age_p <= Ageprob_heart[2])
                    {
                        age_group = "adult";
                        age = random.Next(18, 70);
                    }
                    else
                    {
                        age_group = "over 70";
                        age = random.Next(71, 90);
                    }
                #endregion
                #region allergic reaction
                else
                    if (age_p <= Ageprob_allergy[0])
                {
                    age_group = "infant";
                    age = random.Next(1, 3);
                }
                else if (age_p > Ageprob_allergy[0] && age_p <= Ageprob_allergy[1])
                {
                    age_group = "child";
                    age = random.Next(4, 17);
                }
                else if (age_p > Ageprob_allergy[1] && age_p <= Ageprob_allergy[2])
                {
                    age_group = "adult";
                    age = random.Next(18, 70);
                }
                else
                {
                    age_group = "over 70";
                    age = random.Next(71, 90);
                }
                #endregion

                #region Assign patient gender
                a = random.Next(0, 2);
                if (a == 0)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
                #endregion

                #region assign degradation probabilities depending on age for all characteristics
                //the probabilities are percentage values for no degradation occuring (100-probability of degradation occuring)
                #region age dependent hemorrhage change POMDP Logic threshold assignment
                if (age < 3)
                {
                    hem_deg_prob_longtime = 80;
                    hem_deg_prob_shorttime = 90;
                }
                else if (age >= 3 && age < 18)
                {
                    hem_deg_prob_longtime = 85;
                    hem_deg_prob_shorttime = 95;
                }
                else if (age >= 18 && age < 70)
                {
                    hem_deg_prob_longtime = 90;
                    hem_deg_prob_shorttime = 95;
                }
                else if (age >= 70)
                {
                    hem_deg_prob_longtime = 60;
                    hem_deg_prob_shorttime = 85;
                }
                #endregion
                #region age dependent airway change POMDP Logic threshold assignment
                if (age < 3)
                {
                    hem_deg_prob_longtime = 80;
                    hem_deg_prob_shorttime = 95;
                }
                else if (age >= 3 && age < 18)
                {
                    hem_deg_prob_longtime = 85;
                    hem_deg_prob_shorttime = 97;
                }
                else if (age >= 18 && age < 70)
                {
                    hem_deg_prob_longtime = 90;
                    hem_deg_prob_shorttime = 98;
                }
                else if (age >= 18 && age < 70)
                {
                    hem_deg_prob_longtime = 60;
                    hem_deg_prob_shorttime = 90;
                }
                #endregion
                #region age dependent circulation degragation change POMDP Logic threshold assignment
                if (age < 3)
                {
                    circ0to1if1_deg_prob_longtime -= 0;
                    circ0to2if1_deg_prob_longtime -= 0;
                    circ1to2if1_deg_prob_longtime -= 0;
                    circ0to1if2_deg_prob_longtime -= 0;
                    circ0to2if2_deg_prob_longtime -= 0;
                    circ1to2if2_deg_prob_longtime -= 0;
                    circ0to1if1_deg_prob_shorttime -= 0;
                    circ0to2if1_deg_prob_shorttime -= 0;
                    circ1to2if1_deg_prob_shorttime -= 0;
                    circ0to1if2_deg_prob_shorttime -= 0;
                    circ0to2if2_deg_prob_shorttime -= 0;
                    circ1to2if2_deg_prob_shorttime -= 0;

                }
                else if (age >= 3 && age < 18)
                {
                    circ0to1if1_deg_prob_longtime -= 0;
                    circ0to2if1_deg_prob_longtime -= 0;
                    circ1to2if1_deg_prob_longtime -= 0;
                    circ0to1if2_deg_prob_longtime -= 0;
                    circ0to2if2_deg_prob_longtime -= 0;
                    circ1to2if2_deg_prob_longtime -= 0;
                    circ0to1if1_deg_prob_shorttime -= 0;
                    circ0to2if1_deg_prob_shorttime -= 0;
                    circ1to2if1_deg_prob_shorttime -= 0;
                    circ0to1if2_deg_prob_shorttime -= 0;
                    circ0to2if2_deg_prob_shorttime -= 0;
                    circ1to2if2_deg_prob_shorttime -= 0;
                }
                else if (age >= 18 && age < 70)
                {
                    circ0to1if1_deg_prob_longtime -= 5;
                    circ0to2if1_deg_prob_longtime -= 5;
                    circ1to2if1_deg_prob_longtime -= 5;
                    circ0to1if2_deg_prob_longtime -= 5;
                    circ0to2if2_deg_prob_longtime -= 5;
                    circ1to2if2_deg_prob_longtime -= 5;
                    circ0to1if1_deg_prob_shorttime -= 5;
                    circ0to2if1_deg_prob_shorttime -= 5;
                    circ1to2if1_deg_prob_shorttime -= 5;
                    circ0to1if2_deg_prob_shorttime -= 5;
                    circ0to2if2_deg_prob_shorttime -= 5;
                    circ1to2if2_deg_prob_shorttime -= 5;
                }
                else if (age >= 18 && age < 70)
                {
                    circ0to1if1_deg_prob_longtime -= 10;
                    circ0to2if1_deg_prob_longtime -= 10;
                    circ1to2if1_deg_prob_longtime -= 10;
                    circ0to1if2_deg_prob_longtime -= 10;
                    circ0to2if2_deg_prob_longtime -= 10;
                    circ1to2if2_deg_prob_longtime -= 10;
                    circ0to1if1_deg_prob_shorttime -= 10;
                    circ0to2if1_deg_prob_shorttime -= 10;
                    circ1to2if1_deg_prob_shorttime -= 10;
                    circ0to1if2_deg_prob_shorttime -= 10;
                    circ0to2if2_deg_prob_shorttime -= 10;
                    circ1to2if2_deg_prob_shorttime -= 10;
                }
                #endregion

                #region age dependent cardiac arrest minor major probability
                if (age < 3)
                {
                    major_heart_attack_probability = 10;
                }
                else if (age >= 3 && age < 18)
                {
                    major_heart_attack_probability = 10;
                }
                else if (age >= 18 && age < 70)
                {
                    if (gender == "Male")
                    {
                        major_heart_attack_probability = 30;
                    }
                    else
                    {
                        major_heart_attack_probability = 15;
                    }
                }
                else if (age >= 18 && age < 70)
                {
                    if (gender == "Male")
                    {
                        major_heart_attack_probability = 60;
                    }
                    else
                    {
                        major_heart_attack_probability = 40;
                    }
                }
                #endregion
                #endregion

                #region initialize variables for individual characteristics
                #region set initial hemorrhage level - depending on injury type
                if (wound_type == "Gunshot")
                {
                    if (wound_loc_ID == 1 || wound_loc_ID == 2 || wound_loc_ID == 3 || wound_loc_ID == 12 || wound_loc_ID == 13 || wound_loc_ID == 14)
                    {
                        a = random.Next(0, 100);
                        if (a <= 95) // probability of serious bleed from gunshot wound if in head chest/midriff etc
                        {
                            hemorrhage = 2;
                            initial_hemorrhage = hemorrhage;
                        }
                        else
                        {
                            hemorrhage = 1;
                            initial_hemorrhage = hemorrhage;
                        }
                    }
                    else
                    {
                        {
                            a = random.Next(0, 100);
                            if (a <= 70)
                            {
                                hemorrhage = 2;
                                initial_hemorrhage = hemorrhage;
                            }
                            else
                            {
                                hemorrhage = 1;
                                initial_hemorrhage = hemorrhage;
                            }
                        }
                    }
                }
                else if (wound_type == "Blunt Force Trauma") // blow to the head probability of bleeding 
                {

                    if (wound_loc_ID == 1 || wound_loc_ID == 12)
                    {
                        a = random.Next(0, 100);
                        if (a <= 40)
                        {
                            hemorrhage = 0;
                            initial_hemorrhage = hemorrhage;
                        }
                        else if (a > 40 && a < 70)
                        {
                            hemorrhage = 1;
                            initial_hemorrhage = hemorrhage;
                        }
                        else
                        {
                            hemorrhage = 2;
                            initial_hemorrhage = hemorrhage; ;
                        }
                    }
                    else
                    {
                        a = random.Next(0, 100);
                        if (a <= 50)
                        {
                            hemorrhage = 0;
                            initial_hemorrhage = hemorrhage;
                        }
                        else if (a > 50 && a < 80)
                        {
                            hemorrhage = 1;
                            initial_hemorrhage = hemorrhage;
                        }
                        else
                        {
                            hemorrhage = 2;
                            initial_hemorrhage = hemorrhage; ;
                        }
                    }
                }
                else // chance of bleeding fof other injuries
                {
                    a = random.Next(0, 100);
                    if (a <= 80)                        //80% chance of no hemorrhage
                    {
                        hemorrhage = 0;
                        initial_hemorrhage = hemorrhage;
                    }
                    else if (a > 80 && a < 95)          //15% chance of minor hemorrhage
                    {
                        hemorrhage = 1;
                        initial_hemorrhage = hemorrhage;
                    }
                    else
                    {
                        hemorrhage = 2;
                        initial_hemorrhage = hemorrhage; ;
                    }
                }
                #endregion

                #region set consciousness level depedent on hemorrhage level
                if (hemorrhage == 2) //if heavy bleeding probability of various consciouness levels.
                {
                    a = random.Next(0, 100);
                    if (a < 25)
                    {
                        consciousness = 0;
                        initial_consciousness = consciousness;
                    }
                    else if (a >= 25 && a < 50)
                    {
                        consciousness = 1;
                        initial_consciousness = consciousness;
                    }
                    else
                    {
                        consciousness = 2;
                        initial_consciousness = consciousness; ;
                    }
                }
                else if (hemorrhage == 1)
                {
                    a = random.Next(0, 100);
                    if (a < 60)
                    {
                        consciousness = 0;
                        initial_consciousness = consciousness;
                    }
                    else if (a >= 60 && a < 80)
                    {
                        consciousness = 1;
                        initial_consciousness = consciousness;
                    }
                    else
                    {
                        consciousness = 2;
                        initial_consciousness = consciousness; ;
                    }
                }
                #endregion

                #region initialize cardiac/circulation variables 

                #region Cardiac Event
                if (wound_type_ID == 3)
                {
                    a = random.Next(0, 100);
                    if (a < major_heart_attack_probability) // age dependent probability that the heart attack is serious
                    {
                        circulation = 1;
                        initial_circulation = circulation;
                    }
                    else // 40% chance of major problem
                    {
                        circulation = 2;
                        initial_circulation = circulation;
                    }
                }
                #endregion

                #region cardiac variables dependent on other injury types

                #region gunshot
                if (wound_type_ID == 0) // if gunshot wound and if...
                {
                    if (hemorrhage == 2) //...heavy bleeding, then this probability of heart problem
                    {
                        a = random.Next(0, 100);
                        if (a < 10) // 10% probability of no heart problem 
                        {
                            circulation = 0;
                            initial_circulation = circulation;
                        }
                        else if (a >= 10 && a < 60) //50% chance of minor heart problem
                        {
                            circulation = 1;
                            initial_circulation = circulation;
                        }
                        else // 40% chance of major problem
                        {
                            circulation = 2;
                            initial_circulation = circulation;
                        }
                    }
                    else if (hemorrhage == 1) // light bleeding then these probabilities of heart problem
                    {
                        a = random.Next(0, 100);
                        if (a < 50) // 50% probability of no heart problem 
                        {
                            circulation = 0;
                            initial_circulation = circulation;
                        }
                        else if (a >= 50 && a < 90) //40% chance of minor heart problem
                        {
                            circulation = 1;
                            initial_circulation = circulation;
                        }
                        else // 10% chance of major problem
                        {
                            circulation = 2;
                            initial_circulation = circulation;
                        }
                    }
                }
                #endregion

                #region blunt force trauma
                else if (wound_type_ID == 1)
                {
                    if (hemorrhage == 2) //if BFT and heavy bleed then the following probability of heart problems
                    {
                        if (wound_loc_ID == 1 || wound_loc_ID == 2 || wound_loc_ID == 3 || wound_loc_ID == 12 || wound_loc_ID == 13 || wound_loc_ID == 14)
                        {
                            a = random.Next(0, 100);
                            if (a < 10) // 10% probability of no heart problem 
                            {
                                circulation = 0;
                                initial_circulation = circulation;
                            }
                            else if (a >= 10 && a < 60) //50% chance of minor heart problem
                            {
                                circulation = 1;
                                initial_circulation = circulation;
                            }
                            else // 40% chance of major problem
                            {
                                circulation = 2;
                                initial_circulation = circulation;
                            }
                        }
                        else // chances of heart problem if injury is to limbs
                        {
                            a = random.Next(0, 100);
                            if (a < 60) // 60% probability of no heart problem 
                            {
                                circulation = 0;
                                initial_circulation = circulation;
                            }
                            else if (a >= 60 && a < 90) //30% chance of minor heart problem
                            {
                                circulation = 1;
                                initial_circulation = circulation;
                            }
                            else // 10% chance of major problem
                            {
                                circulation = 2;
                                initial_circulation = circulation;
                            }
                        }
                    }
                    else if (hemorrhage < 2) // if light or no bleeding then the following probability of heart problems (also dependent on injury location)
                    {
                        if (wound_loc_ID == 1 || wound_loc_ID == 2 || wound_loc_ID == 3 || wound_loc_ID == 12 || wound_loc_ID == 13 || wound_loc_ID == 14)
                        {
                            a = random.Next(0, 100);
                            if (a < 30) // 30% probability of no heart problem 
                            {
                                circulation = 0;
                                initial_circulation = circulation;
                            }
                            else if (a >= 30 && a < 70) //40% chance of minor heart problem
                            {
                                circulation = 1;
                                initial_circulation = circulation;
                            }
                            else // 30% chance of major problem
                            {
                                circulation = 2;
                                initial_circulation = circulation;
                            }
                        }
                        else //if the imact is to limbs
                        {
                            a = random.Next(0, 100);
                            if (a < 90) // 90% probability of no heart problem 
                            {
                                circulation = 0;
                                initial_circulation = circulation;
                            }
                            else if (a >= 90 && a < 98) //8% chance of minor heart problem
                            {
                                circulation = 1;
                                initial_circulation = circulation;
                            }
                            else // 2% chance of major problem
                            {
                                circulation = 2;
                                initial_circulation = circulation;
                            }
                        }
                    }

                }
                #endregion

                #region Drowning 

                if (wound_type_ID == 2)
                {
                    a = random.Next(0, 100);
                    if (a < 50)
                    {
                        circulation = 1;
                        initial_circulation = circulation;
                    }
                    else // 50% chance of major problem
                    {
                        circulation = 2;
                        initial_circulation = circulation;
                    }
                }
                #endregion

                #region Allergy 

                if (wound_type_ID == 4)
                {
                    a = random.Next(0, 100); // 40% chance of no heart problem
                    if (a < 40)
                    {
                        circulation = 0;
                        initial_circulation = circulation;
                    }
                    else if (a >= 40 && a < 80) // 40% chance of minor heart problem
                    {
                        circulation = 1;
                        initial_circulation = circulation;
                    }
                    else
                    {
                        circulation = 2;
                        initial_circulation = circulation;
                    }
                }
                #endregion

                #endregion

                #region set breathing based on cardiac 
                if (circulation == 2)
                {
                    a = random.Next(0, 100);
                    if (a < 90)
                    {
                        breathing = 2;
                        initial_breathing = breathing;
                    }
                    else
                    {
                        breathing = 1;
                        initial_breathing = breathing;
                    }
                }
                else if (circulation == 1)
                {
                    a = random.Next(0, 100);
                    if (a < 10)
                    {
                        breathing = 0;
                        initial_breathing = breathing;
                    }
                    else if (a >= 10 && a < 90)
                    {
                        breathing = 1;
                        initial_breathing = breathing;
                    }
                    else
                    {
                        breathing = 2;
                        initial_breathing = breathing;
                    }
                }
                #endregion

                #region set consciousness based on cardiac 
                if (circulation == 2)
                {
                    a = random.Next(0, 100);
                    if (a < 90)
                    {
                        consciousness = 2;
                        initial_consciousness = consciousness;
                    }
                    else
                    {
                        consciousness = 1;
                        initial_consciousness = consciousness;
                    }
                }
                else if (circulation == 1)
                {
                    a = random.Next(0, 100);
                    if (a < 33)
                    {
                        consciousness = 1;
                        initial_consciousness = consciousness;
                    }
                    else if (a >= 33 && a < 66)
                    {
                        consciousness = 1;
                        initial_consciousness = consciousness;
                    }
                    else
                    {
                        consciousness = 2;
                        initial_consciousness = consciousness;
                    }
                }
                #endregion

                #endregion

                #region initialize airway dependent on injury type

                if (wound_type_ID < 2) // chance of bloacked airway for gunshot or bft
                {
                    a = random.Next(0, 100);
                    if (a < 50) // 50% chanve of no airway problem
                    {
                        airway = 0;
                        initial_airway = airway;
                    }
                    else if (a >= 50 && a < 80) // 30% chance of minor problem
                    {
                        airway = 1;
                        initial_airway = airway;
                    }
                    else // 20% chance of blocked airway
                    {
                        airway = 2;
                        initial_airway = airway;
                    }
                }
                else if (wound_type_ID == 2) // drowning
                {
                    a = random.Next(0, 100);
                    if (a < 20) // 20% chanve of no airway problem
                    {
                        airway = 0;
                        initial_airway = airway;
                    }
                    else if (a >= 20 && a < 50) // 30% chance of minor problem
                    {
                        airway = 1;
                        initial_airway = airway;
                    }
                    else //50% chance of major blockage (water)
                    {
                        airway = 2;
                        initial_airway = airway;
                    }
                }
                else if (wound_type_ID == 3) //heart attack
                {
                    airway = 0;
                    initial_airway = airway;
                }
                else // allergy
                {
                    a = random.Next(0, 100);
                    if (a < 40) // 10% chanve of no airway problem
                    {
                        airway = 1;
                        initial_airway = airway;
                    }
                    else if (a >= 40) // 50% chance of minor problem
                    {
                        airway = 2;
                        initial_airway = airway;
                    }
                }
                #endregion

                #region set breathing dependent on airway
                if (airway == 2)
                {
                    breathing = 2;
                    initial_breathing = breathing;
                }
                else if (airway == 1)
                {
                    a = random.Next(0, 100);
                    if (a < 2) // 2% chance of no difficulties
                    {
                        if (breathing < 1)
                        {
                            breathing = 0;
                            initial_breathing = breathing;
                        }
                    }
                    else if (a >= 2 && a < 90) //88% chance of minor brething difficulties
                    {
                        if (breathing < 2)
                        {
                            breathing = 1;
                            initial_breathing = breathing;
                        }
                    }
                    else //10% chance of severe breathing problem
                    {
                        breathing = 2;
                        initial_breathing = breathing;
                    }
                }
                #endregion

                #region initialize consiousness dependent on injury type

                if (wound_type_ID == 0)
                {
                    if (wound_loc_ID == 1 || wound_loc_ID == 2 || wound_loc_ID == 12 || wound_loc_ID == 13)
                    {
                        a = random.Next(0, 100);
                        if (a < 5) // 5% chanve of no consciousness problem
                        {
                            if (consciousness == 0)
                            {
                                consciousness = 0;
                            }
                            initial_consciousness = consciousness;
                        }
                        else if (a >= 5 && a < 40) // 35% chance of minor problem
                        {
                            if (consciousness <= 1)
                            {
                                consciousness = 1;
                            }
                            initial_consciousness = consciousness;
                        }
                        else
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 2;
                            }
                            initial_consciousness = consciousness;
                        }
                    }
                    else
                    {
                        a = random.Next(0, 100);
                        if (a < 50) // 50% chanve of no consciousness problem
                        {
                            if (consciousness == 0)
                            {
                                consciousness = 0;
                            }
                            initial_consciousness = consciousness;
                        }
                        else if (a >= 50 && a < 80) // 30% chance of minor problem
                        {
                            if (consciousness <= 1)
                            {
                                consciousness = 1;
                            }
                            initial_consciousness = consciousness;
                        }
                        else
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 2;
                            }
                            initial_consciousness = consciousness;
                        }
                    }
                }
                else if (wound_type_ID == 1)
                {
                    if (wound_loc_ID == 1 || wound_loc_ID == 2 || wound_loc_ID == 12 || wound_loc_ID == 13)
                    {
                        a = random.Next(0, 100);
                        if (a < 5) // 5% chanve of no consciousness problem
                        {
                            if (consciousness == 0)
                            {
                                consciousness = 0;
                            }
                            initial_consciousness = consciousness;
                        }
                        else if (a >= 5 && a < 50) // 45% chance of minor problem
                        {
                            if (consciousness <= 1)
                            {
                                consciousness = 1;
                            }
                            initial_consciousness = consciousness;
                        }
                        else //50% chance of serious problem
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 2;
                            }
                            initial_consciousness = consciousness;
                        }
                    }
                    else
                    {
                        a = random.Next(0, 100);
                        if (a < 30) // 30% chanve of no consciousness problem
                        {
                            if (consciousness == 0)
                            {
                                consciousness = 0;
                            }
                            initial_consciousness = consciousness;
                        }
                        else if (a >= 30 && a < 70) // 40% chance of minor problem
                        {
                            if (consciousness <= 1)
                            {
                                consciousness = 1;
                            }
                            initial_consciousness = consciousness;
                        }
                        else //30% chance of serious problem
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 2;
                            }
                            initial_consciousness = consciousness;
                        }
                    }

                }
                else if (wound_type_ID == 3)
                {
                    if (cir == 2)
                    {
                        a = random.Next(0, 100);
                        if (a < 5) // 5% chanve of no consciousness problem
                        {
                            if (consciousness == 0)
                            {
                                consciousness = 0;
                            }
                            initial_consciousness = consciousness;
                        }
                        else if (a >= 5 && a < 50) // 45% chance of minor problem
                        {
                            if (consciousness <= 1)
                            {
                                consciousness = 1;
                            }
                            initial_consciousness = consciousness;
                        }
                        else //50% chance of serious problem
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 2;
                            }
                            initial_consciousness = consciousness;
                        }
                    }
                    else if (cir == 1)
                    {
                        a = random.Next(0, 100);
                        if (a < 60) // 60% chanve of no consciousness problem
                        {
                            if (consciousness == 0)
                            {
                                consciousness = 0;
                            }
                            initial_consciousness = consciousness;
                        }
                        else if (a >= 60 && a < 90) // 30% chance of minor problem
                        {
                            if (consciousness <= 1)
                            {
                                consciousness = 1;
                            }
                            initial_consciousness = consciousness;
                        }
                        else //10% chance of serious problem
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 2;
                            }
                            initial_consciousness = consciousness;
                        }
                    }
                }
                else if (wound_type_ID == 4) //chance of consciousness problem dependent on allergy
                {
                    a = random.Next(0, 100);
                    if (a < 25) // 25% chanve of no conc problem
                    {
                        consciousness = 0;
                        initial_consciousness = consciousness;
                    }
                    else if (a >= 25 && a < 75) // 50% chance of minor problem
                    {
                        airway = 1;
                        initial_airway = airway;
                    }
                    else //25% chance of being unconscious
                    {
                        airway = 2;
                    }
                }

                if (wound_type_ID == 4) //chance of airway problem dependent on allergy
                {
                    a = random.Next(0, 100);
                    if (a < 66) // 50% chance of minor problem
                    {
                        airway = 1;
                        initial_airway = airway;
                        breathing = 1;
                        initial_breathing = breathing;
                    }
                    else //25% chance of being unconscious
                    {
                        airway = 2;
                        breathing = 2;
                        initial_breathing = breathing;
                    }
                }

                if (wound_type_ID == 4) //chance of consciousness problem dependent on allergy
                {
                    if (breathing == 2)
                    {
                        a = random.Next(0, 100);
                        if (a < 50) // 50% chanve of minor conc problem
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 1;
                            }
                        }
                        else //50% chance of being unconscious
                        {
                            consciousness = 2;
                            initial_consciousness = consciousness;
                        }
                    }
                    else if (breathing == 1)
                    {
                        a = random.Next(0, 100);
                        if (a < 10) // 10% chanve of no conc problem
                        {
                            {
                                consciousness = 0;
                                initial_consciousness = consciousness;
                            }
                        }
                        else if (a >= 10 && a < 70) // 60% chance of minor problem
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 1;
                            }
                            initial_consciousness = consciousness;
                        }
                        else //25% chance of being unconscious
                        {
                            consciousness = 2;
                            initial_consciousness = consciousness;
                        }
                    }
                }

                #endregion

                #endregion

                #endregion

                #endregion

                #region confirm descriptions in case no treatment applied


                if (airway == 0) { airway_description = "airway clear"; }
                if (airway == 1) { airway_description = "airway partially blocked"; }
                if (airway == 2) { airway_description = "airway blocked"; }
                if (consciousness == 0) { consciousness_description = "fully conscious"; }
                if (consciousness == 1) { consciousness_description = "partially conscious"; }
                if (consciousness == 2) { consciousness_description = "unconscious"; }
                if (circulation == 0) { circulation_description = "no heart problem"; }
                if (circulation == 1) { circulation_description = "heart arrhythmia/tachycardia"; }
                if (circulation == 2) { circulation_description = "heart attack"; }
                if (hemorrhage == 0) { hemorrhage_description = "no bleeding"; }
                if (hemorrhage == 1) { hemorrhage_description = "light bleeding"; }
                if (hemorrhage == 2) { hemorrhage_description = "heavy bleeding"; }
                if (breathing == 0) { breathing_description = "no breathing difficulties"; }
                if (breathing == 1) { breathing_description = "some breathing difficulties"; }
                if (breathing == 2) { breathing_description = "not breathing"; }

                #endregion

                #region Update UI
                subject = "update patient initialization info"; //check if this is ok! 
                Environment_control_function();
                #endregion

                #region tell EMS agent that patient status is available and treatment required
                // send message to EMS agnet regarding intial status available
                from = "patient";
                subject = "Initial status available";
                Ems();
                #endregion
            }
            #endregion
            else if (subject == "treatment")
            {
                Treatment(action);
            }
            else if (subject == "patient_degradation")
            {

                #region Patient degradation logic

                #region increase hemorrhage degradation if initial hemorrhage score was 2
                if (initial_hemorrhage == 2)
                {
                    hem_deg_prob_longtime -= (20 * patient_hem_instability_score); // change 30 to a more random variable later
                    hem_deg_prob_shorttime -= (10 * patient_hem_instability_score); // same change later

                    if (hem_deg_prob_longtime < 50) //lowest hem deg prob can go (remember this is the likelyhood the patient will not change
                    {
                        hem_deg_prob_longtime = 50;
                    }
                    if (hem_deg_prob_shorttime < 60)
                    {
                        hem_deg_prob_shorttime = 60;
                    }
                    if (hem_deg_prob_longtime > 90) //lowest hem deg prob can go (remember this is the likelyhood the patient will not change
                    {
                        hem_deg_prob_longtime = 90;
                    }
                    if (hem_deg_prob_shorttime > 99)
                    {
                        hem_deg_prob_shorttime = 99;
                    }
                }
                #endregion

                //#region apply patient degradation based on rules
                #region hemorrhage degradation - long term and short term probabilities
                Random chr_rnd = new Random();
                if (initial_hemorrhage == 2)
                    if (hemorrhage == 1 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
                    {
                        int hem_rand = chr_rnd.Next(1, 101);
                        if (hem_rand > hem_deg_prob_longtime)
                        {
                            hemorrhage = 2;
                            treatment_timeline.Items.Add("Patient bleeding worsened - now heavy bleeding");

                            patient_hem_instability_score += 1;
                            hem_deg_prob1to2 = 100 - hem_deg_prob_longtime;
                        }
                        else
                        {
                            hemorrhage = hemorrhage;
                            treatment_timeline.Items.Add("still light bleeding");
                            patient_hem_instability_score -= 1;
                            hem_deg_prob1to1 = hem_deg_prob_longtime;
                        }
                    }
                    else if (hemorrhage == 1 && stopwatch_hem.Elapsed.TotalSeconds < 60)
                    {
                        int hem_rand = chr_rnd.Next(1, 101);
                        if (hem_rand > hem_deg_prob_shorttime)
                        {
                            hemorrhage = 2;
                            treatment_timeline.Items.Add("Patient bleeding worsened - now heavy bleeding");
                            patient_hem_instability_score += 1;
                            hem_deg_prob1to2 = 100 - hem_deg_prob_shorttime;
                        }
                        else
                        {
                            hemorrhage = hemorrhage;
                            treatment_timeline.Items.Add("still light bleeding");
                            patient_hem_instability_score -= 1;
                            hem_deg_prob1to1 = hem_deg_prob_shorttime;
                        }
                    }
                    else if (initial_hemorrhage == 1)
                        if (hemorrhage == 0 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
                        {
                            int hem_rand = chr_rnd.Next(1, 101);
                            if (hem_rand > hem_deg_prob_longtime)
                            {
                                hemorrhage = 1;
                                treatment_timeline.Items.Add("Patient bleeding worsened - now light bleeding");
                                patient_hem_instability_score += 1;
                                hem_deg_prob0to1 = hem_rand - hem_deg_prob_longtime;
                            }
                            else
                            {
                                hemorrhage = hemorrhage;
                                treatment_timeline.Items.Add("bleeding No worse");
                                patient_hem_instability_score -= 1;
                                hem_deg_prob0to0 = hem_rand;
                            }
                        }
                        else if (hemorrhage == 0 && stopwatch_hem.Elapsed.TotalSeconds < 60)
                        {
                            int hem_rand = chr_rnd.Next(1, 101);
                            if (hem_rand > hem_deg_prob_shorttime)
                            {
                                hemorrhage = 1;
                                treatment_timeline.Items.Add("Patient bleeding worsened - now light bleeding");
                                patient_hem_instability_score += 1;
                                hem_deg_prob0to1 = hem_rand- hem_deg_prob_shorttime;
                            }
                            else
                            {
                                hemorrhage = hemorrhage;
                                treatment_timeline.Items.Add("bleeding No worse");
                                patient_hem_instability_score -= 1;
                                hem_deg_prob0to0 = hem_rand;
                            }
                        }
                        else { hemorrhage = 0; }

                #endregion

                #region circulation degradation  due to hemmorhage- long term and short term probabilities

                hemcirc0to1_deg_prob_longtime += (5 * patient_circ_instability_score);
                hemcirc0to2_deg_prob_longtime += (5 * patient_circ_instability_score);
                hemcirc0to1_deg_prob_shorttime += (5 * patient_circ_instability_score);
                hemcirc0to2_deg_prob_shorttime += (5 * patient_circ_instability_score);

                if (hemcirc0to1_deg_prob_shorttime < 20) { hemcirc0to1_deg_prob_shorttime = 20; } // set minimum and maximum thresholds
                if (hemcirc0to2_deg_prob_shorttime < 18) { hemcirc0to2_deg_prob_shorttime = 18; }
                if (hemcirc0to1_deg_prob_longtime < 20) { hemcirc0to1_deg_prob_longtime = 20; }
                if (hemcirc0to2_deg_prob_longtime < 20) { hemcirc0to2_deg_prob_longtime = 20; }
                if (hemcirc0to2_deg_prob_longtime > 50) { hemcon0to1_deg_prob_longtime = 100 - hemcon0to2_deg_prob_longtime; } //probability of severe goes up
                if (hemcirc0to2_deg_prob_longtime > 75) { hemcirc0to2_deg_prob_longtime = 75; }
                if (hemcirc0to2_deg_prob_longtime == 75) { hemcirc0to1_deg_prob_longtime = 25; }

                Random chr_rnd_circ = new Random();
                if (hemorrhage == 2 && circulation == 0 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
                {
                    int hem_rand = chr_rnd_circ.Next(1, 101);
                    if (hem_rand < hemcirc0to1_deg_prob_longtime)
                    {
                        circulation = 1;
                        treatment_timeline.Items.Add("Patient heart worsened - now experiencing tachycardia/bradycardia");
                        patient_circ_instability_score += 1;
                    }
                    else if (hem_rand > hemcirc0to1_deg_prob_longtime && hem_rand <= (hemcirc0to2_deg_prob_longtime + hemcirc0to1_deg_prob_longtime))
                    {
                        circulation = 2;
                        treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                        patient_circ_instability_score += 2;
                    }
                    else
                    {
                        patient_circ_instability_score -= 1;
                        circulation = circulation;
                        treatment_timeline.Items.Add("heart no worse");
                    }
                }
                else if (hemorrhage == 2 && circulation == 0 && stopwatch_hem.Elapsed.TotalSeconds < 60)
                {
                    int hem_rand = chr_rnd_circ.Next(1, 101);
                    if (hem_rand < hemcirc0to1_deg_prob_shorttime)
                    {
                        circulation = 1;
                        treatment_timeline.Items.Add("Patient heart worsened - now experiencing tachycardia/bradycardia");
                        patient_circ_instability_score += 1;
                    }
                    else if (hem_rand > hemcirc0to1_deg_prob_shorttime && hem_rand <= (hemcirc0to2_deg_prob_shorttime + hemcirc0to1_deg_prob_shorttime))
                    {
                        circulation = 2;
                        treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                        patient_circ_instability_score += 2;
                    }
                    else
                    {
                        circulation = circulation;
                        treatment_timeline.Items.Add("heart no worse");
                        patient_circ_instability_score -= 1;
                    }
                }
                else if (hemorrhage == 2 && circulation == 1 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
                {
                    int hem_rand = chr_rnd_circ.Next(1, 101);
                    if (hem_rand > hemcirc1to2_deg_prob_longtime)
                    {
                        circulation = 2;
                        treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                        patient_circ_instability_score += 1;
                    }
                    else
                    {
                        circulation = circulation;
                        treatment_timeline.Items.Add("heart no worse");
                        patient_circ_instability_score -= 1;
                    }
                }
                else if (hemorrhage == 2 && circulation == 1 && stopwatch_hem.Elapsed.TotalSeconds < 60)
                {
                    int hem_rand = chr_rnd_circ.Next(1, 101);
                    if (hem_rand > hemcirc1to2_deg_prob_shorttime)
                    {
                        circulation = 2;
                        treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                        patient_circ_instability_score += 1;
                    }
                    else
                    {
                        circulation = circulation;
                        treatment_timeline.Items.Add("heart no worse");
                        patient_circ_instability_score -= 1;
                    }
                }

                #endregion

                #region circulation degradation  long term and short term probabilities

                circ0to1if1_deg_prob_longtime += (5 * patient_circ_instability_score);
                circ0to2if1_deg_prob_longtime += (5 * patient_circ_instability_score);
                circ1to2if1_deg_prob_longtime += (5 * patient_circ_instability_score);
                circ0to1if2_deg_prob_longtime += (5 * patient_circ_instability_score);
                circ0to2if2_deg_prob_longtime += (5 * patient_circ_instability_score);
                circ1to2if2_deg_prob_longtime += (5 * patient_circ_instability_score);
                circ0to1if1_deg_prob_shorttime += (5 * patient_circ_instability_score);
                circ0to2if1_deg_prob_shorttime += (5 * patient_circ_instability_score);
                circ1to2if1_deg_prob_shorttime += (5 * patient_circ_instability_score);
                circ0to1if2_deg_prob_shorttime += (5 * patient_circ_instability_score);
                circ0to2if2_deg_prob_shorttime += (5 * patient_circ_instability_score);
                circ1to2if2_deg_prob_shorttime += (5 * patient_circ_instability_score);



                if (circ0to1if1_deg_prob_longtime < 40) { circ0to1if1_deg_prob_longtime = 40; }
                if (circ0to1if1_deg_prob_longtime > 60) { circ0to1if1_deg_prob_longtime = 60; }
                if (circ0to2if1_deg_prob_longtime < 20) { circ0to2if1_deg_prob_longtime = 20; }
                if (circ0to2if1_deg_prob_longtime > 40) { circ0to2if1_deg_prob_longtime = 40; }
                if (circ1to2if1_deg_prob_longtime < 30) { circ1to2if1_deg_prob_longtime = 30; }
                if (circ1to2if1_deg_prob_longtime > 50) { circ1to2if1_deg_prob_longtime = 50; }
                if (circ0to1if2_deg_prob_longtime < 50) { circ0to1if2_deg_prob_longtime = 50; }
                if (circ0to1if2_deg_prob_longtime > 70) { circ0to1if2_deg_prob_longtime = 70; }
                if (circ0to2if2_deg_prob_longtime < 25) { circ0to2if2_deg_prob_longtime = 25; }
                if (circ0to2if2_deg_prob_longtime > 45) { circ0to2if2_deg_prob_longtime = 45; }
                if (circ1to2if2_deg_prob_longtime < 50) { circ1to2if2_deg_prob_longtime = 50; }
                if (circ1to2if2_deg_prob_longtime > 70) { circ1to2if2_deg_prob_longtime = 70; }

                if ((circ0to1if2_deg_prob_longtime + circ0to2if2_deg_prob_longtime) > 100) { circ0to1if2_deg_prob_longtime = 100 - circ0to2if2_deg_prob_longtime; }

                if (circ0to1if1_deg_prob_shorttime < 30) { circ0to1if1_deg_prob_shorttime = 30; }
                if (circ0to1if1_deg_prob_shorttime > 50) { circ0to1if1_deg_prob_shorttime = 50; }
                if (circ0to2if1_deg_prob_shorttime < 5) { circ0to2if1_deg_prob_shorttime = 5; }
                if (circ0to2if1_deg_prob_shorttime > 25) { circ0to2if1_deg_prob_shorttime = 25; }
                if (circ1to2if1_deg_prob_shorttime < 15) { circ1to2if1_deg_prob_shorttime = 15; }
                if (circ1to2if1_deg_prob_shorttime > 35) { circ1to2if1_deg_prob_shorttime = 35; }
                if (circ0to1if2_deg_prob_shorttime < 40) { circ0to1if2_deg_prob_shorttime = 40; }
                if (circ0to1if2_deg_prob_shorttime > 60) { circ0to1if2_deg_prob_shorttime = 60; }
                if (circ0to2if2_deg_prob_shorttime < 15) { circ0to2if2_deg_prob_shorttime = 15; }
                if (circ0to2if2_deg_prob_shorttime > 35) { circ0to2if2_deg_prob_shorttime = 35; }
                if (circ1to2if2_deg_prob_shorttime < 30) { circ1to2if2_deg_prob_shorttime = 30; }
                if (circ1to2if2_deg_prob_shorttime > 50) { circ1to2if2_deg_prob_shorttime = 50; }

                Random chr_rnd_heart = new Random();
                if (initial_circulation == 1 && circulation == 0 && stopwatch_cir.Elapsed.TotalSeconds >= 60)
                {
                    int hem_rand = chr_rnd_heart.Next(1, 101);
                    if (hem_rand < circ0to1if1_deg_prob_longtime)
                    {
                        circulation = 1;
                        treatment_timeline.Items.Add("Patient heart worsened - now experiencing tachycardia/bradycardia");
                        patient_circ_instability_score += 1;
                    }
                    else if (hem_rand > circ0to1if1_deg_prob_longtime && hem_rand <= circ0to2if1_deg_prob_longtime)
                    {
                        circulation = 2;
                        treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                        patient_circ_instability_score += 2;
                    }
                    else
                    {
                        circulation = circulation;
                        treatment_timeline.Items.Add("heart no worse");
                        patient_circ_instability_score -= 1;
                    }
                }
                else if (initial_circulation == 1 && circulation == 0 && stopwatch_cir.Elapsed.TotalSeconds < 60)
                {
                    int hem_rand = chr_rnd_heart.Next(1, 101);
                    if (hem_rand < circ0to1if1_deg_prob_shorttime)
                    {
                        circulation = 1;
                        treatment_timeline.Items.Add("Patient heart worsened - now experiencing tachycardia/bradycardia");
                        patient_circ_instability_score += 1;
                    }
                    else if (hem_rand > circ0to1if1_deg_prob_shorttime && hem_rand <= circ0to2if1_deg_prob_shorttime)
                    {
                        circulation = 2;
                        treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                        patient_circ_instability_score += 2;
                    }
                    else
                    {
                        circulation = circulation;
                        treatment_timeline.Items.Add("heart no worse");
                        patient_circ_instability_score -= 1;
                    }
                }
                else if (initial_circulation == 2 && circulation == 0 && stopwatch_cir.Elapsed.TotalSeconds >= 60)
                {
                    int hem_rand = chr_rnd_heart.Next(1, 101);
                    if (hem_rand < circ0to1if2_deg_prob_longtime)
                    {
                        circulation = 1;
                        treatment_timeline.Items.Add("Patient heart worsened - now experiencing tachycardia/bradycardia");
                        patient_circ_instability_score += 1;
                    }
                    else if (hem_rand > circ0to1if2_deg_prob_longtime && hem_rand <= circ0to2if2_deg_prob_longtime)
                    {
                        circulation = 2;
                        treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                        patient_circ_instability_score += 2;
                    }
                    else
                    {
                        circulation = circulation;
                        treatment_timeline.Items.Add("heart no worse");
                        patient_circ_instability_score -= 1;
                    }
                }
                else if (initial_circulation == 2 && circulation == 0 && stopwatch_hem.Elapsed.TotalSeconds < 60)
                {
                    int hem_rand = chr_rnd_heart.Next(1, 101);
                    if (hem_rand < circ0to1if2_deg_prob_shorttime)
                    {
                        circulation = 1;
                        treatment_timeline.Items.Add("Patient heart worsened - now experiencing tachycardia/bradycardia");
                        patient_circ_instability_score += 1;
                    }
                    else if (hem_rand > circ0to1if2_deg_prob_shorttime && hem_rand <= circ0to2if2_deg_prob_shorttime)
                    {
                        circulation = 2;
                        treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                        patient_circ_instability_score += 2;
                    }
                    else
                    {
                        circulation = circulation;
                        treatment_timeline.Items.Add("heart no worse");
                        patient_circ_instability_score -= 1;
                    }
                }
                else if (initial_circulation == 1 && circulation == 1 && stopwatch_cir.Elapsed.TotalSeconds >= 60)
                {
                    int hem_rand = chr_rnd_heart.Next(1, 101);
                    if (hem_rand < circ1to2if1_deg_prob_longtime)
                    {
                        circulation = 2;
                        treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                        patient_circ_instability_score += 1;
                    }
                    else
                    {
                        circulation = circulation;
                        treatment_timeline.Items.Add("heart no worse");
                        patient_circ_instability_score -= 1;
                    }
                }
                else if (initial_circulation == 1 && circulation == 1 && stopwatch_cir.Elapsed.TotalSeconds < 60)
                {
                    int hem_rand = chr_rnd_heart.Next(1, 101);
                    if (hem_rand < circ1to2if1_deg_prob_shorttime)
                    {
                        circulation = 2;
                        treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                        patient_circ_instability_score += 1;
                    }
                    else
                    {
                        circulation = circulation;
                        treatment_timeline.Items.Add("heart no worse");
                        patient_circ_instability_score -= 1;
                    }
                }
                else if (initial_circulation == 2 && circulation == 1 && stopwatch_cir.Elapsed.TotalSeconds >= 60)
                {
                    int hem_rand = chr_rnd_heart.Next(1, 101);
                    if (hem_rand < circ1to2if2_deg_prob_longtime)
                    {
                        circulation = 2;
                        treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                        patient_circ_instability_score += 1;
                    }
                    else
                    {
                        circulation = circulation;
                        treatment_timeline.Items.Add("heart no worse");
                        patient_circ_instability_score -= 1;
                    }
                }
                else if (initial_circulation == 2 && circulation == 1 && stopwatch_hem.Elapsed.TotalSeconds < 60)
                {
                    int hem_rand = chr_rnd_heart.Next(1, 101);
                    if (hem_rand < circ1to2if2_deg_prob_shorttime)
                    {
                        circulation = 2;
                        treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                        patient_circ_instability_score += 1;
                    }
                    else
                    {
                        circulation = circulation;
                        treatment_timeline.Items.Add("heart no worse");
                        patient_circ_instability_score -= 1;
                    }
                }

                #endregion

                #region consciousness degradation  due to breathing- long term and short term probabilities

                brecon0to1_deg_prob_longtime += (5 * patient_con_instability_score);
                brecon0to2_deg_prob_longtime += (5 * patient_con_instability_score);
                brecon0to1_deg_prob_shorttime += (5 * patient_con_instability_score);
                brecon0to2_deg_prob_shorttime += (5 * patient_con_instability_score);
                brecon1to2_deg_prob_shorttime += (5 * patient_con_instability_score);
                brecon1to2_deg_prob_longtime += (patient_con_instability_score);

                if (brecon0to1_deg_prob_shorttime < 20) { brecon0to1_deg_prob_shorttime = 20; } // set minimum and maximum thresholds
                if (brecon0to2_deg_prob_shorttime < 18) { brecon0to2_deg_prob_shorttime = 18; }
                if (brecon0to1_deg_prob_longtime < 20) { brecon0to1_deg_prob_longtime = 20; }
                if (brecon0to2_deg_prob_longtime < 20) { brecon0to2_deg_prob_longtime = 20; }
                if (brecon0to2_deg_prob_longtime > 50) { brecon0to1_deg_prob_longtime = 100 - brecon0to2_deg_prob_longtime; } //probability of severe goes up
                if (brecon0to2_deg_prob_longtime > 75) { brecon0to2_deg_prob_longtime = 75; }
                if (brecon0to2_deg_prob_longtime == 75) { brecon0to1_deg_prob_longtime = 25; }

                Random chr_rnd_conc_bre = new Random();
                if (breathing == 2 && consciousness == 0 && stopwatch_bre.Elapsed.TotalSeconds >= 60)
                {
                    int brecon_rand = chr_rnd_conc_bre.Next(1, 101);
                    if (brecon_rand < brecon0to1_deg_prob_longtime)
                    {
                        consciousness = 1; //change to 1 after testing check
                        treatment_timeline.Items.Add("Patient consciousness worsened - now semi-conscious");
                        patient_con_instability_score += 1;
                    }
                    else if (brecon_rand > brecon0to1_deg_prob_longtime && brecon_rand <= (brecon0to2_deg_prob_longtime + brecon0to1_deg_prob_longtime))
                    {
                        consciousness = 2; //change to 1 after testing check
                        treatment_timeline.Items.Add("Patient consciousness worsened - now unconscious");
                        patient_con_instability_score += 2;
                    }
                    else
                    {
                        consciousness = consciousness;
                        treatment_timeline.Items.Add("conc No worse");
                        patient_con_instability_score -= 1;
                    }
                }
                else if (breathing == 2 && consciousness == 0 && stopwatch_bre.Elapsed.TotalSeconds < 60)
                {
                    int brecon_rand = chr_rnd_conc_bre.Next(1, 101);
                    if (brecon_rand < brecon0to1_deg_prob_shorttime)
                    {
                        consciousness = 1;
                        treatment_timeline.Items.Add("Patient consciousness worsened - now semi-conscious");
                        patient_con_instability_score += 1;
                    }
                    else if (brecon_rand > brecon0to1_deg_prob_shorttime && brecon_rand <= (brecon0to2_deg_prob_shorttime + brecon0to1_deg_prob_shorttime))
                    {
                        consciousness = 2;
                        treatment_timeline.Items.Add("Patient consciousness worsened - now unconscious");
                        patient_instability_score += 2;
                    }
                    else
                    {
                        consciousness = consciousness;
                        treatment_timeline.Items.Add("conc No worse");
                        patient_instability_score -= 1;
                    }
                }
                else if (breathing == 2 && consciousness == 1 && stopwatch_bre.Elapsed.TotalSeconds >= 60)
                {
                    int brecon_rand = chr_rnd_conc_bre.Next(1, 101);
                    if (brecon_rand > brecon1to2_deg_prob_longtime)
                    {
                        consciousness = 2; //change to 1 after testing check
                        treatment_timeline.Items.Add("Patient consciousness worsened - now unconscious");
                        patient_instability_score += 1;
                    }
                    else
                    {
                        consciousness = consciousness;
                        treatment_timeline.Items.Add("conc No worse");
                        patient_con_instability_score -= 1;
                    }
                }
                else if (breathing == 2 && consciousness == 1 && stopwatch_bre.Elapsed.TotalSeconds < 60)
                {
                    int brecon_rand = chr_rnd_conc_bre.Next(1, 101);
                    if (brecon_rand > brecon1to2_deg_prob_shorttime)
                    {
                        consciousness = 2; //change to 1 after testing check
                        treatment_timeline.Items.Add("Patient consciousness worsened - now unconscious");
                        patient_con_instability_score += 1;
                    }
                    else
                    {
                        consciousness = consciousness;
                        treatment_timeline.Items.Add("conc No worse");
                        patient_con_instability_score -= 1;
                    }
                }
































                subject = "update UI";
                Environment_control_function();

                #endregion

                #region consciousness degradation  due to hemmorhage- long term and short term probabilities

                hemcon0to1_deg_prob_longtime += (5 * patient_con_instability_score);
                hemcon0to2_deg_prob_longtime += (5 * patient_con_instability_score);
                hemcon0to1_deg_prob_shorttime += (5 * patient_con_instability_score);
                hemcon0to2_deg_prob_shorttime += (5 * patient_con_instability_score);
                hemcon1to2_deg_prob_shorttime += (5 * patient_con_instability_score);
                hemcon1to2_deg_prob_longtime += (5 * patient_con_instability_score);

                if (hemcon0to1_deg_prob_shorttime < 20) { hemcon0to1_deg_prob_shorttime = 20; } // set minimum and maximum thresholds
                if (hemcon0to2_deg_prob_shorttime < 18) { hemcon0to2_deg_prob_shorttime = 18; }
                if (hemcon0to1_deg_prob_longtime < 20) { hemcon0to1_deg_prob_longtime = 20; }
                if (hemcon0to2_deg_prob_longtime < 20) { hemcon0to2_deg_prob_longtime = 20; }

                if (hemcon0to1_deg_prob_longtime > 50) { hemcon0to1_deg_prob_longtime = 50; }
                if (hemcon0to2_deg_prob_longtime > 50) { hemcon0to1_deg_prob_longtime = 100 - hemcon0to2_deg_prob_longtime; } //probability of severe goes up
                if (hemcon0to2_deg_prob_longtime > 75) { hemcon0to2_deg_prob_longtime = 75; }
                if (hemcon0to2_deg_prob_longtime == 75) { hemcon0to1_deg_prob_longtime = 25; }
                if (hemcon1to2_deg_prob_shorttime > 40) { hemcon1to2_deg_prob_shorttime = 40; }
                if (hemcon1to2_deg_prob_longtime > 95) { hemcon1to2_deg_prob_longtime = 95; }

                Random chr_rnd_conc = new Random();
                if (hemorrhage == 2 && consciousness == 0 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
                {
                    int hem_rand = chr_rnd_conc.Next(1, 101);
                    if (hem_rand < hemcon0to1_deg_prob_longtime)
                    {
                        consciousness = 1; //change to 1 after testing check
                        treatment_timeline.Items.Add("Patient consciousness worsened - now semi-conscious");
                        patient_con_instability_score += 1;
                    }
                    else if (hem_rand > hemcon0to1_deg_prob_longtime && hem_rand <= (hemcon0to2_deg_prob_longtime + hemcon0to1_deg_prob_longtime))
                    {
                        consciousness = 2; //change to 1 after testing check
                        treatment_timeline.Items.Add("Patient consciousness worsened - now unconscious");
                        patient_con_instability_score += 2;
                    }
                    else
                    {
                        consciousness = consciousness;
                        treatment_timeline.Items.Add("conc No worse");
                        patient_con_instability_score -= 1;
                    }
                }
                else if (hemorrhage == 2 && consciousness == 0 && stopwatch_hem.Elapsed.TotalSeconds < 60)
                {
                    int hem_rand = chr_rnd_conc.Next(1, 101);
                    if (hem_rand < hemcon0to1_deg_prob_shorttime)
                    {
                        consciousness = 1;
                        treatment_timeline.Items.Add("Patient consciousness worsened - now semi-conscious");
                        patient_con_instability_score += 1;
                    }
                    else if (hem_rand > hemcon0to1_deg_prob_shorttime && hem_rand <= (hemcon0to2_deg_prob_shorttime + hemcon0to1_deg_prob_shorttime))
                    {
                        consciousness = 2;
                        treatment_timeline.Items.Add("Patient consciousness worsened - now unconscious");
                        patient_instability_score += 2;
                    }
                    else
                    {
                        consciousness = consciousness;
                        treatment_timeline.Items.Add("conc No worse");
                        patient_instability_score -= 1;
                    }
                }
                else if (hemorrhage == 2 && consciousness == 1 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
                {
                    int hem_rand = chr_rnd_conc.Next(1, 101);
                    if (hem_rand > hemcon1to2_deg_prob_longtime)
                    {
                        consciousness = 2; //change to 1 after testing check
                        treatment_timeline.Items.Add("Patient consciousness worsened - now unconscious");
                        patient_instability_score += 1;
                    }
                    else
                    {
                        consciousness = consciousness;
                        treatment_timeline.Items.Add("conc No worse");
                        patient_con_instability_score -= 1;
                    }
                }
                else if (hemorrhage == 2 && consciousness == 1 && stopwatch_hem.Elapsed.TotalSeconds < 60)
                {
                    int hem_rand = chr_rnd_conc.Next(1, 101);
                    if (hem_rand > hemcon1to2_deg_prob_shorttime)
                    {
                        consciousness = 2; //change to 1 after testing check
                        treatment_timeline.Items.Add("Patient consciousness worsened - now unconscious");
                        patient_con_instability_score += 1;
                    }
                    else
                    {
                        consciousness = consciousness;
                        treatment_timeline.Items.Add("conc no worse");
                        patient_con_instability_score -= 1;
                    }
                }

                #endregion

                #region airway degradation if allergic reaction

                airway_close_prob0to1 += (10 * patient_air_instability_score);
                airway_close_prob1to2 += (10 * patient_air_instability_score);

                if (airway_close_prob0to1 < 0) { airway_close_prob0to1 = 0; }
                if (airway_close_prob1to2 < 0) { airway_close_prob1to2 = 0; }

                if (wound_type == "Allergic Reaction")
                {
                    Random chr_rnd_air = new Random();
                    if (airway == 1 && stopwatch_air.Elapsed.TotalSeconds < 30)
                    {
                        int air_rand = chr_rnd_air.Next(1, 101);
                        if (air_rand < airway_close_prob1to2)
                        {
                            airway = 2;
                            treatment_timeline.Items.Add("Airway closed - treatment required");
                            patient_air_instability_score += 1;
                        }
                        else
                        {
                            airway = airway;
                            treatment_timeline.Items.Add("Airway no worse");
                        }
                    }
                    else if (airway == 1 && stopwatch_air.Elapsed.TotalSeconds > 30)
                    {
                        int air_rand = chr_rnd_air.Next(1, 101);
                        if (air_rand < airway_close_prob1to2)
                        {
                            airway = 2;
                            treatment_timeline.Items.Add("Airway closed - treatment required");
                            patient_air_instability_score += 1;
                        }
                        else
                        {
                            airway = airway;
                            treatment_timeline.Items.Add("Airway no worse");
                            patient_air_instability_score += 1;

                        }
                    }
                    else if (airway == 0 && stopwatch_air.Elapsed.TotalSeconds < 30)
                    {
                        int air_rand = chr_rnd_air.Next(1, 101);
                        if (air_rand < airway_close_prob0to1)
                        {
                            airway = 1;
                            treatment_timeline.Items.Add("Airway closing due to inflamation");
                            patient_air_instability_score += 1;
                        }
                        else
                        {
                            airway = airway;
                            treatment_timeline.Items.Add("Airway no worse");
                        }
                    }
                    else if (airway == 0 && stopwatch_air.Elapsed.TotalSeconds > 30)
                    {
                        int air_rand = chr_rnd_air.Next(1, 101);
                        if (air_rand < airway_close_prob0to1)
                        {
                            airway = 1;
                            treatment_timeline.Items.Add("Airway closing due to inflamation");
                            patient_air_instability_score += 1;
                        }
                        else
                        {
                            airway = airway;
                            treatment_timeline.Items.Add("Airway no worse");
                            patient_air_instability_score += 1;

                        }
                    }
                }



                #endregion

                #region breathing degradation due to airway (very likely)

                breair_prob0to1 += (5 * patient_bre_instability_score);
                breair_prob1to2 += (5 * patient_bre_instability_score);
                breair_prob0to2 += (5 * patient_bre_instability_score); // if airway is at 2, probability breathing will go to 2 - the 'else' is breathing will go to 1  -cannot be zero if airway is 2


                if (breair_prob0to2 > 100) { breair_prob0to2 = 100; }
                if (breair_prob1to2 > 100) { breair_prob1to2 = 100; }
                if (breair_prob0to1 > 100) { breair_prob0to1 = 100; }

                Random chr_rnd_breair = new Random();
                if (airway == 1 && breathing == 0 && stopwatch_air.Elapsed.TotalSeconds > 10)
                {
                    int breair_rand = chr_rnd_breair.Next(1, 101);
                    if (breair_rand < breair_prob0to1)
                    {
                        breathing = 1;
                        treatment_timeline.Items.Add("breathing weakening");
                        patient_bre_instability_score += 1;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("breathing ok despite some airway inflammation");
                    }
                }
                else if (airway == 2 && breathing == 1 && stopwatch_air.Elapsed.TotalSeconds > 10)
                {
                    int breair_rand = chr_rnd_breair.Next(1, 101);
                    if (breair_rand < breair_prob1to2)
                    {
                        breathing = 2;
                        stopwatch_bre.Reset();
                        treatment_timeline.Items.Add("breathing stopped");
                        patient_bre_instability_score += 1;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("breathing still weak");
                        patient_bre_instability_score += 1;
                        stopwatch_bre.Reset();
                    }
                }
                else if (airway == 2 && breathing == 0 && stopwatch_air.Elapsed.TotalSeconds > 10)
                {
                    int breair_rand = chr_rnd_breair.Next(1, 101);
                    if (breair_rand < breair_prob0to2)
                    {
                        breathing = 2;

                        treatment_timeline.Items.Add("breathing stopped");
                        patient_bre_instability_score += 1;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("Airway closing due to inflamation");
                        patient_bre_instability_score += 1;
                        breathing = 1;
                        stopwatch_bre.Reset();
                    }
                }



                #endregion

                #region breathing degradation

                bre_prob0to1if0 += (3 * patient_bre_instability_score);
                bre_prob1to2if0 += (3 * patient_bre_instability_score);
                bre_prob0to2if0 += (1 * patient_bre_instability_score); // if airway is at 2, probability breathing will go to 2 - the 'else' is breathing will go to 1  -cannot be zero if airway is 2

                bre_prob0to1if1 += (3 * patient_bre_instability_score);
                bre_prob1to2if1 += (3 * patient_bre_instability_score);
                bre_prob0to2if1 += (1 * patient_bre_instability_score);
                bre_prob0to1if2 += (5 * patient_bre_instability_score);
                bre_prob1to2if2 += (5 * patient_bre_instability_score);
                bre_prob0to2if2 += (3 * patient_bre_instability_score);

                if (bre_prob0to2if0 < 1) { bre_prob0to2if0 = 1; }
                if (bre_prob1to2if0 < 1) { bre_prob1to2if0 = 1; }
                if (bre_prob0to1if0 < 1) { bre_prob0to1if0 = 1; }
                if (bre_prob0to2if1 < 1) { bre_prob0to2if1 = 1; }
                if (bre_prob1to2if1 < 1) { bre_prob1to2if1 = 1; }
                if (bre_prob0to1if1 < 1) { bre_prob0to1if1 = 1; }
                if (bre_prob0to2if2 < 10) { bre_prob0to2if2 = 10; }
                if (bre_prob1to2if2 < 20) { bre_prob1to2if2 = 20; }
                if (bre_prob0to1if2 < 20) { bre_prob0to1if2 = 20; }

                if (bre_prob0to2if0 >50) { bre_prob0to2if0 = 50; }
                if (bre_prob1to2if0 > 50) { bre_prob1to2if0 = 50; }
                if (bre_prob0to1if0 > 50) { bre_prob0to1if0 = 50; }
                if (bre_prob0to2if1 > 50) { bre_prob0to2if1 = 50; }
                if (bre_prob1to2if1 > 50) { bre_prob1to2if1 = 50; }
                if (bre_prob0to1if1 > 50) { bre_prob0to1if1 = 50; }
                if (bre_prob0to2if2 > 50) { bre_prob0to2if2 = 50; }
                if (bre_prob1to2if2 > 50) { bre_prob1to2if2 = 50; }
                if (bre_prob0to1if2 > 50) { bre_prob0to1if2 = 50; }

                Random chr_rnd_bre = new Random();
                if (airway == 0 && initial_breathing == 1 && breathing == 0)
                {
                    int bre_rand = chr_rnd_bre.Next(1, 101);
                    if (bre_rand < bre_prob0to1if1)
                    {
                        breathing = 1;
                        treatment_timeline.Items.Add("breathing weakening");
                        patient_bre_instability_score += 1;
                        breath_deg_prob0to1 = bre_prob0to1if1;
                    }
                    else if (bre_rand > bre_prob0to1if1 && bre_rand < (bre_prob0to2if1+ bre_prob0to1if1))
                    {
                        breathing = 2;
                        patient_bre_instability_score += 1;
                        treatment_timeline.Items.Add("breathing stopped");
                        breath_deg_prob0to2 = bre_prob0to2if1 - bre_prob0to1if1;
                    }
                    else 
                    {
                        breathing = 0;
                        patient_bre_instability_score -= 2;
                        stopwatch_bre.Reset();
                        breath_deg_prob0to0 = 100 - (bre_prob0to2if1 + bre_prob0to1if1);
                    }
                }
                else if (airway == 0 && initial_breathing == 1 && breathing == 1 )
                {
                    int bre_rand = chr_rnd_bre.Next(1, 101);
                    if (bre_rand < bre_prob1to2if1)
                    {
                        breathing = 2;

                        treatment_timeline.Items.Add("breathing stopped");
                        patient_bre_instability_score += 1;
                        breath_deg_prob1to2 = bre_prob1to2if1;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("breathing still weak");
                        breath_deg_prob1to1 = 100 - bre_prob1to2if1;
                    }
                }
                else if (airway == 0 && initial_breathing == 2 && breathing == 0)
                {
                    int bre_rand = chr_rnd_bre.Next(1, 101);
                    if (bre_rand < bre_prob0to1if2)
                    {
                        breathing = 1;
                        treatment_timeline.Items.Add("breathing weakening");
                        patient_bre_instability_score += 1;
                        breath_deg_prob0to1 = bre_prob0to1if2;
                    }
                    else if (bre_rand > bre_prob0to1if2 && bre_rand < (bre_prob0to2if2 + bre_prob0to1if2))
                    {
                        breathing = 2;
                        patient_bre_instability_score += 1;
                        treatment_timeline.Items.Add("breathing stopped");
                        breath_deg_prob0to2 = bre_prob0to2if2 - bre_prob0to1if2;
                    }
                    else
                    {
                        breathing = 0;
                        patient_bre_instability_score -= 2;
                        stopwatch_bre.Reset();
                        breath_deg_prob0to0 = 100 - (bre_prob0to2if2 + bre_prob0to1if2);
                    }
                }
                else if (airway == 0 && initial_breathing == 2 && breathing == 1)
                {
                    int bre_rand = chr_rnd_bre.Next(1, 101);
                    if (bre_rand < bre_prob1to2if2)
                    {
                        breathing = 2;
                        treatment_timeline.Items.Add("breathing stopped");
                        patient_bre_instability_score += 1;
                        breath_deg_prob1to2 = bre_prob1to2if2;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("breathing still weak");
                        breath_deg_prob1to1 = 100 - bre_prob1to2if2;
                    }
                }
                else if (airway == 0 && initial_breathing == 0 && breathing == 0)
                {
                    int bre_rand = chr_rnd_bre.Next(1, 101);
                    if (bre_rand < bre_prob0to1if0)
                    {
                        breathing = 1;
                        treatment_timeline.Items.Add("breathing weakening");
                        patient_bre_instability_score += 1;
                        breath_deg_prob0to1 = bre_prob0to1if0;
                    }
                    else if (bre_rand > bre_prob0to1if0 && bre_rand < (bre_prob0to2if0 + bre_prob0to1if0))
                    {
                        breathing = 2;
                        patient_bre_instability_score += 1;
                        treatment_timeline.Items.Add("breathing stopped");
                        breath_deg_prob0to2 = bre_prob0to2if0-bre_prob0to1if0;
                    }
                    else
                    {
                        breathing = 0;
                        patient_bre_instability_score -= 2;
                        stopwatch_bre.Reset();
                        breath_deg_prob0to0 = 100 - (bre_prob0to2if0 + bre_prob0to1if0);
                    }
                }
                else if (airway == 0 && initial_breathing == 2 && breathing == 1)
                {
                    int bre_rand = chr_rnd_bre.Next(1, 101);
                    if (bre_rand < bre_prob1to2if0)
                    {
                        breathing = 2;

                        treatment_timeline.Items.Add("breathing stopped");
                        patient_bre_instability_score += 1;
                        breath_deg_prob1to2 = bre_prob1to2if0;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("breathing still weak");
                        breath_deg_prob1to1 = 100 - bre_prob1to2if0;
                    }
                }
                #endregion

                #endregion

                to = "EMS"; subject = "Recommend next treatment";
                Ems();
            }

        #region treatment
        void Treatment(string action)
            {
                #region action recommended from AI.....
                if (action == "check hemorrhage")
                {
                    Hemorrhagecheck();
                }
                if (action == "check airway")
                {
                    Airwaycheck();
                }
                if (action == "check consciousness")
                {
                    Consciousnesscheck();
                }
                if (action == "check breathing")
                {
                    Breathingcheck();
                }
                if (action == "check circulation")
                {
                    Circulationcheck();
                }
                if (action == "check CPR")
                {
                    CPRcheck();
                }
                #endregion

                //perform check and treat condition depending on message from AI - Change to EMS? 
                #region hemorrhage check
                async void Hemorrhagecheck() // add async if moving the 'wait functions in here
                {
                    hem1to0_cure_probability -= (4 * hem1_count);
                    hem2to0_cure_probability -= (3 * hem2_count);
                    hem2to1_cure_probability -= (8 * hem2_count);

                    int hem1to1_cure_probability = 100 - hem1to0_cure_probability;
                    int hem1_cure_cumprob = hem1to1_cure_probability + hem1to0_cure_probability;

                    int hem2to2_cure_probability = 100 - (hem2to0_cure_probability + hem2to1_cure_probability);
                    int hem2to1_cumprob = hem2to1_cure_probability + hem2to0_cure_probability;
                    int hem2_cure_cumprob = hem2to1_cure_probability + hem2to0_cure_probability; //+ hem2to2_cure_probability;

                    if (hem2to0_cure_probability < 3) { hem2to0_cure_probability = 3; }
                    if (hem2to1_cure_probability < 15) { hem2to1_cure_probability = 15; }

                    int hemorrhage_timedelay = 0;
                    int hemorrhage1_timedelay = 3000;
                    int hemorrhage2_timedelay = 5000;

                    if (hemorrhage == 2) { hemorrhage_timedelay = hemorrhage2_timedelay; MessageLabel = "attempting to stop heavy bleeding"; }
                    else if (hemorrhage == 1) { hemorrhage_timedelay = hemorrhage1_timedelay; MessageLabel = "attempting to stop light bleeding"; }
                    else if (hemorrhage == 0) { hemorrhage_timedelay = 2000; MessageLabel = "Patient not bleeding, check other injuries"; }

                    treatment_timeline.Items.Add("Checking hemorrhage");

                    #region show patient treating message
                    Button_air_Intubate.Enabled = false;
                    Button_air_clearair.Enabled = false;
                    Button_breath_aspirate.Enabled = false;
                    Button_breath_Oxygen.Enabled = false;
                    Button_circ_chest.Enabled = false;
                    Button_circ_drugs.Enabled = false;
                    Button_conc_drugs.Enabled = false;
                    Button_hemm_torniquet.Enabled = false;
                    Button_hemm_treat.Enabled = false;
                    Button_CPR.Enabled = false;


                    var treatment_message = new Message_form();
                    treatment_message.Show();
                    await Task.Delay(hemorrhage_timedelay);
                    treatment_message.Close();

                    Button_air_Intubate.Enabled = true;
                    Button_air_clearair.Enabled = true;
                    Button_breath_aspirate.Enabled = true;
                    Button_breath_Oxygen.Enabled = true;
                    Button_circ_chest.Enabled = true;
                    Button_circ_drugs.Enabled = true;
                    Button_conc_drugs.Enabled = true;
                    Button_hemm_torniquet.Enabled = true;
                    Button_hemm_treat.Enabled = true;
                    Button_CPR.Enabled = true;

                    // end display treatment message
                    #endregion

                    if (hemorrhage == 0)
                    {
                        int x;
                        Random r = new Random();
                        x = r.Next(0, 101);

                        if (x <= 101) // 100% probability of successfully treating no hemorrhage
                        {
                            hemorrhage = 0;
                            hemorrhage_description = "no bleeding";
                            treatment_timeline.Items.Add("No bleeding");

                            mistake_count += 1;
                        }

                    }
                    else if (hemorrhage == 1)
                    {
                        treatment_timeline.Items.Add("Applying treatment for light Bleeding");
                        int x;
                        Random r = new Random();
                        x = r.Next(0, 101);
                        if (x <= hem1to0_cure_probability)
                        {
                            hemorrhage = 0;
                            hemorrhage_description = "treatment Successful light bleeding stopped";
                            treatment_timeline.Items.Add("Bleeding stopped");
                            stopwatch_hem.Reset();
                            total_success_count = total_success_count + 1;
                            patient_hem_instability_score -= 2;
                        }
                        else
                        {
                            hemorrhage = 1;
                            hemorrhage_description = "some bleeding";
                            treatment_timeline.Items.Add("light bleeding not stopped");
                            total_unsuccess_count = total_unsuccess_count + 1;
                        }

                        hem1_count += 1;
                        hemorrhage_timedelay = hemorrhage1_timedelay;
                    }
                    else // ...if hemorrhage = 2
                    {
                        treatment_timeline.Items.Add("heavy bleeding");
                        treatment_timeline.Items.Add("Applying Treatment - Tourniquet");

                        int x;
                        Random r = new Random();
                        x = r.Next(0, 101);
                        if (x <= hem2to0_cure_probability)
                        {
                            hemorrhage = 0;
                            hemorrhage_description = "heavy bleeding stopped";
                            treatment_timeline.Items.Add("Bleeding stopped");
                            stopwatch_hem.Reset();
                            total_success_count = total_success_count + 1;
                            patient_hem_instability_score -= 1;
                        }
                        else if (x <= hem2to1_cumprob && x > hem2to0_cure_probability)
                        {
                            treatment_timeline.Items.Add("Bleeding partially stopped");
                            hemorrhage_description = "heavy bleeding partially stopped";
                            hemorrhage = 1;
                            total_partial_success_count = total_partial_success_count + 1;
                            patient_hem_instability_score -= 1;
                        }
                        else
                        {
                            treatment_timeline.Items.Add("Bleeding not stopped");
                            hemorrhage_description = "heavy bleeding";
                            hemorrhage = 2;
                            total_unsuccess_count = total_unsuccess_count + 1;
                            patient_hem_instability_score += 2;
                        }

                        hem2_count += +1;
                        hemorrhage_timedelay = hemorrhage2_timedelay;
                    }

                    from = "treatment"; to = "UIController"; subject = "update UI";
                    Environment_control_function();

                    from = "treatment"; to = "EMS"; subject = "Recommend next treatment";
                    Ems();

                } // end of hemorrhage check
                #endregion
                #region consciousness check
                async void Consciousnesscheck()
                {
                    #region set consciousness treatment variables

                    int conc2to1_cumprob = conc2to1_cure_probability + conc2to0_cure_probability;

                    int consciousness_timedelay = 0;
                    int consciousness1_timedelay = 3000;
                    int consciousness2_timedelay = 5000;

                    if (consciousness == 2) { consciousness_timedelay = consciousness2_timedelay; MessageLabel = "attempting to revive unconscious patient"; }
                    else if (consciousness == 1) { consciousness_timedelay = consciousness1_timedelay; MessageLabel = "attempting to revive partially conscious patient"; }
                    else { consciousness_timedelay = 2000; MessageLabel = "patient conscious, check other injuries"; }
                    #endregion

                    treatment_timeline.Items.Add("Checking Consciousness");

                    #region show treating patient message
                    // display treatment message - requires 'async' in the private void statement

                    Button_air_Intubate.Enabled = false;
                    Button_air_clearair.Enabled = false;
                    Button_breath_aspirate.Enabled = false;
                    Button_breath_Oxygen.Enabled = false;
                    Button_circ_chest.Enabled = false;
                    Button_circ_drugs.Enabled = false;
                    Button_conc_drugs.Enabled = false;
                    Button_hemm_torniquet.Enabled = false;
                    Button_hemm_treat.Enabled = false;
                    Button_CPR.Enabled = false;


                    var treatment_message = new Message_form();
                    treatment_message.Show();
                    await Task.Delay(consciousness_timedelay);
                    treatment_message.Close();

                    Button_air_Intubate.Enabled = true;
                    Button_air_clearair.Enabled = true;
                    Button_breath_aspirate.Enabled = true;
                    Button_breath_Oxygen.Enabled = true;
                    Button_circ_chest.Enabled = true;
                    Button_circ_drugs.Enabled = true;
                    Button_conc_drugs.Enabled = true;
                    Button_hemm_torniquet.Enabled = true;
                    Button_hemm_treat.Enabled = true;
                    Button_CPR.Enabled = true;

                    // end display treatment message
                    #endregion

                    if (consciousness == 0)
                    {
                        treatment_timeline.Items.Add("Patient fully Conscious");
                        consciousness = 0;
                        consciousness_description = "fully conscious";

                        mistake_count += 1;
                    }
                    else if (consciousness == 1)
                    {
                        treatment_timeline.Items.Add("Patient Partially Conscious");
                        treatment_timeline.Items.Add("Providing Oxygen");

                        if (breathing == 0 && hemorrhage == 0 && airway == 0 && circulation == 0) //cure probability increases
                        {
                            conc1to0_cure_probability += (5 * conc1_count);
                            conc1to1_cure_probability = 100 - conc1to0_cure_probability;
                        }
                        else //cure probability reduces if other injuries present
                        {
                            conc1to0_cure_probability -= (8 * conc1_count);
                            conc1to1_cure_probability = 100 - conc1to0_cure_probability;
                        }

                        if (conc1to0_cure_probability < 15) { conc1to0_cure_probability = 15; }
                        if (conc1to1_cure_probability > 85) { conc1to0_cure_probability = 85; }
                        if (conc1to0_cure_probability > 85) { conc1to0_cure_probability = 85; }
                        if (conc1to1_cure_probability < 15) { conc1to0_cure_probability = 15; }

                        int conc1_cure_cumprob = conc1to1_cure_probability + conc1to0_cure_probability;
                        int x;

                        Random r = new Random();
                        x = r.Next(0, 101);
                        if (x <= conc1to0_cure_probability)
                        {
                            consciousness = 0;
                            consciousness_description = "previously partially conscious but now fully conscious";
                            treatment_timeline.Items.Add("Full consciousness restored");
                            stopwatch_con.Reset();
                            total_success_count = total_success_count + 1;
                            patient_con_instability_score -= 2;
                        }
                        else
                        {
                            consciousness = 1;
                            consciousness_description = "partially conscious";
                            treatment_timeline.Items.Add("Patient Still not fully conscious");
                            total_unsuccess_count = total_unsuccess_count + 1;

                        }
                        conc1_count += 1;
                        consciousness_timedelay = consciousness1_timedelay;
                    }
                    else // ...if consciousness = 2
                    {
                        treatment_timeline.Items.Add("Patient unconscious");
                        treatment_timeline.Items.Add("Attempting Treatment"); //Add specific consciousness treatment ADD
                        if (breathing == 0 && hemorrhage == 0 && airway == 0 && circulation == 0) // cure probability increases
                        {
                            conc2to0_cure_probability += (2 * conc2_count);
                            conc2to1_cure_probability += (8 * conc2_count);
                            conc2to2_cure_probability -= (10 * conc2_count);
                            if (conc2to0_cure_probability > 30) { conc2to0_cure_probability = 30; }
                            if (conc2to1_cure_probability > 50) { conc2to1_cure_probability = 50; }
                            conc2to2_cure_probability = 100 - (conc2to0_cure_probability + conc2to1_cure_probability);

                            conc2to1_cumprob = conc2to1_cure_probability + conc2to0_cure_probability;

                        }
                        else // cure probability decreases if other injuries are present
                        {
                            conc2to0_cure_probability = 10 - (1 * conc2_count);
                            conc2to1_cure_probability = 50 - (4 * conc2_count);
                            conc2to2_cure_probability = 40 + (5 * conc2_count);
                            if (conc2to0_cure_probability < 5) { conc2to0_cure_probability = 5; }
                            if (conc2to1_cure_probability < 10) { conc2to1_cure_probability = 10; }
                            if (conc2to2_cure_probability < 15) { conc2to2_cure_probability = 15; }

                            conc2to1_cumprob = conc2to1_cure_probability + conc2to0_cure_probability;

                        }

                        int x;
                        Random r = new Random();
                        x = r.Next(0, 101);
                        if (x <= conc2to0_cure_probability)
                        {
                            treatment_timeline.Items.Add("Full consciousness restored");
                            consciousness = 0;
                            consciousness_description = "previously unconscious but now fully conscious";
                            stopwatch_con.Reset();
                            total_success_count = total_success_count + 1;
                            patient_con_instability_score -= 3;
                        }
                        else if (x <= conc2to1_cumprob && x > conc2to0_cure_probability)
                        {
                            treatment_timeline.Items.Add("Patient partially responsive");
                            consciousness = 1;
                            consciousness_description = "previously unconscious but coming round";
                            total_partial_success_count = total_partial_success_count + 1;
                            patient_con_instability_score -= 2;
                        }
                        else
                        {
                            treatment_timeline.Items.Add("Patient still unconscious");
                            consciousness = 2;
                            consciousness_description = "unconscious";
                            total_unsuccess_count = total_unsuccess_count + 1;
                        }
                        conc2_count = conc2_count + 1;
                        consciousness_timedelay = consciousness2_timedelay;
                    }

                    //this section all commented out for now - move to patient section? multiple checks? 
                    #region consciousness deterioration control - move to patient MDP section

                    #endregion // this section all commented out

                    from = "treatment"; to = "UIController"; subject = "update UI";
                    Environment_control_function();

                    from = "treatment"; to = "EMS"; subject = "Recommend next treatment";
                    Ems();

                }// end of consciousness check
                #endregion
                #region Breathing check
                async void Breathingcheck()
                {

                    int breath2to1_cumprob = breath2to1_cure_probability + breath2to0_cure_probability;
                    int breath2_successprob_reduction = 4;
                    int breathing_timedelay = 0;
                    int breathing1_timedelay = 3000;
                    int breathing2_timedelay = 10000;

                    if (breathing == 2) { breathing_timedelay = breathing2_timedelay; MessageLabel = "attempting to restore breath function"; }
                    else if (breathing == 1) { breathing_timedelay = breathing1_timedelay; MessageLabel = "attempting to restore normal breathing to patient"; }
                    else { breathing_timedelay = 2000; MessageLabel = "Patient breathing normally, check other injuries"; }

                    treatment_timeline.Items.Add("Checking breathing");

                    #region show treating patient message
                    // display treatment message - requires 'async' in the private void statement

                    Button_air_Intubate.Enabled = false;
                    Button_air_clearair.Enabled = false;
                    Button_breath_aspirate.Enabled = false;
                    Button_breath_Oxygen.Enabled = false;
                    Button_circ_chest.Enabled = false;
                    Button_circ_drugs.Enabled = false;
                    Button_conc_drugs.Enabled = false;
                    Button_hemm_torniquet.Enabled = false;
                    Button_hemm_treat.Enabled = false;
                    Button_CPR.Enabled = false;


                    var treatment_message = new Message_form();
                    treatment_message.Show();
                    await Task.Delay(breathing_timedelay);
                    treatment_message.Close();

                    Button_air_Intubate.Enabled = true;
                    Button_air_clearair.Enabled = true;
                    Button_breath_aspirate.Enabled = true;
                    Button_breath_Oxygen.Enabled = true;
                    Button_circ_chest.Enabled = true;
                    Button_circ_drugs.Enabled = true;
                    Button_conc_drugs.Enabled = true;
                    Button_hemm_torniquet.Enabled = true;
                    Button_hemm_treat.Enabled = true;
                    Button_CPR.Enabled = true;

                    // end display treatment message
                    #endregion

                    if (breathing == 0)
                    {
                        breathing_description = "no breathing problem";
                        treatment_timeline.Items.Add("Patient breathing normally");
                        breathing = 0;

                        mistake_count += 1;
                    }
                    else if (breathing == 1)
                    {
                        treatment_timeline.Items.Add("Patient breathing is not normal");
                        treatment_timeline.Items.Add("providing oxygen");

                        breath1to0_cure_probability = breath1to0_cure_probability - (patient_bre_instability_score * breath1_count);
                        if (breath1to0_cure_probability < 10) { breath1to0_cure_probability = 10; }
                        int x;
                        Random r = new Random();
                        x = r.Next(0, 101);
                        if (x <= breath1to0_cure_probability)
                        {
                            breathing = 0;
                            breathing_description = "previously had breathing problems but now normal";
                            treatment_timeline.Items.Add("Breathing restored");
                            stopwatch_bre.Reset();
                            total_success_count = total_success_count + 1;
                            patient_bre_instability_score -= 2;
                        }
                        else
                        {
                            breathing = 1;
                            breathing_description = "weak or irregular breathing";
                            treatment_timeline.Items.Add("breathing remains erratic");
                            total_unsuccess_count = total_unsuccess_count + 1;
                            stopwatch_bre.Reset();
                        }
                        breath1_count = breath1_count + 1;
                        breathing_timedelay = breathing1_timedelay;
                    }
                    else // ...if breathing = 2
                    {
                        treatment_timeline.Items.Add("Patient not breathing");
                        treatment_timeline.Items.Add("Aspirating Patient");
                        breath2to0_cure_probability = breath2to0_cure_probability - (breath2_successprob_reduction * breath2_count);
                        breath2to1_cure_probability = breath2to1_cure_probability - (breath2_successprob_reduction * breath2_count);
                        if (breath2to0_cure_probability < 5) { breath2to0_cure_probability = 5; }
                        if (breath2to1_cure_probability < 10) { breath2to1_cure_probability = 10; }
                        breath2to1_cumprob = breath2to1_cure_probability + breath2to0_cure_probability;

                        int x;
                        Random r = new Random();
                        x = r.Next(0, 101);
                        if (x <= breath2to0_cure_probability)
                        {
                            breathing_description = "previously not breathing but now normal";
                            treatment_timeline.Items.Add("Breathing restored to normal");
                            breathing = 0;
                            stopwatch_bre.Reset();
                            total_success_count = total_success_count + 1;
                            patient_bre_instability_score -= 3;
                        }
                        else if (x <= breath2to1_cumprob && x > breath2to0_cure_probability)
                        {
                            breathing_description = "previously not breathing, improved but not breathing normally";
                            treatment_timeline.Items.Add("breathing restored but still erratic");
                            breathing = 1;
                            stopwatch_bre.Reset();
                            total_partial_success_count = total_partial_success_count + 1;
                            patient_bre_instability_score -= 2;
                        }
                        else
                        {
                            breathing_description = "not breathing";
                            treatment_timeline.Items.Add("Patient still not breathing");
                            breathing = 2;
                            total_unsuccess_count = total_unsuccess_count + 1;
                        }
                        breath2_count += 1;
                        breathing_timedelay = breathing2_timedelay;
                    }

                    from = "treatment"; to = "UIController"; subject = "update UI";
                    Environment_control_function();

                    from = "treatment"; to = "EMS"; subject = "Recommend next treatment";
                    Ems();

                }// end of breathing check
                #endregion
                #region circulation check
                async void Circulationcheck()
                {

                    // *******************************SET VARIABLES FOR THE TREATMENT*********************************************************
                    #region Treatment variables additional to the top level variables defined at the top of code
                    int circ1_successprob_reduction = 10; // the reduction in the probability of success with each repeated treatment from level 1
                    int circ2_0_successprob_reduction = 1; // the reduction in the probability of success with each repeated treatment from level 2 to 0
                    int circ2_1_successprob_reduction = 2; // the reduction in the probability of success with each repeated treatment from level 2 to 1

                    int circ2to1_cumprob = circ2to1_cure_probability + circ2to0_cure_probability; // cumulative success probabiligy

                    int circ_timedelay = 0;
                    int circ1_timedelay = 3000;
                    int circ2_timedelay = 10000;

                    if (circulation == 2) { circ_timedelay = circ2_timedelay; MessageLabel = "attempting to re-start the heart"; }
                    else if (circulation == 1) { circ_timedelay = circ1_timedelay; MessageLabel = "attempting to restore normal heart function to patient"; }
                    else { circ_timedelay = 2000; MessageLabel = "heart not affected, check other injuries"; }
                    #endregion

                    // *********************** Treatment Logic and message to Listbox **********************************************************
                    #region Treatment Logic
                    treatment_timeline.Items.Add("Checking pulse");

                    #region show treating patient message
                    //display treatment message - requires 'async' in the private void statement

                    Button_air_Intubate.Enabled = false;
                    Button_air_clearair.Enabled = false;
                    Button_breath_aspirate.Enabled = false;
                    Button_breath_Oxygen.Enabled = false;
                    Button_circ_chest.Enabled = false;
                    Button_circ_drugs.Enabled = false;
                    Button_conc_drugs.Enabled = false;
                    Button_hemm_torniquet.Enabled = false;
                    Button_hemm_treat.Enabled = false;
                    Button_CPR.Enabled = false;


                    var treatment_message = new Message_form();
                    treatment_message.Show();                                                        // open message box
                    await Task.Delay(circ_timedelay);                                                //leave message on screen for as long as delay is set dependent on procedure
                    treatment_message.Close();                                                       // close message box
                                                                                                     // end display treatment message

                    Button_air_Intubate.Enabled = true;
                    Button_air_clearair.Enabled = true;
                    Button_breath_aspirate.Enabled = true;
                    Button_breath_Oxygen.Enabled = true;
                    Button_circ_chest.Enabled = true;
                    Button_circ_drugs.Enabled = true;
                    Button_conc_drugs.Enabled = true;
                    Button_hemm_torniquet.Enabled = true;
                    Button_hemm_treat.Enabled = true;
                    Button_CPR.Enabled = true;

                    #endregion

                    if (circulation == 0)  // If there is no problem
                    {
                        circulation_description = "no heart problem";
                        treatment_timeline.Items.Add("pulse is normal");                            //patient condition message
                        circulation = 0;                                                            //set patient condition quantifier to 0 'normal'

                        mistake_count += 1;
                    }
                    else if (circulation == 1)  // if there is a minor problem
                    {
                        treatment_timeline.Items.Add("Pulse is elevated/weak/erratic");             //Patient condition message
                        treatment_timeline.Items.Add("providing treatment");                        //ADD treatment here
                        circ1to0_cure_probability -= (circ1_successprob_reduction * circ1_count);    //partial success probability accounting for repeat procedures

                        if (circ1to0_cure_probability < 2) { circ1to0_cure_probability = 2; }

                        int x;
                        Random r = new Random();
                        x = r.Next(0, 101);                                                         //generate random number between 1 and 100 for outcome 
                        if (x <= circ1to0_cure_probability)
                        {
                            circulation_description = "weak or irregular pulse now normal";
                            treatment_timeline.Items.Add("pulse returned to normal");             //Patient condition returned to normal message
                            circulation = 0;
                            stopwatch_cir.Reset();                                                //patient condition quantifier set to 0 - 'normal'
                            total_success_count += 1;                                            //global counter for successful procedures
                            patient_circ_instability_score -= 2;
                        }
                        else
                        {
                            circulation_description = "weak or irregular pulse or heartbeat";
                            treatment_timeline.Items.Add("Pulse still elevated/weak/erratic");      //Patient condition unchanged message
                            circulation = 1;                                                        //Patient condition quantifier set to 1 - no change - still some problem
                            total_unsuccess_count += 1;                                             //global counter for unsucessful procedures
                        }
                        circ1_count += 1;                                                           //counter for level 1 circulation procedures (successful or unsuccessful)
                        circ_timedelay = circ1_timedelay;                                           //set appropriate time delay for a level 1 procedure
                    }
                    else // ...if patient is critical or there is a serious problem
                    {
                        treatment_timeline.Items.Add("Patient Heart in Cardiac Arrest");            // Patient condition message
                        treatment_timeline.Items.Add("attempting resusitation");
                        circ2to0_cure_probability -= (circ2_0_successprob_reduction * circ2_count); //complete success probability accounting for repeats
                        circ2to1_cure_probability -= (circ2_1_successprob_reduction * circ2_count); //partial success probability accounting for repeats
                        if (circ2to0_cure_probability < 1) { circ2to0_cure_probability = 1; }
                        if (circ2to1_cure_probability < 2) { circ2to1_cure_probability = 2; }
                        circ2to1_cumprob = circ2to1_cure_probability + circ2to0_cure_probability;   // set cumulative success and partial success probability

                        int x;
                        Random r = new Random();
                        x = r.Next(0, 101);                                                         //generate random number between 1 and 100
                        if (x <= circ2to0_cure_probability)
                        {
                            circulation_description = "previously in cardiac arrest but now normal";
                            treatment_timeline.Items.Add("Pulse returned to normal");               // patient condition returned to normal message
                            circulation = 0;                                                        // patient condition quantifier set to 0 - 'normal'
                            stopwatch_cir.Reset();
                            total_success_count += 1;                                               // global counter for successful procedures
                            patient_circ_instability_score -= 3;
                        }
                        else if (x <= circ2to1_cumprob && x > circ2to0_cure_probability)
                        {
                            circulation_description = "previously in cardiac arrest but now pulse is weak or irregular";
                            treatment_timeline.Items.Add("Heart function restored but not normal"); //patient partially stabliized message
                            circulation = 1;                                                        //patient condition quantifier set to 1 - some problems
                            total_partial_success_count += 1;                                       // counter for partial successful procedures
                            patient_circ_instability_score -= 2;
                            stopwatch_cir.Reset();
                        }
                        else
                        {
                            circulation_description = "in cardiac arrest";
                            treatment_timeline.Items.Add("Patient Still in Cardiac Arrest");        //patient still critical
                            circulation = 2;                                                        //patient condition quantifier set to 2 - critical
                            total_unsuccess_count += 1;                                             // counter for unsuccessful procedures
                        }
                        circ2_count += 1;                                                           //counter for total critical procedures (successful and unsuccessful)
                        circ_timedelay = circ2_timedelay;                                           //set timedelay to variable for critical procedure
                    }
                    #endregion

                    from = "treatment"; to = "UIController"; subject = "update UI";
                    Environment_control_function();

                    from = "treatment"; to = "EMS"; subject = "Recommend next treatment";
                    Ems();

                }// end of circulation check
                #endregion
                #region airwayclear
                async void Airwaycheck()
                {
                    // *******************************SET VARIABLES FOR THE TREATMENT*********************************************************
                    #region Treatment variables
                    int airway_2to1_cumprob = airway_2to1_cure_probability + airway_2to0_cure_probability; // cumulative success probabiligy

                    int airway1_successprob_reduction = 0; // the reduction in the probability of success with each repeated treatment from level 1
                    int airway_2_0_successprob_reduction = 0; // the reduction in the probability of success with each repeated treatment from level 2 to 0
                    int airway_2_1_successprob_reduction = 0; // the reduction in the probability of success with each repeated treatment from level 2 to 1

                    int airway_timedelay = 0;
                    int airway1_timedelay = 3000;
                    int airway2_timedelay = 6000;

                    if (airway == 2) { airway_timedelay = airway2_timedelay; MessageLabel = "attempting to intubate patient"; }
                    else if (airway == 1) { airway_timedelay = airway1_timedelay; MessageLabel = "clearing airway"; }
                    else { airway_timedelay = 2000; MessageLabel = "airway is clear, check other injuries"; }
                    #endregion

                    // *********************** Treatment Logic and message to Listbox **********************************************************
                    #region Treatment Logic
                    treatment_timeline.Items.Add("Checking for airway obstruction");

                    #region show treating patient message
                    // display treatment message - requires 'async' in the private void statement

                    Button_air_Intubate.Enabled = false;
                    Button_air_clearair.Enabled = false;
                    Button_breath_aspirate.Enabled = false;
                    Button_breath_Oxygen.Enabled = false;
                    Button_circ_chest.Enabled = false;
                    Button_circ_drugs.Enabled = false;
                    Button_conc_drugs.Enabled = false;
                    Button_hemm_torniquet.Enabled = false;
                    Button_hemm_treat.Enabled = false;
                    Button_CPR.Enabled = false;


                    var treatment_message = new Message_form();
                    treatment_message.Show();                                                        // open message box
                    await Task.Delay(airway_timedelay);                                                //leave message on screen for as long as delay is set dependent on procedure
                    treatment_message.Close();                                                       // close message box
                                                                                                     // end display treatment message


                    Button_air_Intubate.Enabled = true;
                    Button_air_clearair.Enabled = true;
                    Button_breath_aspirate.Enabled = true;
                    Button_breath_Oxygen.Enabled = true;
                    Button_circ_chest.Enabled = true;
                    Button_circ_drugs.Enabled = true;
                    Button_conc_drugs.Enabled = true;
                    Button_hemm_torniquet.Enabled = true;
                    Button_hemm_treat.Enabled = true;
                    Button_CPR.Enabled = true;

                    #endregion

                    if (airway == 0)  // If there is no problem
                    {
                        treatment_timeline.Items.Add("no obstruction to airway");                            //patient condition message
                        airway_description = "airway clear";
                        airway = 0;                                                            //set patient condition quantifier to 0 'normal'

                        mistake_count += 1;
                    }
                    else if (airway == 1)  // if there is a minor problem
                    {
                        treatment_timeline.Items.Add("partial blockage of airway");             //Patient condition message
                        treatment_timeline.Items.Add("attempting airway clearance");
                        airway_1to0_cure_probability -= (airway1_successprob_reduction * airway1_count);    //partial success probability accounting for repeat procedures
                        int x;
                        Random r = new Random();
                        x = r.Next(0, 101);                                                         //generate random number between 1 and 100 for outcome 
                        if (x <= airway_1to0_cure_probability)
                        {
                            treatment_timeline.Items.Add("airway cleared");             //Patient condition returned to normal message
                            airway_description = "partially blocked airway now clear";
                            airway = 0;                                                      //patient condition quantifier set to 0 - 'normal'
                            stopwatch_air.Reset();
                            total_success_count += 1;                                             //global counter for successful procedures
                            patient_air_instability_score -= 2;
                        }
                        else
                        {
                            treatment_timeline.Items.Add("airway still blocked");      //Patient condition unchanged message
                            airway_description = "airway still partially blocked";
                            airway = 1;                                                        //Patient condition quantifier set to 1 - no change - still some problem
                            total_unsuccess_count += 1;                                             //global counter for unsucessful procedures
                        }
                        airway1_count += 1;                                                           //counter for level 1 airway procedures (successful or unsuccessful)
                        airway_timedelay = airway1_timedelay;                                           //set appropriate time delay for a level 1 procedure
                    }
                    else // ...if patient is critical or there is a serious problem
                    {
                        treatment_timeline.Items.Add("Patient airway is completely blocked");            // Patient condition message
                        treatment_timeline.Items.Add("attempting airway clearance");
                        airway_2to0_cure_probability -= (airway_2_0_successprob_reduction * airway2_count); //complete success probability accounting for repeats
                        airway_2to1_cure_probability -= (airway_2_1_successprob_reduction * airway2_count); //partial success probability accounting for repeats
                        airway_2to1_cumprob = airway_2to1_cure_probability + airway_2to0_cure_probability;   // set cumulative success and partial success probability

                        int x;
                        Random r = new Random();
                        x = r.Next(0, 101);                                                         //generate random number between 1 and 100
                        if (x <= airway_2to0_cure_probability)
                        {
                            treatment_timeline.Items.Add("Intubation success - Airway clear and stable");                     // patient condition returned to normal message
                            airway_description = "blocked airway now clear";
                            airway = 0;                                                         // patient condition quantifier set to 0 - 'normal'
                            stopwatch_air.Reset();
                            total_success_count += 1;                                           // global counter for successful procedures
                            patient_air_instability_score -= 10;                                // airway stabilisesd
                            airway_close_prob0to1 = 0;
                            airway_close_prob1to2 = 0;

                        }
                        else if (x <= airway_2to1_cumprob && x > airway_2to0_cure_probability)
                        {
                            treatment_timeline.Items.Add("Airway partially cleared");           //patient partially stabliized message
                            airway_description = "airway partially clear";
                            airway = 1;                                                         //patient condition quantifier set to 1 - some problems
                            total_partial_success_count += 1;                                   // counter for partial successful procedures
                        }
                        else
                        {
                            treatment_timeline.Items.Add("Airway still blocked");               //patient still critical
                            airway_description = "airway blocked";
                            airway = 2;                                                         //patient condition quantifier set to 2 - critical
                            total_unsuccess_count += 1;                                         // counter for unsuccessful procedures
                        }
                        airway2_count += 1;                                                     //counter for total critical procedures (successful and unsuccessful)
                        airway_timedelay = airway2_timedelay;                                   //set timedelay to variable for critical procedure
                    }
                    #endregion

                    from = "treatment"; to = "UIController"; subject = "update UI";
                    Environment_control_function();

                    from = "treatment"; to = "EMS"; subject = "Recommend next treatment";
                    Ems();
                }// end of airway check

                #endregion
                #region cpr check
                async void CPRcheck()
                {
                    // *******************************SET VARIABLES FOR THE TREATMENT*********************************************************
                    #region Treatment variables
                    int cpr2to1_cumprob = cpr2to1_cure_probability + cpr2to0_cure_probability; // cumulative success probabiligy
                    int cpr2_0_successprob_reduction = 1; // the reduction in the probability of success with each repeated treatment from level 2 to 0
                    int cpr2_1_successprob_reduction = 2; // the reduction in the probability of success with each repeated treatment from level 2 to 1

                    int cpr_timedelay = 0;
                    int cpr2_timedelay = 10000;

                    if (circulation == 2 && breathing == 2)
                    {
                        cpr_timedelay = cpr2_timedelay;
                        MessageLabel = "attempting CPR on patient"; // Message pop up content
                    }
                    else
                    {
                        cpr_timedelay = 2000;
                        MessageLabel = "Cardio and Pulmonary function is ok, check other injuries";
                        mistake_count += 1;
                    }
                    #endregion

                    // *********************** Treatment Logic and message to Listbox **********************************************************
                    #region Treatment Logic
                    treatment_timeline.Items.Add("Checking cardio and pulmonary response");

                    #region show treating patient message
                    //display treatment message - requires 'async' in the private void statement

                    Button_air_Intubate.Enabled = false;
                    Button_air_clearair.Enabled = false;
                    Button_breath_aspirate.Enabled = false;
                    Button_breath_Oxygen.Enabled = false;
                    Button_circ_chest.Enabled = false;
                    Button_circ_drugs.Enabled = false;
                    Button_conc_drugs.Enabled = false;
                    Button_hemm_torniquet.Enabled = false;
                    Button_hemm_treat.Enabled = false;
                    Button_CPR.Enabled = false;


                    var treatment_message = new Message_form();
                    treatment_message.Show();                                                        // open message box
                    await Task.Delay(cpr_timedelay);                                                //leave message on screen for as long as delay is set dependent on procedure
                    treatment_message.Close();                                                       // close message box

                    Button_air_Intubate.Enabled = true;
                    Button_air_clearair.Enabled = true;
                    Button_breath_aspirate.Enabled = true;
                    Button_breath_Oxygen.Enabled = true;
                    Button_circ_chest.Enabled = true;
                    Button_circ_drugs.Enabled = true;
                    Button_conc_drugs.Enabled = true;
                    Button_hemm_torniquet.Enabled = true;
                    Button_hemm_treat.Enabled = true;
                    Button_CPR.Enabled = true;

                    // end display treatment message
                    #endregion

                    if (circulation == 2 && breathing == 2)  // If CPR needed
                    {
                        // add defibrillation after 2 repetitions
                        if (cpr2_count < 3)
                        #region Vary message depending on count
                        {
                            treatment_timeline.Items.Add("Patient in cardiac arrest and not breathing");            // Patient condition message
                            treatment_timeline.Items.Add("attempting CPR resusitation");
                            MessageLabel = "attempting CPR"; // Message pop up content
                        }
                        else
                        {
                            treatment_timeline.Items.Add("Patient in cardiac arrest and not breathing");            // Patient condition message
                            treatment_timeline.Items.Add("attempting CPR with defibrillation");
                            MessageLabel = "attempting defibrillation due to unresponsiveness to CPR"; // Message pop up content
                        }
                        #endregion

                        cpr2to0_cure_probability -= (cpr2_0_successprob_reduction * cpr2_count); //complete success probability accounting for repeats
                        cpr2to1_cure_probability -= (cpr2_1_successprob_reduction * cpr2_count); //partial success probability accounting for repeats
                        if (cpr2to0_cure_probability < 1) { cpr2to0_cure_probability = 1; }
                        if (cpr2to1_cure_probability < 2) { cpr2to1_cure_probability = 2; }
                        cpr2to1_cumprob = cpr2to1_cure_probability + cpr2to0_cure_probability;   // set cumulative success and partial success probability

                        Random rCPR = new Random();
                        int CPRtest = rCPR.Next(0, 101);                                                         //generate random number between 1 and 100
                        if (CPRtest <= cpr2to0_cure_probability)
                        {
                            breathing_description = "previously not breathing but now normal";
                            circulation_description = "previously in cardiac arrest but now normal";
                            treatment_timeline.Items.Add("Pulse and breathing returned to normal");               // patient condition returned to normal message
                            circulation = 0;
                            breathing = 0;                                                                        // patient condition quantifier set to 0 - 'normal'
                            stopwatch_cir.Reset();
                            stopwatch_bre.Reset();
                            total_success_count += 1;                                               // global counter for successful procedures
                        }
                        else if (CPRtest <= cpr2to1_cumprob && CPRtest > cpr2to0_cure_probability)
                        {
                            breathing_description = "previously not breathing, improved but not breathing normally";
                            circulation_description = "previously in cardiac arrest but now pulse is weak or irregular";
                            treatment_timeline.Items.Add("Heart function and breathing partially restored but not normal"); //patient partially stabliized message
                            circulation = 1;
                            breathing = 1;
                            stopwatch_cir.Reset();
                            stopwatch_bre.Reset();                                                  //patient condition quantifier set to 1 - some problems
                            total_partial_success_count += 1;                                       // counter for partial successful procedures
                        }
                        else
                        {
                            breathing_description = "not breathing";
                            circulation_description = "in cardiac arrest";
                            treatment_timeline.Items.Add("Patient Still in Cardiac Arrest");
                            treatment_timeline.Items.Add("Patient Still not breathing");            //patient still critical
                            circulation = 2;
                            breathing = 2;                                                          //patient condition quantifier set to 2 - critical
                            total_unsuccess_count += 1;                                             // counter for unsuccessful procedures
                        }
                        cpr2_count += 1;                                                           //counter for total critical procedures (successful and unsuccessful)
                        cpr_timedelay = cpr2_timedelay;                                           //set timedelay to variable for critical procedure
                    }
                    #endregion

                    from = "treatment"; to = "UIController"; subject = "update UI";
                    Environment_control_function();

                    from = "treatment"; to = "EMS"; subject = "Recommend next treatment";
                    Ems();

                }// end of circulation check
                #endregion



            } // end of Treatment method

            #endregion
        }
        #endregion

        void set_prediction_metrics()
        {
            #region set prediction metrics

            #region hemorrhage
            float cum_hem_0to1_prob = 0;
            float cum_hem_1to2_prob = 0;
            float cum_hem_0to2_prob = 0;

            if (initial_hemorrhage == 0)
            {
                cum_hem_0to1_prob = 0;
                cum_hem_1to2_prob = 0;
                cum_hem_0to2_prob = 0;
            }
            else if (initial_hemorrhage >2)
            {
                 cum_hem_0to1_prob = (100 - hem_deg_prob0to1); 
                 cum_hem_1to2_prob = (100 - hem_deg_prob1to2);
                 cum_hem_0to2_prob = (100 - (hem_deg_prob1to2/2));
            }

                float cum_hem_0nodeg_prob = 100 - (cum_hem_0to2_prob + cum_hem_0to1_prob);
            float cum_hem_1nodeg_prob = 50 - (cum_hem_0to1_prob + cum_hem_0to2_prob);
            float cum_hem_2to1_prob = hem2to1_cure_probability;
            float cum_hem_2to0_prob = hem2to0_cure_probability;
            float cum_hem_1to0_prob = hem1to0_cure_probability/2;
            float cum_hem_2noimp_prob = 100 - (cum_hem_2to0_prob + cum_hem_2to1_prob);
            float cum_hem_1noimp_prob = 50 - cum_hem_1to0_prob;

            #endregion

            #region circulation

            float cum_cir_0to1_prob = 1;
            if (initial_circulation == 0 && hemorrhage == 2 && circulation == 0 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
            { cum_cir_0to1_prob = hemcirc0to1_deg_prob_longtime; }
            else if (initial_circulation == 0 && hemorrhage == 2 && circulation == 0 && stopwatch_hem.Elapsed.TotalSeconds < 60)
            { cum_cir_0to1_prob = hemcirc0to1_deg_prob_shorttime; }
            else if (initial_circulation == 0 && hemorrhage < 2 && circulation == 0)
            { cum_cir_0to1_prob = 0f; }
            else if (initial_circulation == 2 && circulation == 0 && stopwatch_cir.Elapsed.TotalSeconds < 60)
            { cum_cir_0to1_prob = circ0to1if2_deg_prob_shorttime; }
            else if (initial_circulation == 2 && circulation == 0 && stopwatch_cir.Elapsed.TotalSeconds >= 60)
            { cum_cir_0to1_prob = circ0to1if2_deg_prob_longtime; }
            else if (initial_circulation == 1 && circulation == 0 && stopwatch_cir.Elapsed.TotalSeconds < 60)
            { cum_cir_0to1_prob = circ0to1if1_deg_prob_shorttime; }
            else if (initial_circulation == 1 && circulation == 0 && stopwatch_cir.Elapsed.TotalSeconds >= 60)
            { cum_cir_0to1_prob = circ0to1if1_deg_prob_longtime; }
            float cum_cir_1to2_prob = 1;
            if (initial_circulation == 0 && hemorrhage == 2 && circulation == 1 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
            { cum_cir_1to2_prob = hemcirc1to2_deg_prob_longtime/2f; }
            else if (initial_circulation == 0 && hemorrhage == 2 && circulation == 1 && stopwatch_hem.Elapsed.TotalSeconds < 60)
            { cum_cir_1to2_prob = hemcirc1to2_deg_prob_shorttime/2f; }
            else if (initial_circulation == 0 && hemorrhage < 2 && circulation == 1)
            { cum_cir_1to2_prob = 0f; }
            else if (initial_circulation == 2 && circulation == 1 && stopwatch_hem.Elapsed.TotalSeconds < 60)
            { cum_cir_1to2_prob = circ1to2if2_deg_prob_shorttime/2f; }
            else if (initial_circulation == 2 && circulation == 1 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
            { cum_cir_1to2_prob = circ1to2if2_deg_prob_longtime/2f; }
            else if (initial_circulation == 1 && circulation == 1 && stopwatch_hem.Elapsed.TotalSeconds < 60)
            { cum_cir_1to2_prob = circ1to2if1_deg_prob_shorttime/2f; }
            else if (initial_circulation == 1 && circulation == 1 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
            { cum_cir_1to2_prob = circ1to2if1_deg_prob_longtime/2f; }
            float cum_cir_0to2_prob = 1f;
            if (initial_circulation == 0 && hemorrhage == 2 && circulation == 0 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
            { cum_cir_0to2_prob = hemcirc0to2_deg_prob_longtime; }
            else if (initial_circulation == 0 && hemorrhage == 2 && circulation == 0 && stopwatch_hem.Elapsed.TotalSeconds < 60)
            { cum_cir_0to2_prob = hemcirc0to2_deg_prob_shorttime; }
            else if (initial_circulation == 0 && hemorrhage < 2 && circulation == 0)
            { cum_cir_0to2_prob = 0f; }
            else if (initial_circulation == 2 && circulation == 0 && stopwatch_hem.Elapsed.TotalSeconds < 60)
            { cum_cir_0to2_prob = circ0to2if2_deg_prob_shorttime; }
            else if (initial_circulation == 2 && circulation == 0 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
            { cum_cir_0to2_prob = circ0to2if2_deg_prob_longtime; }
            else if (initial_circulation == 1 && circulation == 0 && stopwatch_hem.Elapsed.TotalSeconds < 60)
            { cum_cir_0to2_prob = circ0to2if1_deg_prob_shorttime; }
            else if (initial_circulation == 1 && circulation == 0 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
            { cum_cir_0to2_prob = circ0to2if1_deg_prob_longtime; }
            float cum_cir_0nodeg_prob = 100f - (cum_cir_0to2_prob + cum_cir_0to1_prob);
            float cum_cir_1nodeg_prob = 50f - cum_cir_1to2_prob;
            float cum_cir_2to1_prob;
            if (breathing == 2) { cum_cir_2to1_prob = cpr2to1_cure_probability; }
            else { cum_cir_2to1_prob = circ2to1_cure_probability; }
            float cum_cir_2to0_prob;
            if (breathing == 2) { cum_cir_2to0_prob = cpr2to0_cure_probability; }
            else { cum_cir_2to0_prob = circ2to0_cure_probability; }
            float cum_cir_1to0_prob = circ1to0_cure_probability/2f;
            float cum_cir_2noimp_prob = 100 - (cum_cir_2to0_prob + cum_cir_2to1_prob);
            float cum_cir_1noimp_prob = 50- cum_cir_1to0_prob;

            #endregion

            #region airway

            float cum_air_0to1_prob = airway_close_prob0to1;
            float cum_air_1to2_prob = airway_close_prob1to2/2f;
            float cum_air_0to2_prob = 0;
            float cum_air_0nodeg_prob = 100 - (cum_air_0to2_prob + cum_air_0to1_prob);
            float cum_air_1nodeg_prob = 50 - cum_air_1to2_prob;
            float cum_air_2to1_prob = airway_2to1_cure_probability;
            float cum_air_2to0_prob = airway_2to0_cure_probability;
            float cum_air_1to0_prob = airway_1to0_cure_probability/2f;
            float cum_air_2noimp_prob = 100 - (cum_air_2to0_prob + cum_air_2to1_prob);
            float cum_air_1noimp_prob = 50 - cum_air_1to0_prob;

            #endregion

            #region consciousness
            float cum_con_0to1_prob = 1;
            if (hemorrhage == 2 && breathing < 2 && consciousness == 0 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
            { cum_con_0to1_prob = hemcon0to1_deg_prob_longtime; }
            else if (hemorrhage == 2 && breathing < 2 && consciousness == 0 && stopwatch_hem.Elapsed.TotalSeconds < 60)
            { cum_con_0to1_prob = hemcon0to1_deg_prob_shorttime; }
            else if (hemorrhage < 2 && breathing < 2 && consciousness == 0)
            { cum_con_0to1_prob = 0; }
            else if (hemorrhage < 2 && breathing == 2 && consciousness == 0 && stopwatch_bre.Elapsed.TotalSeconds >= 60)
            { cum_con_0to1_prob = brecon0to1_deg_prob_longtime; }
            else if (hemorrhage < 2 && breathing == 2 && consciousness == 0 && stopwatch_bre.Elapsed.TotalSeconds < 60)
            { cum_con_0to1_prob = brecon0to1_deg_prob_shorttime; }
            else if (hemorrhage < 2 && breathing < 2 && consciousness == 0)
            { cum_con_0to1_prob = 0; }
            float cum_con_1to2_prob = 1;
            if (hemorrhage == 2 && breathing == 2 && consciousness == 1 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
            { cum_con_1to2_prob = hemcon1to2_deg_prob_longtime / 2f; }
            else if (hemorrhage == 2 && breathing == 2 && consciousness == 1 && stopwatch_hem.Elapsed.TotalSeconds < 60)
            { cum_con_1to2_prob = hemcon1to2_deg_prob_shorttime / 2f; }
            else if (hemorrhage == 2 && breathing < 2 && consciousness == 1 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
            { cum_con_1to2_prob = hemcon1to2_deg_prob_longtime/2f; }
            else if (hemorrhage == 2 && breathing < 2 && consciousness == 1 && stopwatch_hem.Elapsed.TotalSeconds < 60)
            { cum_con_1to2_prob = hemcon1to2_deg_prob_shorttime/2f; }
            else if (hemorrhage < 2 && breathing == 2 && consciousness == 1 && stopwatch_bre.Elapsed.TotalSeconds >= 60)
            { cum_con_1to2_prob = brecon1to2_deg_prob_longtime/2f; }
            else if (hemorrhage < 2 && breathing == 2 && consciousness == 1 && stopwatch_bre.Elapsed.TotalSeconds < 60)
            { cum_con_1to2_prob = brecon1to2_deg_prob_shorttime/2f; }
            else if (hemorrhage < 2 && breathing < 2 && consciousness == 1)
            { cum_con_1to2_prob = 0; }
            float cum_con_0to2_prob = 1;
            if (hemorrhage == 2 && breathing < 2 && consciousness == 0 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
            { cum_con_0to2_prob = hemcon0to2_deg_prob_longtime; }
            else if (hemorrhage == 2 && breathing < 2 && consciousness == 0 && stopwatch_hem.Elapsed.TotalSeconds < 60)
            { cum_con_0to2_prob = hemcon0to2_deg_prob_shorttime; }
            else if (hemorrhage < 2 && breathing < 2 && consciousness == 0)
            { cum_con_0to2_prob = 0; }
            else if (hemorrhage < 2 && breathing == 2 && consciousness == 0 && stopwatch_hem.Elapsed.TotalSeconds >= 60)
            { cum_con_0to2_prob = brecon0to2_deg_prob_longtime; }
            else if (hemorrhage < 2 && breathing == 2 && consciousness == 0 && stopwatch_hem.Elapsed.TotalSeconds < 60)
            { cum_con_0to2_prob = brecon0to2_deg_prob_shorttime; }
            else if (hemorrhage < 2 && breathing < 2 && consciousness == 0)
            { cum_con_0to2_prob = 0; }

            float cum_con_0nodeg_prob = 100 - (cum_con_0to2_prob + cum_con_0to1_prob);
            float cum_con_1nodeg_prob = 50 - cum_con_1to2_prob;
            float cum_con_2to1_prob = conc2to1_cure_probability;
            float cum_con_2to0_prob = conc2to0_cure_probability;
            float cum_con_1to0_prob = conc1to0_cure_probability/2f;
            float cum_con_2noimp_prob = 100f - (cum_con_2to0_prob + cum_con_2to1_prob);
            float cum_con_1noimp_prob = 50f - cum_con_1to0_prob;

            #endregion

            #region breathing

            float cum_bre_0to1_prob =0;
            if (airway == 0) { cum_bre_0to1_prob = breath_deg_prob0to1; }
            else { cum_bre_0to1_prob = breair_prob0to1; }

            float cum_bre_0to2_prob = 0;
            if (airway == 0) { cum_bre_0to2_prob = breath_deg_prob0to2; }
            else { cum_bre_0to2_prob = breair_prob0to2; }

            float cum_bre_0nodeg_prob = 0;
            if (airway == 0) { cum_bre_0nodeg_prob = breath_deg_prob0to0; }
            else { cum_bre_0nodeg_prob = 100 - (cum_bre_0to2_prob + cum_bre_0to1_prob); }

            float cum_bre_1to2_prob =0;
            if (airway ==0) { cum_bre_1to2_prob = breath_deg_prob1to2/2f; }
            else { cum_bre_1to2_prob = breair_prob1to2 / 2f; }


            float cum_bre_1nodeg_prob;
            if (airway == 0) { cum_bre_1nodeg_prob = breath_deg_prob1to1/2f; }
            else { cum_bre_1nodeg_prob = 50 - cum_bre_1to2_prob; }


            float cum_bre_1to0_prob = breath1to0_cure_probability/2f;
            float cum_bre_1noimp_prob = 50 - cum_bre_1to0_prob;
            float cum_bre_2to0_prob;
            if (circulation == 2) { cum_bre_2to0_prob = cpr2to0_cure_probability; }
            else { cum_bre_2to0_prob = breath2to0_cure_probability; }
            float cum_bre_2to1_prob;
            if (circulation == 2) { cum_bre_2to1_prob = cpr2to1_cure_probability; }
            else { cum_bre_2to1_prob = breath2to1_cure_probability; }
            float cum_bre_2noimp_prob = 100 - (cum_bre_2to0_prob + cum_bre_2to1_prob);
            #endregion

            #endregion

            #region set hem heights
            float hem_zero_level = 0.3f;
            float hem_one_level = 0.4f;
            float hem_two_level = 0.3f;

            if (hemorrhage == 0)
            {
                hem_zero_level = (cum_hem_0to2_prob + cum_hem_0to1_prob + cum_hem_0nodeg_prob) /100f;
                hem_one_level = (cum_hem_0to2_prob + cum_hem_0to1_prob) / 100f;
                hem_two_level = cum_hem_0to2_prob/100f;
            }
            else if (hemorrhage == 1)
            {
                hem_zero_level = (cum_hem_1to2_prob + cum_hem_1noimp_prob + cum_hem_1nodeg_prob + cum_hem_1to0_prob)/100f;
                hem_one_level = (cum_hem_1to2_prob + cum_hem_1noimp_prob + cum_hem_1nodeg_prob)/100f;
                hem_two_level = cum_hem_1to2_prob/100f;
            }
            else if (hemorrhage == 2)
            {
                hem_zero_level = (cum_hem_2noimp_prob + cum_hem_2to1_prob + cum_hem_2to0_prob)/100f;
                hem_one_level = (cum_hem_2noimp_prob + cum_hem_2to1_prob)/100f;
                hem_two_level = cum_hem_2noimp_prob/100f;
            }
            #endregion

            #region set con levels
            float con_zero_level = 0.3f;
            float con_one_level = 0.4f;
            float con_two_level = 0.3f;

            if (consciousness == 0)
            {
                con_zero_level = (cum_con_0to2_prob + cum_con_0to1_prob + cum_con_0nodeg_prob) / 100f;
                con_one_level = (cum_con_0to2_prob + cum_con_0to1_prob) / 100f;
                con_two_level = cum_con_0to2_prob / 100f;
            }
            else if (consciousness == 1)
            {
                con_zero_level = (cum_con_1to2_prob + cum_con_1noimp_prob + cum_con_1nodeg_prob + cum_con_1to0_prob) / 100f;
                con_one_level = (cum_con_1to2_prob + cum_con_1noimp_prob + cum_con_1nodeg_prob) / 100f;
                con_two_level = cum_con_1to2_prob/100f;
            }
            else if (consciousness == 2)
            {
                con_zero_level = (cum_con_2noimp_prob + cum_con_2to1_prob + cum_con_2to0_prob) / 100f;
                con_one_level = (cum_con_2noimp_prob + cum_con_2to1_prob) / 100f;
                con_two_level = cum_con_2noimp_prob / 100f;
            }

            #endregion

            #region set cir levels
            float cir_zero_level = 0.3f;
            float cir_one_level = 0.4f;
            float cir_two_level = 0.3f;

            if (circulation == 0)
            {
                cir_zero_level = (cum_cir_0to2_prob + cum_cir_0to1_prob + cum_cir_0nodeg_prob) / 100f;
                cir_one_level = (cum_cir_0to2_prob + cum_cir_0to1_prob) / 100f;
                cir_two_level = cum_cir_0to2_prob / 100f;
            }
            else if (circulation == 1)
            {
                cir_zero_level = (cum_cir_1to2_prob + cum_cir_1noimp_prob + cum_cir_1nodeg_prob + cum_cir_1to0_prob) / 100f;
                cir_one_level = (cum_cir_1to2_prob + cum_cir_1noimp_prob + cum_cir_1nodeg_prob) / 100f;
                cir_two_level = cum_cir_1to2_prob/100f;
            }
            else if (circulation == 2)
            {
                cir_zero_level = (cum_cir_2noimp_prob + cum_cir_2to1_prob + cum_cir_2to0_prob) / 100f;
                cir_one_level = (cum_cir_2noimp_prob + cum_cir_2to1_prob) / 100f;
                cir_two_level = cum_cir_2noimp_prob / 100f;
            }

            #endregion

            #region set air levels
            float air_zero_level = 0.3f;
            float air_one_level = 0.4f;
            float air_two_level = 0.3f;

            if (airway == 0)
            {
                air_zero_level = (cum_air_0to2_prob + cum_air_0to1_prob + cum_air_0nodeg_prob) / 100f;
                air_one_level = (cum_air_0to2_prob + cum_air_0to1_prob) / 100f;
                air_two_level = cum_air_0to2_prob / 100f;
            }
            else if (airway == 1)
            {
                air_zero_level = (cum_air_1to2_prob + cum_air_1noimp_prob + cum_air_1nodeg_prob + cum_air_1to0_prob) / 100f;
                air_one_level = (cum_air_1to2_prob + cum_air_1noimp_prob + cum_air_1nodeg_prob) / 100f;
                air_two_level = cum_air_1to2_prob/100f;
            }
            else if (airway == 2)
            {
                air_zero_level = (cum_air_2noimp_prob + cum_air_2to1_prob + cum_air_2to0_prob) / 100f;
                air_one_level = (cum_air_2noimp_prob + cum_air_2to1_prob) / 100f;
                air_two_level = cum_air_2noimp_prob / 100f;
            }

            #endregion

            #region set bre levels
            float bre_zero_level = 0.3f;
            float bre_one_level = 0.4f;
            float bre_two_level = 0.3f;

            if (breathing == 0)
            {
                bre_zero_level = (cum_bre_0to2_prob + cum_bre_0to1_prob + cum_bre_0nodeg_prob) / 100f;
                bre_one_level = (cum_bre_0to2_prob + cum_bre_0to1_prob) / 100f;
                bre_two_level = cum_bre_0to2_prob / 100f;
            }
            else if (breathing == 1)
            {
                bre_zero_level = (cum_bre_1to2_prob + cum_bre_1noimp_prob + cum_bre_1nodeg_prob + cum_bre_1to0_prob) / 100f;
                bre_one_level = (cum_bre_1to2_prob + cum_bre_1noimp_prob + cum_bre_1nodeg_prob) / 100f;
                bre_two_level = cum_bre_1to2_prob/100f;
            }
            else if (breathing == 2)
            {
                bre_zero_level = (cum_bre_2noimp_prob + cum_bre_2to1_prob + cum_bre_2to0_prob) / 100f;
                bre_one_level = (cum_bre_2noimp_prob + cum_bre_2to1_prob) / 100f;
                bre_two_level = cum_bre_2noimp_prob / 100f;
            }

            #endregion


            float[] fail_prob = { cir_two_level, bre_two_level, con_two_level, hem_two_level, air_two_level };

            overall_success_prob = (100f - (fail_prob.Max() * 100f));

            success_prob.Text = Convert.ToString(overall_success_prob);


            // add the indicator drawing part here
            hem_pred_panel.BackgroundImage = DrawPredictor(hem_two_level, hem_one_level);
            air_pred_panel.BackgroundImage = DrawPredictor(air_two_level, air_one_level);
            bre_pred_panel.BackgroundImage = DrawPredictor(bre_two_level, bre_one_level);
            conc_pred_panel.BackgroundImage = DrawPredictor(con_two_level, con_one_level);
            circ_pred_panel.BackgroundImage = DrawPredictor(cir_two_level, cir_one_level);

            #region draw the predictor
            Bitmap DrawPredictor(float worse_level, float stay_level)
            {
                #region assign variables
                int wid = 80; //indicator sise sizing
                int hgt = 400;

                Color bg_color = Color.FromName("White");
                Color worsen_color = Color.FromName("Red");
                Color outline_color = Color.FromName("Black");
                Color improve_color = Color.FromName("green");
                Color stay_color = Color.FromName("orange");


                //float indicator_level = 1f - (0.333f * pat_char_stat); //determine the indicator level depending on the specific patient statistic;
                bool striped = true; // add the stripes across the UI
                #endregion

                #region draw the indicator and return to calling class/object
                Bitmap bm = new Bitmap(wid, hgt);
                using (Graphics gr = Graphics.FromImage(bm))
                {
                    // OLD CODE FROM EXAMPLE - to rotate indicator if needed - kept in for future reference - DELETE LATER
                    #region old code
                    // If the indicator has a horizontal orientation,
                    // rotate so we can draw it vertically.
                    //if (wid > hgt)
                    //{
                    //    gr.RotateTransform(90, MatrixOrder.Append);
                    //    gr.TranslateTransform(wid, 0, MatrixOrder.Append);
                    //    int temp = wid;
                    //    wid = hgt;
                    //    hgt = temp;
                    //}
                    #endregion

                    // Draw the battery.
                    DrawVerticalpredictor(gr, stay_level, worse_level, wid, hgt, bg_color, worsen_color, outline_color, stay_color, improve_color, striped);
                }
                return bm;
                #endregion
            }
            #endregion


            #region code to actually construct rectangles for indicators
            // Draw a vertically oriented battery with
            // the indicated percentage filled in.
            void DrawVerticalpredictor(Graphics gr, float stay_level, float worse_level, int wid, int hgt, Color bg_color, Color Worse_Color, Color outline_color, Color Stay_Color, Color Improve_Color, bool striped)
            {
                gr.Clear(bg_color);
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                // Make a rectangle for the main body.
                float thickness = hgt*(1f/(400f/160f)) / 20f;
                RectangleF body_rect = new RectangleF(
                    thickness * 0.5f, thickness * 1f,
                    wid - thickness, hgt - thickness * 2f);

                using (Pen pen = new Pen(outline_color, thickness))
                {
                    // Fill the body
                    using (Brush brush = new SolidBrush(Improve_Color))
                    {
                        gr.FillRectangle(brush, body_rect);
                    }

                    ;
                    //Color worse_Color = Color.FromName("red");

                    // Fill the required area with color.
                    float stay_hgt = body_rect.Height * stay_level;
                    RectangleF stay_rect = new RectangleF(
                        body_rect.Left, body_rect.Bottom - stay_hgt,
                        body_rect.Width, stay_hgt);
                    using (Brush brush = new SolidBrush(Stay_Color))
                    {
                        gr.FillRectangle(brush, stay_rect);
                    }

                    // Fill the required area with color.
                    float worse_hgt = body_rect.Height * worse_level;
                    RectangleF worse_rect = new RectangleF(
                        body_rect.Left, body_rect.Bottom - worse_hgt,
                        body_rect.Width, worse_hgt);
                    using (Brush brush = new SolidBrush(Worse_Color))
                    {
                        gr.FillRectangle(brush, worse_rect);
                    }




                    // Optionally stripe multiples of 25%
                    if (striped)
                        for (int i = 1; i <= 3; i++)
                        {
                            float y = body_rect.Bottom -
                                i * 0.25f * body_rect.Height;
                            gr.DrawLine(pen, body_rect.Left, y,
                                body_rect.Right, y);
                        }

                    // Draw the main body.
                    gr.DrawPath(pen, MakeRoundedRect(
                        body_rect, thickness, thickness,
                        true, true, true, true));

                    // Draw the positive terminal. //
                    //RectangleF terminal_rect = new RectangleF(
                    //    wid / 2f - thickness, 0,
                    //    2 * thickness, thickness);
                    //gr.DrawPath(pen, MakeRoundedRect(
                    //    terminal_rect, thickness / 2f, thickness / 2f,
                    //    true, true, false, false));
                }
            }

            // Draw a rectangle in the indicated Rectangle
            // rounding the indicated corners.
            #region code to make rounded rectangle
            GraphicsPath MakeRoundedRect(
                RectangleF rect, float xradius, float yradius,
                bool round_ul, bool round_ur, bool round_lr, bool round_ll)
            {
                // Make a GraphicsPath to draw the rectangle.
                PointF point1, point2;
                GraphicsPath path = new GraphicsPath();

                // Upper left corner.
                if (round_ul)
                {
                    RectangleF corner = new RectangleF(
                        rect.X, rect.Y,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 180, 90);
                    point1 = new PointF(rect.X + xradius, rect.Y);
                }
                else point1 = new PointF(rect.X, rect.Y);

                // Top side.
                if (round_ur)
                    point2 = new PointF(rect.Right - xradius, rect.Y);
                else
                    point2 = new PointF(rect.Right, rect.Y);
                path.AddLine(point1, point2);

                // Upper right corner.
                if (round_ur)
                {
                    RectangleF corner = new RectangleF(
                        rect.Right - 2 * xradius, rect.Y,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 270, 90);
                    point1 = new PointF(rect.Right, rect.Y + yradius);
                }
                else point1 = new PointF(rect.Right, rect.Y);

                // Right side.
                if (round_lr)
                    point2 = new PointF(rect.Right, rect.Bottom - yradius);
                else
                    point2 = new PointF(rect.Right, rect.Bottom);
                path.AddLine(point1, point2);

                // Lower right corner.
                if (round_lr)
                {
                    RectangleF corner = new RectangleF(
                        rect.Right - 2 * xradius,
                        rect.Bottom - 2 * yradius,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 0, 90);
                    point1 = new PointF(rect.Right - xradius, rect.Bottom);
                }
                else point1 = new PointF(rect.Right, rect.Bottom);

                // Bottom side.
                if (round_ll)
                    point2 = new PointF(rect.X + xradius, rect.Bottom);
                else
                    point2 = new PointF(rect.X, rect.Bottom);
                path.AddLine(point1, point2);

                // Lower left corner.
                if (round_ll)
                {
                    RectangleF corner = new RectangleF(
                        rect.X, rect.Bottom - 2 * yradius,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 90, 90);
                    point1 = new PointF(rect.X, rect.Bottom - yradius);
                }
                else point1 = new PointF(rect.X, rect.Bottom);

                // Left side.
                if (round_ul)
                    point2 = new PointF(rect.X, rect.Y + yradius);
                else
                    point2 = new PointF(rect.X, rect.Y);
                path.AddLine(point1, point2);

                // Join with the start point.
                path.CloseFigure();

                return path;
            }
            #endregion

            #endregion










        }



        // EMS Agent Start
        #region create New EMS Agent
        public void Ems()
        {
            if (from == "patient" && subject == "Initial status available")
            {
                RecommendTreatment();
            }
            else if (subject == "Recommend next treatment")
            {
                RecommendTreatment();
            }
            else if (subject == "check_patient_degradation")
            {
                subject = "patient_degradation";
                treatment_timeline.Items.Add("Checking patient condition");
                patient();
            }
            void RecommendTreatment()
            {
                #region action message definition //defines the AI agent response to information transferred from EMS agent
                string act = null;
                hem = hemorrhage;
                con = consciousness;
                air = airway;
                bre = breathing;
                cir = circulation;

                //  hierachy of action
                if (hem == 2 && cir == 2 && bre == 2)
                    if ((circ2_count - 1) <= hem2_count)
                    {
                        act = "check CPR";
                    }
                    else
                    {
                        act = "check hemorrhage";
                    }
                else if (cir == 2 && hem == 2 && bre < 2)
                    if ((circ2_count) <= hem2_count)
                    {
                        act = "check circulation";
                    }
                    else
                    {
                        act = "check hemorrhage";
                    }
                else if (cir == 2 && bre == 2 && hem < 2)
                {
                    act = "check CPR";
                }
                else if (cir == 2 && hem < 2 && bre < 2)
                {
                    act = "check circulation";
                }
                else if (hem == 2 && cir < 2 && bre < 2)
                {
                    act = "check hemorrhage";
                }
                else if (hem == 2 && bre ==2 && cir < 2)
                    if (hem2_count - 1 <= breath2_count)
                    {
                        act = "check hemorrhage";
                    }
                    else
                    {
                        act = "check breathing";
                    }
                else if (cir == 2)
                {
                    act = "check circulation";
                }
                else if (hem == 2)
                {
                    act = "check hemorrhage";
                }
                else if (air == 2)
                {
                    act = "check airway";
                }
                else if (bre == 2 && cir < 2 && hem < 2)
                {
                    act = "check breathing";
                }
                else if (con == 2)
                {
                    act = "check consciousness";
                }
                else if (hem == 1 && cir == 1 && bre == 1)
                    if ((circ1_count - 1) <= hem1_count)
                    {
                        act = "check circulation";
                    }
                    else
                    {
                        act = "check hemorrhage";
                    }
                else if (cir == 1 && bre == 1 && hem < 1)
                {
                    act = "check circulation";
                }
                else if (hem == 1 && cir == 1 && bre < 1)
                    if ((circ1_count - 1) <= hem1_count)
                    {
                        act = "check circulation";
                    }
                    else
                    {
                        act = "check hemorrhage";
                    }
                else if (bre == 1 && hem == 1 && cir < 1)
                    if ((hem1_count) <= breath1_count)
                    {
                        act = "check hemorrhage";
                    }
                    else
                    {
                        act = "check breathing";
                    }
                else if (cir == 1 && hem < 1 && bre < 1)
                {
                    act = "check circulation";
                }
                else if (hem == 1 && cir < 1 && bre < 1)
                {
                    act = "check hemorrhage";
                }
                else if (air == 1)
                {
                    act = "check airway";
                }
                else if (bre == 1 && cir < 1 && hem < 1)
                {
                    act = "check breathing";
                }
                else if (hem > 0)
                {
                    act = "check hemorrhage";
                }
                else if (cir > 0)
                {
                    act = "check circulation";
                }
                else if (air > 0)
                {
                    act = "check airway";
                }
                else if (bre > 0)
                {
                    act = "check breathing";
                }
                else if (con > 0)
                {
                    act = "check consciousness";
                }
                else
                {
                    patstablecall(overall_success_prob);

                    async void patstablecall(float overall_success_prob)
                    {


                        var pat_stable = new Patient_Stable(overall_success_prob);
                        pat_stable.Show();
                        await Task.Delay(2000);
                        pat_stable.Close();

                        treatment_timeline.Items.Add("Patient stablized");
                    }
                }
                // In this block AI will perform its value function MDP and  recommend the actions
                /*  Get the patient variable value here and recommend EMS to perform that action*/
                /*  Markov decision process will be coded here and based of the output it will 
                    select actions and reply EMS to apply that action
                    Make that suggestion to cahneg action automatically*/

                /************************************************************************************************

                                    Had to add MARKOV DECISION PROCESS CODE OVER HERE for suggestion ///         

                **************************************************************************************************/

                action = act;
                subject = "highlight buttons";
                Environment_control_function();
                #endregion
            }

        } // End of EMS agent
        #endregion

        //Create AI agent thread
        #region AI thread generate 

        SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
        SpeechSynthesizer speech = new SpeechSynthesizer();

        public void AI()
        {
            #region patient degradation checker
            if (subject == "start degradation check reminder")
            {
                degrade_caller();
            }
            
            async void degrade_caller()
            {
                
                do
                {
                    //ask EMS to check patient degradation every 20s 
                    await Task.Delay(20200);
                    subject = "check_patient_degradation";
                    Ems();
                    await Task.Delay(50);

                    string current_time = Convert.ToString(stopwatch_sim.Elapsed);

                } while (stopwatch_sim.Elapsed.TotalSeconds < 110);

            }
            #endregion

            // Speaking function for EMS for which action to take
            #region AI speech elements
            void speakAI(string action)
            {
                if (action == " apply torniquet")
                {
                    speech.SpeakAsync(" You should probably " + action);
                }
                if (action == " check consciousness")
                {
                    speech.SpeakAsync(" You should probably " + action);
                }
                if (action == " check airway")
                {
                    speech.SpeakAsync(" You should probably " + action);
                }
                if (action == " check breathing")
                {
                    speech.SpeakAsync(" You should probably " + action);
                }
                if (action == " check circulation")
                {
                    speech.SpeakAsync(" You should probably " + action);
                }
            }
            #endregion

            #region AI transfer info to transfer of care form

            #endregion

            #region determine patient baseline characteristics at the end of TOC

            #endregion
        }
        #endregion

        // define patient health variables
        #region Variables of patient health
        public int age_p { get; set; }
        public static int age { get; set; }
        public static int hemorrhage { get; set; }
        public static int consciousness { get; set; }
        public static int airway { get; set; }
        public static int breathing { get; set; }
        public static int circulation { get; set; }
        public static string gender { get; set; }
        public static int initial_hemorrhage { get; set; }
        public static int initial_consciousness { get; set; }
        public static int initial_airway { get; set; }
        public static int initial_breathing { get; set; }
        public static int initial_circulation { get; set; }

        #endregion

        //button click events for UI
        #region button events        

        // click transfer button
        #region AI patient transfer button click event
        private void AI_patient_transfer_button_Click(object sender, EventArgs e)
        {
            //starttime = DateTime.Now;
            //var inputform_click = new inputform(treatment_timeline.Items);
            ////Inputform1.Update();
            //inputform_click.Show();

        }

        private void AI_patient_transfer_button_Click_1(object sender, EventArgs e)
        {
            //starttime = DateTime.Now;
            var transfer_form_start = new inputform(treatment_timeline.Items);
            transfer_form_start.Show();
            UICounter = 0; //Reset UI counter
            counter = 120; // reset timer counter
            cpr2_count = 0; //reset cpr counter
            airway1_count = 0;
            airway2_count = 0;
            circ2_count = 0;
            circ1_count = 0;
            mistake_count = 0;
            total_unsuccess_count = 0;
            total_success_count = 0;
            conc1_count = 0;
            conc2_count = 0;
            hem1_count = 0;
            hem2_count = 0;
            breath1_count = 0;
            breath2_count = 0;
            patient_circ_instability_score = 0;
            patient_bre_instability_score = 0;
            patient_con_instability_score = 0;
            patient_hem_instability_score = 0;
            patient_air_instability_score = 0;
            stopwatch_air.Stop();
            stopwatch_air.Reset();
            stopwatch_hem.Stop();
            stopwatch_hem.Reset();
            stopwatch_con.Stop();
            stopwatch_con.Reset();
            stopwatch_cir.Stop();
            stopwatch_cir.Reset();
            stopwatch_bre.Stop();
            stopwatch_bre.Reset();
            blocker.Visible = true;
            blocker.SendToBack();

            if (hemorrhage>1||circulation>1||breathing>1||airway>1)
            {
                criticality = "critical";
            }
            else if ((hemorrhage + circulation + breathing + airway + consciousness) >= 4)
            {
                criticality = "critical";
            }
            else if ((hemorrhage + circulation + breathing + airway + consciousness) >= 1)
            {
                criticality = "priority";
            }
            else
            {
                criticality = "routine";
            }

            this.Hide();
        }
        #endregion

        // button actions to implement highlighted treatment option
        #region buttons to action the treatment

        private void Button_hemm_torniquet_Click(object sender, EventArgs e)
        {
            Button_hemm_torniquet.BackColor = Color.FromName("Menu");
            action = "check hemorrhage";
            subject = "treatment";
            patient();
            //Treatment("check hemorrhage");
        }
        private void Button_hemm_treat_Click(object sender, EventArgs e)
        {
            Button_hemm_treat.BackColor = Color.FromName("Menu");
            action = "check hemorrhage";
            subject = "treatment";
            patient();
            //Treatment("check hemorrhage");
        }

        private void Button_conc_drugs_Click(object sender, EventArgs e)
        {
            Button_conc_drugs.BackColor = Color.FromName("Menu");
            action = "check consciousness";
            subject = "treatment";
            patient();
            //Treatment("check consciousness");
        }

        private void Button_air_Intubate_Click(object sender, EventArgs e)
        {
            Button_air_Intubate.BackColor = Color.FromName("Menu");
            action = "check airway";
            subject = "treatment";
            patient();
            //Treatment("check airway");
        }

        private void Button_air_clearair_Click(object sender, EventArgs e)
        {
            Button_air_clearair.BackColor = Color.FromName("Menu");
            action = "check airway";
            subject = "treatment";
            patient();
            //Treatment("check airway");
        }

        private void Button_circ_CPR_Click(object sender, EventArgs e)
        {
            Button_circ_chest.BackColor = Color.FromName("Menu");
            action = "check circulation";
            subject = "treatment";
            patient();
            //Treatment("check circulation");
        }

        private void Button_circ_drugs_Click(object sender, EventArgs e)
        {
            Button_circ_drugs.BackColor = Color.FromName("Menu");
            action = "check circulation";
            subject = "treatment";
            patient();            
            //Treatment("check circulation");
        }

        private void Button_breath_Oxygen_Click(object sender, EventArgs e)
        {
            Button_breath_Oxygen.BackColor = Color.FromName("Menu");
            action = "check breathing";
            subject = "treatment";
            patient();
            //Treatment("check breathing");
        }

        private void Button_breath_rescuebreath_Click(object sender, EventArgs e)
        {
            Button_breath_aspirate.BackColor = Color.FromName("Menu");
            action = "check breathing";
            subject = "treatment";
            patient();
            //Treatment("check breathing");
        }
        private void Button_CPR_Click(object sender, EventArgs e)
        {
            Button_CPR.BackColor = Color.FromName("Menu");
            action = "check CPR";
            subject = "treatment";
            patient();
            //Treatment("check CPR");
        }
        #endregion

        #endregion

        // Dump old event handlers in here until I can work out how to clear them!!!
        #region unused_code

        private void sim_finalair_box_TextChanged(object sender, EventArgs e)
        {
        }

        private void sim_finalbreath_box_TextChanged(object sender, EventArgs e)
        {
        }

        private void sim_finalcirc_box_TextChanged(object sender, EventArgs e)
        {
        }

        private void sim_finalconc_box_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void give_drugshem_Click_1(object sender, EventArgs e)
        {
        }

        private void give_drugscir_Click_1(object sender, EventArgs e)
        {
        }

        private void give_drugsair_Click_1(object sender, EventArgs e)
        {
        }

        private void give_drugsbre_Click(object sender, EventArgs e)
        {
        }

        private void sim_finalhem_box_TextChanged(object sender, EventArgs e)
        {
        }

        private void Button_breath_drugs_Click(object sender, EventArgs e)
        {
        }
        private void Sim_inthem_box_TextChanged(object sender, EventArgs e)
        {
        }

        private void apply_torniquet_Click(object sender, EventArgs e)
        {
        }

        private void sim_gender_label_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void sim_intbreath_box_TextChanged(object sender, EventArgs e)
        {
        }

        private void sim_intcirc_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void sim_injury_location_label_Click(object sender, EventArgs e)
        {
        }

        private void treatment_timeline_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
        #endregion

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void Injury_level_description_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
    }
}