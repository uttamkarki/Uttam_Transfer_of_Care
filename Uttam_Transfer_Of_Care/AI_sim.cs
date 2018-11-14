using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using AIMLbot;
using System.Diagnostics;
using WMPLib;
using WaveLib;
using WaveLib.AudioMixer;
using WaveLib.WaveServices;
using System.Threading;
using System.Windows.Input;


namespace Uttam_Transfer_Of_Care
{
    delegate void message_fPtoEMS(Message msg);
    delegate void message_fEMStoMax(Message msg);
    delegate void message_fMaxtoEMS(Message msg);
    delegate void message_fEMStoP(Message msg);
    

    public partial class AI_sim : Form
    {
        public static DateTime starttime;
        public delegate void DELEGATE();
        public string from { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string state { get; set; }
        public string action { get; set; }
        
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

        }

        // Calling other forms
        Uttam_Transfer_Of_Care.inputform Inputform = new Uttam_Transfer_Of_Care.inputform();
        Hidden_form underlying_form = new Hidden_form();
        AI_Interface aI_Interface = new AI_Interface();


        //button click events
        #region button events        

        private void AI_patient_transfer_button_Click(object sender, EventArgs e)
        {
            starttime = DateTime.Now;
            var inputform_click = new Uttam_Transfer_Of_Care.inputform();
            Inputform.Update();
            inputform_click.Show();

        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }


        private void AI_patient_transfer_button_Click_1(object sender, EventArgs e)
        {
            starttime = DateTime.Now;
            var transfer_form_start = new inputform();
            transfer_form_start.Show();

        }

