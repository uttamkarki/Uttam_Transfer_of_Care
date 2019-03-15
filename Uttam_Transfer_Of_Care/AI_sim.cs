using System;
using System.Diagnostics;
using System.Drawing;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;


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
        public static int hem1_count = 0;
        public static int hem2_count = 0;
        public static int conc1_count = 0;
        public static int conc2_count = 0;
        public static int air1_count = 0;
        public static int air2_count = 0;
        public static int airway1_count = 0;
        public static int airway2_count = 0;
        public static int breath1_count = 0;
        public static int breath2_count = 0;
        public static int circ1_count = 0;
        public static int circ2_count = 0;
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

        #endregion

        // if not needed delete these values cause I have already created a new set of variables
        #region Start values for model variables

        public static string var_age = "25";
        public static string var_gender = "male";
        public static string var_hem = "2";
        public static string var_air = "0";
        public static string var_conc = "1";
        public static string var_breath = "0";
        public static string var_circ = "0";
        public static string var_injury = "Gunshot wound";
        public static string var_injury_location = "lower left leg";


        #endregion

        public AI_sim()
        {
            InitializeComponent();
        }

        Stopwatch stopwatch = new Stopwatch();

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

        // ***************Calling other forms - Commented out as doesn't seem to do anything*************************
        //inputform Inputform1 = new inputform(ListBox.ObjectCollection objectcollection);
        //hidden_form underlying_form = new hidden_form();
        //ai_interface ai_interface = new ai_interface();
        //***********************************************************************************************************

        // Creating new patient agent   
        #region Create New Patient Thread

        public void patient()
        {
            if (from == "called by main function")
            {
                assignvalue();
                Thread.Sleep(500);
                predictedvalue();

                // message signal sent by patient to EMS regarding intial status available
                from = "Patient";
                to = "EMS";
                subject = "Treatment Required";
                state = "Initial status available";

                var Th_PtoEMS = new Thread(() => Ems());
                Th_PtoEMS.Start();
            } // called by main function ends
            
            else if (from == "EMS" && to == "patient")
            {
                if (subject == "Applied Treatment")
                {
                    // UI controller will do all of update in form about recent changes made in patient health
                    UIController();
 
                } // end of if loop

            }// called by EMS ends
            else if (from == "Stopwatch" && to == "UIcontroller")
            {
                DELEGATE del = new DELEGATE(UIController);
                this.Invoke(del); 
            }
            else
            {
            }

            #region assigning initial patient attributes randomly
            void assignvalue()
            {
                Random random = new Random();
                int a;

                #region set wound location strings
                wound_loc_ID = random.Next(1, 23);
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
                #endregion

                //currently set to 1 injury type 'gunshot' for ease of simulation
                #region set wound type
                wound_type_ID = random.Next(0, 1);
                if (wound_type_ID == 0) wound_type = "gunshot";
                else if (wound_type_ID == 1) wound_type = "blunt force trauma";
                else if (wound_type_ID == 2) wound_type = "drowning";
                else wound_type = "allergic  reaction";
                #endregion 

                //coarse probability distributiuons for each of the four age categories
                #region Define age category probabilities by injury type
                int[] Ageprob_gunshot = { 10, 20, 90, 100 };
                int[] Ageprob_bft = { 20,40,80,100 };
                int[] Ageprob_drowning = { 30, 80, 85, 100 }; // cumulative probabilities as age group increases
                int[] Ageprob_allergy = { 25, 50, 75, 100 };
                #endregion

                #region assign age group and age for display depending on type of injury
                age_p = random.Next(0, 101); //set random seed for probability of any given age

                #region gunshot wounds
                if (wound_type_ID == 0) 
                     if (age_p <= Ageprob_gunshot[0])
                        {
                            age_group = "small child";
                            age = random.Next(1,3);
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
                #endregion

                a = random.Next(0, 2);
                if (a == 0)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
                
                a = random.Next(0, 3);
                if (a == 0)
                {
                    hemorrhage = 0;
                    initial_hemorrhage = hemorrhage;
                }
                else if (a == 1)
                {
                    hemorrhage = 1;
                    initial_hemorrhage = hemorrhage;
                }
                else
                {
                    hemorrhage = 2;
                    initial_hemorrhage = hemorrhage; ;
                }
                a = random.Next(0, 3);
                if (a == 0)
                {
                    consciousness = 0;
                    initial_consciousness = consciousness;
                }
                else if (a == 1)
                {
                    consciousness = 1;
                    initial_consciousness = consciousness;
                }
                else
                {
                    consciousness = 2;
                    initial_consciousness = consciousness; ;
                }
                a = random.Next(0, 2);
                if (a == 0)
                {
                    airway = 0;
                    initial_airway = airway;
                }
                else
                {
                    airway = 1;
                    initial_airway = airway; ;
                }
                a = random.Next(0, 2);
                if (a == 0)
                {
                    breathing = 0;
                    initial_breathing = breathing;
                }
                else
                {
                    breathing = 1;
                    initial_breathing = breathing; ;
                }
                a = random.Next(0, 2);
                if (a == 0)
                {
                    circulation = 0;
                    initial_circulation = circulation;
                }
                else
                {
                    circulation = 1;
                    initial_circulation = circulation; ;
                }

                Thread.Sleep(1000);
                DELEGATE del = new DELEGATE(UIController);
                this.Invoke(del);
                
            }   // Assign Value method end...
            #endregion
            void predictedvalue()
            {
                using (var reader = new StreamReader("D:\\AI_brain1.csv"))
                {
                    List<string> list_ini_hem = new List<string>();
                    List<string> list_ini_con = new List<string>();
                    List<string> list_ini_air = new List<string>();
                    List<string> list_ini_bre = new List<string>();
                    List<string> list_ini_cir = new List<string>();
                    List<string> list_time = new List<string>();
                    List<string> list_fin_hem = new List<string>();
                    List<string> list_fin_con = new List<string>();
                    List<string> list_fin_air = new List<string>();
                    List<string> list_fin_bre = new List<string>();
                    List<string> list_fin_cir = new List<string>();
                    List<string> list_tourniquet_count = new List<string>();
                    List<string> list_consciousness_count = new List<string>();
                    List<string> list_airway_count = new List<string>();
                    List<string> list_breathing_count = new List<string>();
                    List<string> list_circulation_count = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        list_ini_hem.Add(values[0]);
                        list_ini_con.Add(values[1]);
                        list_ini_air.Add(values[2]);
                        list_ini_bre.Add(values[3]);
                        list_ini_cir.Add(values[4]);
                        list_time.Add(values[5]);
                        list_fin_hem.Add(values[6]);
                        list_fin_con.Add(values[7]);
                        list_fin_air.Add(values[8]);
                        list_fin_bre.Add(values[9]);
                        list_fin_cir.Add(values[10]);
                        //list_action.Add(values[11]);
                        list_tourniquet_count.Add(values[11]);
                        list_consciousness_count.Add(values[12]);
                        list_airway_count.Add(values[13]);
                        list_breathing_count.Add(values[14]);
                        list_circulation_count.Add(values[15]);

                    }
                    for (int i = 0; i < 243; i++)
                    {
                        if ((list_ini_hem[i] == Convert.ToString(initial_hemorrhage)) && (list_ini_con[i] == Convert.ToString(initial_consciousness)) &&
                                (list_ini_air[i] == Convert.ToString(initial_airway)) && (list_ini_bre[i] == Convert.ToString(initial_breathing)) &&
                                (list_ini_cir[i] == Convert.ToString(initial_circulation)))
                        {
                            predicted_hem = list_fin_hem[i];
                            predicted_con = list_fin_con[i];
                            predicted_air = list_fin_air[i];
                            predicted_bre = list_fin_bre[i];
                            predicted_cir = list_fin_cir[i];
                            predicted_time = list_time[i];
                            hemorrhage_count = list_tourniquet_count[i];
                            consciousness_count = list_consciousness_count[i];
                            airway_count = list_airway_count[i];
                            breathing_count = list_breathing_count[i];
                            circulation_count = list_circulation_count[i];
                            DELEGATE del = new DELEGATE(UIController);
                            this.Invoke(del);
                        }
                        
                    }
                }
            }
            // method to update user interface with most recent patient attributes - 
            // called as a delegate 'del' from within Patient thread
            // UIcontroller control
            #region update patient attributes

            void UIController()
            {
                if (sim_inthem_box.Text == " ")
                {
                    Thread.Sleep(5);
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
                    
                    Thread.Sleep(5);
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

                Thread.Sleep(5);
                sim_finalair_box.Text = Convert.ToString(airway);                    // changes the final state of the patient at given time in AI_Sim from
                sim_finalbreath_box.Text = Convert.ToString(breathing);
                sim_finalcirc_box.Text = Convert.ToString(circulation);
                sim_finalconc_box.Text = Convert.ToString(consciousness);
                sim_finalhem_box.Text = Convert.ToString(hemorrhage);
            }
            #endregion

        }  // end of Patient agent 
        #endregion

        // EMS Agent Start
        #region create New EMS Agent thread
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
                    var Th_EMStoAI = new Thread(() => AI());                       // sending info from patient to AI for recommendation
                    Th_EMStoAI.Start();

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
                    int hem = hemorrhage; int con = consciousness; int air = airway; int bre = breathing; int cir = circulation;
                    if (hem > 0)
                    {
                        act = "check hemorrhage";
                    }
                    else if(con > 0)
                    {
                        act = "check consciousness";
                    }
                    else if (air > 0)
                    {
                        act = "check airway";
                    }
                    else if (cir > 0)
                    {
                        act = "check circulation";
                    }
                    else if (bre > 0)
                    {
                        act = "check breathing";
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
                        Button_hemm_torniquet.BackColor = Color.GreenYellow;
                    }

                    if (action == "check consciousness")
                    {
                        Button_conc_drugs.BackColor = Color.GreenYellow;
                    }

                    if (action == "check airway")
                    {
                        Button_air_Intubate.BackColor = Color.GreenYellow;
                    }

                    if (action == "check breathing")
                    {
                        Button_breath_Oxygen.BackColor = Color.GreenYellow;
                    }

                    if (action == "check circulation")
                    {
                        Button_circ_CPR.BackColor = Color.GreenYellow;
                    }

                } // end of Display Action function
                #endregion
            }
            #endregion
            var Th_speakAI = new Thread(() => speakAI(action));
                Th_speakAI.Start();
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

        // Click start Simulation button - starts a Patient Agent thread and EMS agent thread
        #region start simulation button click
        private void button1_Click_2(object sender, EventArgs e)
        {
            button1.BackColor = Color.Empty;
            from = "called by main function";
            Thread Patient = new Thread(() => patient());
            Patient.Start();
            Thread EMS = new Thread(() => Ems());
            EMS.Start();
        }
        #endregion

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

        private async void Button_hemm_torniquet_Click(object sender, EventArgs e)
        {
            Button_hemm_torniquet.BackColor = Color.Empty;

            // display treatment message - requires 'async' in the private void statement
            var treatment_message = new Message_form();
            treatment_message.Show();
            await Task.Delay(3000);
            treatment_message.Close();
            // end display treatment message

            Thread.Sleep(5);
            Treatment("check hemorrhage");
        }

        private async void Button_hemm_treat_Click(object sender, EventArgs e)
        {
            // display treatment message - requires 'async' in the private void statement
            var treatment_message = new Message_form();
            treatment_message.Show();
            await Task.Delay(3000);
            treatment_message.Close();
            // end display treatment message

            Thread.Sleep(5);
            Treatment("check hemorrhage");
        }

        private async void Button_conc_drugs_Click(object sender, EventArgs e)
        {
            Button_conc_drugs.BackColor = Color.Empty;

            // display treatment message - requires 'async' in the private void statement
            var treatment_message = new Message_form();
            treatment_message.Show();
            await Task.Delay(3000);
            treatment_message.Close();
            // end display treatment message

            Thread.Sleep(5);
            Treatment("check consciousness");
        }

        private async void Button_air_Intubate_Click(object sender, EventArgs e)
        {
            Button_air_Intubate.BackColor = Color.Empty;

            // display treatment message - requires 'async' in the private void statement
            var treatment_message = new Message_form();
            treatment_message.Show();
            await Task.Delay(3000);
            treatment_message.Close();
            // end display treatment message

            Thread.Sleep(5);
            Treatment("check airway");
        }

        private async void Button_air_clearair_Click(object sender, EventArgs e)
        {
            Button_air_clearair.BackColor = Color.Empty;

            // display treatment message - requires 'async' in the private void statement
            var treatment_message = new Message_form();
            treatment_message.Show();
            await Task.Delay(3000);
            treatment_message.Close();
            // end display treatment message

            //Thread.Sleep(5);
            Treatment("check airway");
        }

        private async void Button_circ_CPR_Click(object sender, EventArgs e)
        {
            Button_circ_CPR.BackColor = Color.Empty;

            // display treatment message - requires 'async' in the private void statement
            var treatment_message = new Message_form();
            treatment_message.Show();
            await Task.Delay(3000);
            treatment_message.Close();
            // end display treatment message

            Thread.Sleep(5);
            Treatment("check circulation");
        }

        private async void Button_circ_drugs_Click(object sender, EventArgs e)
        {
            Button_circ_drugs.BackColor = Color.Empty;

            // display treatment message - requires 'async' in the private void statement
            var treatment_message = new Message_form();
            treatment_message.Show();
            await Task.Delay(3000);
            treatment_message.Close();
            // end display treatment message

            Thread.Sleep(5);
            Treatment("check circulation");
        }

        private async void Button_breath_Oxygen_Click(object sender, EventArgs e)
        {
            Button_breath_Oxygen.BackColor = Color.Empty;

            // display treatment message - requires 'async' in the private void statement
            var treatment_message = new Message_form();
            treatment_message.Show();
            await Task.Delay(3000);
            treatment_message.Close();
            // end display treatment message

            Thread.Sleep(5);
            Treatment("check breathing");
        }

        private async void Button_breath_rescuebreath_Click(object sender, EventArgs e)
        {
            Button_breath_rescuebreath.BackColor = Color.Empty;

            // display treatment message - requires 'async' in the private void statement
            var treatment_message = new Message_form();
            treatment_message.Show();
            await Task.Delay(3000);
            treatment_message.Close();
            // end display treatment message

            Thread.Sleep(5);
            Treatment("check breathing");
        }

        #endregion


        #endregion

        int Stopwatch_hem_Function_Call;
        int Stopwatch_conc_Function_Call;
        int Stopwatch_air_Function_Call;
        int Stopwatch_circ_Function_Call;
        int Stopwatch_breath_Function_Call;
        Stopwatch stopwatch_hem = new Stopwatch();
        Stopwatch stopwatch_con = new Stopwatch();
        Stopwatch stopwatch_air = new Stopwatch();
        Stopwatch stopwatch_bre = new Stopwatch();
        Stopwatch stopwatch_cir = new Stopwatch();
        public void stopwatch_record()
        {
            if (Stopwatch_hem_Function_Call == 1)
            {
                stopwatch_hem.Start();
                while (stopwatch_hem.Elapsed.Seconds <= 10)
                {

                }
                if(hemorrhage > 0 && hemorrhage < 2)
                {
                    hemorrhage = hemorrhage + 1;
                }
                DELEGATE del = new DELEGATE(patient);
                this.Invoke(del);
            }
            else if (Stopwatch_conc_Function_Call == 2)
            {
                stopwatch_con.Start();
                while (stopwatch_hem.Elapsed.Seconds <= 10)
                {

                }
                if(consciousness > 0 && consciousness < 2)
                {
                    consciousness = consciousness + 1;
                }
                DELEGATE del = new DELEGATE(patient);
                this.Invoke(del);
            }
            else if (Stopwatch_air_Function_Call == 3)
            {
                stopwatch_air.Start();
                while (stopwatch_hem.Elapsed.Seconds <= 10)
                {

                }
                if(airway > 0 && airway < 2)
                {
                    airway = airway + 1;
                }
                DELEGATE del = new DELEGATE(patient);
                this.Invoke(del);
            }
            else if (Stopwatch_breath_Function_Call == 4)
            {
                stopwatch_bre.Start();
                while (stopwatch_hem.Elapsed.Seconds <= 10)
                {

                }
                if(breathing >0 && breathing < 2)
                {
                    breathing = breathing + 1;
                }
                DELEGATE del = new DELEGATE(patient);
                this.Invoke(del);
            }
            else if (Stopwatch_circ_Function_Call == 5)
            {
                stopwatch_cir.Start();
                while (stopwatch_hem.Elapsed.Seconds <= 10)
                {

                }
                if (circulation > 0 && circulation < 2)
                {
                    circulation = circulation + 1;
                }
                DELEGATE del = new DELEGATE(patient);
                this.Invoke(del);
            }
            else
            {

            }
            from = "Stopwatch"; to = "UIcontroller";
            patient();
        }
        
        
        
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
            #endregion

            //perform check and treat condition depending on message from AI - Change to EMS? 
            #region hemorrhage check
            async void Hemorrhagecheck()
            {
                
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
                        treatment_timeline.Items.Add("No hemorrhage");
                    }
                    // commented out this part as this is only a check - degredation should be generated in patient thread

                    //else if (x <= 95 && x > 85)
                    //{
                    //    hemorrhage = 1;
                    //}
                    //else
                    //{
                    //    hemorrhage = 2;
                    //}
                }
                else if (hemorrhage == 1)
                {
                    treatment_timeline.Items.Add("light Bleeding");
                    int hem1to0_cure_probability = 90 - (5 * hem1_count);
                    int hem1to1_cure_probability = 10 + (5 * hem1_count);
                    int hem1_cure_cumprob = hem1to1_cure_probability + hem1to0_cure_probability;

                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= hem1to0_cure_probability)
                    {
                        hemorrhage = 0;
                        hemorrhage_description = "light bleeding stopped";
                        treatment_timeline.Items.Add("Bleeding stopped");
                        total_success_count = total_success_count + 1;
                    }
                    else  
                    {
                        hemorrhage = 1;
                        hemorrhage_description = "some bleeding";
                        treatment_timeline.Items.Add("light bleeding not stopped");
                        total_unsuccess_count = total_unsuccess_count + 1;
                    }

                    hem1_count = hem1_count + 1;
                }
                else // ...if hemorrhage = 2
                {
                    treatment_timeline.Items.Add("heavy bleeding");
                    int hem2to0_cure_probability = 30 - (3 * hem2_count);
                    int hem2to1_cure_probability = 60 - (3 * hem2_count);
                    int hem2to2_cure_probability = 10 + (6 * hem2_count);
                    int hem2to1_cumprob = hem2to1_cure_probability + hem2to0_cure_probability;
                    int hem2_cure_cumprob = hem2to1_cure_probability + hem2to0_cure_probability + hem2to2_cure_probability;

                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= hem2to0_cure_probability)
                    {
                        hemorrhage = 0;
                        hemorrhage_description = "heavy bleeding stopped";
                        treatment_timeline.Items.Add("Bleeding stopped");
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

                    hem2_count = hem2_count + 1;
                }

                #region show patient treating message
                // display treatment message - requires 'async' in the private void statement
                var treatment_message = new Message_form();
                treatment_message.Show();
                await Task.Delay(3000);
                treatment_message.Close();
                // end display treatment message
                #endregion

                // do we need the thread to sleep if displaying message?  maybe lock the app? 
                System.Threading.Thread.Sleep(5000);
                sim_finalhem_box.Text = Convert.ToString(hemorrhage);
                Thread.Sleep(5);
                if(Stopwatch_hem_Function_Call != 1)
                {
                    Stopwatch_hem_Function_Call = 1;
                    stopwatch_hem.Reset();
                    Thread th_stopwatch_hem = new Thread(stopwatch_record);
                    th_stopwatch_hem.Start();
                }

            } // end of hemorrhage check
            #endregion
            #region consciousness check
            async void Consciousnesscheck()
            {
                int conc1to0_cure_probability;
                int conc1to1_cure_probability = 0;
                int conc2to0_cure_probability = 10;

                int conc2to1_cure_probability = 50;
                int conc2to2_cure_probability = 40;
                int conc2to1_cumprob = conc2to1_cure_probability + conc2to0_cure_probability;

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

                    if (breathing == 0 && hemorrhage == 0 && airway == 0 && circulation == 0)
                    {
                        conc1to0_cure_probability = 60 + (5 * conc1_count);
                        conc1to1_cure_probability = 40 - (5 * conc1_count);
                    }
                    else
                    {
                        conc1to0_cure_probability = 60 - (5 * conc1_count);
                        conc1to1_cure_probability = 40 + (5 * conc1_count);
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
                        total_success_count = total_success_count + 1;
                    }
                    else  
                    {
                        consciousness = 1;
                        consciousness_decription = "partially conscious";
                        treatment_timeline.Items.Add("Patient Still not fully conscious");
                        total_unsuccess_count = total_unsuccess_count + 1;
                    }
                    conc1_count = conc1_count + 1;
                }
                else // ...if consciousness = 2
                {
                    treatment_timeline.Items.Add("Patient unconscious");
                    if (breathing == 0 && hemorrhage == 0 && airway == 0 && circulation == 0)
                    { 
                        conc2to0_cure_probability = 10 + (2 * conc2_count);
                        conc2to1_cure_probability = 50 + (8 * conc2_count);
                        conc2to2_cure_probability = 40 - (10 * conc2_count);
                        conc2to1_cumprob = conc2to1_cure_probability + conc2to0_cure_probability;
                    }
                    else
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
                }

                #region show treating patient message
                // display treatment message - requires 'async' in the private void statement
                var treatment_message = new Message_form();
                treatment_message.Show();
                await Task.Delay(3000);
                treatment_message.Close();
                // end display treatment message
                #endregion

                // do we need the thread to sleep if displaying message?  maybe lock the app? 
                System.Threading.Thread.Sleep(2500);
                sim_finalconc_box.Text = Convert.ToString(consciousness);
                Thread.Sleep(5);
                if (Stopwatch_conc_Function_Call != 1)
                {
                    Stopwatch_conc_Function_Call = 1;
                    stopwatch_con.Reset();
                    Thread th_stopwatch_conc = new Thread(stopwatch_record);
                    th_stopwatch_conc.Start();
                }

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
            #region airway check
            async void Airwaycheck()
            {
                treatment_timeline.Items.Add("Checking airway");
                    if (airway == 0)
                    {
                    airway_description = "airway clear";
                    treatment_timeline.Items.Add("No obstruction");
                        airway = 0;
                    }
                    else if (airway == 1)
                    {
                        treatment_timeline.Items.Add("Partial airway obstruction");
                        int air1to0_cure_probability = 80;
                        int x;
                        Random r = new Random();
                        x = r.Next(0, 101);
                        if (x <= air1to0_cure_probability)
                        {
                            airway_description = "airway was partially blocked but now clear";
                            airway = 0;
                            treatment_timeline.Items.Add("Airway cleared");
                            total_success_count = total_success_count + 1;
                        }
                        else
                        {
                            airway_description = "airway partially blocked";
                            airway = 1;
                            treatment_timeline.Items.Add("Airway still partially obstructed");
                            total_unsuccess_count = total_unsuccess_count + 1;
                        }
                    air1_count = air1_count + 1;
                    }
                else // ...if airway = 2
                {
                    treatment_timeline.Items.Add("Airway fully blocked");
                    int air2to0_cure_probability = 30;
                    int air2to1_cure_probability = 30;
                    int air2to2_cure_probability = 40;
                    int air2to1_cumprob = air2to1_cure_probability + air2to0_cure_probability;
                    int air2_cure_cumprob = air2to1_cure_probability + air2to0_cure_probability + air2to2_cure_probability;
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= air2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("Airway fully cleared");
                        airway_description = "airway was blocked but now clear";
                        airway = 0;
                        total_success_count = total_success_count + 1;
                    }
                    else if (x <= air2to1_cumprob && x > air2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("Airway partially cleared");
                        airway_description = "airway was blocked but now partially clear";
                        airway = 1;
                        total_partial_success_count = total_partial_success_count + 1;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("Airway still blocked");
                        airway_description = "airway blocked";
                        airway = 2;
                        total_unsuccess_count = total_unsuccess_count + 1;
                    }
                    air2_count = air2_count + 1;
                }

                #region show treating patient message
                // display treatment message - requires 'async' in the private void statement
                var treatment_message = new Message_form();
                treatment_message.Show();
                await Task.Delay(3000);
                treatment_message.Close();
                // end display treatment message
                #endregion

                System.Threading.Thread.Sleep(5000);
                sim_finalair_box.Text = Convert.ToString(airway);
                Thread.Sleep(5);
                if (Stopwatch_air_Function_Call != 1)
                {
                    Stopwatch_air_Function_Call = 1;
                    stopwatch_air.Reset();
                    Thread th_stopwatch_air = new Thread(stopwatch_record);
                    th_stopwatch_air.Start();
                }


            } // end of airway check
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
                    breath1to0_cure_probability = breath1to0_cure_probability - (breath1_successprob_reduction * breath1_count);
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= breath1to0_cure_probability)
                    {
                        breathing = 0;
                        breathing_description = "previously had breathing problems but now normal";
                        treatment_timeline.Items.Add("Breathing restored");
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
                }
                else // ...if breathing = 2
                {
                    treatment_timeline.Items.Add("Patient not breathing");
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
                }

                #region show treating patient message
                // display treatment message - requires 'async' in the private void statement
                var treatment_message = new Message_form();
                treatment_message.Show();
                await Task.Delay(3000);
                treatment_message.Close();
                // end display treatment message
                #endregion

                System.Threading.Thread.Sleep(2500);
                sim_finalbreath_box.Text = Convert.ToString(breathing);
                Thread.Sleep(5);
                if (Stopwatch_breath_Function_Call != 1)
                {
                    Stopwatch_breath_Function_Call = 1;
                    stopwatch_bre.Reset();
                    Thread th_stopwatch_breath = new Thread(stopwatch_record);
                    th_stopwatch_breath.Start();
                }


            }// end of breathing check
            #endregion

            // this is the main template for creating new treatment controls - contains good comments and full variable definition
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

                int circ2to0_cure_probability = 10; // the probability that circulation treatment from level 2 to 0 is successful
                int circ2to1_cure_probability = 10; // the probability that circulation treatment from level 2 to 1 is successful
                int circ2to1_cumprob = circ2to1_cure_probability + circ2to0_cure_probability; // cumulative success probabiligy
                int circ2_0_successprob_reduction = 1; // the reduction in the probability of success with each repeated treatment from level 2 to 0
                int circ2_1_successprob_reduction = 1; // the reduction in the probability of success with each repeated treatment from level 2 to 1

                int circ_timedelay = 0;
                int circ1_timedelay = 3000;
                int circ2_timedelay = 5000;
                #endregion

                // *********************** Treatment Logic and message to Listbox **********************************************************
                #region Treatment Logic
                treatment_timeline.Items.Add("Checking for pulse");
                if (circulation == 0)  // If there is no problem
                {
                    circulation_description = "";
                    treatment_timeline.Items.Add("pulse is normal");                            //patient condition message
                    circulation = 0;                                                            //set patient condition quantifier to 0 'normal'
                }
                else if (circulation == 1)  // if there is a minor problem
                {
                    treatment_timeline.Items.Add("Pulse is elevated/weak/erratic");             //Patient condition message
                    circ1to0_cure_probability -= (circ1_successprob_reduction* circ1_count);    //partial success probability accounting for repeat procedures
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);                                                         //generate random number between 1 and 100 for outcome 
                    if (x <= circ1to0_cure_probability)
                    {
                        circulation_description = "weak or irregular pulse now normal";
                        treatment_timeline.Items.Add("pulse returned to normal");             //Patient condition returned to normal message
                        circulation = 0;                                                      //patient condition quantifier set to 0 - 'normal'
                        total_success_count += 1;                                             //global counter for successful procedures
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
                // display treatment message - requires 'async' in the private void statement
                var treatment_message = new Message_form();
                treatment_message.Show();                                                        // open message box
                await Task.Delay(circ_timedelay);                                                //leave message on screen for as long as delay is set dependent on procedure
                treatment_message.Close();                                                       // close message box
                // end display treatment message
                #endregion

                System.Threading.Thread.Sleep(2500);                                            //Sleep current thread for appropriate delay
                sim_finalcirc_box.Text = Convert.ToString(circulation);                         //convert condition quantifier to string for transfer to UI and display
                Thread.Sleep(5);
                if (Stopwatch_circ_Function_Call != 1)
                {
                    Stopwatch_circ_Function_Call = 1;
                    stopwatch_cir.Reset();
                    Thread th_stopwatch_circ = new Thread(stopwatch_record);
                    th_stopwatch_circ.Start();
                }

            }// end of circulation check
            #endregion

            //as yet unused treatment - made for multiple treatment option
            #region airwayclear
            async void airway_clear()
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
                int airway2_timedelay = 3000;
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
                    airway_1to0_cure_probability -= (airway1_successprob_reduction * airway1_count);    //partial success probability accounting for repeat procedures
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);                                                         //generate random number between 1 and 100 for outcome 
                    if (x <= airway_1to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("airway cleared");             //Patient condition returned to normal message
                        airway = 0;                                                      //patient condition quantifier set to 0 - 'normal'
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

                System.Threading.Thread.Sleep(2500);                                            //Sleep current thread for appropriate delay
                sim_finalair_box.Text = Convert.ToString(airway);                         //convert condition quantifier to string for transfer to UI and display

            }// end of airway check
            #endregion

            from = "EMS"; to = "AI"; subject = "Recommend treatment";
            var Th_EMStoAI = new Thread(() => AI());                                            // sending update info of patient to AI for recommendation
            Th_EMStoAI.Start();
        } // end of Treatment method

        #endregion

        #region determine patient baseline characteristics at the end of TOC






        #endregion

        // Dump old event handlers in here until I can work out how to clear them!!!
        #region unused_code
        #region used button click events
        private void sim_finalair_box_TextChanged(object sender, EventArgs e)
        {
            Stopwatch_air_Function_Call = 3;
            stopwatch_air.Reset();
            Thread th_stopwatch_air = new Thread(stopwatch_record);
            th_stopwatch_air.Start();
        }

        private void sim_finalbreath_box_TextChanged(object sender, EventArgs e)
        {
            Stopwatch_breath_Function_Call = 4;
            stopwatch_bre.Reset();
            Thread th_stopwatch_bre = new Thread(stopwatch_record);
            th_stopwatch_bre.Start();
        }

        private void sim_finalcirc_box_TextChanged(object sender, EventArgs e)
        {
            Stopwatch_circ_Function_Call = 5;
            stopwatch_cir.Reset();
            Thread th_stopwatch_con = new Thread(stopwatch_record);
            th_stopwatch_con.Start();
        }

        private void sim_finalconc_box_TextChanged(object sender, EventArgs e)
        {
            Stopwatch_conc_Function_Call = 2;
            stopwatch_con.Reset();
            Thread th_stopwatch_con = new Thread(stopwatch_record);
            th_stopwatch_con.Start();
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
            System.Media.SystemSounds.Beep.Play();
            Stopwatch_hem_Function_Call = 1;
            stopwatch_hem.Reset();
            Thread th_stopwatch_hem = new Thread(stopwatch_record);
            th_stopwatch_hem.Start();
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

        private void treatment_timeline_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
#endregion
    }
}