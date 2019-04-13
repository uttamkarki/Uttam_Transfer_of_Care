using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Extended;
using Plasmoid.Extensions;
using System.Drawing.Drawing2D;


namespace Uttam_Transfer_Of_Care
{

    using System.Runtime.InteropServices;

    public partial class Message_form : Form
    {

        public Message_form()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 200, 200));
            
        }

        //private void draw_edge(object sender, PaintEventArgs e)
        //{
        //    System.Drawing.Graphics g = this.CreateGraphics();
        //    g.SmoothingMode = SmoothingMode.AntiAlias;

        //    //g.FillRoundedRectangle(new SolidBrush(Color.FromName("AliceBlue")), 700, 150, 1600, 300, 50);
        //    g.DrawRoundedRectangle(new Pen(Color.FromName("turquoise"), 10f), 100, 110, 100, 200, 20);
        //    float xpos = this.Location.X;
        //    float ypos = this.Location.Y;
        //    float frm_width = this.Width - 10;
        //    float frm_height = this.Height - 10;
        //}

        private void Message_form_Load(object sender, EventArgs e)
        {

            Treatment_label_box.Text = AI_sim.MessageLabel;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            draw_edge();




        }

        async void draw_edge()
        {
            

            float xpos = this.Location.X-10;
            float ypos = this.Location.Y-10;
            float frm_width = this.Width;
            float frm_height = this.Height;

            System.Drawing.Graphics g = this.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            await Task.Delay(50);

            g.FillRoundedRectangle(new SolidBrush(Color.FromName("turquoise")), xpos, ypos, frm_width, frm_height, 100);
            g.DrawRoundedRectangle(new Pen(Color.FromName("turquoise"), 30f), 0, 0, frm_width, frm_height, 100);
            await Task.Delay(50);

            g.FillRoundedRectangle(new SolidBrush(Color.FromName("turquoise")), xpos, ypos, frm_width, frm_height, 100);
            g.DrawRoundedRectangle(new Pen(Color.FromName("turquoise"), 30f), 0, 0, frm_width, frm_height, 100);

        }


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        private void Treatment_label_box_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }

}