        private void sim_inthem_box_TextChanged(object sender, EventArgs e)
        {
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            give_drugscon.BackColor = Color.Empty;
            Thread.Sleep(5);
            Treatment("check consciousness");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void apply_torniquet_Click(object sender, EventArgs e)
        {
            apply_torniquet.BackColor = Color.Empty;
            Thread.Sleep(5);
            Treatment("apply torniquet");
        }

        private void sim_gender_label_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
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

        // Creating new patient agent   

        #region Create Patient
        public void patient()
        {
            if (from == "called by main function")
            {
                assignvalue();

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

            else
            {

            }

            #region assigning initial patient attributes randomly
            /*    assigning initial patient attributes randomly 
                Age [0,100]
                Gender [M for Male and F for Female]
                Hemorrhage[0,2] - 0 = No hemorrhage, 1 = hemorrhage present, 2 = High hemorrhage
                Consciousness[0,2] - 0 = Unconscious, 1 = partially concious , 2 = Fully conscious
                Airway[0,1] - 0 = Abnormal/Blocked , 1 = Normal/cleared
                Breathing[0,1] - 0 = Abnormal/Blocked , 1 = Normal/cleared
                Circulation[0,1] - 0 = Abnormal/Blocked , 1 = Normal/cleared 
            */
            #endregion

            void assignvalue()
            {
                Random random = new Random();
                int a;
                age = random.Next(0, 101);
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
                       
            // method to update user interface with most recent patient attributes
            #region update patient attributes
            void UIController()
            {
                if (sim_inthem_box.Text == " ")
                {
                    Thread.Sleep(5);
                    sim_gender_label.Text = gender;
                    sim_age_label.Text = Convert.ToString(age);
                    sim_intair_box.Text = Convert.ToString(airway);                    // changes the final state of the patient at given time in AI_Sim from
                    sim_intbreath_box.Text = Convert.ToString(breathing);
                    sim_intcirc_box.Text = Convert.ToString(circulation);
                    sim_intconc_box.Text = Convert.ToString(consciousness);
                    sim_inthem_box.Text = Convert.ToString(hemorrhage);
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

        } // End of EMS agent


        // AI Agent Start
        SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
        SpeechSynthesizer speech = new SpeechSynthesizer();
        public void AI()
        {
            if(from == "EMS" && to == "AI")
            {
                string act = null;
                if(subject == "Recommend treatment")
                {
                    int hem = hemorrhage; int con = consciousness; int air = airway; int bre = breathing; int cir = circulation;
                    if (hem > 0)
                    {
                        act = "apply torniquet";
                    }
                    else if(con < 2)
                    {
                        act = "check consciousness";
                    }
                    else if (air < 1)
                    {
                        act = "check airway";
                    }
                    else if (cir < 1)
                    {
                        act = "check circulation";
                    }
                    else if (bre < 1)
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

                // after getting the necessary actions
                // send message back
                action = act;
                /*from = "AI"; to = "EMS"; subject = "recommended action"; action = act;
                Thread Th_Ai = new Thread(() => Ems());
                Th_Ai.Start();*/

                void DisplayAction()
                {
                    listBox1.Items.Add($"Display Action (EMS): Thread Running {action}.....");
                    if (action == "apply torniquet")
                    {
                        apply_torniquet.BackColor = Color.GreenYellow;
                    }

                    if (action == "check consciousness")
                    {
                        give_drugscon.BackColor = Color.GreenYellow;
                    }

                    if (action == "check airway")
                    {
                        give_drugsair.BackColor = Color.GreenYellow;
                    }

                    if (action == "check breathing")
                    {
                        give_drugsbre.BackColor = Color.GreenYellow;
                    }

                    if (action == "check circulation")
                    {
                        give_drugscir.BackColor = Color.GreenYellow;
                    }

                } // end of Display Action function
            }
            var Th_speakAI = new Thread(() => speakAI(action));
                Th_speakAI.Start();
            // Speaking function for EMS for which action to take
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

        }

        // variables hemorrhage, circulation....
        #region Variables of patient health
        public int age { get; set; }
        public int hemorrhage { get; set; }
        public int consciousness { get; set; }
        public int airway { get; set; }
        public int breathing { get; set; }
        public int circulation { get; set; }
        public string gender { get; set; }
        public int initial_hemorrhage { get; set; }
        public int initial_consciousness { get; set; }
        public int initial_airway { get; set; }
        public int initial_breathing { get; set; }
        public int initial_circulation { get; set; }

        #endregion 

        public void Treatment(string action)
        {
            // copy the transition probability code from previous means
            /* Write a code for patient treatment. It is same as before. 
               Depending on the action that is passed, apply recommended actions*/

            void Hemorrhagecheck()
            {
                listBox1.Items.Add("Applying Torniquet");
                if (hemorrhage == 0)
                {
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= 85)
                    {
                        hemorrhage = 0;
                    }
                    else if (x <= 95 && x > 85)
                    {
                        hemorrhage = 1;
                    }
                    else
                    {
                        hemorrhage = 2;
                    }


                }
                else if (hemorrhage == 1)
                {
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= 50)
                    {
                        hemorrhage = 0;
                    }
                    else if (x <= 95 && x > 50)
                    {
                        hemorrhage = 1;
                    }
                    else
                    {
                        hemorrhage = 2;
                    }

                }
                else
                {
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= 35)
                    {
                        hemorrhage = 0;
                    }
                    else if (x <= 95 && x > 35)
                    {
                        hemorrhage = 1;
                    }
                    else
                    {
                        hemorrhage = 2;
                    }
                }
                System.Threading.Thread.Sleep(5000);
                sim_finalhem_box.Text = Convert.ToString(hemorrhage);

            } // end of hemorrhage check
            void Consciousnesscheck()
            {
                listBox1.Items.Add("Checking Consciousness");
                if (hemorrhage == 0 && circulation == 1)
                {
                    consciousness = 2;
                }
                if (consciousness == 0)
                {
                    if (hemorrhage == 2 && circulation == 0)
                    {
                        Hemorrhagecheck();
                        Circulationcheck();
                    }
                    else if (hemorrhage == 0 && circulation == 0)
                    {
                        Circulationcheck();
                    }
                    else if (hemorrhage == 0 && circulation == 1)
                    {
                        consciousness = 2;
                    }
                    else
                    {
                        Hemorrhagecheck();
                    }
                }
                else if (consciousness == 1)
                {
                    if (hemorrhage == 0 && circulation == 0)
                    {
                        Circulationcheck();
                        consciousness = 2;
                    }
                    else if (hemorrhage == 1 && circulation == 0)
                    {
                        Hemorrhagecheck();
                        Circulationcheck();
                    }
                    else
                    {
                        Hemorrhagecheck();
                    }


                }
                else
                {
                    consciousness = 2;
                }
                System.Threading.Thread.Sleep(2500);
                sim_finalconc_box.Text = Convert.ToString(consciousness);
            }// end of consciousness check
            void Airwaycheck()
            {
                listBox1.Items.Add("Clearing Airway");
                    if (airway == 0)
                    {
                        int x;
                        Random r = new Random();
                        x = r.Next(0, 101);
                        if (x <= 10)
                        {
                            airway = 0;
                        }
                        else
                        {
                            airway = 1;
                        }
                    }
                    else
                    {
                        airway = 1;
                    }
                    System.Threading.Thread.Sleep(5000);
                    sim_finalair_box.Text = Convert.ToString(airway);

            } // end of airway check
            void Breathingcheck()
            {
                listBox1.Items.Add("Checking Breathing");
                if (breathing == 0)
                {
                    if (airway == 0)
                    {
                        Airwaycheck();
                    }
                    else
                    {
                        int x;
                        Random r = new Random();
                        x = r.Next(0, 101);
                        if (x <= 10)
                        {
                            breathing = 0;
                        }
                        else
                        {
                            breathing = 1;
                        }
                    }
                }
                else
                {
                    breathing = 1;
                }
                System.Threading.Thread.Sleep(2500);
                sim_finalbreath_box.Text = Convert.ToString(breathing);

            }// end of breathing check
            void Circulationcheck()
            {
                listBox1.Items.Add("Checking Circulation");
                if (circulation == 0)
                {
                    if (hemorrhage >= 1 && breathing == 0)
                    {
                        Hemorrhagecheck();
                        Breathingcheck();
                    }
                    else if (hemorrhage >= 1 && breathing == 1)
                    {
                        Hemorrhagecheck();
                    }
                    else if (hemorrhage == 0 && breathing == 0)
                    {
                        Breathingcheck();
                    }
                    else
                    {
                        circulation = 1;
                    }
                }
                else
                {
                    circulation = 1;
                }
                System.Threading.Thread.Sleep(2500);
                sim_finalcirc_box.Text = Convert.ToString(circulation);

            }// end of circulation check

            #region action recommended from AI  checking hemorrhage.....
            // direction for checking hemorrhage from AI
            if (action == "apply torniquet")
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

            from = "EMS"; to = "AI"; subject = "Recommend treatment";
            var Th_EMStoAI = new Thread(() => AI());                       // sending update info of patient to AI for recommendation
            Th_EMStoAI.Start();

        } // end of Treatment method

        private void give_drugshem_Click_1(object sender, EventArgs e)
        {
            give_drugshem.BackColor = Color.Empty;
            Thread.Sleep(5);
            Treatment("give drugs");

        }

        private void give_drugscir_Click_1(object sender, EventArgs e)
        {
            give_drugscir.BackColor = Color.Empty;
            Thread.Sleep(5);
            Treatment("check circulation");
        }

        private void give_drugsair_Click_1(object sender, EventArgs e)
        {
            give_drugsair.BackColor = Color.Empty;
            Thread.Sleep(5);
            Treatment("check airway");
        }

        private void give_drugsbre_Click(object sender, EventArgs e)
        {
            give_drugsbre.BackColor = Color.Empty;
            Thread.Sleep(5);
            Treatment("check breathing");

        }

        private void sim_finalhem_box_TextChanged(object sender, EventArgs e)
        {

        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
}