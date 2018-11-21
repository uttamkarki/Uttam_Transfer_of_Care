using System;
using System.Diagnostics;
using System.Drawing;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


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
        public static int breath1_count = 0;
        public static int breath2_count = 0;
        public static int circ1_count = 0;
        public static int circ2_count = 0;

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
            #endregion

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
                    int hem1to0_cure_probability = 90;
                    int hem1to1_cure_probability = 10;
                    int hem1_cure_cumprob = hem1to1_cure_probability + hem1to0_cure_probability;
                    
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= hem1to0_cure_probability)
                    {
                        hemorrhage = 0;
                        treatment_timeline.Items.Add("Bleeding stopped");
                    }
                    else  // if (x <= hem1_cure_cumprob && x > hem1to0_cure_probability) - commented out but correct if other else included
                    {
                        hemorrhage = 1;
                    }
                    //else
                    //{
                    //    hemorrhage = 2;
                    //}
                }
                else // ...if hemorrhage = 2
                {
                    treatment_timeline.Items.Add("heavy bleeding");
                    int hem2to0_cure_probability = 30;
                    int hem2to1_cure_probability = 60;
                    int hem2to2_cure_probability = 10;
                    int hem2to1_cumprob = hem2to1_cure_probability + hem2to0_cure_probability;
                    int hem2_cure_cumprob = hem2to1_cure_probability + hem2to0_cure_probability + hem2to2_cure_probability;
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= hem2to0_cure_probability)
                    {
                        hemorrhage = 0;
                        treatment_timeline.Items.Add("Bleeding stopped");
                    }
                    else if (x <= hem2to1_cumprob && x > hem2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("Bleeding partially stopped");
                        hemorrhage = 1;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("Bleeding not stopped");
                        hemorrhage = 2;
                    }
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

            } // end of hemorrhage check
            #endregion
            #region consciousness check
            async void Consciousnesscheck()
            {
                treatment_timeline.Items.Add("Checking Consciousness");
                if (consciousness == 0)
                {
                    treatment_timeline.Items.Add("Patient fully Conscious");
                        consciousness = 0;
                }
                else if (consciousness == 1)
                {
                    treatment_timeline.Items.Add("Patient Partially Conscious");
                    int conc1to0_cure_probability = 60;
                    int conc1to1_cure_probability = 40;
                    int conc1_cure_cumprob = conc1to1_cure_probability + conc1to0_cure_probability;
                    int x;

                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= conc1to0_cure_probability)
                    {
                        consciousness = 0;
                        treatment_timeline.Items.Add("Full consciousness restored");
                    }
                    else  
                    {
                        consciousness = 1;
                        treatment_timeline.Items.Add("Patient Still not fully conscious");
                    }
                }
                else // ...if consciousness = 2
                {
                    treatment_timeline.Items.Add("Patient unconscious");
                    int conc2to0_cure_probability = 10;
                    int conc2to1_cure_probability = 50;
                    int conc2to2_cure_probability = 40;
                    int conc2to1_cumprob = conc2to1_cure_probability + conc2to0_cure_probability;
                    int conc2_cure_cumprob = conc2to1_cure_probability + conc2to0_cure_probability + conc2to2_cure_probability;
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= conc2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("Full consciousness restored");
                        consciousness = 0;
                    }
                    else if (x <= conc2to1_cumprob && x > conc2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("Patient partially responsive");
                        consciousness = 1;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("Patient still unconscious");
                        consciousness = 2;
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

                // do we need the thread to sleep if displaying message?  maybe lock the app? 
                System.Threading.Thread.Sleep(2500);
                sim_finalconc_box.Text = Convert.ToString(consciousness);

                //this section all commented out for now - move to patient section
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
                            airway = 0;
                            treatment_timeline.Items.Add("Airway cleared");    
                        }
                        else
                        {
                            airway = 1;
                            treatment_timeline.Items.Add("Airway still partially obstructed");
                        }
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
                        airway = 0;
                    }
                    else if (x <= air2to1_cumprob && x > air2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("Airway partially cleared");
                        airway = 1;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("Airway still blocked");
                        airway = 2;
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

                System.Threading.Thread.Sleep(5000);
                sim_finalair_box.Text = Convert.ToString(airway);

            } // end of airway check
            #endregion
            #region Breathing check
            async void Breathingcheck()
            {
                treatment_timeline.Items.Add("Checking breathing");
                if (breathing == 0)
                {
                    treatment_timeline.Items.Add("Patient breathing normally");
                    breathing = 0;
                }
                else if (breathing == 1)
                {
                    treatment_timeline.Items.Add("Patient breathing is not normal");
                    int breath1to0_cure_probability = 20;
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= breath1to0_cure_probability)
                    {
                        breathing = 0;
                        treatment_timeline.Items.Add("Breathing restored");
                    }
                    else
                    {
                        breathing = 1;
                        treatment_timeline.Items.Add("breathing remains erratic");
                    }
                }
                else // ...if breathing = 2
                {
                    treatment_timeline.Items.Add("Patient not breathing");
                    int breath2to0_cure_probability = 10;
                    int breath2to1_cure_probability = 60;
                    int breath2to2_cure_probability = 30;
                    int breath2to1_cumprob = breath2to1_cure_probability + breath2to0_cure_probability;
                    int breath2_cure_cumprob = breath2to1_cure_probability + breath2to0_cure_probability + breath2to2_cure_probability;
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= breath2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("Breathing restored to normal");
                        breathing = 0;
                    }
                    else if (x <= breath2to1_cumprob && x > breath2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("breathing restored but still erratic");
                        breathing = 1;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("Patient still not breathing");
                        breathing = 2;
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

            }// end of breathing check
            #endregion
            #region circulation check
            async void Circulationcheck()
            {
                #region  I think this relates to how patient deteriorates given multiple symptoms - move to patient agent
                treatment_timeline.Items.Add("Checking Circulation");
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
                #endregion

                treatment_timeline.Items.Add("Checking for pulse");
                if (circulation == 0)
                {
                    treatment_timeline.Items.Add("pulse is normal");
                    circulation = 0;
                }
                else if (circulation == 1)
                {
                    treatment_timeline.Items.Add("Pulse is elevated/weak/erratic");
                    int circ1to0_cure_probability = 70;
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= circ1to0_cure_probability)
                    {
                        circulation = 0;
                        treatment_timeline.Items.Add("pulse returned to normal");
                    }
                    else
                    {
                        circulation = 1;
                        treatment_timeline.Items.Add("Pulse still elevated/weak/erratic");
                    }
                }
                else // ...if circulation = 2
                {
                    treatment_timeline.Items.Add("Patient Heart in Cardiac Arrest");
                    int circ2to0_cure_probability = 10;
                    int circ2to1_cure_probability = 10;
                    int circ2to2_cure_probability = 80;
                    int circ2to1_cumprob = circ2to1_cure_probability + circ2to0_cure_probability;
                    int circ2_cure_cumprob = circ2to1_cure_probability + circ2to0_cure_probability + circ2to2_cure_probability;
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 101);
                    if (x <= circ2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("Pulse returned to normal");
                        circulation = 0;
                    }
                    else if (x <= circ2to1_cumprob && x > circ2to0_cure_probability)
                    {
                        treatment_timeline.Items.Add("Heart function restored but not normal");
                        circulation = 1;
                    }
                    else
                    {
                        treatment_timeline.Items.Add("Patient Still in Cardiac Arrest");
                        circulation = 2;
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
                sim_finalcirc_box.Text = Convert.ToString(circulation);

            }// end of circulation check
            #endregion

            from = "EMS"; to = "AI"; subject = "Recommend treatment";
            var Th_EMStoAI = new Thread(() => AI());                       // sending update info of patient to AI for recommendation
            Th_EMStoAI.Start();
        } // end of Treatment method

        #endregion

        // Dump old event handlers in here until I can work out how to clear them!!!
        #region unused button click events
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
        #endregion

    }
}