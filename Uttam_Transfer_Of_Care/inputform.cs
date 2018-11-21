using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;

using System.Drawing.Drawing2D;

namespace Uttam_Transfer_Of_Care
{
    public partial class inputform : Form
    {
        public static bool torso_on = false;
        public static bool head_on = false;
        public static bool ms_on = false;
        public static bool leftleg_on = false;
        public static bool leftlowleg_on = false;
        public static bool rightleg_on = false;
        public static bool rightlowleg_on = false;
        public static bool leftarm_on = false;
        public static bool leftlowarm_on = false;
        public static bool rightarm_on = false;
        public static bool rightlowarm_on = false;
        public static bool backtorso_on = false;
        public static bool backhead_on = false;
        public static bool backms_on = false;
        public static bool backleftleg_on = false;
        public static bool backleftlowleg_on = false;
        public static bool backrightleg_on = false;
        public static bool backrightlowleg_on = false;
        public static bool backleftarm_on = false;
        public static bool backleftlowarm_on = false;
        public static bool backrightarm_on = false;
        public static bool backrightlowarm_on = false;
        public static float GP_scaler = 1.25F;

        //set colors for the torso button areas 

        public static Color onColor = Color.Red;
        public static Color offColor = Color.LightGray;
        #region set initial metric definintions (5 = no input)

        public static int var_breathing = 5;
        public static int var_hemorrage = 5;
        public static int var_consciousness = 5;
        public static int var_airways = 5;
        public static int var_circulation = 5;
        public static int var_medication = 5;
        public static int var_criticality = 5;
        public static int var_age = 5;
        public static int var_gender = 5;
        public static string var_injury_type = "no injury mechanism given";
        #endregion
        #region set metric strings ("no data entered")
        public static string var_breathing_text = "no entry";
        public static string var_hemorrage_text = "no entry";
        public static string var_consciousness_text = "no entry";
        public static string var_airways_text = "no entry";
        public static string var_circulation_text = "no entry";
        public static string var_medication_text = "no entry";
        public static string var_criticality_text = "no entry";
        public static string var_age_text = "no entry";
        public static string var_gender_text = "no entry";
        public static string var_injury_type_text = "no entry";
        public static string var_injury_location_text = "no entry";
        public static DateTime endtime;
        public static int Size_multiplier = 1;
        private ListBox.ObjectCollection items;

        #endregion

        public inputform(ListBox.ObjectCollection objectCollection)
        {
            InitializeComponent();
            this.treatment_timeline_result.Items.AddRange(objectCollection);
        }


