using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class AI_sim : Form
    {

        public static DateTime starttime;

        // Uttam Model Start
        #region Uttam Sim Model
        //Adding Speech Recogniton Engine
        SpeechRecognitionEngine recognize = new SpeechRecognitionEngine();
        SpeechRecognitionEngine secondbot = new SpeechRecognitionEngine();
        SpeechRecognitionEngine recognize_hemorrhage_command = new SpeechRecognitionEngine();
        SpeechRecognitionEngine recognize_consciousness_command = new SpeechRecognitionEngine();
        SpeechRecognitionEngine recognize_airway_command = new SpeechRecognitionEngine();
        SpeechRecognitionEngine recognize_respiration_command = new SpeechRecognitionEngine();
        SpeechRecognitionEngine recognize_circulation_command = new SpeechRecognitionEngine();


        // *****************************************************
        // link to Uttam Model start conditions for these values 
        // *****************************************************
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
        // public static time Sim_start_time = 0;


        #endregion

        public AI_sim()
        {
            InitializeComponent();
        }

        // Adding Speech dictionary. Speech for which it will be giving responses will be added in dictionary

        private Dictionary<string, string> command = new Dictionary<string, string>();
        Dictionary<string, string> hemorrhagecommand = new Dictionary<string, string>();
        Dictionary<string, string> consciousnesscommand = new Dictionary<string, string>();
        Dictionary<string, string> airwaycommand = new Dictionary<string, string>();
        Dictionary<string, string> respirationcommand = new Dictionary<string, string>();
        Dictionary<string, string> circulationcommand = new Dictionary<string, string>();
        //Mixer Library will mute and unmute the microphone as per recognition engine state
        /*public void Loadspeech1()
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "Ok Max", "Max", "Max Start", "Hey Max", "Start" });
            GrammarBuilder gb1 = new GrammarBuilder();
            gb1.Append(ch);
            Grammar gr = new Grammar(gb1);
            secondbot.LoadGrammarAsync(gr);
            secondbot.SetInputToDefaultAudioDevice();
            secondbot.RecognizeAsync(RecognizeMode.Multiple);
            secondbot.SpeechRecognized += Secondbot_SpeechRecognized;
        }

        private void Secondbot_SpeechRecognized(object s, SpeechRecognizedEventArgs b)
        {
            if (b.Result.Text.Equals("Ok Max"))
            {
                //label1.Text = "You:" + b.Result.Text;
                recognize.RecognizeAsync(RecognizeMode.Multiple);
                secondbot.RecognizeAsyncStop();
            }
            else if (b.Result.Text.Equals("Max"))
            {
                //label1.Text = "You:" + b.Result.Text;
                recognize.RecognizeAsync(RecognizeMode.Multiple);
                secondbot.RecognizeAsyncStop();
            }
            else if (b.Result.Text.Equals("Max Start"))
            {
                //label1.Text = "You:" + b.Result.Text;
                recognize.RecognizeAsync(RecognizeMode.Multiple);
                secondbot.RecognizeAsyncStop();
            }
            else if (b.Result.Text.Equals("Hey Max"))
            {
                //label1.Text = "You:" + b.Result.Text;
                recognize.RecognizeAsync(RecognizeMode.Multiple);
                secondbot.RecognizeAsyncStop();
            }
            else if (b.Result.Text.Equals("Start"))
            {
                //label1.Text = "You:" + b.Result.Text;
                recognize.RecognizeAsync(RecognizeMode.Multiple);
                secondbot.RecognizeAsyncStop();
            }
            SpeechSynthesizer speak1 = new SpeechSynthesizer();
            if (DateTime.Now.Hour < 12)
            {
                speak1.SpeakAsync("Hello! Good Morning Sir. Lets start transfer of care.");
                //label2.Text = "Virtual Assistant:Hello! Good Morning Sir. Lets start transfer of care.";
            }
            else if (DateTime.Now.Hour < 17)
            {
                speak1.SpeakAsync("Hello! Good Afternoon Sir. Let's complete the handover");
                //label2.Text = "Virtual Assistant:Hello! Good Afternoon Sir. Let's complete the handover";
            }
            else
            {
                speak1.SpeakAsync("Hello! Good Evening Sir. Lets start");
                //label2.Text = "Virtual Assistant:Hello! Good Evening Sir. Lets start";
            }

        }*/
        Mixers mmixer = new Mixers();


        public void Loadspeech()
        {
            try
            {
                recognize.LoadGrammar(new DictationGrammar());
                recognize.SetInputToDefaultAudioDevice();
                recognize.RecognizeAsync(RecognizeMode.Multiple);
                recognize.SpeechRecognized += Recognize_SpeechRecognized;

                //The below line doesn't have any impact for the project. If you want recognition to give 
                //random answer to speech those aren't added in dictionary, you can enable it
                /*bot = new Bot();
                bot.loadSettings();

                user = new User("Paramedic", bot);


                bot.isAcceptingUserInput = false;
                bot.loadAIMLFromFiles();
                bot.isAcceptingUserInput = true;*/

                // In command.Add("Say Hello", "greet") below, "greet" is the common command sent to bot for different speech like "Say Hello"
                // and it will reply based o that command
                #region command

                /***************************** Say Hello ****************************/

                command.Add("Say Hello", "greet");
                command.Add("Greet", "greet");
                command.Add("Say hello to sir", "greet");
                command.Add("Say Hello Max", "greet");

                /***************************** Stop AI ****************************/

                command.Add("Get out", "stopai");
                command.Add("Quit", "stopai");
                command.Add("Max keep quiet", "stopai");
                command.Add("Exit", "stopai");

                /***************************** Minimize AI ****************************/

                command.Add("Max Hide", "minimizeai");
                command.Add("Make your window inactive", "minimizeai");
                command.Add("Max minimize", "minimizeai");
                command.Add("minimize", "minimizeai");

                /***************************** Maximize AI ****************************/

                command.Add("Max comeout", "maximizeaic");
                command.Add("comeout Max", "maximizeaic");
                command.Add("Make your window active", "maximizeaia");
                command.Add("Max Where are you?", "maximizeai");
                command.Add("Max maximize", "maximizeaic");
                command.Add("Max Be normal", "maximizeaicn");
                command.Add("Be normal", "maximizeaicn");

                /***************************** Start timer ****************************/

                command.Add("Start timer", "starttimer");
                command.Add("Timer", "starttimer");
                command.Add("Start Stopwatch", "starttimer");
                command.Add("Stopwatch", "starttimer");
                command.Add("Run Stopwatch", "starttimer");
                command.Add("Run Timer", "starttimer");
                command.Add("Search lucky", "searchlucky");

                /***************************** Show AI Form ****************************/

                command.Add("Show AI form", "aiform");
                command.Add("Show form", "aiform");
                command.Add("Final form", "aiform");

                /*****************************Show AI Form ****************************/

                command.Add("Show hidden form", "hiddenform");
                command.Add("hidden form", "hiddenform");

                /***************************** Patient Details ****************************/

                command.Add("Patient details", "patientinfo");
                command.Add("Add patient background", "patientinfo");
                command.Add("Add patient information", "patientinfo");
                command.Add("Patient information", "patientinfo");
                command.Add("Add patient details", "patientinfo");
                command.Add("Input patient info", "patientinfo");
                command.Add("Input patient information", "patientinfo");

                /***************************** Hemorrhage Level ****************************/

                command.Add("Hemorrhage", "hemorrhage");
                command.Add("Input Hemorrhage", "hemorrhage");
                command.Add("Record Hemorrhage", "hemorrhage");
                command.Add("Max Hemorrhage", "hemorrhage");

                /*****************************Consciousness Level ****************************/

                command.Add("Consciousness", "consciousness");
                command.Add("Input consciousness", "consciousness");
                command.Add("Record consciousness", "consciousness");
                command.Add("Max consciousness", "consciousness");

                /*****************************Airway Level *****************************/

                command.Add("Airway", "airway");
                command.Add("Input Airway", "airway");
                command.Add("Record AIrway", "airway");
                command.Add("Max Airway", "airway");

                /*****************************Respiration Level ****************************/

                command.Add("Respiration", "respiration");
                command.Add("Input Respiration", "respiration");
                command.Add("Record Respiration", "respiration");
                command.Add("Max Respiration", "respiration");

                /*****************************Circulation Level ****************************/

                command.Add("Circulation", "circulation");
                command.Add("Input Circulation", "circulation");
                command.Add("Record Circulation", "circulation");
                command.Add("Max Circulation", "circulation");

                /***************************** Check Hemorrhage ****************************/

                command.Add("Check Hemorrhage", "checkhemorrhage");
                command.Add("Treat Hemorrhage", "checkhemorrhage");
                command.Add("Hemorrhage Check", "checkhemorrhage");
                command.Add("Apply tourniquet", "checkhemorrhage");
                command.Add("Give medicine for Hemorrhage", "checkhemorrhage");

                /***************************** Check Consciousness ****************************/

                command.Add("Check Consciousness", "checkconsciousness");
                command.Add("Treat Consciousness", "checkconsciousness");
                command.Add("Consciousness Check", "checkconsciousness");
                command.Add("Give medicine for Consciousness", "checkconsciousness");

                /***************************** Check Airway ****************************/

                command.Add("Check Airway", "checkairway");
                command.Add("Treat Airway", "checkairway");
                command.Add("Airway Check", "checkairway");
                command.Add("Give medicine for Airway", "checkairway");
                command.Add("Clear airway", "checkairway");

                /***************************** Check Respiration ****************************/

                command.Add("Check Respiration", "checkrespiration");
                command.Add("Treat Respiration", "checkrespiration");
                command.Add("Respiration Check", "checkrespiration");
                command.Add("Clear respiration", "checkrespiration");
                command.Add("Give medicine for Respiration", "checkrespiration");

                /***************************** Check Circulation ****************************/

                command.Add("Check Circulation", "checkcirculation");
                command.Add("Treat Circulation", "checkcirculation");
                command.Add("Circulation Check", "checkcirculation");
                command.Add("Check blood flow", "checkcirculation");
                command.Add("Give medicine for Circulation", "checkcirculation");

                /***************************** Know the variable status ****************************/

                /********* All value **********/

                command.Add("Patient Status", "allvalue");
                command.Add("What is the patient status", "allvalue");
                command.Add("What are the variable value", "allvalue");
                command.Add("How is the patient", "allvalue");
                command.Add("Tell me the patient condition", "allvalue");
                command.Add("Tell  me the patient status", "allvalue");
                command.Add("value of patient health", "allvalue");
                command.Add("Patient Condition", "allvalue");
                command.Add("Patient status", "allvalue");

                /********* Hemorrhage value **********/

                command.Add("Hemorrhage value", "hemorrhagevalue");
                command.Add("What is the hemorrhage status", "hemorrhagevalue");
                command.Add("What is the hemorrhage value", "hemorrhagevalue");
                command.Add("How is the hemorrhage", "hemorrhagevalue");
                command.Add("Tell me the hemorrhage level", "hemorrhagevalue");
                command.Add("Tell  me the hemorrhage status", "hemorrhagevalue");
                command.Add("value of hemorrhage", "hemorrhagevalue");
                command.Add("What is the hemorrhage condition", "hemorrhagevalue");
                command.Add("Tell  me the hemorrhage condition", "hemorrhagevalue");
                command.Add("Hemorrhage Condition", "hemorrhagevalue");
                command.Add("Hemorrhage status", "hemorrhagevalue");

                /********* Consciousness value **********/

                command.Add("Consciousness value", "consciousnessvalue");
                command.Add("What is the consciousness status", "consciousnessvalue");
                command.Add("What is the consciousness value", "consciousnessvalue");
                command.Add("How is the consciousness", "consciousnessvalue");
                command.Add("Tell me the consciousness level", "consciousnessvalue");
                command.Add("Tell  me the consciousness status", "consciousnessvalue");
                command.Add("value of consciousness", "consciousnessvalue");
                command.Add("What is the consciousness condition", "consciousnessvalue");
                command.Add("Tell  me the consciousness condition", "consciousnessvalue");
                command.Add("Consciousness Condition", "consciousnessvalue");
                command.Add("Consciousness status", "consciousnessvalue");

                /********* Airway value **********/

                command.Add("Airway value", "airwayvalue");
                command.Add("What is the airway status", "airwayvalue");
                command.Add("What is the airway value", "airwayvalue");
                command.Add("How is the airway", "airwayvalue");
                command.Add("Tell me the airway level", "airwayvalue");
                command.Add("Tell  me the airway status", "airwayvalue");
                command.Add("value of airway", "airwayvalue");
                command.Add("What is the airway condition", "airwayvalue");
                command.Add("Tell  me the airway condition", "airwayvalue");
                command.Add("Airway Condition", "airwayvalue");
                command.Add("Airway status", "airwayvalue");

                /********* Respiration value **********/

                command.Add("Respiration value", "respirationvalue");
                command.Add("What is the respiration status", "respirationvalue");
                command.Add("What is the respiration value", "respirationvalue");
                command.Add("How is the respiration", "respirationvalue");
                command.Add("Tell me the respiration level", "respirationvalue");
                command.Add("Tell  me the respiration status", "respirationvalue");
                command.Add("value of respiration", "respirationvalue");
                command.Add("What is the respiration condition", "respirationvalue");
                command.Add("Tell  me the respiration condition", "respirationvalue");
                command.Add("Respiration Condition", "respirationvalue");
                command.Add("Respiration status", "respirationvalue");

                /********* Circulation value **********/

                command.Add("Circulation value", "circulationvalue");
                command.Add("What is the circulation status", "circulationvalue");
                command.Add("What is the circulation value", "circulationvalue");
                command.Add("How is the circulation", "circulationvalue");
                command.Add("Tell me the circulation level", "circulationvalue");
                command.Add("Tell  me the circulation status", "circulationvalue");
                command.Add("value of circulation", "circulationvalue");
                command.Add("What is the circulation condition", "circulationvalue");
                command.Add("Tell  me the circulation condition", "circulationvalue");
                command.Add("Circulation Condition", "circulationvalue");
                command.Add("Circulation status", "circulationvalue");

                /***************************** Current Time ****************************/

                command.Add("What is the time", "CurrentTime");
                command.Add("What time is it", "CurrentTime");
                command.Add("Tell me the time", "CurrentTime");
                command.Add("Time", "CurrentTime");

                /***************************** Virtual Assistant Name ****************************/
                command.Add("What is your name", "VirtualAssistantName");
                command.Add("Name", "VirtualAssistantName");
                command.Add("Do you have any name", "VirtualAssistantName");
                command.Add("Who are you", "VirtualAssistantName");

                /***************************** Grammar work ****************************/
                string[] cmd = command.Keys.ToArray();
                Choices choices = new Choices(cmd);
                GrammarBuilder gbuilder = new GrammarBuilder();
                gbuilder.Append(choices);
                Grammar grammar = new Grammar(gbuilder);
                grammar.Name = "cmd";
                recognize.LoadGrammar(grammar);

                #endregion
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }

        }
        public int a1;
        public int a2;
        public int a3;
        public int a4;
        public int a5;

        Stopwatch stopwatch = new Stopwatch();
        private void AI_sim_Load(object sender, EventArgs e)
        {
            sim_age_label.Text = var_age;
            sim_gender_label.Text = var_gender;
            sim_inthem_box.Text = var_hem;
            sim_intcirc_box.Text = var_circ;
            sim_intair_box.Text = var_air;
            sim_intbreath_box.Text = var_breath;
            sim_intconc_box.Text = var_conc;
            sim_injury_location_label.Text = var_injury_location;
            sim_injury_type_label.Text = var_injury;

            Loadspeech();

        }
        // Calling other forms

        Uttam_Transfer_Of_Care.inputform Inputform = new Uttam_Transfer_Of_Care.inputform();
        Hidden_form underlying_form = new Hidden_form();
        AI_Interface aI_Interface = new AI_Interface();

        private void Recognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string newcmd;
            SpeechSynthesizer speak = new SpeechSynthesizer();
            speak.Volume = 100;
            speak.Rate = -2;
            string speech = e.Result.Text;
            string answer = string.Empty;

            if (e.Result.Confidence > 0.1f)
            {
                aI_Interface.label1.Text = "You:" + speech;


                switch (e.Result.Grammar.Name)
                {

                    case "cmd":
                        try
                        {
                            /*****************************Hemorrhage * ***************************/

                            if (command[speech] == "hemorrhage")
                            {
                                hem:
                                if (a1 != 5)
                                {
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                                }
                                //newcmd = command[speech];
                                //answer = ProcessCMD(newcmd);
                                Random r2 = new Random();
                                int x2 = r2.Next(0, 5);
                                if (x2 == 0)
                                {
                                    answer = "What is the hemorrhage level?.";
                                }
                                else if (x2 == 2)
                                {
                                    answer = "Okay! Tell me the level.";

                                }
                                else if (x2 == 3)
                                {
                                    answer = "Okay! What Level?";

                                }
                                else if (x2 == 4)
                                {
                                    answer = "Low, Medium or High?";

                                }
                                PromptBuilder build1 = new PromptBuilder();
                                build1.StartSentence();
                                build1.AppendText(answer, PromptEmphasis.Strong);
                                build1.EndSentence();
                                speak.SpeakAsync(build1);
                                aI_Interface.label2.Text = "Virtual Assistant:" + answer;
                                if (aI_Interface.label2.Text == "Virtual Assistant:")
                                {
                                    goto hem;
                                }

                                /*****************************Hemorrhage Value "High" * ***************************/

                                hemorrhagecommand.Add("High", "high");
                                hemorrhagecommand.Add("Level two", "high");
                                hemorrhagecommand.Add("Two", "high");
                                hemorrhagecommand.Add("Excessively bleeding", "high");
                                hemorrhagecommand.Add("High Hemorrhage", "high");
                                hemorrhagecommand.Add("Highly bleeding", "high");
                                hemorrhagecommand.Add("High bleeding", "high");
                                hemorrhagecommand.Add("Normal bleeding", "medium");
                                hemorrhagecommand.Add(" Little bleeding", "medium");
                                hemorrhagecommand.Add("Level one", "medium");
                                hemorrhagecommand.Add("One", "medium");
                                hemorrhagecommand.Add("Level one Hemorrhage", "medium");
                                hemorrhagecommand.Add("Zero", "low");
                                hemorrhagecommand.Add("No Hemorrhage", "low");
                                hemorrhagecommand.Add("None", "low");
                                hemorrhagecommand.Add("Low", "low");

                                string[] cmd = hemorrhagecommand.Keys.ToArray();
                                Choices choices = new Choices(cmd);
                                GrammarBuilder gbuilder = new GrammarBuilder();
                                gbuilder.Append(choices);
                                Grammar grammarh = new Grammar(gbuilder);
                                grammarh.Name = "cmd";
                                recognize_hemorrhage_command.LoadGrammar(grammarh);
                                recognize_hemorrhage_command.SetInputToDefaultAudioDevice();
                                recognize_hemorrhage_command.RecognizeAsync(RecognizeMode.Multiple);
                                recognize_hemorrhage_command.SpeechRecognized += Recognize_hemorrhage_command_SpeechRecognized;
                                if (Convert.ToString(speak.State) == "Speaking")
                                {

                                    while (Convert.ToString(speak.State) != "Ready")
                                    {
                                        a1 = 5;
                                    }
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                                }

                            }
                            /*****************************Consciousness * ***************************/

                            else if (command[speech] == "consciousness")
                            {
                                con:
                                if (a2 != 5)
                                {
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                                }
                                //newcmd = command[speech];
                                //answer = ProcessCMD(newcmd);
                                Random r3 = new Random();
                                int x3 = r3.Next(0, 5);
                                if (x3 == 0)
                                {
                                    answer = "What is the consciousness level?.";
                                }
                                else if (x3 == 2)
                                {
                                    answer = "Okay! Tell me the level.";

                                }
                                else if (x3 == 3)
                                {
                                    answer = "Okay! What Level?";

                                }
                                else if (x3 == 4)
                                {
                                    answer = "Low, Medium or High?";

                                }
                                PromptBuilder build2 = new PromptBuilder();
                                build2.StartSentence();
                                build2.AppendText(answer, PromptEmphasis.Strong);
                                build2.EndSentence();
                                speak.SpeakAsync(build2);
                                aI_Interface.label2.Text = "Virtual Assistant:" + answer;
                                if (aI_Interface.label2.Text == "Virtual Assistant:")
                                {
                                    goto con;
                                }

                                consciousnesscommand.Add("Conscious", "high");
                                consciousnesscommand.Add(" Highly Conscious", "high");
                                consciousnesscommand.Add("Fully Conscious", "high");
                                consciousnesscommand.Add("High consciousness", "high");
                                consciousnesscommand.Add("High", "high");
                                consciousnesscommand.Add("Level two", "high");
                                consciousnesscommand.Add("two", "high");
                                consciousnesscommand.Add("Can respond little", "medium");
                                consciousnesscommand.Add(" Little Conscious", "medium");
                                consciousnesscommand.Add("Level one", "medium");
                                consciousnesscommand.Add("One", "medium");
                                consciousnesscommand.Add("Level one Consciousness", "medium");
                                consciousnesscommand.Add("Unconcious", "low");
                                consciousnesscommand.Add("Not responding", "low");
                                consciousnesscommand.Add("Zero", "low");
                                consciousnesscommand.Add("Low", "low");
                                consciousnesscommand.Add("Can't respond", "low");

                                string[] cmd = consciousnesscommand.Keys.ToArray();
                                Choices choices = new Choices(cmd);
                                GrammarBuilder gbuilder = new GrammarBuilder();
                                gbuilder.Append(choices);
                                Grammar grammarc = new Grammar(gbuilder);
                                grammarc.Name = "cmd";
                                recognize_consciousness_command.LoadGrammar(grammarc);
                                recognize_consciousness_command.SetInputToDefaultAudioDevice();
                                recognize_consciousness_command.RecognizeAsync(RecognizeMode.Multiple);
                                recognize_consciousness_command.SpeechRecognized += Recognize_consciousness_command_SpeechRecognized;
                                if (Convert.ToString(speak.State) == "Speaking")
                                {

                                    while (Convert.ToString(speak.State) != "Ready")
                                    {
                                        a2 = 5;
                                    }
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                                }
                            }
                            /*****************************Airway * ***************************/

                            else if (command[speech] == "airway")
                            {
                                air:
                                if (a3 != 5)
                                {
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                                }
                                //newcmd = command[speech];
                                // answer = ProcessCMD(newcmd);
                                Random r4 = new Random();
                                int x4 = r4.Next(0, 5);
                                if (x4 == 0)
                                {
                                    answer = "What is the airway status?.";
                                }
                                else if (x4 == 2)
                                {
                                    answer = "Okay! Tell me the status.";

                                }
                                else if (x4 == 3)
                                {
                                    answer = "Okay! What Level?";

                                }
                                else if (x4 == 4)
                                {
                                    answer = "normal or Abnormal?";

                                }

                                PromptBuilder build3 = new PromptBuilder();
                                build3.StartSentence();
                                build3.AppendText(answer, PromptEmphasis.Strong);
                                build3.EndSentence();
                                speak.SpeakAsync(build3);
                                aI_Interface.label2.Text = "Virtual Assistant:" + answer;
                                if (aI_Interface.label2.Text == "Virtual Assistant:")
                                {
                                    goto air;
                                }

                                airwaycommand.Add("Normal", "normal");
                                airwaycommand.Add("Breathing fine", "normal");
                                airwaycommand.Add("Abnormal", "abnormal");
                                airwaycommand.Add("Difficulty in breathing", "abnormal");
                                airwaycommand.Add("Good", "normal");
                                airwaycommand.Add("Bad", "abnormal");
                                airwaycommand.Add("One", "normal");
                                airwaycommand.Add("Zero", "abnormal");

                                string[] cmd = airwaycommand.Keys.ToArray();
                                Choices choices = new Choices(cmd);
                                GrammarBuilder gbuilder = new GrammarBuilder();
                                gbuilder.Append(choices);
                                Grammar grammara = new Grammar(gbuilder);
                                grammara.Name = "cmd";
                                recognize_airway_command.LoadGrammar(grammara);
                                recognize_airway_command.SetInputToDefaultAudioDevice();
                                recognize_airway_command.RecognizeAsync(RecognizeMode.Multiple);
                                recognize_airway_command.SpeechRecognized += Recognize_airway_command_SpeechRecognized;
                                if (Convert.ToString(speak.State) == "Speaking")
                                {

                                    while (Convert.ToString(speak.State) != "Ready")
                                    {
                                        a3 = 5;
                                    }
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                                }
                            }

                            /***************************** Respiration ****************************/
                            else if (command[speech] == "respiration")
                            {
                                res:
                                if (a4 != 5)
                                {
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                                }
                                //newcmd = command[speech]; 
                                //answer = ProcessCMD(newcmd);
                                Random r5 = new Random();
                                int x5 = r5.Next(0, 5);
                                if (x5 == 0)
                                {
                                    answer = "What is the respiration Status?.";
                                }
                                else if (x5 == 2)
                                {
                                    answer = "Okay! Tell me the status.";

                                }
                                else if (x5 == 3)
                                {
                                    answer = "Okay! What's the condition?";

                                }
                                else if (x5 == 4)
                                {
                                    answer = "Normal or Abnormal?";

                                }
                                PromptBuilder build4 = new PromptBuilder();
                                build4.StartSentence();
                                build4.AppendText(answer, PromptEmphasis.Strong);
                                build4.EndSentence();
                                speak.SpeakAsync(build4);
                                aI_Interface.label2.Text = "Virtual Assistant:" + answer;
                                if (aI_Interface.label2.Text == "Virtual Assistant:")
                                {
                                    goto res;
                                }

                                respirationcommand.Add("Normal", "normal");
                                respirationcommand.Add("Breathing fine", "normal");
                                respirationcommand.Add("Abnormal", "abnormal");
                                respirationcommand.Add("Difficulty in breathing", "abnormal");
                                respirationcommand.Add("Good", "normal");
                                respirationcommand.Add("Bad", "abnormal");
                                respirationcommand.Add("One", "normal");
                                respirationcommand.Add("Zero", "abnormal");

                                string[] cmd = respirationcommand.Keys.ToArray();
                                Choices choices = new Choices(cmd);
                                GrammarBuilder gbuilder = new GrammarBuilder();
                                gbuilder.Append(choices);
                                Grammar grammarr = new Grammar(gbuilder);
                                grammarr.Name = "cmd";
                                recognize_respiration_command.LoadGrammar(grammarr);
                                recognize_respiration_command.SetInputToDefaultAudioDevice();
                                recognize_respiration_command.RecognizeAsync(RecognizeMode.Multiple);
                                recognize_respiration_command.SpeechRecognized += Recognize_respiration_command_SpeechRecognized;
                                if (Convert.ToString(speak.State) == "Speaking")
                                {

                                    while (Convert.ToString(speak.State) != "Ready")
                                    {
                                        a4 = 5;
                                    }
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                                }
                            }

                            /*****************************Circulation * ***************************/
                            else if (command[speech] == "circulation")
                            {
                                cir:
                                if (a5 != 5)
                                {
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                                }
                                //newcmd = command[speech];
                                //answer = ProcessCMD(newcmd);
                                Random r6 = new Random();
                                int x6 = r6.Next(0, 5);
                                if (x6 == 0)
                                {
                                    answer = "What is the circulation status?.";
                                }
                                else if (x6 == 2)
                                {
                                    answer = "Okay! Tell me the status.";

                                }
                                else if (x6 == 3)
                                {
                                    answer = "Okay! What's the condition?";

                                }
                                else if (x6 == 4)
                                {
                                    answer = "Normal or Abnormal?";

                                }
                                PromptBuilder build5 = new PromptBuilder();
                                build5.StartSentence();
                                build5.AppendText(answer, PromptEmphasis.Strong);
                                build5.EndSentence();
                                speak.SpeakAsync(build5);
                                aI_Interface.label2.Text = "Virtual Assistant:" + answer;
                                if (aI_Interface.label2.Text == "Virtual Assistant:")
                                {
                                    goto cir;
                                }

                                circulationcommand.Add("Normal", "normal");
                                circulationcommand.Add("Fine", "normal");
                                circulationcommand.Add("Abnormal", "abnormal");
                                circulationcommand.Add("Difficulty in circulation", "abnormal");
                                circulationcommand.Add("Good", "normal");
                                circulationcommand.Add("Bad", "abnormal");
                                circulationcommand.Add("One", "normal");
                                circulationcommand.Add("Zero", "abnormal");

                                string[] cmd = circulationcommand.Keys.ToArray();
                                Choices choices = new Choices(cmd);
                                GrammarBuilder gbuilder = new GrammarBuilder();
                                gbuilder.Append(choices);
                                Grammar grammarC = new Grammar(gbuilder);
                                grammarC.Name = "cmd";
                                recognize_circulation_command.LoadGrammar(grammarC);
                                recognize_circulation_command.SetInputToDefaultAudioDevice();
                                recognize_circulation_command.RecognizeAsync(RecognizeMode.Multiple);
                                recognize_circulation_command.SpeechRecognized += Recognize_circulation_command_SpeechRecognized;
                                if (Convert.ToString(speak.State) == "Speaking")
                                {

                                    while (Convert.ToString(speak.State) != "Ready")
                                    {
                                        a5 = 5;
                                    }
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                                }
                            }

                            else if (command[speech] == "checkhemorrhage")
                            {
                                int hemorrhage;
                                mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                                //newcmd = command[speech];
                                //answer = ProcessCMD(newcmd);
                                Random r7 = new Random();
                                int x7 = r7.Next(0, 5);
                                if (x7 == 0)
                                {
                                    answer = "Applying Torniquet";
                                }
                                else if (x7 == 2)
                                {
                                    answer = "Checking Hemorrhage";

                                }
                                else if (x7 == 3)
                                {
                                    answer = "Treating Hemorrhage";

                                }
                                else if (x7 == 4)
                                {
                                    answer = "Giving drugs to a patient";

                                }
                                PromptBuilder build6 = new PromptBuilder();
                                build6.StartSentence();
                                build6.AppendText(answer, PromptEmphasis.Strong);
                                build6.EndSentence();
                                speak.SpeakAsync(build6);
                                aI_Interface.label2.Text = "Virtual Assistant:" + answer;
                                hemorrhage = Hemorrhagecheck();
                                sim_inthem_box.Text = Convert.ToString(hemorrhage);
                                aI_Interface.label2.Text = "Virtual Assistant: Hemorrhage has been treated and updated hemorrhage value is " + hemorrhage;
                                speak.SpeakAsync("Hemorrhage has been treated and updated hemorrhage value is " + hemorrhage);
                                if (Convert.ToString(speak.State) == "Speaking" || Convert.ToString(speak.State) == "Ready")
                                {

                                    while (Convert.ToString(speak.State) != "Ready")
                                    {
                                    }
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                                }
                            }

                            else if (command[speech] == "checkconsciousness")
                            {
                                int consciousness;
                                mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                                //newcmd = command[speech];
                                //answer = ProcessCMD(newcmd);
                                Random r8 = new Random();
                                int x8 = r8.Next(0, 5);
                                if (x8 == 0)
                                {
                                    answer = "Looking patient consciousness status";
                                }
                                else if (x8 == 2)
                                {
                                    answer = "Checking consciousness";

                                }
                                else if (x8 == 3)
                                {
                                    answer = "Treating consciousness";

                                }
                                else if (x8 == 4)
                                {
                                    answer = "Giving drugs to a patient";

                                }
                                PromptBuilder build6 = new PromptBuilder();
                                build6.StartSentence();
                                build6.AppendText(answer, PromptEmphasis.Strong);
                                build6.EndSentence();
                                speak.SpeakAsync(build6);
                                aI_Interface.label2.Text = "Virtual Assistant:" + answer;
                                consciousness = Consciousnesscheck();
                                sim_intconc_box.Text = Convert.ToString(consciousness);
                                aI_Interface.label2.Text = "Virtual Assistant: Consciousness has been treated and updated Consciousness value is " + consciousness;
                                speak.SpeakAsync("Consciousness has been treated and updated Consciousness value is " + consciousness);
                                if (stopwatch.Elapsed.Seconds >= 10)
                                {
                                    value_change();
                                }
                                if (Convert.ToString(speak.State) == "Speaking")
                                {

                                    while (Convert.ToString(speak.State) != "Ready")
                                    {
                                    }
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                                }
                            }

                            else if (command[speech] == "checkairway")
                            {
                                int airway;
                                mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                                //newcmd = command[speech];
                                //answer = ProcessCMD(newcmd);
                                Random r9 = new Random();
                                int x9 = r9.Next(0, 5);
                                if (x9 == 0)
                                {
                                    answer = "Checking airway";
                                }
                                else if (x9 == 2)
                                {
                                    answer = "Clearing airway";

                                }
                                else if (x9 == 3)
                                {
                                    answer = "Treating airway";

                                }
                                else if (x9 == 4)
                                {
                                    answer = "Giving drugs to a patient";

                                }
                                PromptBuilder build6 = new PromptBuilder();
                                build6.StartSentence();
                                build6.AppendText(answer, PromptEmphasis.Strong);
                                build6.EndSentence();
                                speak.SpeakAsync(build6);
                                aI_Interface.label2.Text = "Virtual Assistant:" + answer;
                                airway = Airwaycheck();
                                sim_intair_box.Text = Convert.ToString(airway);
                                aI_Interface.label2.Text = "Virtual Assistant: Airway has been treated and updated Airway value is " + airway;
                                speak.SpeakAsync("Airway has been treated and updated Airway value is " + airway);
                                if (stopwatch.Elapsed.Seconds >= 10)
                                {
                                    value_change();
                                }
                                if (Convert.ToString(speak.State) == "Speaking")
                                {

                                    while (Convert.ToString(speak.State) != "Ready")
                                    {
                                    }
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                                }
                            }

                            else if (command[speech] == "checkrespiration")
                            {
                                int respiration;
                                mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                                //newcmd = command[speech];
                                //answer = ProcessCMD(newcmd);
                                Random r10 = new Random();
                                int x10 = r10.Next(0, 5);
                                if (x10 == 0)
                                {
                                    answer = "Making respiration normal";
                                }
                                else if (x10 == 2)
                                {
                                    answer = "Checking respiration";

                                }
                                else if (x10 == 3)
                                {
                                    answer = "Treating respiration";

                                }
                                else if (x10 == 4)
                                {
                                    answer = "Giving drugs to a patient";

                                }
                                PromptBuilder build6 = new PromptBuilder();
                                build6.StartSentence();
                                build6.AppendText(answer, PromptEmphasis.Strong);
                                build6.EndSentence();
                                speak.SpeakAsync(build6);
                                aI_Interface.label2.Text = "Virtual Assistant:" + answer;
                                respiration = Respirationcheck();
                                sim_intbreath_box.Text = Convert.ToString(respiration);
                                aI_Interface.label2.Text = "Virtual Assistant: Respirationn has been treated and updated respiration value is " + respiration;
                                speak.SpeakAsync("Respiration has been treated and updated respiration value is " + respiration);
                                if (stopwatch.Elapsed.Seconds >= 10)
                                {
                                    value_change();
                                }
                                if (Convert.ToString(speak.State) == "Speaking")
                                {

                                    while (Convert.ToString(speak.State) != "Ready")
                                    {
                                    }
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                                }
                            }

                            else if (command[speech] == "checkcirculation")
                            {
                                int circulation;
                                mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                                //newcmd = command[speech];
                                //answer = ProcessCMD(newcmd);
                                Random r11 = new Random();
                                int x11 = r11.Next(0, 5);
                                if (x11 == 0)
                                {
                                    answer = "Controlling circulation to normal";
                                }
                                else if (x11 == 2)
                                {
                                    answer = "Checking Circulation";

                                }
                                else if (x11 == 3)
                                {
                                    answer = "Treating Circulation";

                                }
                                else if (x11 == 4)
                                {
                                    answer = "Giving drugs to a patient";

                                }
                                PromptBuilder build6 = new PromptBuilder();
                                build6.StartSentence();
                                build6.AppendText(answer, PromptEmphasis.Strong);
                                build6.EndSentence();
                                speak.SpeakAsync(build6);
                                aI_Interface.label2.Text = "Virtual Assistant:" + answer;
                                circulation = Circulationcheck();
                                sim_intcirc_box.Text = Convert.ToString(circulation);
                                aI_Interface.label2.Text = "Virtual Assistant: Circulation has been treated and updated circulation value is " + circulation;
                                speak.SpeakAsync("Circulation has been treated and updated circulation value is " + circulation);
                                if (stopwatch.Elapsed.Seconds >= 10)
                                {
                                    value_change();
                                }
                                if (Convert.ToString(speak.State) == "Speaking")
                                {

                                    while (Convert.ToString(speak.State) != "Ready")
                                    {
                                    }
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                                }
                            }

                            else if (command[speech] == "patientinfo")
                            {
                                speak.SpeakAsync("What is name of the patient?");
                            }

                            else
                            {
                                mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                                newcmd = command[speech];
                                answer = ProcessCMD(newcmd);
                                PromptBuilder build6 = new PromptBuilder();
                                build6.StartSentence();
                                build6.AppendText(answer, PromptEmphasis.Strong);
                                build6.EndSentence();
                                speak.SpeakAsync(build6);
                                aI_Interface.label2.Text = "Virtual Assistant:" + answer;
                                if (Convert.ToString(speak.State) == "Speaking")
                                {

                                    while (Convert.ToString(speak.State) != "Ready")
                                    {
                                    }
                                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                                }
                            }
                            mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;

                        }
                        catch
                        {
                            break;
                        }
                        break;
                    default:
                        answer = GetResponse(speech);
                        break;
                }
            }
        }

        /************************************************************ Recognition Event ***************************************************************/
        private void Recognize_circulation_command_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //recognize_circulation_command.RecognizeAsyncStop();
            int circulation;
            SpeechSynthesizer speakh = new SpeechSynthesizer();
            string speechh = e.Result.Text;
            if (e.Result.Confidence > 0.4f)
            {
                aI_Interface.label1.Text = "You:" + speechh;
                mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                switch (e.Result.Grammar.Name)
                {
                    case "cmd":
                        string newcmd2 = circulationcommand[speechh];
                        if (newcmd2 == "normal")
                        {
                            circulation = 1;
                            speakh.SpeakAsync("Circulation is set to Normal");
                            aI_Interface.label2.Text = "Virtual Assistant: Circulation is set to Normal";
                            sim_intcirc_box.Text = Convert.ToString(circulation);
                            recognize_circulation_command.RecognizeAsyncStop();
                            Inputform.rb_circgood.Checked = true;
                            underlying_form.rb_circgood.Checked = true;
                            treatment(circulation);
                        }
                        else if (newcmd2 == "abnormal")
                        {
                            circulation = 0;
                            speakh.SpeakAsync("Circulation is set to Abnormal");
                            aI_Interface.label2.Text = "Virtual Assistant: Circulation is set to Abnormal";
                            sim_intcirc_box.Text = Convert.ToString(circulation);
                            recognize_circulation_command.RecognizeAsyncStop();
                            Inputform.rb_circstopped.Checked = true;
                            underlying_form.rb_circstopped.Checked = true;
                            treatment(circulation);
                        }
                        break;

                    default:
                        speakh.SpeakAsync("I'm Sorry!Command didn't match. Available commands for circulation are prompted on your screen");
                        label8.Text = ("Available Commands: \nNormal\nfine\nOne\nGood\nBad\nZero\nDifficulty in circulation");
                        break;
                }
                if (Convert.ToString(speakh.State) == "Speaking")
                {

                    while (Convert.ToString(speakh.State) != "Ready")
                    {

                    }
                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                }
            }
            return;
        }

        private void Recognize_respiration_command_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //recognize_respiration_command.RecognizeAsyncStop();
            int respiration;
            SpeechSynthesizer speakh = new SpeechSynthesizer();
            string speechh = e.Result.Text;
            if (e.Result.Confidence > 0.4f)
            {
                aI_Interface.label1.Text = "You:" + speechh;
                mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                switch (e.Result.Grammar.Name)
                {
                    case "cmd":
                        string newcmd2 = respirationcommand[speechh];
                        if (newcmd2 == "normal")
                        {
                            respiration = 1;
                            speakh.SpeakAsync("Respiration is set to Normal");
                            aI_Interface.label2.Text = "Virtual Assistant: Respiration is set to Normal";
                            sim_intbreath_box.Text = Convert.ToString(respiration);
                            recognize_respiration_command.RecognizeAsyncStop();
                            Inputform.rb_breathing_good.Checked = true;
                            underlying_form.rb_breathing_good.Checked = true;
                            treatment(respiration);
                        }
                        else if (newcmd2 == "abnormal")
                        {
                            respiration = 0;
                            speakh.SpeakAsync("Respiration is set to Abnormal");
                            aI_Interface.label2.Text = "Virtual Assistant: Respiration is set to Abnormal";
                            sim_intbreath_box.Text = Convert.ToString(respiration);
                            recognize_respiration_command.RecognizeAsyncStop();
                            Inputform.rb_breathingstopped.Checked = true;
                            underlying_form.rb_breathingstopped.Checked = true;
                            treatment(respiration);
                        }
                        break;

                    default:
                        speakh.SpeakAsync("I'm Sorry!Command didn't match. Available commands for respiration are prompted on your screen");
                        label8.Text = ("Available Commands: \nNormal\nBreathing fine\nOne\nGood\nBad\nZero\nDifficulty in breathing");
                        break;
                }
                if (Convert.ToString(speakh.State) == "Speaking")
                {

                    while (Convert.ToString(speakh.State) != "Ready")
                    {

                    }
                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                }
            }
            return;
        }

        private void Recognize_airway_command_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //recognize_airway_command.RecognizeAsyncStop();
            int airway;
            SpeechSynthesizer speakh = new SpeechSynthesizer();
            string speechh = e.Result.Text;
            if (e.Result.Confidence > 0.4f)
            {
                aI_Interface.label1.Text = "You:" + speechh;
                mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                switch (e.Result.Grammar.Name)
                {
                    case "cmd":
                        string newcmd2 = airwaycommand[speechh];
                        if (newcmd2 == "normal")
                        {
                            airway = 1;
                            speakh.SpeakAsync("Airway is set to Normal");
                            aI_Interface.label2.Text = "Virtual Assistant: Airway is set to Normal";
                            sim_intair_box.Text = Convert.ToString(airway);
                            recognize_airway_command.RecognizeAsyncStop();
                            Inputform.rb_airwaygood.Checked = true;
                            underlying_form.rb_airwaygood.Checked = true;
                            treatment(airway);
                        }
                        else if (newcmd2 == "abnormal")
                        {
                            airway = 0;
                            speakh.SpeakAsync("Airway is set to Abnormal");
                            aI_Interface.label2.Text = "Virtual Assistant: Airway is set to Abnormal";
                            sim_intair_box.Text = Convert.ToString(airway);
                            recognize_airway_command.RecognizeAsyncStop();
                            Inputform.rb_airwayblocked.Checked = true;
                            underlying_form.rb_airwayblocked.Checked = true;
                            treatment(airway);
                        }
                        break;

                    default:
                        speakh.SpeakAsync("I'm Sorry!Command didn't match. Available commands for airway are prompted on your screen");
                        label8.Text = ("Available Commands: \nNormal\nBreathing fine\nOne\nGood\nBad\nZero\nDifficulty in breathing");
                        break;
                }
                if (Convert.ToString(speakh.State) == "Speaking")
                {

                    while (Convert.ToString(speakh.State) != "Ready")
                    {

                    }
                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                }
            }
            return;
        }

        private void Recognize_consciousness_command_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            int consciousness;
            SpeechSynthesizer speakh = new SpeechSynthesizer();
            string speechh = e.Result.Text;
            if (e.Result.Confidence > 0.4f)
            {
                aI_Interface.label1.Text = "You:" + speechh;
                mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                switch (e.Result.Grammar.Name)
                {
                    case "cmd":
                        string newcmd2 = consciousnesscommand[speechh];
                        if (newcmd2 == "high")
                        {
                            consciousness = 2;
                            speakh.SpeakAsync("Consciousness is set to two");
                            aI_Interface.label2.Text = "Virtual Assistant: Consciousness is set to two";
                            sim_intconc_box.Text = Convert.ToString(consciousness);
                            recognize_consciousness_command.RecognizeAsyncStop();
                            Inputform.rb_conscious.Checked = true;
                            underlying_form.rb_conscious.Checked = true;
                            treatment(consciousness);
                        }
                        else if (newcmd2 == "medium")
                        {
                            consciousness = 1;
                            speakh.SpeakAsync("Consciousness is set to one");
                            aI_Interface.label2.Text = "Virtual Assistant: Consciousness is set to one";
                            sim_intconc_box.Text = Convert.ToString(consciousness);
                            recognize_consciousness_command.RecognizeAsyncStop();
                            Inputform.rb_partconscious.Checked = true;
                            underlying_form.rb_partconscious.Checked = true;
                            treatment(consciousness);
                        }
                        else if (newcmd2 == "low")
                        {
                            consciousness = 0;
                            speakh.SpeakAsync("Consciousness is set to zero");
                            aI_Interface.label2.Text = "Virtual Assistant: Consciousness is set to zero";
                            sim_intconc_box.Text = Convert.ToString(consciousness);
                            recognize_consciousness_command.RecognizeAsyncStop();
                            Inputform.rb_unconscious.Checked = true;
                            underlying_form.rb_unconscious.Checked = true;
                            treatment(consciousness);
                        }
                        break;
                    default:
                        speakh.SpeakAsync("I'm Sorry!Command didn't match. Available commands for consciousness are prompted on your screen");
                        label8.Text = ("Available Commands: High\nLevel two\nTwo\nExcessively bleeding\nHigh Hemorrhage\nHighly bleeding\nHigh bleeding\nNormal bleeding\nLittle bleeding\nLevel one\nOne\nHemorrhageZero\nNo Hemorrhage\nNone\nLow");
                        break;
                }
                if (Convert.ToString(speakh.State) == "Speaking")
                {

                    while (Convert.ToString(speakh.State) != "Ready")
                    {

                    }
                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                }
            }
            return;
        }

        private void Recognize_hemorrhage_command_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            int hemorrhage;
            SpeechSynthesizer speakh = new SpeechSynthesizer();
            aI_Interface.label2.Text = "Virtual Assistant:" + Convert.ToString(speakh.State);
            string speechh = e.Result.Text;
            if (e.Result.Confidence > 0.4f)
            {
                aI_Interface.label1.Text = "You:" + speechh;
                mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = true;
                switch (e.Result.Grammar.Name)
                {
                    case "cmd":
                        string newcmd1 = hemorrhagecommand[speechh];
                        int x20;
                        Random r20 = new Random();
                        x20 = r20.Next(0, 100);
                        if (hemorrhagecommand[speechh] == "high")
                        {
                            hemorrhage = 2;
                            if (x20 <= 75)
                            {
                                bloodloss = 1500;
                            }
                            else
                            {
                                bloodloss = 2000;
                            }
                            speakh.SpeakAsync("Hemorrhage is set to two");
                            speakh.SpeakAsync($"{bloodloss}ml blood has already been lost. Treat hemorrhage");
                            aI_Interface.label2.Text = "Virtual Assistant: Hemorrhage is set to two";
                            sim_inthem_box.Text = Convert.ToString(hemorrhage);
                            //label10.Text = $"Blood Loss: {Convert.ToString(bloodloss)} ml";
                            recognize_hemorrhage_command.RecognizeAsyncStop();
                            Inputform.rb_heavybleed.Checked = true;
                            underlying_form.rb_heavybleed.Checked = true;
                            treatment(hemorrhage);
                        }
                        else if (hemorrhagecommand[speechh] == "medium")
                        {
                            hemorrhage = 1;
                            if (x20 <= 75)
                            {
                                bloodloss = 500;
                            }
                            else
                            {
                                bloodloss = 1000;
                            }
                            speakh.SpeakAsync("Hemorrhage is set to one");
                            speakh.SpeakAsync($"{bloodloss} ml blood has already been lost.");
                            aI_Interface.label2.Text = "Virtual Assistant: Hemorrhage is set to one";
                            sim_inthem_box.Text = Convert.ToString(hemorrhage);
                            //label10.Text = $"Blood Loss: {Convert.ToString(bloodloss)} ml";
                            recognize_hemorrhage_command.RecognizeAsyncStop();
                            Inputform.rb_somebleed.Checked = true;
                            underlying_form.rb_somebleed.Checked = true;
                            treatment(hemorrhage);
                        }
                        else if (hemorrhagecommand[speechh] == "low")
                        {
                            hemorrhage = 0;
                            speakh.SpeakAsync("Hemorrhage is set to zero");
                            aI_Interface.label2.Text = "Virtual Assistant: Hemorrhage is set to zero";
                            sim_inthem_box.Text = Convert.ToString(hemorrhage);
                            //label10.Text = $"Blood Loss: -- ";
                            recognize_hemorrhage_command.RecognizeAsyncStop();
                            Inputform.rb_nobleed.Checked = true;
                            underlying_form.rb_nobleed.Checked = true;
                            treatment(hemorrhage);
                        }
                        stopwatch.Start();
                        break;
                    default:
                        speakh.SpeakAsync("I'm Sorry! But the available commands for hemorrhage are prompted on your screen");
                        label8.Text = ("Available Commands: High\nLevel two\nTwo\nExcessively bleeding\nHigh Hemorrhage\nHighly bleeding\nHigh bleeding\nNormal bleeding\nLittle bleeding\nLevel one\nOne\nHemorrhageZero\nNo Hemorrhage\nNone\nLow");
                        break;
                }
                if (Convert.ToString(speakh.State) == "Speaking")
                {

                    while (Convert.ToString(speakh.State) != "Ready")
                    {

                    }
                    mmixer.Recording.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.SRC_MICROPHONE).Mute = false;
                }

            }
            //return;
        }

        /************************************************************ Treatment ***************************************************************/

        public int hemorrhage { get; set; }
        public int consciousness { get; set; }
        public int airway { get; set; }
        public int respiration { get; set; }
        public int circulation { get; set; }
        public int bloodloss { get; set; }

        public void value_change()
        {
            bloodloss = bloodloss + 500;
            label10.Text = $"Blood Loss: {Convert.ToString(bloodloss)} ml";
            SpeechSynthesizer speakh = new SpeechSynthesizer();
            if (bloodloss == 500 || bloodloss == 1000)
            {
                hemorrhage = 1;
                // The below syntax will alert the AI for checking hemorrhage, and so action can be added if needed.
                /*speakh.SpeakAsync("Hemorrhage has changed to 1");
                speakh.SpeakAsync($"{bloodloss}ml blood has already been lost.");*/
                underlying_form.listBox1.Items.Add("Hemorrhage changed to 1");
                underlying_form.listBox1.Items.Add($"{bloodloss} ml blood hass been lost.");
            }
            else if (bloodloss == 1500 || bloodloss == 2000)
            {
                hemorrhage = 2;
                /*speakh.SpeakAsync("Hemorrhage has changed to 2");
                speakh.SpeakAsync($"{bloodloss}ml blood has already been lost. Treat Hemorrhage.");*/
                underlying_form.listBox1.Items.Add("Hemorrhage changed to 2");
                underlying_form.listBox1.Items.Add($"{bloodloss} ml blood hass been lost.");
            }
        }
        int time { get; set; }
        int age { get; set; }
        string name { get; set; }
        string gender { get; set; }
        public int treatment(int a)
        {
            StackTrace stack = new StackTrace();
            if (stack.GetFrame(1).GetMethod().Name == "Recognize_hemorrhage_command_SpeechRecognized")
            {
                hemorrhage = a;
            }
            else if (stack.GetFrame(1).GetMethod().Name == "Recognize_consciousness_command_SpeechRecognized")
            {
                consciousness = a;
            }
            else if (stack.GetFrame(1).GetMethod().Name == "Recognize_airway_command_SpeechRecognized")
            {
                airway = a;
            }
            else if (stack.GetFrame(1).GetMethod().Name == "Recognize_respiration_command_SpeechRecognized")
            {
                respiration = a;
            }
            else if (stack.GetFrame(1).GetMethod().Name == "Recognize_circulation_command_SpeechRecognized")
            {
                circulation = a;
            }
            return 0;
        }
        //It works
        /***************************** The below is the function for checking hemorrhage level *******************************/
        public int Hemorrhagecheck(/*int hem, int con, int air, int res, int cir*/)

        {
            int h = hemorrhage;
            if (hemorrhage == 0)
            {
                int x;
                Random r = new Random();
                x = r.Next(0, 100);
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
                x = r.Next(0, 100);
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
                x = r.Next(0, 100);
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
            var_hem = Convert.ToString(hemorrhage);
            //listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Hemorrhage Check");
            Inputform.listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Hemorrhage Check");
            underlying_form.listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Hemorrhage Check");
            if (h == 1 || h == 2)
            {
                if (hemorrhage == 0)
                {
                    Inputform.rb_bleedingstopped.Checked = true;
                    underlying_form.rb_bleedingstopped.Checked = true;
                }
                else
                {
                    Inputform.radioButton21.Checked = true;
                    underlying_form.radioButton21.Checked = true;
                }
            }
            stopwatch.Reset();
            return hemorrhage;

        }

        /******************************************** The Airway checking **************************************************/

        public int Airwaycheck()
        {
            int a = airway;
            if (airway == 0)
            {
                int x;
                Random r = new Random();
                x = r.Next(0, 100);
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
            var_air = Convert.ToString(airway);
            //listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Airway Check");
            Inputform.listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Airway Check");
            underlying_form.listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Airway Check");
            if (a == 0)
            {
                if (airway == 1)
                {
                    Inputform.rb_airwaycleared.Checked = true;
                    underlying_form.rb_airwaycleared.Checked = true;
                }
                else
                {
                    Inputform.radioButton23.Checked = true;
                    underlying_form.radioButton23.Checked = true;
                }
            }
            return airway;

        }

        /******************************************* The consciousness checking **********************************************/

        public int Consciousnesscheck()
        {
            int c = consciousness;
            if (hemorrhage == 0 && circulation == 1)
            {
                consciousness = 2;
            }
            if (consciousness == 0)
            {
                if (hemorrhage == 2 && circulation == 0)
                {
                    hemorrhage = Hemorrhagecheck();
                    circulation = Circulationcheck();
                }
                else if (hemorrhage == 0 && circulation == 0)
                {
                    circulation = Circulationcheck();
                }
                else if (hemorrhage == 0 && circulation == 1)
                {
                    consciousness = 2;
                }
                else
                {
                    hemorrhage = Hemorrhagecheck();
                }
            }
            else if (consciousness == 1)
            {
                if (hemorrhage == 0 && circulation == 0)
                {
                    circulation = Circulationcheck();
                }
                else if (hemorrhage == 1 && circulation == 0)
                {
                    hemorrhage = Hemorrhagecheck();
                    circulation = Circulationcheck();
                }
                else
                {
                    hemorrhage = Hemorrhagecheck();
                }


            }
            else
            {
                consciousness = 2;
                airway = Airwaycheck();
            }
            System.Threading.Thread.Sleep(2500);
            var_conc = Convert.ToString(consciousness);
            //listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Consciousness Check");
            Inputform.listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Consciousness Check");
            underlying_form.listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Consciousness Check");
            if (c == 0 || c == 1)
            {
                if (consciousness == 2)
                {
                    Inputform.rb_wasunconscious.Checked = true;
                    underlying_form.rb_wasunconscious.Checked = true;
                }
                else
                {
                    Inputform.radioButton22.Checked = true;
                    underlying_form.radioButton22.Checked = true;
                }
            }
            return consciousness;
        }

        /*************************************************** The Respiration checking ***********************************************/

        public int Respirationcheck()
        {
            int R = respiration;
            if (respiration == 0)
            {
                if (airway == 0)
                {
                    airway = Airwaycheck();
                }
                else
                {
                    int x;
                    Random r = new Random();
                    x = r.Next(0, 100);
                    if (x <= 10)
                    {
                        respiration = 0;
                    }
                    else
                    {
                        respiration = 1;
                    }
                }
            }
            else
            {
                respiration = 1;
            }
            System.Threading.Thread.Sleep(2500);
            var_breath = Convert.ToString(respiration);
            //listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Respiration Check");
            Inputform.listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Respiration Check");
            underlying_form.listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Respiration Check");
            if (R == 0)
            {
                if (respiration == 0)
                {
                    Inputform.radioButton24.Checked = true;
                    underlying_form.radioButton24.Checked = true;
                }
                if (respiration == 1)
                {
                    Inputform.rb_breathing_restart.Checked = true;
                    underlying_form.rb_breathing_restart.Checked = true;
                }
            }
            return respiration;
        }

        /************************************************ The circulation checking *****************************************/

        public int Circulationcheck()
        {
            int C = circulation;
            if (circulation == 0)
            {
                if (hemorrhage >= 1 && respiration == 0)
                {
                    hemorrhage = Hemorrhagecheck();
                    respiration = Respirationcheck();
                }
                else if (hemorrhage >= 1 && respiration == 1)
                {
                    hemorrhage = Hemorrhagecheck();
                }
                else if (hemorrhage == 0 && respiration == 0)
                {
                    respiration = Respirationcheck();
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
            label7.Text = Convert.ToString(circulation);
            //listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Circulation Check");
            Inputform.listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Circulation Check");
            underlying_form.listBox1.Items.Add($"{DateTime.Now.TimeOfDay} Circulation Check");
            if (C == 0)
            {
                if (circulation == 0)
                {
                    Inputform.radioButton25.Checked = true;
                    underlying_form.radioButton25.Checked = true;
                }
                else
                {
                    Inputform.rb_circrestart.Checked = true;
                    underlying_form.rb_circrestart.Checked = true;
                }
            }
            return circulation;
        }

        /************************************************ Command Processes *****************************************/
        public string answer;
        private string ProcessCMD(string newcmd)
        {
            //string answer;
            switch (newcmd)
            {
                /***************************** Say hello ****************************/
                case "greet":
                    if (DateTime.Now.Hour < 12)
                    {
                        answer = "Hello! Good Morning Sir. How is it going?";
                    }
                    if (DateTime.Now.Hour < 17)
                    {
                        answer = "Hello! Good Afternoon Sir. How was your morning?";
                    }
                    else
                    {
                        answer = "Hello! Good Evening Sir. How was your day?";
                    }
                    break;



                /***************************** Stop AI ****************************/
                case "stopai":
                    answer = "Good Bye";
                    recognize.RecognizeAsyncStop();
                    secondbot.RecognizeAsync(RecognizeMode.Multiple);
                    break;

                /***************************** Minimize AI ****************************/
                case "minimizeai":
                    answer = "Okay! I am on background";
                    this.WindowState = FormWindowState.Minimized;
                    break;

                /***************************** Maximize AI ****************************/
                case "maximizeaic":
                    answer = "Okay! I am out";
                    this.WindowState = FormWindowState.Maximized;
                    break;
                case "maximizeaia":
                    answer = "I am active";
                    this.WindowState = FormWindowState.Maximized;
                    break;
                case "maximizeai":
                    answer = "I am here. You failed to spot me. I win!";
                    this.WindowState = FormWindowState.Maximized;
                    break;
                case "maximizeain":
                    answer = "I am here and normal";
                    this.WindowState = FormWindowState.Normal;
                    break;

                /***************************** Start Timer ****************************/
                case "starttimer":
                    /*Thread t1 = new Thread(stopwatchthread);
                    t1.IsBackground = true;
                    t1.Start();*/
                    answer = "Starting timer";
                    break;

                /***************************** Show AI form ****************************/
                case "aiform":
                    answer = "Showing AI form";
                    Inputform.Show();
                    break;

                /***************************** Show Final Hidden form ****************************/
                case "hiddenform":
                    answer = "Showing Final form";
                    underlying_form.listBox2.Items.Add($" Hemorrhage: {hemorrhage}");
                    underlying_form.listBox2.Items.Add($"Blood Loss: {bloodloss}");
                    underlying_form.listBox2.Items.Add($"Conciousness: {consciousness}");
                    underlying_form.listBox2.Items.Add($"Airway: {airway}");
                    underlying_form.listBox2.Items.Add($"Respiration: {respiration}");
                    underlying_form.listBox2.Items.Add($"Circulation: {circulation}");
                    underlying_form.Update();
                    underlying_form.Show();
                    break;

                /***************************** Hemorrhage Level ****************************/
                case "hemorrhage":
                    Random r2 = new Random();
                    int x2 = r2.Next(0, 5);
                    if (x2 == 0)
                    {
                        answer = "What is the hemorrhage level?.";
                    }
                    else if (x2 == 2)
                    {
                        answer = "Okay! Tell me the level.";

                    }
                    else if (x2 == 3)
                    {
                        answer = "Okay! What Level?";

                    }
                    else if (x2 == 4)
                    {
                        answer = "Low, Medium or High?";

                    }

                    break;

                /***************************** Consciousness Level ****************************/
                case "consciousness":
                    Random r3 = new Random();
                    int x3 = r3.Next(0, 5);
                    if (x3 == 0)
                    {
                        answer = "What is the consciousness level?.";
                    }
                    else if (x3 == 2)
                    {
                        answer = "Okay! Tell me the level.";

                    }
                    else if (x3 == 3)
                    {
                        answer = "Okay! What Level?";

                    }
                    else if (x3 == 4)
                    {
                        answer = "Low, Medium or High?";

                    }

                    break;

                /***************************** Airway Level ****************************/
                case "airway":
                    Random r4 = new Random();
                    int x4 = r4.Next(0, 5);
                    if (x4 == 0)
                    {
                        answer = "What is the airway status?.";
                    }
                    else if (x4 == 2)
                    {
                        answer = "Okay! Tell me the status.";

                    }
                    else if (x4 == 3)
                    {
                        answer = "Okay! What Level?";

                    }
                    else if (x4 == 4)
                    {
                        answer = "normal or Abnormal?";

                    }

                    break;

                /***************************** Respiration Level ****************************/
                case "respiration":
                    Random r5 = new Random();
                    int x5 = r5.Next(0, 5);
                    if (x5 == 0)
                    {
                        answer = "What is the respiration Status?.";
                    }
                    else if (x5 == 2)
                    {
                        answer = "Okay! Tell me the status.";

                    }
                    else if (x5 == 3)
                    {
                        answer = "Okay! What's the condition?";

                    }
                    else if (x5 == 4)
                    {
                        answer = "Normal or Abnormal?";

                    }

                    break;

                /***************************** Circulation Level ****************************/
                case "circulation":
                    Random r6 = new Random();
                    int x6 = r6.Next(0, 5);
                    if (x6 == 0)
                    {
                        answer = "What is the circulation status?.";
                    }
                    else if (x6 == 2)
                    {
                        answer = "Okay! Tell me the status.";

                    }
                    else if (x6 == 3)
                    {
                        answer = "Okay! What's the condition?";

                    }
                    else if (x6 == 4)
                    {
                        answer = "Normal or Abnormal?";

                    }

                    break;

                /***************************** Checking Hemorrhage update ****************************/
                case "checkhemorrhage":
                    Random r7 = new Random();
                    int x7 = r7.Next(0, 5);
                    if (x7 == 0)
                    {
                        answer = "Applying Torniquet";
                    }
                    else if (x7 == 2)
                    {
                        answer = "Checking Hemorrhage";

                    }
                    else if (x7 == 3)
                    {
                        answer = "Treating Hemorrhage";

                    }
                    else if (x7 == 4)
                    {
                        answer = "Giving drugs to a patient";

                    }
                    break;

                /***************************** Checking Consciousness ****************************/
                case "checkconsciousness":
                    Random r8 = new Random();
                    int x8 = r8.Next(0, 5);
                    if (x8 == 0)
                    {
                        answer = "Looking patient consciousness status";
                    }
                    else if (x8 == 2)
                    {
                        answer = "Checking consciousness";

                    }
                    else if (x8 == 3)
                    {
                        answer = "Treating consciousness";

                    }
                    else if (x8 == 4)
                    {
                        answer = "Giving drugs to a patient";

                    }
                    break;

                /***************************** Checking Airway ****************************/
                case "checkairway":
                    Random r9 = new Random();
                    int x9 = r9.Next(0, 5);
                    if (x9 == 0)
                    {
                        answer = "Checking airway";
                    }
                    else if (x9 == 2)
                    {
                        answer = "Clearing airway";

                    }
                    else if (x9 == 3)
                    {
                        answer = "Treating airway";

                    }
                    else if (x9 == 4)
                    {
                        answer = "Giving drugs to a patient";

                    }
                    break;

                /***************************** Checking Respiration ****************************/
                case "checkrespiration":
                    Random r10 = new Random();
                    int x10 = r10.Next(0, 5);
                    if (x10 == 0)
                    {
                        answer = "Making respiration normal";
                    }
                    else if (x10 == 2)
                    {
                        answer = "Checking respiration";

                    }
                    else if (x10 == 3)
                    {
                        answer = "Treating respiration";

                    }
                    else if (x10 == 4)
                    {
                        answer = "Giving drugs to a patient";

                    }
                    break;

                /***************************** Checking Circulation ****************************/
                case "checkcirculation":
                    Random r11 = new Random();
                    int x11 = r11.Next(0, 5);
                    if (x11 == 0)
                    {
                        answer = "Controlling circulation to normal";
                    }
                    else if (x11 == 2)
                    {
                        answer = "Checking Circulation";

                    }
                    else if (x11 == 3)
                    {
                        answer = "Treating Circulation";

                    }
                    else if (x11 == 4)
                    {
                        answer = "Giving drugs to a patient";

                    }
                    break;

                /***************************** Know the variable value ****************************/

                /********* All variable value **********/

                case "allvalue":
                    Random r17 = new Random();
                    int x17 = r17.Next(0, 5);
                    if (x17 == 0)
                    {
                        answer = $"Patient has hemorrhage {hemorrhage}\n consciousness {consciousness}\n airway {airway}\n respiration {respiration}\n circulation {circulation}";
                    }
                    else if (x17 == 2)
                    {
                        answer = $"Patient health status are: hemorrhage {hemorrhage}\n consciousness {consciousness}\n airway {airway}\n respiration {respiration}\n and circulation {circulation}";

                    }
                    else if (x17 == 3)
                    {
                        answer = $"hemorrhage is at {hemorrhage}\n consciousness {consciousness}\n airway {airway}\n respiration {respiration}\n circulation {circulation}";

                    }
                    else if (x17 == 4)
                    {
                        answer = $"Current patient status is: hemorrhage {hemorrhage}\n consciousness {consciousness}\n airway {airway}\n respiration {respiration}\n and circulation {circulation} ";

                    }
                    break;

                /********* Hemorrhage value **********/

                case "hemorrhagevalue":
                    Random r12 = new Random();
                    int x12 = r12.Next(0, 5);
                    if (x12 == 0)
                    {
                        answer = "Hemorrhage value is " + hemorrhage;
                    }
                    else if (x12 == 2)
                    {
                        answer = $"Patient has hemorrhage level of {hemorrhage} at the moment";

                    }
                    else if (x12 == 3)
                    {
                        answer = "It is " + hemorrhage;

                    }
                    else if (x12 == 4)
                    {
                        answer = "Hemorrhage is at " + hemorrhage;

                    }
                    break;

                /********* Consciousness value **********/

                case "consciousnessvalue":
                    Random r13 = new Random();
                    int x13 = r13.Next(0, 5);
                    if (x13 == 0)
                    {
                        answer = "Consciousness value is " + consciousness;
                    }
                    else if (x13 == 2)
                    {
                        answer = $"Patient has Consciousness level of {consciousness} at the moment";

                    }
                    else if (x13 == 3)
                    {
                        answer = "It is " + consciousness;

                    }
                    else if (x13 == 4)
                    {
                        answer = "Consciousness is at " + consciousness;

                    }
                    break;

                /********* Airway value **********/

                case "airwayvalue":
                    Random r14 = new Random();
                    int x14 = r14.Next(0, 5);
                    if (x14 == 0)
                    {
                        answer = "Airway value is " + airway;
                    }
                    else if (x14 == 2)
                    {
                        answer = $"Patient has airway level of {airway} at the moment";

                    }
                    else if (x14 == 3)
                    {
                        answer = "It is " + airway;

                    }
                    else if (x14 == 4)
                    {
                        answer = "Airway is at " + airway;

                    }
                    break;

                /********* Respiration value **********/

                case "respirationvalue":
                    Random r15 = new Random();
                    int x15 = r15.Next(0, 5);
                    if (x15 == 0)
                    {
                        answer = "Respiration value is " + respiration;
                    }
                    else if (x15 == 2)
                    {
                        answer = $"Patient has respiration level of {respiration} at the moment";

                    }
                    else if (x15 == 3)
                    {
                        answer = "It is " + respiration;

                    }
                    else if (x15 == 4)
                    {
                        answer = "Respiration is at " + respiration;

                    }
                    break;

                /********* Circulation value **********/

                case "circulationvalue":
                    Random r16 = new Random();
                    int x16 = r16.Next(0, 5);
                    if (x16 == 0)
                    {
                        answer = "Circulation value is " + circulation;
                    }
                    else if (x16 == 2)
                    {
                        answer = $"Patient has circulation level of {circulation} at the moment";

                    }
                    else if (x16 == 3)
                    {
                        answer = "It is " + circulation;

                    }
                    else if (x16 == 4)
                    {
                        answer = "Circulation is at " + circulation;

                    }
                    break;

                /***************************** Current Time ****************************/
                case "CurrentTime":
                    answer = DateTime.Now.ToShortTimeString();
                    break;

                /***************************** Virtual Assistant Name ****************************/
                case "VirtualAssistantName":
                    Random r = new Random();
                    int x = r.Next(0, 5);
                    if (x == 0)
                    {
                        answer = "I don't have any name. But you can give me a one.";
                    }
                    else if (x == 2)
                    {
                        answer = "I am virtual. I don't know who am I.";

                    }
                    else if (x == 3)
                    {
                        answer = "Hmmmmmmm. You wanna know my social security too?";

                    }
                    else if (x == 4)
                    {
                        answer = "Seriously?";

                    }
                    break;


            }
            return answer;
        }
        private string GetResponse(string query)
        {
            /*Request request = new Request(query, user, bot);
            Result result = bot.Chat(request);
            return result.Output;*/
            return null;
        }
        #endregion

        //button click events
        #region button events        

        private void AI_patient_transfer_button_Click(object sender, EventArgs e)
        {
            starttime = DateTime.Now;
            var inputform_click = new Uttam_Transfer_Of_Care.inputform();
            Inputform.Update();
            inputform_click.Show();

        }
       

        #endregion

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void AI_patient_transfer_button_Click_1(object sender, EventArgs e)
        {
            starttime = DateTime.Now;
            var transfer_form_start = new inputform();
            transfer_form_start.Show();

        }
    }
}