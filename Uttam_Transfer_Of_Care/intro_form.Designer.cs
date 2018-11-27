namespace Uttam_Transfer_Of_Care
{
    partial class intro_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(intro_form));
            this.aisimstartbutton = new System.Windows.Forms.Button();
            this.standardsimstartbutton = new System.Windows.Forms.Button();
            this.Experience_entry = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Role_entry = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Intro_label = new System.Windows.Forms.Label();
            this.Participant_ID_box = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // aisimstartbutton
            // 
            this.aisimstartbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aisimstartbutton.Location = new System.Drawing.Point(770, 671);
            this.aisimstartbutton.Name = "aisimstartbutton";
            this.aisimstartbutton.Size = new System.Drawing.Size(640, 112);
            this.aisimstartbutton.TabIndex = 15;
            this.aisimstartbutton.Text = "Start AI Sim";
            this.aisimstartbutton.UseVisualStyleBackColor = true;
            this.aisimstartbutton.Click += new System.EventHandler(this.aisimstartbutton_Click_1);
            // 
            // standardsimstartbutton
            // 
            this.standardsimstartbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.standardsimstartbutton.Location = new System.Drawing.Point(52, 671);
            this.standardsimstartbutton.Name = "standardsimstartbutton";
            this.standardsimstartbutton.Size = new System.Drawing.Size(640, 112);
            this.standardsimstartbutton.TabIndex = 14;
            this.standardsimstartbutton.Text = "Start Standard Sim";
            this.standardsimstartbutton.UseVisualStyleBackColor = true;
            this.standardsimstartbutton.Click += new System.EventHandler(this.standardsimstartbutton_Click_1);
            // 
            // Experience_entry
            // 
            this.Experience_entry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Experience_entry.Location = new System.Drawing.Point(246, 509);
            this.Experience_entry.Name = "Experience_entry";
            this.Experience_entry.Size = new System.Drawing.Size(600, 38);
            this.Experience_entry.TabIndex = 13;
            this.Experience_entry.TextChanged += new System.EventHandler(this.Experience_entry_TextChanged_1);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(46, 508);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 41);
            this.label4.TabIndex = 12;
            this.label4.Text = "Experience";
            // 
            // Role_entry
            // 
            this.Role_entry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Role_entry.Location = new System.Drawing.Point(246, 455);
            this.Role_entry.Name = "Role_entry";
            this.Role_entry.Size = new System.Drawing.Size(600, 38);
            this.Role_entry.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(46, 458);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 41);
            this.label3.TabIndex = 10;
            this.label3.Text = "Role";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(482, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(495, 44);
            this.label2.TabIndex = 9;
            this.label2.Text = "Transfer of Care Simulator";
            // 
            // Intro_label
            // 
            this.Intro_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Intro_label.Location = new System.Drawing.Point(33, 81);
            this.Intro_label.Name = "Intro_label";
            this.Intro_label.Size = new System.Drawing.Size(1400, 350);
            this.Intro_label.TabIndex = 8;
            this.Intro_label.Text = resources.GetString("Intro_label.Text");
            this.Intro_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Participant_ID_box
            // 
            this.Participant_ID_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Participant_ID_box.Location = new System.Drawing.Point(246, 567);
            this.Participant_ID_box.Name = "Participant_ID_box";
            this.Participant_ID_box.Size = new System.Drawing.Size(600, 38);
            this.Participant_ID_box.TabIndex = 17;
            this.Participant_ID_box.TextChanged += new System.EventHandler(this.Participant_ID_box_TextChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 566);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 41);
            this.label1.TabIndex = 16;
            this.label1.Text = "ID";
            // 
            // intro_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1479, 795);
            this.Controls.Add(this.Participant_ID_box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.aisimstartbutton);
            this.Controls.Add(this.standardsimstartbutton);
            this.Controls.Add(this.Experience_entry);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Role_entry);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Intro_label);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "intro_form";
            this.Text = "intro_form";
            this.Load += new System.EventHandler(this.intro_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button aisimstartbutton;
        private System.Windows.Forms.Button standardsimstartbutton;
        private System.Windows.Forms.TextBox Experience_entry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Role_entry;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Intro_label;
        private System.Windows.Forms.TextBox Participant_ID_box;
        private System.Windows.Forms.Label label1;
    }
}