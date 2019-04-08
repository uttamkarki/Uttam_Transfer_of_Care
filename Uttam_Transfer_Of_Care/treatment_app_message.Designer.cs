namespace Uttam_Transfer_Of_Care
{
    partial class Message_form
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
            this.Treatment_label_box = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Treatment_label_box
            // 
            this.Treatment_label_box.BackColor = System.Drawing.Color.Transparent;
            this.Treatment_label_box.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Treatment_label_box.Location = new System.Drawing.Point(150, 150);
            this.Treatment_label_box.Name = "Treatment_label_box";
            this.Treatment_label_box.Size = new System.Drawing.Size(600, 200);
            this.Treatment_label_box.TabIndex = 0;
            this.Treatment_label_box.Text = "Applying Treatment";
            this.Treatment_label_box.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Treatment_label_box.Click += new System.EventHandler(this.Treatment_label_box_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Uttam_Transfer_Of_Care.Properties.Resources.img_ajax_processing;
            this.pictureBox1.Location = new System.Drawing.Point(850, 150);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Message_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1174, 529);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Treatment_label_box);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Message_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EMS Agent";
            this.Load += new System.EventHandler(this.Message_form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Treatment_label_box;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}