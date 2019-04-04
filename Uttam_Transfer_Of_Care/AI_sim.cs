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
        public static int cir_count = 0;
        public static int total_success_count = 0;
        public static int total_partial_success_count = 0;
        public static int total_unsuccess_count = 0;
        public static string age_group = "unkown";
        public static string wound_location;
        public static int wound_loc_ID;
        public static int wound_type_ID;
        public static string wound_type;
        public static string breathing_description = "no breathing problem";
        public static string airway_description = "airway clear";
        public static string hemorrhage_description = "no bleeding";
        public static string consciousness_decription = "fully conscious";
        public static string circulation_description = "no heart or circulation problem";
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
        //probabilty that the state will not change
        public static int hem_deg_prob_longtime = 80;
        public static int hem_deg_prob_shorttime = 95;
        public static int circ_deg_prob_longtime = 80;
        public static int circ_deg_prob_shorttime = 95;
        public static int air_deg_prob_longtime = 80;
        public static int air_deg_prob_shorttime = 95;
        public static int conc_deg_prob_longtime = 50;
        public static int conc_deg_prob_shorttime = 75;
        public static int conc_imp_prob_longtime = 50;
        public static int conc_imp_prob_shorttime = 25;
        public static int breath_deg_prob_longtime = 80;
        public static int breath_deg_prob_shorttime = 95;
        public static int patient_instability_score = 0;
        // probability of changing state
        public static int hemcon1to2_deg_prob_shorttime = 25;
        public static int hemcon1to2_deg_prob_longtime = 95;
        public static int hemcon0to2_deg_prob_shorttime = 25;
        public static int hemcon0to1_deg_prob_shorttime = 25;
        public static int hemcon0to1_deg_prob_longtime = 40;
        public static int hemcon0to2_deg_prob_longtime = 40;
        public static int hemcirc1to2_deg_prob_shorttime = 35;
        public static int hemcirc1to2_deg_prob_longtime = 95;
        public static int hemcirc0to2_deg_prob_shorttime = 30;
        public static int hemcirc0to1_deg_prob_shorttime = 20;
        public static int hemcirc0to1_deg_prob_longtime = 45;
        public static int hemcirc0to2_deg_prob_longtime = 45;
        public static int major_heart_attack_probability;

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

        #endregion
        
        public AI_sim()
        {
            InitializeComponent();
        }
        //Stopwatch stopwatch = new Stopwatch(); removed - check

        //determine whether or not to run the AI assited form (set by variable on click from previous form
        #region Set AI assist value
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
        }
        #endregion

        // Click start Simulation button - Initiates environmental controller and subsequently the agents - starts a Patient Agent thread and EMS agent thread
        #region start simulation button click
        private void button1_Click_2(object sender, EventArgs e)
        {
            button1.BackColor = Color.Empty;
            from = "start_environment";
            Environment_control_function();
        }
        #endregion

        //timers and agent start/stop controllers
        public void Environment_control_function()
        {
            #region initiate characteristics stopwatches and start sim stopwatch
                stopwatch_sim.Start();
            #endregion

            #region Start patient and EMS agents
                from = "called by main function";
                patient();
                Ems();
                //Thread Patient = new Thread(() => patient()); //Run Patient agent in thread called Patient RENAME THREAD! 
                //Patient.Start();
                //Thread EMS = new Thread(() => Ems()); // Run EMS agent in thread called EMS RENAME THREAD!
                //EMS.Start(); Remove threading - CHECK!
            #endregion

            #region patient degradation checker
            degrade_caller();

            async void degrade_caller()
            {
                await Task.Delay(10000);
                from = "update_patient_degradation";
                patient();
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
                wound_type_ID = random.Next(0, 4);
                if (wound_type_ID == 0) wound_type = "gunshot";
                else if (wound_type_ID == 1) wound_type = "blunt force trauma";
                else if (wound_type_ID == 2) wound_type = "drowning";
                else if (wound_type_ID == 3) wound_type = "Heart Attack";
                else wound_type = "allergic  reaction";
                #endregion

                #region set wound location strings
                wound_loc_ID = random.Next(1, 23);
                if (wound_type_ID > 1)
                {
                    wound_location = "internal/global";
                }
                else
                {
                    if (wound_loc_ID == 1) wound_location = "front of head";
                    else if (wound_loc_ID == 2) wound_location = "front of torso";
                    else if (wound_loc_ID == 3) wound_location = "front of mid-section";
                    else if (wound_loc_ID == 4) wound_location = "front of upper right arm";
                    else if (wound_loc_ID == 5) wound_location = "front of upper left arm";
                    else if (wound_loc_ID == 6) wound_location = "front of lower right arm";
                    else if (wound_loc_ID == 7) wound_location = "front of lower left arm";
                    else if (wound_loc_ID == 8) wound_location = "front of upper right leg";
                    else if (wound_loc_ID == 9) wound_location = "front of upper left leg";
                    else if (wound_loc_ID == 10) wound_location = "front of lower right leg";
                    else if (wound_loc_ID == 11) wound_location = "front of lower left leg";
                    else if (wound_loc_ID == 12) wound_location = "back of head";
                    else if (wound_loc_ID == 13) wound_location = "back of torso";
                    else if (wound_loc_ID == 14) wound_location = "back of mid-section";
                    else if (wound_loc_ID == 15) wound_location = "back of upper right arm";
                    else if (wound_loc_ID == 16) wound_location = "back of upper left arm";
                    else if (wound_loc_ID == 17) wound_location = "back of lower right arm";
                    else if (wound_loc_ID == 18) wound_location = "back of lower left arm";
                    else if (wound_loc_ID == 19) wound_location = "back of upper right leg";
                    else if (wound_loc_ID == 20) wound_location = "back of upper left leg";
                    else if (wound_loc_ID == 21) wound_location = "back of lower right leg";
                    else wound_location = "back of lower left leg";
                }
                #endregion

                #region Define age category probabilities by injury type
                //coarse probability distributiuons for each of the four age categories
                int[] Ageprob_gunshot = { 10, 20, 90, 100 };
                int[] Ageprob_bft = { 20, 40, 80, 100 };
                int[] Ageprob_drowning = { 30, 80, 85, 100 };
                int[] Ageprob_heart = { 10, 20, 60, 100 };// cumulative probabilities as age group increases
                int[] Ageprob_allergy = { 25, 50, 75, 100 };
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
                        age_group = "small child";
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
                        age_group = "small child";
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
                        age_group = "small child";
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
                        age_group = "small child";
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
                    age_group = "small child";
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
                if (wound_type == "gunshot")
                {
                    if (wound_loc_ID == 1 || wound_loc_ID == 2 || wound_loc_ID == 3 || wound_loc_ID == 12 || wound_loc_ID == 13 || wound_loc_ID == 14)
                    {
                        a = random.Next(0, 100);
                        if (a <= 95)
                        {
                            hemorrhage = 2;
                            initial_hemorrhage = hemorrhage;
                            stopwatch_hem.Start();
                        }
                        else
                        {
                            hemorrhage = 1;
                            initial_hemorrhage = hemorrhage;
                            stopwatch_hem.Start();
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
                                stopwatch_hem.Start();
                            }
                            else
                            {
                                hemorrhage = 1;
                                initial_hemorrhage = hemorrhage;
                                stopwatch_hem.Start();
                            }
                        }
                    }
                 }
                else if (wound_type == "blunt force trauma")
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
                        stopwatch_hem.Start();
                    }
                    else
                    {
                        hemorrhage = 2;
                        initial_hemorrhage = hemorrhage; ;
                        stopwatch_hem.Start();
                    }
                }
                else
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
                        stopwatch_hem.Start();
                    }
                    else
                    {
                        hemorrhage = 2;
                        initial_hemorrhage = hemorrhage; ;
                        stopwatch_hem.Start();
                    }
                }
                #endregion

                        #region set consciousness level depedent on hemorrhage level
                    if (hemorrhage == 2)
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
                            stopwatch_con.Start();
                        }
                        else
                        {
                            consciousness = 2;
                            initial_consciousness = consciousness; ;
                            stopwatch_con.Start();
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
                            stopwatch_con.Start();
                        }
                        else
                        {
                            consciousness = 2;
                            initial_consciousness = consciousness; ;
                            stopwatch_con.Start();
                        }
                    }
                #endregion

                #region initialize cardiac/circulation variables 

                #region Cardiac Event
                else if (wound_type_ID == 3)
                {
                    a = random.Next(0, 100);
                    if (a < major_heart_attack_probability) // age dependent probability that the heart attack is serious
                    {
                        circulation = 1;
                        initial_circulation = circulation;
                        stopwatch_cir.Start();
                    }
                    else // 40% chance of major problem
                    {
                        circulation = 2;
                        initial_circulation = circulation;
                        stopwatch_cir.Start();
                    }
                }
                #endregion

                #region cardiac variables dependent on other injury types

                    #region gunshot
                if (wound_type_ID == 0)
                        {
                            if (hemorrhage == 2)
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
                                        stopwatch_cir.Start();
                                    }
                                else // 40% chance of major problem
                                    {
                                        circulation = 2;
                                        initial_circulation = circulation;
                                        stopwatch_cir.Start();
                                    }
                            }
                            else if (hemorrhage == 1)
                            {
                                a = random.Next(0, 100);
                                if (a < 50) // 500% probability of no heart problem 
                                {
                                    circulation = 0;
                                    initial_circulation = circulation;
                                }
                                else if (a >= 50 && a < 90) //40% chance of minor heart problem
                                {
                                    circulation = 1;
                                    initial_circulation = circulation;
                                    stopwatch_cir.Start();
                                }
                                else // 10% chance of major problem
                                {
                                    circulation = 2;
                                    initial_circulation = circulation;
                                    stopwatch_cir.Start();
                                }
                            }
                        }
                    #endregion

                    #region blunt force trauma
                    else if (wound_type_ID == 1)
                    {
                        if (hemorrhage == 2)
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
                                stopwatch_cir.Start();
                            }
                            else // 40% chance of major problem
                            {
                                circulation = 2;
                                initial_circulation = circulation;
                                stopwatch_cir.Start();
                            }
                        }
                        else if (hemorrhage == 1)
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
                                stopwatch_cir.Start();
                            }
                            else // 10% chance of major problem
                            {
                                circulation = 2;
                                initial_circulation = circulation;
                                stopwatch_cir.Start();
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
                            stopwatch_cir.Start();
                        }
                        else // 50% chance of major problem
                        {
                            circulation = 2;
                            initial_circulation = circulation;
                            stopwatch_cir.Start();
                        }
                    }
                #endregion

                    #region Allergy 

                    if (wound_type_ID > 3)
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
                            stopwatch_cir.Start();
                        }
                        else 
                        {
                            circulation = 2;
                            initial_circulation = circulation;
                            stopwatch_cir.Start();
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
                            stopwatch_bre.Start();
                        }
                        else
                        {
                            breathing = 1;
                            initial_breathing = breathing;
                            stopwatch_bre.Start();
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
                            stopwatch_bre.Start();
                        }
                        else
                        {
                            breathing = 2;
                            initial_breathing = breathing;
                            stopwatch_bre.Start();
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
                            stopwatch_con.Start();
                        }
                        else
                        {
                            consciousness = 1;
                            initial_consciousness = consciousness;
                            stopwatch_con.Start();
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
                            stopwatch_con.Start();
                        }
                        else
                        {
                            consciousness = 2;
                            initial_consciousness = consciousness;
                            stopwatch_con.Start();
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
                    else if (a >= 50 && a <80) // 30% chance of minor problem
                    {
                        airway = 1;
                        initial_airway = airway;
                        stopwatch_air.Start();
                    }
                    else // 20% chance of blocked airway
                    {
                        airway = 2;
                        initial_airway = airway;
                        stopwatch_air.Start();
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
                        stopwatch_air.Start();
                    }
                    else //50% chance of major blockage (water)
                    {
                        airway = 2;
                        initial_airway = airway;
                        stopwatch_air.Start();
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
                    if (a < 10) // 10% chanve of no airway problem
                    {
                        airway = 0;
                        initial_airway = airway;
                    }
                    else if (a >= 10 && a < 60) // 50% chance of minor problem
                    {
                        airway = 1;
                        initial_airway = airway;
                        stopwatch_air.Start();
                    }
                    else // 40% chance of major problem
                    {
                        airway = 2;
                        initial_airway = airway;
                        stopwatch_air.Start();
                    }
                }
                #endregion
 
                    #region set breathing dependent on airway
                if (airway == 2)
                {
                    breathing = 2;
                    initial_breathing = breathing;
                    stopwatch_bre.Start();
                }
                else if (airway ==1)
                {
                    a = random.Next(0, 100);
                    if (a < 2) // 2% chance of no difficulties
                    {
                        breathing = 0;
                        initial_breathing = breathing;
                    }
                    else if (a >=2 && a < 90) //88% chance of minor brething difficulties
                    {
                        breathing = 1;
                        initial_breathing = breathing;
                        stopwatch_bre.Start();
                    }
                    else //10% chance of sever breathing problem
                    {
                        breathing = 2;
                        initial_breathing = breathing;
                        stopwatch_bre.Start();
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
                            stopwatch_con.Start();
                        }
                        else
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 2;
                            }
                            initial_consciousness = consciousness;
                            stopwatch_con.Start();
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
                            stopwatch_con.Start();
                        }
                        else
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 2;
                            }
                            initial_consciousness = consciousness;
                            stopwatch_con.Start();
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
                            stopwatch_con.Start();
                        }
                        else //50% chance of serious problem
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 2;
                            }
                            initial_consciousness = consciousness;
                            stopwatch_con.Start();
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
                            stopwatch_con.Start();
                        }
                        else //30% chance of serious problem
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 2;
                            }
                            initial_consciousness = consciousness;
                            stopwatch_con.Start();
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
                            stopwatch_con.Start();
                        }
                        else //50% chance of serious problem
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 2;
                            }
                            initial_consciousness = consciousness;
                            stopwatch_con.Start();
                        }
                    }
                    else if (cir ==1)
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
                            stopwatch_con.Start();
                        }
                        else //10% chance of serious problem
                        {
                            if (consciousness < 2)
                            {
                                consciousness = 2;
                            }
                            initial_consciousness = consciousness;
                            stopwatch_con.Start();
                        }
                    }
                }
                else //chance of airway problem dependent on allergy
                {
                    a = random.Next(0, 100);
                    if (a < 25) // 25% chanve of no conc problem
                    {
                        consciousness = 0;
                        initial_airway = airway;
                    }
                    else if (a >= 25 && a < 75) // 50% chance of minor problem
                    {
                        airway = 1;
                        initial_airway = airway;
                        stopwatch_con.Start();
                    }
                    else //25% chance of being unconscious
                    {
                        airway = 2;
                        initial_airway = airway;
                        stopwatch_con.Start();
                    }
                }
                #endregion

                #endregion

                //maybe delete this! no longer needed! 
                #region start characteristics timers
                Stopwatch hem_time = new Stopwatch();
                hem_time.Start();
                hem_seconds = hem_time.Elapsed.TotalSeconds;
                Stopwatch conc_time = new Stopwatch();
                conc_time.Start();
                conc_seconds = conc_time.Elapsed.TotalSeconds;
                Stopwatch air_time = new Stopwatch();
                air_time.Start();
                air_seconds = air_time.Elapsed.TotalSeconds;
                Stopwatch breath_time = new Stopwatch();
                breath_time.Start();
                breath_seconds = breath_time.Elapsed.TotalSeconds;
                Stopwatch circ_time = new Stopwatch();
                circ_time.Start();
                circ_seconds = circ_time.Elapsed.TotalSeconds;
                #endregion

                #endregion

                #endregion

                #region Update UI

                DELEGATE del = new DELEGATE(UIController);
                this.Invoke(del);
                #endregion

                #region tell EMS agent that patient status is available and treatment required
                // Thread.Sleep(Patient_Assign_to_Run_Pause_time); // UNDOIFNOTWORKING - just a pause? 
                // send message to EMS agnet regarding intial status available
                from = "Patient";
                to = "EMS";
                subject = "Treatment Required";
                state = "Initial status available";
                Ems();
                //var Th_PtoEMS = new Thread(() => Ems());
                //Th_PtoEMS.Start(); remove threading - check
                #endregion

            }

            #endregion

            else if (from == "update_patient_degradation")
            {
                #region if the message is from the degrader
                    //#region apply patient degradation based on rules
                    #region hemorrhage degradation - long term and short term probabilities
                    Random chr_rnd = new Random();
                    if (hemorrhage == 1 && stopwatch_hem.Elapsed.Seconds >= 30)
                    {
                        int hem_rand = chr_rnd.Next(1, 101);
                        if (hem_rand > hem_deg_prob_longtime)
                        {
                            hemorrhage = 2;
                            treatment_timeline.Items.Add("Patient bleeding worsened - now heavy bleeding");
                            patient_instability_score += 1;
                        }
                        else
                        {
                            hemorrhage = hemorrhage; 
                        }
                    }
                    else if (hemorrhage == 1 && stopwatch_hem.Elapsed.Seconds < 30)
                    {
                        int hem_rand = chr_rnd.Next(1, 101);
                        if (hem_rand > hem_deg_prob_shorttime)
                        {
                            hemorrhage = 2; 
                            treatment_timeline.Items.Add("Patient bleeding worsened - now heavy bleeding");
                            patient_instability_score += 1;
                        }
                        else
                        {
                            hemorrhage = hemorrhage;
                        }
                    }
                    else if (hemorrhage == 2)
                    {
                        hemorrhage = hemorrhage;
                    }
                    else if (hemorrhage == 0)
                    {
                        hemorrhage = hemorrhage;
                    }
                    #endregion

                    #region consciousness degradation  due to hemmorhage- long term and short term probabilities
                    Random chr_rnd_conc = new Random();
                    if (hemorrhage == 2 && consciousness == 0 && stopwatch_hem.Elapsed.Seconds >= 30)
                    {
                        int hem_rand = chr_rnd_conc.Next(1, 101);
                        if (hem_rand < hemcon0to1_deg_prob_longtime)
                        {
                            consciousness = 1; //change to 1 after testing check
                            treatment_timeline.Items.Add("Patient consciousness worsened - now semi-conscious");
                            patient_instability_score += 1;
                        }
                        else if (hem_rand > hemcon0to1_deg_prob_longtime && hem_rand <= (hemcon0to2_deg_prob_longtime + hemcon0to1_deg_prob_longtime))
                        {
                            consciousness = 2; //change to 1 after testing check
                            treatment_timeline.Items.Add("Patient consciousness worsened - now unconscious");
                            patient_instability_score += 2;
                        }
                        else
                        {
                            consciousness = consciousness;
                        }
                    }
                    else if (hemorrhage == 2 && consciousness == 0 && stopwatch_hem.Elapsed.Seconds < 30)
                    {
                        int hem_rand = chr_rnd_conc.Next(1, 101);
                        if (hem_rand < hemcon0to1_deg_prob_shorttime)
                        {
                            consciousness = 1; 
                            treatment_timeline.Items.Add("Patient consciousness worsened - now semi-conscious");
                            patient_instability_score += 1;
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
                        }
                    }
                    else if (hemorrhage == 2 && consciousness ==1 && stopwatch_hem.Elapsed.Seconds >= 30)
                    {
                        int hem_rand = chr_rnd.Next(1, 101);
                        if (hem_rand > hemcon1to2_deg_prob_longtime)
                        {
                            consciousness = 2; //change to 1 after testing check
                            treatment_timeline.Items.Add("Patient consciousness worsened - now unconscious");
                            patient_instability_score += 1;
                        }
                        else
                        {
                            hemorrhage = hemorrhage;
                        }
                    }
                    else if (hemorrhage == 2 && consciousness == 1 && stopwatch_hem.Elapsed.Seconds < 30)
                    {
                        int hem_rand = chr_rnd.Next(1, 101);
                        if (hem_rand > hemcon1to2_deg_prob_shorttime)
                        {
                            consciousness = 2; //change to 1 after testing check
                            treatment_timeline.Items.Add("Patient consciousness worsened - now unconscious");
                            patient_instability_score += 1;
                        }
                        else
                        {
                            hemorrhage = hemorrhage;
                        }
                    }
                    else if (hemorrhage == 2)
                    {
                        hemorrhage = hemorrhage;
                    }
                    else if (hemorrhage == 0)
                    {
                        hemorrhage = hemorrhage;
                    }
                    #endregion

                    #region circulation degradation  due to hemmorhage- long term and short term probabilities
                    Random chr_rnd_circ = new Random();
                    if (hemorrhage == 2 && circulation == 0 && stopwatch_hem.Elapsed.Seconds >= 30)
                    {
                        int hem_rand = chr_rnd_circ.Next(1, 101);
                        if (hem_rand < hemcirc0to1_deg_prob_longtime)
                        {
                            circulation = 1; //change to 1 after testing 
                            stopwatch_cir.Start();
                            treatment_timeline.Items.Add("Patient heart worsened - now experiencing tachycardia/bradycardia");
                            patient_instability_score += 1;
                        }
                        else if (hem_rand > hemcirc0to1_deg_prob_longtime && hem_rand <= (hemcirc0to2_deg_prob_longtime + hemcirc0to1_deg_prob_longtime))
                        {
                            circulation = 2; //change to 1 after testing check
                            stopwatch_cir.Start();
                            treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                            patient_instability_score += 2;
                        }
                        else
                        {
                            circulation = circulation;
                        }
                    }
                    else if (hemorrhage == 2 && circulation == 0 && stopwatch_hem.Elapsed.Seconds < 30)
                    {
                        int hem_rand = chr_rnd_circ.Next(1, 101);
                        if (hem_rand < hemcirc0to1_deg_prob_shorttime)
                        {
                            circulation = 1;
                            stopwatch_cir.Start();
                            treatment_timeline.Items.Add("Patient heart worsened - now experiencing tachycardia/bradycardia");
                            patient_instability_score += 1;
                        }
                        else if (hem_rand > hemcirc0to1_deg_prob_shorttime && hem_rand <= (hemcirc0to2_deg_prob_shorttime + hemcirc0to1_deg_prob_shorttime))
                        {
                            circulation = 2;
                            stopwatch_cir.Start();
                            treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                            patient_instability_score += 2;
                        }
                        else
                        {
                            circulation = circulation;
                        }
                    }
                    else if (hemorrhage == 2 && circulation == 1 && stopwatch_hem.Elapsed.Seconds >= 30)
                    {
                        int hem_rand = chr_rnd_circ.Next(1, 101);
                        if (hem_rand > hemcirc1to2_deg_prob_longtime)
                        {
                            circulation = 2; //change to 1 after testing check
                            treatment_timeline.Items.Add("Patient heart worsened - now in cardiac arrest");
                            patient_instability_score += 1;
                        }
                        else
                        {
                            circulation = circulation;
                        }
                    }
                    else if (hemorrhage == 2 && circulation == 1 && stopwatch_hem.Elapsed.Seconds < 30)
                    {
                        int hem_rand = chr_rnd_circ.Next(1, 101);
                        if (hem_rand > hemcirc1to2_deg_prob_shorttime)
                        {
                            circulation = 2; //change to 1 after testing check
                            treatment_timeline.Items.Add("Patient consciousness worsened - now unconscious");
                            patient_instability_score += 1;
                        }
                        else
                        {
                            circulation = circulation;
                        }
                    }

                    #endregion
                #endregion

                DELEGATE del = new DELEGATE(UIController);
                this.Invoke(del);
            }

            else if (from == "EMS" && to == "patient")
            #region update UI when treatment applied - if message is from EMS to patient and subject is "applied treatment"
            {
                if (subject == "Applied Treatment")
                {
                    // UI controller will do all of update in form about recent changes made in patient health
                    DELEGATE del = new DELEGATE(UIController);
                    this.Invoke(del);
                } // end of if loop
            }  // called by EMS ends
            #endregion

            // This activates the UI controller when message from Stopwatch
            else if (from == "Stopwatch" && to == "UIcontroller")
            {
                DELEGATE del = new DELEGATE(UIController);
                this.Invoke(del);
            }
            else if (subject == "update treatment in UI")
            {
                DELEGATE del = new DELEGATE(UIController);
                this.Invoke(del);
            }
            // method to update user interface with most recent patient attributes - 
            // called as a delegate 'del' from within Patient thread

            // UIcontroller control
            #region update patient attributes

            void UIController()
            {
                if (sim_inthem_box.Text == " ")
                {
                    //Thread.Sleep(5);
                    sim_gender_label.Text = gender;
                    sim_injury_type_label.Text = wound_type;
                    sim_injury_location_label.Text = wound_location;
                    sim_age_label.Text = Convert.ToString(age);
                    sim_intair_box.Text = Convert.ToString(airway);                    // changes the final state of the patient at given time in AI_Sim from
                    sim_intbreath_box.Text = Convert.ToString(breathing);
                    sim_intcirc_box.Text = Convert.ToString(circulation);
                    sim_intconc_box.Text = Convert.ToString(consciousness);
                    sim_inthem_box.Text = Convert.ToString(hemorrhage);
                }
                else if (Predicted_Hem_box.Text == " ") // Printing out the final conditions from the simulation results
                {
                    //Thread.Sleep(5);
                    Predicted_Hem_box.Text = predicted_hem;
                    textBox5.Text = predicted_con;
                    textBox4.Text = predicted_air;
                    Predicted_Breath_Box.Text = predicted_bre;
                    Predicted_Circ_Box.Text = predicted_cir;
                    textBox1.Text = predicted_time;
                    textBox8.Text = hemorrhage_count;
                    textBox6.Text = consciousness_count;
                    textBox3.Text = airway_count;
                    textBox7.Text = breathing_count;
                    textBox2.Text = circulation_count;
                }
                else
                {

                }

                // changes the current (final once sim has stopped) state of the patient at given time in AI_Sim form
                Thread.Sleep(5);
                sim_Current_air_box.Text = Convert.ToString(airway);                    
                sim_Current_breath_box.Text = Convert.ToString(breathing);
                sim_current_circ_box.Text = Convert.ToString(circulation);
                sim_current_conc_box.Text = Convert.ToString(consciousness);
                sim_current_hem_box.Text = Convert.ToString(hemorrhage);

                // add the indicator drawing part here
                hem_ind_panel.BackgroundImage = DrawIndicator(hemorrhage);
                air_ind_panel.BackgroundImage = DrawIndicator(airway);
                breath_ind_panel.BackgroundImage = DrawIndicator(breathing);
                conc_ind_panel.BackgroundImage = DrawIndicator(consciousness);
                circ_ind_panel.BackgroundImage = DrawIndicator(circulation);
            }
            #endregion
            
        }  // end of Patient agent 
        #endregion 

        // EMS Agent Start
        #region create New EMS Agent
        public void Ems()
        {
            if(from == "Patient" && to == "EMS")
            {
                if (subject == "Treatment Required")                         // to see if patient required treatment of not
                {
                    if (state == "Initial status available")
                    {
                        /*  Execute the code over here */
                        /* EMS again have to generate a message for AI*/
                    } // end of "Initial status available" if loop 

                    from = "EMS"; to = "AI"; subject = "Recommend treatment";
                    AI();
                    //var Th_EMStoAI = new Thread(() => AI());                       // sending info from patient to AI for recommendation
                    //Th_EMStoAI.Start();

                } // end of "Treatment Required" if loop 
            }// end of "Patient" if loop

            #region old code - can be added if needed
            /*else if (from == "AI" && to =="EMS")
            {
                if (subject == "recommended action")
                {
                    DELEGATE del = new DELEGATE(DisplayAction);
                    this.Invoke(del);
                    from = "EMS"; to = "patient"; subject = "Treatment completed";
                    var Th_EMStoPatient = new Thread(() => patient());                       // sending info from patient to AI for recommendation
                    Th_EMStoPatient.Start();
                }
                // Do the job sent by AI agent
                
            }*/
            #endregion 

        } // End of EMS agent
        #endregion

        //Create AI agent thread
        #region AI thread generate 

        SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
        SpeechSynthesizer speech = new SpeechSynthesizer();

        public void AI()
        {
            #region actions when message from EMS received
            if (from == "EMS" && to == "AI")
            {
                #region action message definition //defines the AI agent response to information transferred from EMS agent
                string act = null;
                if(subject == "Recommend treatment")
                {
                    hem = hemorrhage;
                    con = consciousness;
                    air = airway;
                    bre = breathing;
                    cir = circulation;
                    //  hierachy of action
                    if (hem == 2 && cir == 2 && bre == 2)
                        if ((cir_count -1)<= hem2_count)
                            {
                                act = "cpr";
                            }
                        else
                            {
                                act = "check hemorrhage";
                            }
                    else if (cir == 2 && bre == 2 && hem < 2)
                    {
                        act = "cpr";
                    }
                    else if (cir == 2 && hem < 2 && bre < 2)
                    {
                        act = "check circulation";
                    }
                    else if (hem == 2 && cir < 2 && bre < 2)
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
                        if ((cir_count - 1) <= hem1_count)
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
                        if ((cir_count -1) <= hem1_count)
                        {
                            act = "check circulation";
                        }
                        else
                        {
                            act = "check hemorrhage";
                        }
                    else if (bre == 1 && hem == 1 && cir < 1)
                        if ((cir_count - 1) <= hem1_count)
                        {
                            act = "check circulation";
                        }
                        else
                        {
                            act = "check hemorrhage";
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
                    // In this block AI will perform its value function MDP and  recommend the actions
                    /*  Get the patient variable value here and recommend EMS to perform that action*/
                    /*  Markov decision process will be coded here and based of the output it will 
                        select actions and reply EMS to apply that action
                        Make that suggestion to cahneg action automatically*/

                    /************************************************************************************************

                                        Had to add MARKOV DECISION PROCESS CODE OVER HERE for suggestion ///         

                    **************************************************************************************************/

                    action = act;
                    DELEGATE del = new DELEGATE(DisplayAction);
                    this.Invoke(del);
                }
                #endregion

                // after getting the necessary actions
                // send message back
                action = act;
                /*from = "AI"; to = "EMS"; subject = "recommended action"; action = act;
                Thread Th_Ai = new Thread(() => Ems());
                Th_Ai.Start();*/

                #region highlight appropriate buttons
                void DisplayAction()
                {
                    treatment_timeline.Items.Add($"Display Action (EMS): Thread Running {action}.....");
                    if (action == "check hemorrhage")
                    {
                        if (hem == 2)
                        {
                            Button_hemm_torniquet.BackColor = Color.GreenYellow;
                        }
                        else
                        {
                            Button_hemm_treat.BackColor = Color.GreenYellow;
                        }
                    }
                    if (action == "check consciousness")
                    { 
                            Button_conc_drugs.BackColor = Color.GreenYellow;
                    }

                    if (action == "check airway")
                    {
                        if (air == 2)
                        {
                            Button_air_Intubate.BackColor = Color.GreenYellow;
                        }
                        else
                        {
                            Button_air_clearair.BackColor = Color.GreenYellow;
                        }
                    }
                    if (action == "check circulation")
                    {
                        if (cir == 2)
                        {
                            Button_circ_chest.BackColor = Color.GreenYellow;
                        }
                        else
                        {
                            Button_circ_drugs.BackColor = Color.GreenYellow;
                        }
                    }
                    if (action == "check breathing")
                    {
                        if (bre == 2)
                        {
                            Button_breath_Oxygen.BackColor = Color.GreenYellow;
                        }
                        else
                        {
                            Button_breath_aspirate.BackColor = Color.GreenYellow;
                        }
                    }
                    if (action == "cpr")
                    {
                        Button_CPR.BackColor = Color.GreenYellow;
                    }

                } // end of Display Action function
                #endregion
            }
            #endregion
            //var Th_speakAI = new Thread(() => speakAI(action));
            //    Th_speakAI.Start();
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
            starttime = DateTime.Now;
            var inputform_click = new inputform(treatment_timeline.Items);
            //Inputform1.Update();
            inputform_click.Show();
        }

        private void AI_patient_transfer_button_Click_1(object sender, EventArgs e)
        {
            starttime = DateTime.Now;
            var transfer_form_start = new inputform(treatment_timeline.Items);
            transfer_form_start.Show();   
        }
        #endregion

        // button actions to implement highlighted treatment option
        #region action the treatment

        private void Button_hemm_torniquet_Click(object sender, EventArgs e)
        {
            Button_hemm_torniquet.BackColor = Color.Empty;
            Treatment("check hemorrhage");
        }
        private void Button_hemm_treat_Click(object sender, EventArgs e)
        {
            Button_hemm_torniquet.BackColor = Color.Empty;
            Treatment("check hemorrhage");
        }

        private void Button_conc_drugs_Click(object sender, EventArgs e)
        {
            Button_conc_drugs.BackColor = Color.Empty;
            Treatment("check consciousness");
        }

        private void Button_air_Intubate_Click(object sender, EventArgs e)
        {
            Button_air_Intubate.BackColor = Color.Empty;
            Treatment("check airway");
        }

        private void Button_air_clearair_Click(object sender, EventArgs e)
        {
            Button_air_clearair.BackColor = Color.Empty;
            Treatment("check airway");
        }

        private void Button_circ_CPR_Click(object sender, EventArgs e)
        {
            Button_circ_chest.BackColor = Color.Empty;
            Treatment("check circulation");
        }

        private void Button_circ_drugs_Click(object sender, EventArgs e)
        {
            Button_circ_drugs.BackColor = Color.Empty;
            Treatment("check circulation");
        }

        private void Button_breath_Oxygen_Click(object sender, EventArgs e)
        {
            Button_breath_Oxygen.BackColor = Color.Empty;
            Treatment("check breathing");
        }

        private void Button_breath_rescuebreath_Click(object sender, EventArgs e)
        {
            Button_breath_aspirate.BackColor = Color.Empty;
            Treatment("check breathing");
        }
        private void Button_CPR_Click(object sender, EventArgs e)
        {
            Button_CPR.BackColor = Color.Empty;
            Treatment("check CPR");
        }
        #endregion

        #endregion

        // draw the indicators for UI
        #region draw the indicator
        public Bitmap DrawIndicator(int pat_char_stat)
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

        #region code to actually construct rectangles for indicators
        // Draw a vertically oriented battery with
        // the indicated percentage filled in.
        private void DrawVerticalIndicator(Graphics gr, float indicator_level, int wid, int hgt, Color bg_color, Color outline_color, Color charged_color, Color uncharged_color, bool striped)
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
        private GraphicsPath MakeRoundedRect(
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

        // I'm not sure I like this implementation as it makes the UI very juddery. I think we should take out any thread pausing as it is very frustrating
        #region timing functions etc. stopwatch for each treatment 
        int Stopwatch_hem_Function_Call;
        int Stopwatch_conc_Function_Call;
        int Stopwatch_air_Function_Call;
        int Stopwatch_circ_Function_Call;
        int Stopwatch_breath_Function_Call;
       // private object stopwatch_hem;
        //private object stopWatch_hem;


        //Initiate stopwatches for each patient Characteristic? don't know why these are here and what they do?


        //public void stopwatch_record()
        //{
        //    if (Stopwatch_hem_Function_Call == 1)
        //    {
        //        stopwatch_hem.Start();
        //        while (stopwatch_hem.Elapsed.Seconds <= 10)
        //        {

        //        }
        //        if(hemorrhage > 0 && hemorrhage < 2)
        //        {
        //            hemorrhage = hemorrhage + 1;
        //        }
        //        DELEGATE del = new DELEGATE(patient);
        //        this.Invoke(del);
        //    }
        //    else if (Stopwatch_conc_Function_Call == 2)
        //    {
        //        stopwatch_con.Start();
        //        while (stopwatch_hem.Elapsed.Seconds <= 10)
        //        {

        //        }
        //        if(consciousness > 0 && consciousness < 2)
        //        {
        //            consciousness = consciousness + 1;
        //        }
        //        DELEGATE del = new DELEGATE(patient);
        //        this.Invoke(del);
        //    }
        //    else if (Stopwatch_air_Function_Call == 3)
        //    {
        //        stopwatch_air.Start();
        //        while (stopwatch_hem.Elapsed.Seconds <= 10)
        //        {

        //        }
        //        if(airway > 0 && airway < 2)
        //        {
        //            airway = airway + 1;
        //        }
        //        DELEGATE del = new DELEGATE(patient);
        //        this.Invoke(del);
        //    }
        //    else if (Stopwatch_breath_Function_Call == 4)
        //    {
        //        stopwatch_bre.Start();
        //        while (stopwatch_hem.Elapsed.Seconds <= 10)
        //        {

        //        }
        //        if(breathing >0 && breathing < 2)
        //        {
        //            breathing = breathing + 1;
        //        }
        //        DELEGATE del = new DELEGATE(patient);
        //        this.Invoke(del);
        //    }
        //    else if (Stopwatch_circ_Function_Call == 5)
        //    {
        //        stopwatch_cir.Start();
        //        while (stopwatch_hem.Elapsed.Seconds <= 10)
        //        {

        //        }
        //        if (circulation > 0 && circulation < 2)
        //        {
        //            circulation = circulation + 1;
        //        }
        //        DELEGATE del = new DELEGATE(patient);
        //        this.Invoke(del);
        //    }
        //    else
        //    {

        //    }
        //    from = "Stopwatch"; to = "UIcontroller";
        //    patient();
        //}

        #endregion

        //treatment controls to be actioned by EMS agent by message or UI interface
        #region treatment
        public void Treatment(string action)
        {
            // copy the transition probability code from previous means
            /* Write a code for patient treatment. It is same as before. 
               Depending on the action that is passed, apply recommended actions*/

            #region action recommended from AI.....
            // direction for checking hemorrhage from AI
            if (action == "check hemorrhage")
            {
                Hemorrhagecheck();
            }

            // direction for check airway suggestion from AI
            if (action == "check airway")
            {
                Airwaycheck();
            }

            // direction for checking consciousness from AI
            if (action == "check consciousness")
            {
                Consciousnesscheck();
            }

            // direction for checking respiration from AI
            if (action == "check breathing")
            {
                Breathingcheck();
            }

            // direction from checking circulation from AI
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
                int hem1to0_cure_probability = 90 - (5 * hem);
                int hem1to1_cure_probability = 10 + (5 * hem);
                int hem1_cure_cumprob = hem1to1_cure_probability + hem1to0_cure_probability;
                int hem2to0_cure_probability = 30 - (3 * hem2_count);
                int hem2to1_cure_probability = 60 - (3 * hem2_count);
                int hem2to2_cure_probability = 10 + (6 * hem2_count);
                int hem2to1_cumprob = hem2to1_cure_probability + hem2to0_cure_probability;
                int hem2_cure_cumprob = hem2to1_cure_probability + hem2to0_cure_probability + hem2to2_cure_probability;

                int hemorrhage_timedelay = 0;
                int hemorrhage1_timedelay = 3000;
                int hemorrhage2_timedelay = 5000;

                treatment_timeline.Items.Add("Checking hemorrhage");
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
                    }
                    else if (x <= hem2to1_cumprob && x > hem2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("Bleeding partially stopped");
                        hemorrhage_description = "heavy bleeding partially stopped";
                        hemorrhage = 1;
                        total_partial_success_count = total_partial_success_count + 1;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("Bleeding not stopped");
                        hemorrhage_description = "heavy bleeding";
                        hemorrhage = 2;
                        total_unsuccess_count = total_unsuccess_count + 1;
                    }

                    hem2_count += + 1;
                    hemorrhage_timedelay = hemorrhage2_timedelay;
                }

                #region show patient treating message
                // display treatment message - requires 'async' in the private void statement
                var treatment_message = new Message_form(); //Duplicated Treatment message code - check if broken
                treatment_message.Show();
                await Task.Delay(hemorrhage_timedelay);
                treatment_message.Close();
                // end display treatment message
                #endregion

            } // end of hemorrhage check
            #endregion
            #region consciousness check
            async void Consciousnesscheck()
            {
                #region set consciousness treatment variables
                int conc1to0_cure_probability = 50;
                int conc1to1_cure_probability = 50;
                int conc2to0_cure_probability = 20;

                int conc2to1_cure_probability = 30;
                int conc2to2_cure_probability = 50;
                int conc2to1_cumprob = conc2to1_cure_probability + conc2to0_cure_probability;

                int consciousness_timedelay = 0;
                int consciousness1_timedelay = 3000;
                int consciousness2_timedelay = 5000;
                #endregion

                treatment_timeline.Items.Add("Checking Consciousness");
                if (consciousness == 0)
                {
                    treatment_timeline.Items.Add("Patient fully Conscious");
                        consciousness = 0;
                    consciousness_decription = "fully conscious";
                }
                else if (consciousness == 1)
                {
                    treatment_timeline.Items.Add("Patient Partially Conscious");
                    treatment_timeline.Items.Add("Providing Oxygen");

                    if (breathing == 0 && hemorrhage == 0 && airway == 0 && circulation == 0) //cure probability increases
                    {
                        conc1to0_cure_probability += (5 * conc1_count);
                        conc1to1_cure_probability -= (5 * conc1_count);
                    }
                    else //cure probability reduces if other injuries present
                    {
                        conc1to0_cure_probability -= (5 * conc1_count);
                        conc1to1_cure_probability += (5 * conc1_count);
                    }

                    int conc1_cure_cumprob = conc1to1_cure_probability + conc1to0_cure_probability;
                    int x;

                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= conc1to0_cure_probability)
                    {
                        consciousness = 0;
                        consciousness_decription = "previously partially conscious but now fully conscious";
                        treatment_timeline.Items.Add("Full consciousness restored");
                        stopwatch_con.Reset();
                        total_success_count = total_success_count + 1;
                    }
                    else  
                    {
                        consciousness = 1;
                        consciousness_decription = "partially conscious";
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
                        conc2to0_cure_probability = 10 + (2 * conc2_count);
                        conc2to1_cure_probability = 50 + (8 * conc2_count);
                        conc2to2_cure_probability = 40 - (10 * conc2_count);
                        conc2to1_cumprob = conc2to1_cure_probability + conc2to0_cure_probability;
                    }
                    else // cure probability decreases if other injuries are present
                    {
                        conc2to0_cure_probability = 10 - (1 * conc2_count);
                        conc2to1_cure_probability = 50 - (4 * conc2_count);
                        conc2to2_cure_probability = 40 + (5 * conc2_count);
                        conc2to1_cumprob = conc2to1_cure_probability + conc2to0_cure_probability;
                    }

                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= conc2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("Full consciousness restored");
                        consciousness = 0;
                        consciousness_decription = "previously unconscious but now fully conscious";
                        stopwatch_con.Reset();
                        total_success_count = total_success_count + 1;
                    }
                    else if (x <= conc2to1_cumprob && x > conc2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("Patient partially responsive");
                        consciousness = 1;
                        consciousness_decription = "previously unconscious but coming round";
                        total_partial_success_count = total_partial_success_count + 1;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("Patient still unconscious");
                        consciousness = 2;
                        consciousness_decription = "unconscious";
                        total_unsuccess_count = total_unsuccess_count + 1;
                    }
                    conc2_count = conc2_count + 1;
                    consciousness_timedelay = consciousness2_timedelay;
                }

                #region show treating patient message
                // display treatment message - requires 'async' in the private void statement
                var treatment_message = new Message_form();
                treatment_message.Show();
                await Task.Delay(consciousness_timedelay);
                treatment_message.Close();
                // end display treatment message
                #endregion

                // do we need the thread to sleep if displaying message?  maybe lock the app? 
                //System.Threading.Thread.Sleep(2500);
                //sim_current_conc_box.Text = Convert.ToString(consciousness);
                //Thread.Sleep(5);
                //if (Stopwatch_conc_Function_Call != 1)
                //{
                //    Stopwatch_conc_Function_Call = 1;
                //    stopwatch_con.Reset();
                //    Thread th_stopwatch_conc = new Thread(stopwatch_record);
                //    th_stopwatch_conc.Start();
                //}

                //this section all commented out for now - move to patient section? multiple checks? 
                #region consciousness deterioration control - move to patient MDP section
                //if (hemorrhage == 0 && circulation == 0)
                //{
                //    consciousness = 0;
                //}

                //if (consciousness == 0)
                //{
                //    if (hemorrhage == 2 && circulation == 0)
                //    {
                //        Hemorrhagecheck();
                //        Circulationcheck();
                //    }
                //    else if (hemorrhage == 0 && circulation == 0)
                //    {
                //        Circulationcheck();
                //    }
                //    else if (hemorrhage == 0 && circulation == 1)
                //    {
                //        consciousness = 2;
                //    }
                //    else
                //    {
                //        Hemorrhagecheck();
                //    }
                //}
                //else if (consciousness == 1)
                //{
                //    if (hemorrhage == 0 && circulation == 0)
                //    {
                //        Circulationcheck();
                //        consciousness = 2;
                //    }
                //    else if (hemorrhage == 1 && circulation == 0)
                //    {
                //        Hemorrhagecheck();
                //        Circulationcheck();
                //    }
                //    else
                //    {
                //        Hemorrhagecheck();
                //    }
                //}
                //else
                //{
                //    consciousness = 2;
                //}
                #endregion // this section all commented out



            }// end of consciousness check
            #endregion
            #region Breathing check
            async void Breathingcheck()
            {
                int breath1to0_cure_probability = 60;
                int breath1_successprob_reduction = 5;

                int breath2to0_cure_probability = 10;
                int breath2to1_cure_probability = 60;
                int breath2to1_cumprob = breath2to1_cure_probability + breath2to0_cure_probability;
                int breath2_successprob_reduction = 5;
                int breathing_timedelay = 0;
                int breathing1_timedelay = 3000;
                int breathing2_timedelay = 5000;

                treatment_timeline.Items.Add("Checking breathing");
                if (breathing == 0)
                {
                    breathing_description = "no breathing problem";
                    treatment_timeline.Items.Add("Patient breathing normally");
                    breathing = 0;
                }
                else if (breathing == 1)
                {
                    treatment_timeline.Items.Add("Patient breathing is not normal");
                    treatment_timeline.Items.Add("providing oxygen");
                    breath1to0_cure_probability = breath1to0_cure_probability - (breath1_successprob_reduction * breath1_count);
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
                    }
                    else
                    {
                        breathing = 1;
                        breathing_description = "weak or irregular breathing";
                        treatment_timeline.Items.Add("breathing remains erratic");
                        total_unsuccess_count = total_unsuccess_count + 1;
                    }
                    breath1_count = breath1_count + 1;
                    breathing_timedelay = breathing1_timedelay;
                }
                else // ...if breathing = 2
                {
                    treatment_timeline.Items.Add("Patient not breathing");
                    treatment_timeline.Items.Add("Aspirating Lungs");
                    breath2to0_cure_probability = breath2to0_cure_probability - (breath2_successprob_reduction * breath2_count);
                    breath2to1_cure_probability = breath2to1_cure_probability - (breath2_successprob_reduction * breath2_count);
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
                    }
                    else if (x <= breath2to1_cumprob && x > breath2to0_cure_probability)
                    {
                        breathing_description = "previously not breathing, improved but not breathing normally";
                        treatment_timeline.Items.Add("breathing restored but still erratic");
                        breathing = 1;
                        total_partial_success_count = total_partial_success_count + 1;
                    }
                    else
                    {
                        breathing_description = "not breathing";
                        treatment_timeline.Items.Add("Patient still not breathing");
                        breathing = 2;
                        total_unsuccess_count = total_unsuccess_count + 1;
                    }
                    breath2_count = breath2_count + 1;
                    breathing_timedelay = breathing2_timedelay;
                }

                #region show treating patient message
                // display treatment message - requires 'async' in the private void statement
                var treatment_message = new Message_form();
                treatment_message.Show();
                await Task.Delay(breathing_timedelay);
                treatment_message.Close();
                // end display treatment message
                #endregion

            }// end of breathing check
            #endregion
            #region circulation check
            async void Circulationcheck()
            {
                // This is the previous code for 2 check controls to be utilized - check this! 
                #region  I think this relates to how patient deteriorates given multiple symptoms - move to patient agent
                //treatment_timeline.Items.Add("Checking Circulation");
                //if (circulation == 0)
                //{
                //    if (hemorrhage >= 1 && breathing == 0)
                //    {
                //        Hemorrhagecheck();
                //        Breathingcheck();
                //    }
                //    else if (hemorrhage >= 1 && breathing == 1)
                //    {
                //        Hemorrhagecheck();
                //    }
                //    else if (hemorrhage == 0 && breathing == 0)
                //    {
                //        Breathingcheck();
                //    }
                //    else
                //    {
                //        circulation = 1;
                //    }
                //}
                //else
                //{
                //    circulation = 1;
                //}
                #endregion

                // *******************************SET VARIABLES FOR THE TREATMENT*********************************************************
                #region Treatment variables
                int circ1to0_cure_probability = 70; // the probability that circulation treatment from level 1 to 0 is successful
                int circ1_successprob_reduction = 5; // the reduction in the probability of success with each repeated treatment from level 1

                int circ2to0_cure_probability = 15; // the probability that circulation treatment from level 2 to 0 is successful
                int circ2to1_cure_probability = 30; // the probability that circulation treatment from level 2 to 1 is successful
                int circ2to1_cumprob = circ2to1_cure_probability + circ2to0_cure_probability; // cumulative success probabiligy
                int circ2_0_successprob_reduction = 1; // the reduction in the probability of success with each repeated treatment from level 2 to 0
                int circ2_1_successprob_reduction = 2; // the reduction in the probability of success with each repeated treatment from level 2 to 1

                int circ_timedelay = 0;
                int circ1_timedelay = 3000;
                int circ2_timedelay = 5000;
                #endregion

                // *********************** Treatment Logic and message to Listbox **********************************************************
                #region Treatment Logic
                treatment_timeline.Items.Add("Checking pulse");
                if (circulation == 0)  // If there is no problem
                {
                    circulation_description = "";
                    treatment_timeline.Items.Add("pulse is normal");                            //patient condition message
                    circulation = 0;                                                            //set patient condition quantifier to 0 'normal'
                }
                else if (circulation == 1)  // if there is a minor problem
                {
                    treatment_timeline.Items.Add("Pulse is elevated/weak/erratic");             //Patient condition message
                    treatment_timeline.Items.Add("providing treatment");                        //ADD treatment here 
                    circ1to0_cure_probability -= (circ1_successprob_reduction* circ1_count);    //partial success probability accounting for repeat procedures
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
                    }
                    else if (x <= circ2to1_cumprob && x > circ2to0_cure_probability)
                    {
                        circulation_description = "previously in cardiac arrest but now pulse is weak or irregular";
                        treatment_timeline.Items.Add("Heart function restored but not normal"); //patient partially stabliized message
                        circulation = 1;                                                        //patient condition quantifier set to 1 - some problems
                        total_partial_success_count += 1;                                       // counter for partial successful procedures
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

                #region show treating patient message
                //display treatment message - requires 'async' in the private void statement
                var treatment_message = new Message_form();
                treatment_message.Show();                                                        // open message box
                await Task.Delay(circ_timedelay);                                                //leave message on screen for as long as delay is set dependent on procedure
                treatment_message.Close();                                                       // close message box
                 // end display treatment message
                #endregion

            }// end of circulation check
            #endregion
            #region airwayclear
            async void Airwaycheck()
            {
                 // *******************************SET VARIABLES FOR THE TREATMENT*********************************************************
                #region Treatment variables
                int airway_1to0_cure_probability = 50; // the probability that airway treatment from level 1 to 0 is successful
                int airway1_successprob_reduction = 0; // the reduction in the probability of success with each repeated treatment from level 1

                int airway_2to0_cure_probability = 33; // the probability that airway treatment from level 2 to 0 is successful
                int airway_2to1_cure_probability = 33; // the probability that airway treatment from level 2 to 1 is successful
                int airway_2to1_cumprob = airway_2to1_cure_probability + airway_2to0_cure_probability; // cumulative success probabiligy
                int airway_2_0_successprob_reduction = 0; // the reduction in the probability of success with each repeated treatment from level 2 to 0
                int airway_2_1_successprob_reduction = 0; // the reduction in the probability of success with each repeated treatment from level 2 to 1

                int airway_timedelay = 0;
                int airway1_timedelay = 3000;
                int airway2_timedelay = 5000;
                #endregion

                // *********************** Treatment Logic and message to Listbox **********************************************************
                #region Treatment Logic
                treatment_timeline.Items.Add("Checking for airway obstruction");
                if (airway == 0)  // If there is no problem
                {
                    treatment_timeline.Items.Add("no obstruction to airway");                            //patient condition message
                    airway = 0;                                                            //set patient condition quantifier to 0 'normal'
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
                        airway = 0;                                                      //patient condition quantifier set to 0 - 'normal'
                        stopwatch_air.Reset();
                        total_success_count += 1;                                             //global counter for successful procedures
                    }
                    else
                    {
                        treatment_timeline.Items.Add("airway still blocked");      //Patient condition unchanged message
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
                        treatment_timeline.Items.Add("Airway cleared");                     // patient condition returned to normal message
                        airway = 0;                                                         // patient condition quantifier set to 0 - 'normal'
                        stopwatch_air.Reset();
                        total_success_count += 1;                                           // global counter for successful procedures
                    }
                    else if (x <= airway_2to1_cumprob && x > airway_2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("Airway partially cleared");           //patient partially stabliized message
                        airway = 1;                                                         //patient condition quantifier set to 1 - some problems
                        total_partial_success_count += 1;                                   // counter for partial successful procedures
                    }
                    else
                    {
                        treatment_timeline.Items.Add("Airway still blocked");               //patient still critical
                        airway = 2;                                                         //patient condition quantifier set to 2 - critical
                        total_unsuccess_count += 1;                                         // counter for unsuccessful procedures
                    }
                    airway2_count += 1;                                                     //counter for total critical procedures (successful and unsuccessful)
                    airway_timedelay = airway2_timedelay;                                   //set timedelay to variable for critical procedure
                }
                #endregion

                #region show treating patient message
                // display treatment message - requires 'async' in the private void statement
                var treatment_message = new Message_form();
                treatment_message.Show();                                                        // open message box
                await Task.Delay(airway_timedelay);                                                //leave message on screen for as long as delay is set dependent on procedure
                treatment_message.Close();                                                       // close message box
                // end display treatment message
                #endregion

                sim_Current_air_box.Text = Convert.ToString(airway);                         //convert condition quantifier to string for transfer to UI and display

            }// end of airway check

            #endregion
            #region cpr check
            async void CPRcheck()
            {
                // *******************************SET VARIABLES FOR THE TREATMENT*********************************************************
                #region Treatment variables
                int cpr2to0_cure_probability = 10; // the probability that circulation treatment from level 2 to 0 is successful
                int cpr2to1_cure_probability = 20; // the probability that circulation treatment from level 2 to 1 is successful
                int cpr2to1_cumprob = cpr2to1_cure_probability + cpr2to0_cure_probability; // cumulative success probabiligy
                int cpr2_0_successprob_reduction = 1; // the reduction in the probability of success with each repeated treatment from level 2 to 0
                int cpr2_1_successprob_reduction = 2; // the reduction in the probability of success with each repeated treatment from level 2 to 1

                int cpr_timedelay = 0;
                int cpr2_timedelay = 6000;
                #endregion

                // *********************** Treatment Logic and message to Listbox **********************************************************
                #region Treatment Logic
                treatment_timeline.Items.Add("Checking pulse");
                if (circulation == 2 && breathing == 2 )  // If CPR needed
                 {
                    // add defibrillation after 2 repetitions
                    if (cir_count < 2)
                    {
                        treatment_timeline.Items.Add("Patient in cardiac arrest and not breathing");            // Patient condition message
                        treatment_timeline.Items.Add("attempting CPR resusitation");
                    }
                    else
                    {
                        treatment_timeline.Items.Add("Patient in cardiac arrest and not breathing");            // Patient condition message
                        treatment_timeline.Items.Add("attempting CPR with defibrillation");
                    }
                    cpr2to0_cure_probability -= (cpr2_0_successprob_reduction * circ2_count); //complete success probability accounting for repeats
                    cpr2to1_cure_probability -= (cpr2_1_successprob_reduction * circ2_count); //partial success probability accounting for repeats
                    cpr2to1_cumprob = cpr2to1_cure_probability + cpr2to0_cure_probability;   // set cumulative success and partial success probability

                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);                                                         //generate random number between 1 and 100
                    if (x <= cpr2to0_cure_probability)
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
                    else if (x <= cpr2to1_cumprob && x > cpr2to0_cure_probability)
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
                        treatment_timeline.Items.Add("Patient Still not breathing");        //patient still critical
                        circulation = 2;                                                        //patient condition quantifier set to 2 - critical
                        total_unsuccess_count += 1;                                             // counter for unsuccessful procedures
                    }
                    cir_count += 1;                                                           //counter for total critical procedures (successful and unsuccessful)
                    cpr_timedelay = cpr2_timedelay;                                           //set timedelay to variable for critical procedure
                }
                #endregion

                #region show treating patient message
                //display treatment message - requires 'async' in the private void statement
                var treatment_message = new Message_form();
                treatment_message.Show();                                                        // open message box
                await Task.Delay(cpr_timedelay);                                                //leave message on screen for as long as delay is set dependent on procedure
                treatment_message.Close();                                                       // close message box
                                                                                                 // end display treatment message
                #endregion

            }// end of circulation check
            #endregion

            from = "treatment"; to = "patient"; subject = "update treatment in UI";
            patient();

            from = "EMS"; to = "AI"; subject = "Recommend treatment";
            AI();
            //var Th_EMStoAI = new Thread(() => AI());                                            // sending update info of patient to AI for recommendation
            //Th_EMStoAI.Start(); removing threading - check
        } // end of Treatment method

        #endregion

        #region determine patient baseline characteristics at the end of TOC

        #endregion












































        // Dump old event handlers in here until I can work out how to clear them!!!
        #region unused_code
        #region used button click events

        private void sim_finalair_box_TextChanged(object sender, EventArgs e)
        {
            //Stopwatch_air_Function_Call = 3;
            //stopwatch_air.Reset();
            //Thread th_stopwatch_air = new Thread(stopwatch_record);
            //th_stopwatch_air.Start();
        }

        private void sim_finalbreath_box_TextChanged(object sender, EventArgs e)
        {
            //Stopwatch_breath_Function_Call = 4;
            //stopwatch_bre.Reset();
            //Thread th_stopwatch_bre = new Thread(stopwatch_record);
            //th_stopwatch_bre.Start();
        }

        private void sim_finalcirc_box_TextChanged(object sender, EventArgs e)
        {
            //Stopwatch_circ_Function_Call = 5;
            //stopwatch_cir.Reset();
            //Thread th_stopwatch_con = new Thread(stopwatch_record);
            //th_stopwatch_con.Start();
        }

        private void sim_finalconc_box_TextChanged(object sender, EventArgs e)
        {
            //Stopwatch_conc_Function_Call = 2;
            //stopwatch_con.Reset();
            //Thread th_stopwatch_con = new Thread(stopwatch_record);
            //th_stopwatch_con.Start();
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
            //System.Media.SystemSounds.Beep.Play();
            //Stopwatch_hem_Function_Call = 1;
            //stopwatch_hem.Reset();
            //Thread th_stopwatch_hem = new Thread(stopwatch_record);
            //th_stopwatch_hem.Start();
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
        #endregion

        #region old code - stored here for neatness and to avoid breaking code
        // old Predict patient state code

        #region patient state prediction

        //void predictedvalue()
        //{
        //    using (var reader = new StreamReader("C:\\temp\\AI_brain1.csv"))
        //    {
        //        List<string> list_ini_hem = new List<string>();
        //        List<string> list_ini_con = new List<string>();
        //        List<string> list_ini_air = new List<string>();
        //        List<string> list_ini_bre = new List<string>();
        //        List<string> list_ini_cir = new List<string>();
        //        List<string> list_time = new List<string>();
        //        List<string> list_fin_hem = new List<string>();
        //        List<string> list_fin_con = new List<string>();
        //        List<string> list_fin_air = new List<string>();
        //        List<string> list_fin_bre = new List<string>();
        //        List<string> list_fin_cir = new List<string>();
        //        List<string> list_tourniquet_count = new List<string>();
        //        List<string> list_consciousness_count = new List<string>();
        //        List<string> list_airway_count = new List<string>();
        //        List<string> list_breathing_count = new List<string>();
        //        List<string> list_circulation_count = new List<string>();
        //        while (!reader.EndOfStream)
        //        {
        //            var line = reader.ReadLine();
        //            var values = line.Split(',');

        //            list_ini_hem.Add(values[0]);
        //            list_ini_con.Add(values[1]);
        //            list_ini_air.Add(values[2]);
        //            list_ini_bre.Add(values[3]);
        //            list_ini_cir.Add(values[4]);
        //            list_time.Add(values[5]);
        //            list_fin_hem.Add(values[6]);
        //            list_fin_con.Add(values[7]);
        //            list_fin_air.Add(values[8]);
        //            list_fin_bre.Add(values[9]);
        //            list_fin_cir.Add(values[10]);
        //            //list_action.Add(values[11]);
        //            list_tourniquet_count.Add(values[11]);
        //            list_consciousness_count.Add(values[12]);
        //            list_airway_count.Add(values[13]);
        //            list_breathing_count.Add(values[14]);
        //            list_circulation_count.Add(values[15]);

        //        }
        //        for (int i = 0; i < 243; i++)
        //        {
        //            if ((list_ini_hem[i] == Convert.ToString(initial_hemorrhage)) && (list_ini_con[i] == Convert.ToString(initial_consciousness)) &&
        //                    (list_ini_air[i] == Convert.ToString(initial_airway)) && (list_ini_bre[i] == Convert.ToString(initial_breathing)) &&
        //                    (list_ini_cir[i] == Convert.ToString(initial_circulation)))
        //            {
        //                predicted_hem = list_fin_hem[i];
        //                predicted_con = list_fin_con[i];
        //                predicted_air = list_fin_air[i];
        //                predicted_bre = list_fin_bre[i];
        //                predicted_cir = list_fin_cir[i];
        //                predicted_time = list_time[i];

        //                hemorrhage_count = list_tourniquet_count[i];
        //                consciousness_count = list_consciousness_count[i];
        //                airway_count = list_airway_count[i];
        //                breathing_count = list_breathing_count[i];
        //                circulation_count = list_circulation_count[i];

        //                DELEGATE del = new DELEGATE(UIController);
        //                this.Invoke(del);
        //            }

        //        }
        //    }
        //}


        #endregion
        //old crappy event
        private void treatment_timeline_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
        //public void patient_degrade()
        //{
        //    #region request patient condition updates every interval for duration of sim

        //    int Patient_deg_interval = 10000; //interval in millisecods (system timers default)
        //    int patient_count_limit = 120 / (Patient_deg_interval / 1000);      //define limiter as 2 minutes independent of interval
        //    int patient_deg_counter = 0;                                        // reset counter
        //    patient_deg_timer = new System.Windows.Forms.Timer                  // initiate the patient degradation check timer
        //    {
        //        Interval = Patient_deg_interval                                 // set the timer interval
        //    };

        //    do
        //    {
        //        from = "update_patient_degradation";
        //        patient();
        //        patient_deg_timer.Enabled = true;                               // start the timer pause for given interval
        //        patient_deg_counter += 1;
        //        Thread.Sleep(Patient_deg_interval);
        //    } while (patient_deg_counter < patient_count_limit);
        //    #endregion

        //    //from = "called by main function";
        //    //    Thread EMS = new Thread(() => Ems()); // Run EMS agent in thread called EMS RENAME THREAD!
        //    //    EMS.Start();
        //}
        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

    }
}