        private void Inputform_Load(object sender, EventArgs e)
        {
            //var Screenres = System.Windows.SystemParameters.PrimaryScreenHeight;
            var screenheight = Screen.PrimaryScreen.Bounds.Height;
            var screenwidth = Screen.PrimaryScreen.Bounds.Width;

            if (AI_sim.AI_assist == true)
            {
                // for (int i = 0; i < AI_sim.Treatment_timeline.Items.Count; i++)
                // {
                //     treatment_timeline_result.Items.Add(lb1.Items[i].ToString());
                // }
                
            }
            //treatment_timeline_result.Items.Add(AI_sim.treatment_info_to_transfer);

            #region make shaped buttons  

            #region shape the buttons
                #region 4k standard button shapes
                    Point[] head = {
                    //new Point(20,  150),
                    new Point(20 * Size_multiplier,  250 * Size_multiplier),
                    new Point(36 * Size_multiplier,  234 * Size_multiplier),
                    new Point(32 * Size_multiplier,  172 * Size_multiplier),
                    new Point(22 * Size_multiplier, 170 * Size_multiplier),
                    new Point(8 * Size_multiplier, 140 * Size_multiplier),
                    new Point(8 * Size_multiplier, 122 * Size_multiplier),
                    new Point(18 * Size_multiplier, 118 * Size_multiplier),
                    new Point(14 * Size_multiplier, 64 * Size_multiplier),
                    new Point(24 * Size_multiplier, 42 * Size_multiplier),
                    new Point(42 * Size_multiplier, 22 * Size_multiplier),
                    new Point(90 * Size_multiplier, 12 * Size_multiplier),
                    new Point(138 * Size_multiplier,  22 * Size_multiplier),
                    new Point(156 * Size_multiplier,  42 * Size_multiplier),
                    new Point(166 * Size_multiplier,  64 * Size_multiplier),
                    new Point(162 * Size_multiplier, 118 * Size_multiplier),
                    new Point(172 * Size_multiplier, 122 * Size_multiplier),
                    new Point(172 * Size_multiplier, 140 * Size_multiplier),
                    new Point(158 * Size_multiplier, 170 * Size_multiplier),
                    new Point(148 * Size_multiplier, 172 * Size_multiplier),
                    new Point(144 * Size_multiplier, 234 * Size_multiplier),
                    new Point(160 * Size_multiplier, 250 * Size_multiplier),
                    //new Point(90,  150),
                    };
                    Point[] torso = {
                    new Point(50 * Size_multiplier,  0 * Size_multiplier),
                    new Point(28 * Size_multiplier,  14 * Size_multiplier),
                    new Point(0 * Size_multiplier,  28 * Size_multiplier),
                    new Point(0 * Size_multiplier, 80 * Size_multiplier),
                    new Point(6 * Size_multiplier, 294 * Size_multiplier),
                    new Point(8 * Size_multiplier, 350 * Size_multiplier),
                    new Point(0 * Size_multiplier, 380 * Size_multiplier),
                    new Point(0 * Size_multiplier, 400 * Size_multiplier),
                    new Point(240 * Size_multiplier, 400 * Size_multiplier),
                    new Point(240 * Size_multiplier, 380 * Size_multiplier),
                    new Point(232 * Size_multiplier, 350 * Size_multiplier),
                    new Point(234 * Size_multiplier, 294 * Size_multiplier),
                    new Point(240 * Size_multiplier, 80 * Size_multiplier),
                    new Point(240 * Size_multiplier, 28 * Size_multiplier),
                    new Point(212 * Size_multiplier, 14 * Size_multiplier),
                    new Point(190 * Size_multiplier,0 * Size_multiplier)
                };
                    Point[] midsection = {
                    new Point(0 * Size_multiplier,  0 * Size_multiplier),
                    new Point(2 * Size_multiplier,  18 * Size_multiplier),
                    new Point(6 * Size_multiplier,  40 * Size_multiplier),
                    new Point(50 * Size_multiplier, 50 * Size_multiplier),
                    new Point(70 * Size_multiplier, 60 * Size_multiplier),
                    new Point(110 * Size_multiplier, 80 * Size_multiplier),
                    new Point(130 * Size_multiplier, 80 * Size_multiplier),
                    new Point(170 * Size_multiplier, 60 * Size_multiplier),
                    new Point(190 * Size_multiplier, 50 * Size_multiplier),
                    new Point(234 * Size_multiplier, 40 * Size_multiplier),
                    new Point(238 * Size_multiplier, 18 * Size_multiplier),
                    new Point(240 * Size_multiplier, 0 * Size_multiplier),
                };
                    Point[] rightleg = {
                    new Point(6 * Size_multiplier,  0 * Size_multiplier),
                    new Point(50 * Size_multiplier,  10 * Size_multiplier),
                    new Point(70 * Size_multiplier,  20 * Size_multiplier),
                    new Point(110 * Size_multiplier, 40 * Size_multiplier),
                    new Point(106 * Size_multiplier, 200 * Size_multiplier),
                    new Point(100 * Size_multiplier, 320 * Size_multiplier),
                    new Point(30 * Size_multiplier, 320 * Size_multiplier),
                    new Point(14 * Size_multiplier, 200 * Size_multiplier),
                };
                    Point[] lowleg = {
                    new Point(30 * Size_multiplier,  0 * Size_multiplier),
                    new Point(100 * Size_multiplier,  0 * Size_multiplier),
                    new Point(90 * Size_multiplier,  160 * Size_multiplier),
                    new Point(80 * Size_multiplier, 240 * Size_multiplier),
                    new Point(50 * Size_multiplier, 240 * Size_multiplier),
                    new Point(40 * Size_multiplier, 160 * Size_multiplier),
                };
                    Point[] leftleg = {
                    new Point(104 * Size_multiplier,  0 * Size_multiplier),
                    new Point(60 * Size_multiplier,  10 * Size_multiplier),
                    new Point(40 * Size_multiplier,  20 * Size_multiplier),
                    new Point(0 * Size_multiplier, 40 * Size_multiplier),
                    new Point(4 * Size_multiplier, 200 * Size_multiplier),
                    new Point(10 * Size_multiplier, 320 * Size_multiplier),
                    new Point(80 * Size_multiplier, 320 * Size_multiplier),
                    new Point(96 * Size_multiplier, 200 * Size_multiplier),
                };
                    Point[] leftarm = {
                    new Point(0 * Size_multiplier,  0 * Size_multiplier),
                    new Point(30 * Size_multiplier,  12 * Size_multiplier),
                    new Point(40 * Size_multiplier,  18 * Size_multiplier),
                    new Point(46 * Size_multiplier, 28 * Size_multiplier),
                    new Point(52 * Size_multiplier, 38 * Size_multiplier),
                    new Point(60 * Size_multiplier, 120 * Size_multiplier),
                    new Point(66 * Size_multiplier, 240 * Size_multiplier),
                    new Point(20 * Size_multiplier, 240 * Size_multiplier),
                    new Point(0 * Size_multiplier, 100 * Size_multiplier),
                };
                    Point[] rightarm = {
                    new Point(66 * Size_multiplier,  0 * Size_multiplier),
                    new Point(36 * Size_multiplier,  12 * Size_multiplier),
                    new Point(26 * Size_multiplier,  18 * Size_multiplier),
                    new Point(24 * Size_multiplier, 28 * Size_multiplier),
                    new Point(18 * Size_multiplier, 38 * Size_multiplier),
                    new Point(6 * Size_multiplier, 120 * Size_multiplier),
                    new Point(0 * Size_multiplier, 240 * Size_multiplier),
                    new Point(46 * Size_multiplier, 240 * Size_multiplier),
                    new Point(66 * Size_multiplier, 100 * Size_multiplier),
                };
                    Point[] lowarm = {
                    new Point(0 * Size_multiplier,  0 * Size_multiplier),
                    new Point(46 * Size_multiplier,  0 * Size_multiplier),
                    new Point(40 * Size_multiplier,  120 * Size_multiplier),
                    new Point(4 * Size_multiplier, 120 * Size_multiplier),
                };
                #endregion
                #region  Larger buttons
                    Point[] headbig = {
                    new Point(25 * Size_multiplier,  312 * Size_multiplier),
                    new Point(45 * Size_multiplier,  293 * Size_multiplier),
                    new Point(40 * Size_multiplier,  215 * Size_multiplier),
                    new Point(28 * Size_multiplier, 213 * Size_multiplier),
                    new Point(10 * Size_multiplier, 175 * Size_multiplier),
                    new Point(10 * Size_multiplier, 153 * Size_multiplier),
                    new Point(23 * Size_multiplier, 148 * Size_multiplier),
                    new Point(18 * Size_multiplier, 80 * Size_multiplier),
                    new Point(30 * Size_multiplier, 53 * Size_multiplier),
                    new Point(53 * Size_multiplier, 28 * Size_multiplier),
                    new Point(113 * Size_multiplier, 15 * Size_multiplier),
                    new Point(173 * Size_multiplier,  28 * Size_multiplier),
                    new Point(195 * Size_multiplier,  53 * Size_multiplier),
                    new Point(208 * Size_multiplier,  80 * Size_multiplier),
                    new Point(203 * Size_multiplier, 148 * Size_multiplier),
                    new Point(215 * Size_multiplier, 153 * Size_multiplier),
                    new Point(215 * Size_multiplier, 175 * Size_multiplier),
                    new Point(198 * Size_multiplier, 213 * Size_multiplier),
                    new Point(185 * Size_multiplier, 215 * Size_multiplier),
                    new Point(180 * Size_multiplier, 293 * Size_multiplier),
                    new Point(200 * Size_multiplier, 312 * Size_multiplier),
                    //new Point(90,  150),
                };
                    Point[] torsobig = {
                    new Point(63 * Size_multiplier,  0 * Size_multiplier),
                    new Point(35 * Size_multiplier,  18 * Size_multiplier),
                    new Point(0 * Size_multiplier,  35 * Size_multiplier),
                    new Point(0 * Size_multiplier, 100 * Size_multiplier),
                    new Point(8 * Size_multiplier, 368 * Size_multiplier),
                    new Point(10 * Size_multiplier, 438 * Size_multiplier),
                    new Point(0 * Size_multiplier, 475 * Size_multiplier),
                    new Point(0 * Size_multiplier, 500 * Size_multiplier),
                    new Point(300 * Size_multiplier, 500 * Size_multiplier),
                    new Point(300 * Size_multiplier, 475 * Size_multiplier),
                    new Point(290 * Size_multiplier, 438 * Size_multiplier),
                    new Point(293 * Size_multiplier, 368 * Size_multiplier),
                    new Point(300 * Size_multiplier, 100 * Size_multiplier),
                    new Point(300 * Size_multiplier, 35 * Size_multiplier),
                    new Point(265 * Size_multiplier, 18 * Size_multiplier),
                    new Point(238 * Size_multiplier,0 * Size_multiplier)
                };
                    Point[] midsectionbig = {
                    new Point(0 * Size_multiplier,  0 * Size_multiplier),
                    new Point(3 * Size_multiplier,  23 * Size_multiplier),
                    new Point(8 * Size_multiplier,  50 * Size_multiplier),
                    new Point(63 * Size_multiplier, 62 * Size_multiplier),
                    new Point(88 * Size_multiplier, 75 * Size_multiplier),
                    new Point(138 * Size_multiplier, 100 * Size_multiplier),
                    new Point(163 * Size_multiplier, 100 * Size_multiplier),
                    new Point(213 * Size_multiplier, 75 * Size_multiplier),
                    new Point(238 * Size_multiplier, 62 * Size_multiplier),
                    new Point(293 * Size_multiplier, 50 * Size_multiplier),
                    new Point(298 * Size_multiplier, 23 * Size_multiplier),
                    new Point(300 * Size_multiplier, 0 * Size_multiplier),
                };
                    Point[] rightlegbig = {
                    new Point(8 * Size_multiplier,  0 * Size_multiplier),
                    new Point(63 * Size_multiplier,  13 * Size_multiplier),
                    new Point(88 * Size_multiplier,  25 * Size_multiplier),
                    new Point(138 * Size_multiplier, 50 * Size_multiplier),
                    new Point(133 * Size_multiplier, 250 * Size_multiplier),
                    new Point(125 * Size_multiplier, 400 * Size_multiplier),
                    new Point(38 * Size_multiplier, 400 * Size_multiplier),
                    new Point(18 * Size_multiplier, 250 * Size_multiplier),
                };
                    Point[] lowlegbig = {
                    new Point(38 * Size_multiplier,  0 * Size_multiplier),
                    new Point(125 * Size_multiplier,  0 * Size_multiplier),
                    new Point(113 * Size_multiplier,  200 * Size_multiplier),
                    new Point(100 * Size_multiplier, 300 * Size_multiplier),
                    new Point(53 * Size_multiplier, 300 * Size_multiplier),
                    new Point(50 * Size_multiplier, 200 * Size_multiplier),
                };
                    Point[] leftlegbig = {
                    new Point(130 * Size_multiplier,  0 * Size_multiplier),
                    new Point(75 * Size_multiplier,  13 * Size_multiplier),
                    new Point(50 * Size_multiplier,  25 * Size_multiplier),
                    new Point(0 * Size_multiplier, 50 * Size_multiplier),
                    new Point(5 * Size_multiplier, 250 * Size_multiplier),
                    new Point(13 * Size_multiplier, 400 * Size_multiplier),
                    new Point(100 * Size_multiplier, 400 * Size_multiplier),
                    new Point(120 * Size_multiplier, 250 * Size_multiplier),
                };
                    Point[] leftarmbig = {
                    new Point(0 * Size_multiplier,  0 * Size_multiplier),
                    new Point(38 * Size_multiplier,  15 * Size_multiplier),
                    new Point(50 * Size_multiplier,  23 * Size_multiplier),
                    new Point(57 * Size_multiplier, 35 * Size_multiplier),
                    new Point(65 * Size_multiplier, 47 * Size_multiplier),
                    new Point(75 * Size_multiplier, 150 * Size_multiplier),
                    new Point(82 * Size_multiplier, 300 * Size_multiplier),
                    new Point(25 * Size_multiplier, 300 * Size_multiplier),
                    new Point(0 * Size_multiplier, 125 * Size_multiplier),
                };
                    Point[] rightarmbig = {
                    new Point(82 * Size_multiplier,  0 * Size_multiplier),
                    new Point(45 * Size_multiplier,  15 * Size_multiplier),
                    new Point(33 * Size_multiplier,  23 * Size_multiplier),
                    new Point(30 * Size_multiplier, 35 * Size_multiplier),
                    new Point(23 * Size_multiplier, 47 * Size_multiplier),
                    new Point(8 * Size_multiplier, 150 * Size_multiplier),
                    new Point(0 * Size_multiplier, 300 * Size_multiplier),
                    new Point(58 * Size_multiplier, 300 * Size_multiplier),
                    new Point(82 * Size_multiplier, 125 * Size_multiplier),
                };
                    Point[] lowarmbig = {
                    new Point(0 * Size_multiplier,  0 * Size_multiplier),
                    new Point(58 * Size_multiplier,  0 * Size_multiplier),
                    new Point(50 * Size_multiplier,  150 * Size_multiplier),
                    new Point(5 * Size_multiplier, 150 * Size_multiplier),
                };
            #endregion
                #region HD 1080p buttons
                    Point[] headsmall = {
                            //new Point(20,  150),
                            new Point(10 * Size_multiplier,  125 * Size_multiplier),
                            new Point(18 * Size_multiplier,  117 * Size_multiplier),
                            new Point(16 * Size_multiplier,  86 * Size_multiplier),
                            new Point(11 * Size_multiplier, 85 * Size_multiplier),
                            new Point(4 * Size_multiplier, 70 * Size_multiplier),
                            new Point(4 * Size_multiplier, 61 * Size_multiplier),
                            new Point(9 * Size_multiplier, 59 * Size_multiplier),
                            new Point(7 * Size_multiplier, 32 * Size_multiplier),
                            new Point(12 * Size_multiplier, 21 * Size_multiplier),
                            new Point(21 * Size_multiplier, 11 * Size_multiplier),
                            new Point(45 * Size_multiplier, 6 * Size_multiplier),
                            new Point(69 * Size_multiplier,  11 * Size_multiplier),
                            new Point(78 * Size_multiplier,  21 * Size_multiplier),
                            new Point(83 * Size_multiplier,  32 * Size_multiplier),
                            new Point(81 * Size_multiplier, 59 * Size_multiplier),
                            new Point(86 * Size_multiplier, 61 * Size_multiplier),
                            new Point(86 * Size_multiplier, 70 * Size_multiplier),
                            new Point(79 * Size_multiplier, 85 * Size_multiplier),
                            new Point(74 * Size_multiplier, 86 * Size_multiplier),
                            new Point(72 * Size_multiplier, 117 * Size_multiplier),
                            new Point(80 * Size_multiplier, 125 * Size_multiplier),
                            //new Point(90,  150),
                            }; // made smaller
                    Point[] torsosmall = {
                            new Point(25 * Size_multiplier,  0 * Size_multiplier),
                            new Point(14 * Size_multiplier,  7 * Size_multiplier),
                            new Point(0 * Size_multiplier,  14 * Size_multiplier),
                            new Point(0 * Size_multiplier, 40 * Size_multiplier),
                            new Point(6 * Size_multiplier, 147 * Size_multiplier),
                            new Point(8 * Size_multiplier, 175 * Size_multiplier),
                            new Point(0 * Size_multiplier, 190 * Size_multiplier),
                            new Point(0 * Size_multiplier, 200 * Size_multiplier),
                            new Point(120 * Size_multiplier, 200 * Size_multiplier),
                            new Point(120 * Size_multiplier, 190 * Size_multiplier),
                            new Point(116 * Size_multiplier, 175 * Size_multiplier),
                            new Point(117 * Size_multiplier, 147 * Size_multiplier),
                            new Point(120 * Size_multiplier, 40 * Size_multiplier),
                            new Point(120 * Size_multiplier, 14 * Size_multiplier),
                            new Point(106 * Size_multiplier, 7 * Size_multiplier),
                            new Point(95 * Size_multiplier,0 * Size_multiplier)
                        }; // made smaller
                    Point[] midsectionsmall = {
                            new Point(0 * Size_multiplier,  0 * Size_multiplier),
                            new Point(1 * Size_multiplier,  9 * Size_multiplier),
                            new Point(3 * Size_multiplier,  20 * Size_multiplier),
                            new Point(25 * Size_multiplier, 25 * Size_multiplier),
                            new Point(35 * Size_multiplier, 30 * Size_multiplier),
                            new Point(55 * Size_multiplier, 40 * Size_multiplier),
                            new Point(65 * Size_multiplier, 40 * Size_multiplier),
                            new Point(85 * Size_multiplier, 30 * Size_multiplier),
                            new Point(95 * Size_multiplier, 25 * Size_multiplier),
                            new Point(117 * Size_multiplier, 20 * Size_multiplier),
                            new Point(119 * Size_multiplier, 9 * Size_multiplier),
                            new Point(120 * Size_multiplier, 0 * Size_multiplier),
                        }; // made smaller
                    Point[] rightlegsmall = {
                            new Point(3 * Size_multiplier,  0 * Size_multiplier),
                            new Point(25 * Size_multiplier,  5 * Size_multiplier),
                            new Point(35 * Size_multiplier,  10 * Size_multiplier),
                            new Point(55 * Size_multiplier, 20 * Size_multiplier),
                            new Point(53 * Size_multiplier, 100 * Size_multiplier),
                            new Point(50 * Size_multiplier, 160 * Size_multiplier),
                            new Point(15 * Size_multiplier, 160 * Size_multiplier),
                            new Point(7 * Size_multiplier, 100 * Size_multiplier),
                        };// made smaller
                    Point[] lowlegsmall = {
                            new Point(15 * Size_multiplier,  0 * Size_multiplier),
                            new Point(50 * Size_multiplier,  0 * Size_multiplier),
                            new Point(45 * Size_multiplier,  80 * Size_multiplier),
                            new Point(40 * Size_multiplier, 120 * Size_multiplier),
                            new Point(25 * Size_multiplier, 120 * Size_multiplier),
                            new Point(20 * Size_multiplier, 80 * Size_multiplier),
                        }; // made smaller
                    Point[] leftlegsmall = {
                            new Point(57 * Size_multiplier,  0 * Size_multiplier),
                            new Point(30 * Size_multiplier,  5 * Size_multiplier),
                            new Point(20 * Size_multiplier,  10 * Size_multiplier),
                            new Point(0 * Size_multiplier, 20 * Size_multiplier),
                            new Point(2 * Size_multiplier, 100 * Size_multiplier),
                            new Point(5 * Size_multiplier, 160 * Size_multiplier),
                            new Point(40 * Size_multiplier, 160 * Size_multiplier),
                            new Point(48 * Size_multiplier, 100 * Size_multiplier),
                        }; // made smaller
                    Point[] leftarmsmall = {
                            new Point(0 * Size_multiplier,  0 * Size_multiplier),
                            new Point(15 * Size_multiplier,  6 * Size_multiplier),
                            new Point(20 * Size_multiplier,  9 * Size_multiplier),
                            new Point(23 * Size_multiplier, 14 * Size_multiplier),
                            new Point(26 * Size_multiplier, 19 * Size_multiplier),
                            new Point(30 * Size_multiplier, 60 * Size_multiplier),
                            new Point(33 * Size_multiplier, 120 * Size_multiplier),
                            new Point(10 * Size_multiplier, 120 * Size_multiplier),
                            new Point(0 * Size_multiplier, 50 * Size_multiplier),
                        }; // made smaller
                    Point[] rightarmsmall = {
                            new Point(33 * Size_multiplier,  0 * Size_multiplier),
                            new Point(18 * Size_multiplier,  6 * Size_multiplier),
                            new Point(13 * Size_multiplier,  9 * Size_multiplier),
                            new Point(12 * Size_multiplier, 14 * Size_multiplier),
                            new Point(9 * Size_multiplier, 19 * Size_multiplier),
                            new Point(3 * Size_multiplier, 60 * Size_multiplier),
                            new Point(0 * Size_multiplier, 120 * Size_multiplier),
                            new Point(23 * Size_multiplier, 120 * Size_multiplier),
                            new Point(33 * Size_multiplier, 50 * Size_multiplier),
                        }; // made smaller
                    Point[] lowarmsmall = {
                            new Point(0 * Size_multiplier,  0 * Size_multiplier),
                            new Point(23 * Size_multiplier,  0 * Size_multiplier),
                            new Point(20 * Size_multiplier,  60 * Size_multiplier),
                            new Point(2 * Size_multiplier, 60 * Size_multiplier),
                        };// made smaller

            #endregion
            #endregion

            #region Constrain the buttons to the regions
            if (screenheight > 2159)
            {
                #region large graphics paths
                GraphicsPath polygon_path_head = new GraphicsPath(FillMode.Winding);
                polygon_path_head.AddPolygon(head);

                GraphicsPath polygon_path_torso = new GraphicsPath(FillMode.Winding);
                polygon_path_torso.AddPolygon(torso);

                GraphicsPath polygon_path_ms = new GraphicsPath(FillMode.Winding);
                polygon_path_ms.AddPolygon(midsection);

                GraphicsPath polygon_path_rightleg = new GraphicsPath(FillMode.Winding);
                polygon_path_rightleg.AddPolygon(rightleg);

                GraphicsPath polygon_path_lowleg = new GraphicsPath(FillMode.Winding);
                polygon_path_lowleg.AddPolygon(lowleg);

                GraphicsPath polygon_path_leftleg = new GraphicsPath(FillMode.Winding);
                polygon_path_leftleg.AddPolygon(leftleg);

                GraphicsPath polygon_path_rightarm = new GraphicsPath(FillMode.Winding);
                polygon_path_rightarm.AddPolygon(rightarm);

                GraphicsPath polygon_path_leftarm = new GraphicsPath(FillMode.Winding);
                polygon_path_leftarm.AddPolygon(leftarm);

                GraphicsPath polygon_path_lowarm = new GraphicsPath(FillMode.Winding);
                polygon_path_lowarm.AddPolygon(lowarm);
                #endregion

                #region Convert_paths_to_regions

                Region polygon_region_head = new Region(polygon_path_head); // head
                Region polygon_region_torso = new Region(polygon_path_torso); // torso
                Region polygon_region_ms = new Region(polygon_path_ms); // midsection
                Region polygon_region_rightleg = new Region(polygon_path_rightleg); // rightleg
                Region polygon_region_lowleg = new Region(polygon_path_lowleg); // low leg (all 4)
                Region polygon_region_leftleg = new Region(polygon_path_leftleg); // leftleg
                Region polygon_region_rightarm = new Region(polygon_path_rightarm); // rightarm
                Region polygon_region_leftarm = new Region(polygon_path_leftarm); // left arm
                Region polygon_region_lowarm = new Region(polygon_path_lowarm); // low arm (all 4)


                #endregion

                #region large buttons
                #region front buttons
                button_head.Region = polygon_region_head; //head
                button_torso.Region = polygon_region_torso; //torso
                button_ms.Region = polygon_region_ms; //midsection
                button_rightleg.Region = polygon_region_rightleg; //rightleg
                button_rightlowleg.Region = polygon_region_lowleg; //rightlowleg
                button_leftleg.Region = polygon_region_leftleg; //rightlowleg
                button_leftlowleg.Region = polygon_region_lowleg; //left low leg (uses right low leg region as identical)
                button_leftarm.Region = polygon_region_leftarm; // left arm
                button_rightarm.Region = polygon_region_rightarm; // right arm
                button_leftlowarm.Region = polygon_region_lowarm; // left arm
                button_rightlowarm.Region = polygon_region_lowarm; // right arm
                #endregion
                #region back buttons
                button_backhead.Region = polygon_region_head; //backhead
                button_backtorso.Region = polygon_region_torso; //backtorso
                button_backms.Region = polygon_region_ms; //backms
                button_backleftleg.Region = polygon_region_rightleg; //back right leg
                button_backrightleg.Region = polygon_region_leftleg; //back left leg 
                button_backrightlowleg.Region = polygon_region_lowleg; //back left low leg (uses right low leg region as identical)
                button_backleftlowleg.Region = polygon_region_lowleg; //back left low leg (uses right low leg region as identical)
                button_backleftarm.Region = polygon_region_rightarm; // back left arm 
                button_backrightarm.Region = polygon_region_leftarm; // back right arm
                button_backleftlowarm.Region = polygon_region_lowarm; // back left arm 
                button_backrightlowarm.Region = polygon_region_lowarm; // back right low arm
                #endregion
                #endregion
            }
            else
            {
                #region generate small graphics paths 
                GraphicsPath polygon_path_headsmall = new GraphicsPath(FillMode.Winding);
                polygon_path_headsmall.AddPolygon(headsmall);

                GraphicsPath polygon_path_torsosmall = new GraphicsPath(FillMode.Winding);
                polygon_path_torsosmall.AddPolygon(torsosmall);

                GraphicsPath polygon_path_mssmall = new GraphicsPath(FillMode.Winding);
                polygon_path_mssmall.AddPolygon(midsectionsmall);

                GraphicsPath polygon_path_rightlegsmall = new GraphicsPath(FillMode.Winding);
                polygon_path_rightlegsmall.AddPolygon(rightlegsmall);

                GraphicsPath polygon_path_lowlegsmall = new GraphicsPath(FillMode.Winding);
                polygon_path_lowlegsmall.AddPolygon(lowlegsmall);

                GraphicsPath polygon_path_leftlegsmall = new GraphicsPath(FillMode.Winding);
                polygon_path_leftlegsmall.AddPolygon(leftlegsmall);

                GraphicsPath polygon_path_rightarmsmall = new GraphicsPath(FillMode.Winding);
                polygon_path_rightarmsmall.AddPolygon(rightarmsmall);

                GraphicsPath polygon_path_leftarmsmall = new GraphicsPath(FillMode.Winding);
                polygon_path_leftarmsmall.AddPolygon(leftarmsmall);

                GraphicsPath polygon_path_lowarmsmall = new GraphicsPath(FillMode.Winding);
                polygon_path_lowarmsmall.AddPolygon(lowarm);
                #endregion // make graphics paths 

                #region convert small graphics paths to regions
                Region polygon_region_headsmall = new Region(polygon_path_headsmall); // head
                Region polygon_region_torsosmall = new Region(polygon_path_torsosmall); // torso
                Region polygon_region_mssmall = new Region(polygon_path_mssmall); // midsection
                Region polygon_region_rightlegsmall = new Region(polygon_path_rightlegsmall); // rightleg
                Region polygon_region_lowlegsmall = new Region(polygon_path_lowlegsmall); // low leg (all 4)
                Region polygon_region_leftlegsmall = new Region(polygon_path_leftlegsmall); // leftleg
                Region polygon_region_rightarmsmall = new Region(polygon_path_rightarmsmall); // rightarm
                Region polygon_region_leftarmsmall = new Region(polygon_path_leftarmsmall); // left arm
                Region polygon_region_lowarmsmall = new Region(polygon_path_lowarmsmall); // low arm (all 4)
                #endregion

                #region small buttons
                #region front buttons
                button_head.Region = polygon_region_headsmall; //head
                button_torso.Region = polygon_region_torsosmall; //torso
                button_ms.Region = polygon_region_mssmall; //midsection
                button_rightleg.Region = polygon_region_rightlegsmall; //rightleg
                button_rightlowleg.Region = polygon_region_lowlegsmall; //rightlowleg
                button_leftleg.Region = polygon_region_leftlegsmall; //rightlowleg
                button_leftlowleg.Region = polygon_region_lowlegsmall; //left low leg (uses right low leg region as identical)
                button_leftarm.Region = polygon_region_leftarmsmall; // left arm
                button_rightarm.Region = polygon_region_rightarmsmall; // right arm
                button_leftlowarm.Region = polygon_region_lowarmsmall; // left arm
                button_rightlowarm.Region = polygon_region_lowarmsmall; // right arm
                #endregion

                #region back buttons
                button_backhead.Region = polygon_region_headsmall; //backhead
                button_backtorso.Region = polygon_region_torsosmall; //backtorso
                button_backms.Region = polygon_region_mssmall; //backms
                button_backleftleg.Region = polygon_region_rightlegsmall; //back right leg
                button_backrightleg.Region = polygon_region_leftlegsmall; //back left leg 
                button_backrightlowleg.Region = polygon_region_lowlegsmall; //back left low leg (uses right low leg region as identical)
                button_backleftlowleg.Region = polygon_region_lowlegsmall; //back left low leg (uses right low leg region as identical)
                button_backleftarm.Region = polygon_region_rightarmsmall; // back left arm 
                button_backrightarm.Region = polygon_region_leftarmsmall; // back right arm
                button_backleftlowarm.Region = polygon_region_lowarmsmall; // back left arm 
                button_backrightlowarm.Region = polygon_region_lowarmsmall; // back right low arm
                #endregion
                #endregion
            }
            #endregion

            #region Enlarge buttons to fit 


            ////head
            //button_head.SetBounds(
            //     button_head.Location.X,
            //     button_head.Location.Y,
            //     (200 * Size_multiplier), 100);
            ////torso
            //button_torso.SetBounds(
            //     button_torso.Location.X,
            //     button_torso.Location.Y,
            //     280, 400);
            //// midsection
            //button_ms.SetBounds(
            //     button_ms.Location.X,
            //     button_ms.Location.Y,
            //     300, 130);
            //// rightleg
            //button_rightleg.SetBounds(
            //     button_rightleg.Location.X,
            //     button_rightleg.Location.Y,
            //     116, 340);
            ////right low leg
            //button_rightlowleg.SetBounds(
            //     button_rightlowleg.Location.X,
            //     button_rightlowleg.Location.Y,
            //     100, 260);
            ////left leg
            //button_leftleg.SetBounds(
            //     button_leftleg.Location.X,
            //     button_leftleg.Location.Y,
            //     116, 340);
            ////left low leg
            //button_leftlowleg.SetBounds(
            //     button_leftlowleg.Location.X,
            //     button_leftlowleg.Location.Y,
            //     100, 2600);
            ////left arm
            //button_leftarm.SetBounds(
            //     button_leftarm.Location.X,
            //     button_leftarm.Location.Y,
            //     100, 260);
            ////right arm
            //button_rightarm.SetBounds(
            //     button_rightarm.Location.X,
            //     button_rightarm.Location.Y,
            //     100, 260);
            ////left low arm
            //button_leftlowarm.SetBounds(
            //     button_leftlowarm.Location.X,
            //     button_leftlowarm.Location.Y,
            //     100, 260);
            ////right low arm
            //button_rightlowarm.SetBounds(
            //     button_rightlowarm.Location.X,
            //     button_rightlowarm.Location.Y,
            //     100, 260);

            ////backhead
            //button_backhead.SetBounds(
            //    button_backhead.Location.X,
            //    button_backhead.Location.Y,
            //     200, 250);
            ////backtorso
            //button_backtorso.SetBounds(
            //     button_backtorso.Location.X,
            //     button_backtorso.Location.Y,
            //     280, 400);
            //// back midsection
            //button_backms.SetBounds(
            //     button_backms.Location.X,
            //     button_backms.Location.Y,
            //     300, 130);
            //// rightleg
            //button_backleftleg.SetBounds(
            //     button_backleftleg.Location.X,
            //     button_backleftleg.Location.Y,
            //     116, 340);
            ////back right low leg
            //button_backrightlowleg.SetBounds(
            //     button_backrightlowleg.Location.X,
            //     button_backrightlowleg.Location.Y,
            //     100, 260);
            ////back left leg
            //button_backrightleg.SetBounds(
            //     button_backrightleg.Location.X,
            //     button_backrightleg.Location.Y,
            //     116, 340);
            ////back left low leg
            //button_backleftlowleg.SetBounds(
            //     button_backleftlowleg.Location.X,
            //     button_backleftlowleg.Location.Y,
            //     100, 260);
            ////back left arm
            //button_backleftarm.SetBounds(
            //     button_backleftarm.Location.X,
            //     button_backleftarm.Location.Y,
            //     100, 260);
            ////back right arm
            //button_backrightarm.SetBounds(
            //     button_backrightarm.Location.X,
            //     button_backrightarm.Location.Y,
            //     100, 260);
            ////back left low arm
            //button_backleftlowarm.SetBounds(
            //     button_backleftlowarm.Location.X,
            //     button_backleftlowarm.Location.Y,
            //     100, 260);
            ////back right low arm
            //button_backrightarm.SetBounds(
            //     button_backrightarm.Location.X,
            //     button_backrightarm.Location.Y,
            //     100, 260);
            #endregion

            #endregion
        }   

