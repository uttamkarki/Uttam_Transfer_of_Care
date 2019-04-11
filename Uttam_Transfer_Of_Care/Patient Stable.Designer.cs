namespace Uttam_Transfer_Of_Care
{
    partial class Patient_Stable
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
            this.stable_label_box = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // stable_label_box
            // 
            this.stable_label_box.Font = new System.Drawing.Font("Century Gothic", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stable_label_box.Location = new System.Drawing.Point(300, 150);
            this.stable_label_box.Name = "stable_label_box";
            this.stable_label_box.Size = new System.Drawing.Size(600, 200);
            this.stable_label_box.TabIndex = 0;
            this.stable_label_box.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Patient_Stable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1174, 529);
            this.Controls.Add(this.stable_label_box);
            this.Name = "Patient_Stable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient_Stable";
            this.Load += new System.EventHandler(this.Patient_Stable_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label stable_label_box;
    }
}