        #region front button events
        //head
        private void button_head_Click_1(object sender, EventArgs e)
        {
            #region turn off all other regions

            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "front head";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (head_on == true)
            {
                head_on = false;
                button_head.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                head_on = true;
                button_head.BackColor = onColor;
            }
        }
        //torso
        private void button_torso_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "front torso";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (torso_on == true)
            {
                torso_on = false;
                button_torso.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                torso_on = true;
                button_torso.BackColor = onColor;
            }
        }

        private void button_ms_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "front mid-section";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (ms_on == true)
            {
                ms_on = false;
                button_ms.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                ms_on = true;
                button_ms.BackColor = onColor;
            }

        }
        private void button_rightleg_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "front right leg";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (rightleg_on == true)
            {
                rightleg_on = false;
                button_rightleg.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                rightleg_on = true;
                button_rightleg.BackColor = onColor;
            }
        }
        private void button_rightlowleg_Click(object sender, EventArgs e)
        {

            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "front right low leg";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (rightlowleg_on == true)
            {
                rightlowleg_on = false;
                button_rightlowleg.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                rightlowleg_on = true;
                button_rightlowleg.BackColor = onColor;
            }
        }
        private void button_leftleg_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "front left leg";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (leftleg_on == true)
            {
                leftleg_on = false;
                button_leftleg.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                leftleg_on = true;
                button_leftleg.BackColor = onColor;
            }
        }
        private void button_leftlowleg_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "front left low leg";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (leftlowleg_on == true)
            {
                leftlowleg_on = false;
                button_leftlowleg.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                leftlowleg_on = true;
                button_leftlowleg.BackColor = onColor;
            }
        }
        private void button_rightlowarm_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "front right low arm";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (rightlowarm_on == true)
            {
                rightlowarm_on = false;
                button_rightlowarm.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                rightlowarm_on = true;
                button_rightlowarm.BackColor = onColor;
            }
        }
        private void button_rightarm_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "front right arm";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion

            if (rightarm_on == true)
            {
                rightarm_on = false;
                button_rightarm.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                rightarm_on = true;
                button_rightarm.BackColor = onColor;
            }
        }
        private void button_leftarm_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "front left arm";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (leftarm_on == true)
            {
                leftarm_on = false;
                button_leftarm.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                leftarm_on = true;
                button_leftarm.BackColor = onColor;
            }
        }

        private void button_leftlowarm_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "front left low arm";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (leftlowarm_on == true)
            {
                leftlowarm_on = false;
                button_leftlowarm.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                leftlowarm_on = true;
                button_leftlowarm.BackColor = onColor;
            }
        }
        #endregion

        #region back button events
        private void button_backhead_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "back head";
            #region turn off all back regions

            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion

            if (backhead_on == true)
            {
                backhead_on = false;
                button_backhead.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                backhead_on = true;
                button_backhead.BackColor = onColor;

            }
        }

        private void button_backtorso_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "back torso";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (backtorso_on == true)
            {
                backtorso_on = false;
                button_backtorso.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                backtorso_on = true;
                button_backtorso.BackColor = onColor;

            }
        }

        private void button_backms_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "back mid-section";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (backms_on == true)
            {
                backms_on = false;
                button_backms.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                backms_on = true;
                button_backms.BackColor = onColor;

            }
        }

        private void button_backrightleg_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "back right leg";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (backrightleg_on == true)
            {
                backrightleg_on = false;
                button_backrightleg.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                backrightleg_on = true;
                button_backrightleg.BackColor = onColor;
            }
        }

        private void button_backrightlowleg_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "back right low leg";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (backrightlowleg_on == true)
            {
                backrightlowleg_on = false;
                button_backrightlowleg.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                backrightlowleg_on = true;
                button_backrightlowleg.BackColor = onColor;
            }
        }

        private void button_backleftleg_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "back left leg";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (backleftleg_on == true)
            {
                backleftleg_on = false;
                button_backleftleg.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                backleftleg_on = true;
                button_backleftleg.BackColor = onColor;
            }
        }

        private void button_backleftlowleg_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "back left low leg";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (backleftlowleg_on == true)
            {
                backleftlowleg_on = false;
                button_backleftlowleg.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                backleftlowleg_on = true;
                button_backleftlowleg.BackColor = onColor;
            }
        }
        private void button_backleftarm_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "back left arm";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (backleftarm_on == true)
            {
                backleftarm_on = false;
                button_backleftarm.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                backleftarm_on = true;
                button_backleftarm.BackColor = onColor;
            }
        }

        private void button_backleftlowarm_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "back left low arm";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            #endregion
            if (backleftlowarm_on == true)
            {
                backleftlowarm_on = false;
                button_backleftlowarm.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                backleftlowarm_on = true;
                button_backleftlowarm.BackColor = onColor;
            }
        }
        private void button_backrightarm_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "back right arm";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightlowarm_on = false;
            button_backrightlowarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (backrightarm_on == true)
            {
                backrightarm_on = false;
                button_backrightarm.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                backrightarm_on = true;
                button_backrightarm.BackColor = onColor;
            }
        }
        private void button_backrightlowarm_Click(object sender, EventArgs e)
        {
            #region turn off all other regions
            head_on = false;
            button_head.BackColor = offColor;
            torso_on = false;
            button_torso.BackColor = offColor;
            ms_on = false;
            button_ms.BackColor = offColor;
            rightleg_on = false;
            button_rightleg.BackColor = offColor;
            rightlowleg_on = false;
            button_rightlowleg.BackColor = offColor;
            leftleg_on = false;
            button_leftleg.BackColor = offColor;
            leftlowleg_on = false;
            button_leftlowleg.BackColor = offColor;
            rightarm_on = false;
            button_rightarm.BackColor = offColor;
            rightlowarm_on = false;
            button_rightlowarm.BackColor = offColor;
            leftarm_on = false;
            button_leftarm.BackColor = offColor;
            leftlowarm_on = false;
            button_leftlowarm.BackColor = offColor;
            #endregion
            var_injury_location_text = "back right low arm";
            #region turn off all back regions
            backhead_on = false;
            button_backhead.BackColor = offColor;
            backtorso_on = false;
            button_backtorso.BackColor = offColor;
            backms_on = false;
            button_backms.BackColor = offColor;
            backrightleg_on = false;
            button_backrightleg.BackColor = offColor;
            backrightlowleg_on = false;
            button_backrightlowleg.BackColor = offColor;
            backleftleg_on = false;
            button_backleftleg.BackColor = offColor;
            backleftlowleg_on = false;
            button_backleftlowleg.BackColor = offColor;
            backrightarm_on = false;
            button_backrightarm.BackColor = offColor;
            backleftarm_on = false;
            button_backleftarm.BackColor = offColor;
            backleftlowarm_on = false;
            button_backleftlowarm.BackColor = offColor;
            #endregion
            if (backrightlowarm_on == true)
            {
                backrightlowarm_on = false;
                button_backrightlowarm.BackColor = offColor;
                var_injury_location_text = "no entry";
            }
            else
            {
                backrightlowarm_on = true;
                button_backrightlowarm.BackColor = onColor;
            }
        }
        #endregion

        #region Radio Button Events //all radio button events in here - arranged in sub groups
        #region Breathing group events

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void rb_breathingstopped_CheckedChanged(object sender, EventArgs e)
        {
            var_breathing = 2;
            var_breathing_text = rb_breathingstopped.Text;
        }

        private void rb_breathing_good_CheckedChanged(object sender, EventArgs e)
        {
            var_breathing = 0;
            var_breathing_text = rb_breathing_good.Text;
        }

        private void rb_breathing_bad_CheckedChanged(object sender, EventArgs e)
        {
            var_breathing = 1;
            var_breathing_text = rb_breathingstopped.Text;
        }
        private void rb_breathing_restart_CheckedChanged(object sender, EventArgs e)
        {
            var_breathing = 3;
            var_breathing_text = rb_breathing_restart.Text;
        }
        #endregion

        #region Circulation group events
        private void rb_circgood_CheckedChanged(object sender, EventArgs e)
        {
            var_circulation = 0;
            var_circulation_text = rb_circgood.Text;
        }

        private void rb_circrestart_CheckedChanged(object sender, EventArgs e)
        {
            var_circulation = 3;
            var_circulation_text = rb_circrestart.Text;
        }

        private void rb_circbad_CheckedChanged(object sender, EventArgs e)
        {
            var_circulation = 1;
            var_circulation_text = rb_circstopped.Text;
        }

        private void rb_circstopped_CheckedChanged(object sender, EventArgs e)
        {
            var_circulation = 2;
            var_circulation_text = rb_circstopped.Text;
        }
        #endregion

        #region Airway group events

        private void rb_airwaygood_CheckedChanged(object sender, EventArgs e)
        {
            var_airways = 0;
            var_airways_text = rb_airwaygood.Text;
        }

        private void rb_airwaycleared_CheckedChanged(object sender, EventArgs e)
        {
            var_airways = 3;
            var_airways_text = rb_airwaycleared.Text;
        }

        private void rb_airwaybad_CheckedChanged(object sender, EventArgs e)
        {
            var_airways = 1;
            var_airways_text = rb_airwayblocked.Text;
        }

        private void rb_airwayblocked_CheckedChanged(object sender, EventArgs e)
        {
            var_airways = 2;
            var_airways_text = rb_airwayblocked.Text;
        }

        #endregion

        #region Consciousness group events

        private void rb_conscious_CheckedChanged(object sender, EventArgs e)
        {
            var_consciousness = 0;
            var_consciousness_text = rb_conscious.Text;
        }

        private void rb_wasunconscious_CheckedChanged(object sender, EventArgs e)
        {
            var_consciousness = 3;
            var_consciousness_text = rb_wasunconscious.Text;
        }

        private void rb_partconscious_CheckedChanged(object sender, EventArgs e)
        {
            var_consciousness = 1;
            var_consciousness_text = rb_partconscious.Text;
        }

        private void rb_unconscious_CheckedChanged(object sender, EventArgs e)
        {
            var_consciousness = 2;
            var_consciousness_text = rb_unconscious.Text;
        }

        #endregion

        #region haemorrage group events

        private void rb_nobleed_CheckedChanged(object sender, EventArgs e)
        {
            var_hemorrage = 0;
            var_hemorrage_text = rb_nobleed.Text;
        }

        private void rb_bleedingstopped_CheckedChanged(object sender, EventArgs e)
        {
            var_hemorrage = 3;
            var_hemorrage_text = rb_bleedingstopped.Text;
        }

        private void rb_somebleed_CheckedChanged(object sender, EventArgs e)
        {
            var_hemorrage = 1;
            var_hemorrage_text = rb_somebleed.Text;
        }

        private void rb_heavybleed_CheckedChanged(object sender, EventArgs e)
        {
            var_hemorrage = 2;
            var_hemorrage_text = rb_heavybleed.Text;
        }

        #endregion

        #region medication group events

        private void rb_nomeds_CheckedChanged(object sender, EventArgs e)
        {
            var_medication = 0;
            var_medication_text = rb_nomeds.Text;
        }

        private void rb_med_adr_CheckedChanged(object sender, EventArgs e)
        {
            var_medication = 2;
            var_medication_text = rb_med_adr.Text;
        }

        private void rb_med_morphine_CheckedChanged(object sender, EventArgs e)
        {
            var_medication = 3;
            var_medication_text = rb_med_morphine.Text;
        }

        private void rb_med_fluids_CheckedChanged(object sender, EventArgs e)
        {
            var_medication = 1;
            var_medication_text = rb_med_fluids.Text;
        }
        #endregion

        #region Age group events

        private void rb_age_infant_CheckedChanged(object sender, EventArgs e)
        {
            var_age = 0;
            var_age_text = rb_age_infant.Text;
        }

        private void rb_age_adult_CheckedChanged(object sender, EventArgs e)
        {
            var_age = 2;
            var_age_text = rb_age_adult.Text;
        }

        private void rb_age_child_CheckedChanged(object sender, EventArgs e)
        {
            var_age = 1;
            var_age_text = rb_age_child.Text;
        }

        private void rb_age_70plus_CheckedChanged(object sender, EventArgs e)
        {
            var_age = 3;
            var_age_text = rb_age_70plus.Text;
        }

        #endregion

        #region gender group events

        private void rb_gender_male_CheckedChanged(object sender, EventArgs e)
        {
            var_gender = 0;
            var_gender_text = rb_gender_male.Text;
        }

        private void rb_gender_female_CheckedChanged(object sender, EventArgs e)
        {
            var_gender = 1;
            var_gender_text = rb_gender_female.Text;
        }
        #endregion

        #region criticality events
        private void status_routine_CheckedChanged(object sender, EventArgs e)
        {
            var_criticality = 0;
            var_criticality_text = "routine";
        }

        private void status_priority_CheckedChanged(object sender, EventArgs e)
        {
            var_criticality = 1;
            var_criticality_text = "priority";
        }

        private void status_critical_CheckedChanged(object sender, EventArgs e)
        {
            var_criticality = 2;
            var_criticality_text = "critical";
        }
        #endregion

        #endregion

        private void textentry_injury_TextChanged(object sender, EventArgs e)
        {
            var_injury_type = textentry_injury.Text;
            var_injury_type_text = textentry_injury.Text;
        }

        public void Submit_toc_button_Click(object sender, EventArgs e)
        {
            endtime = DateTime.Now;
            var output = new Output();
            output.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}