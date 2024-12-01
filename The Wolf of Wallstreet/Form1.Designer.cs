namespace The_Wolf_of_Wallstreet
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.timerDelay = new System.Windows.Forms.Timer(this.components);
            this.timerAnim = new System.Windows.Forms.Timer(this.components);
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerDelay
            // 
            this.timerDelay.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerAnim
            // 
            this.timerAnim.Interval = 50;
            this.timerAnim.Tick += new System.EventHandler(this.timerAnim_Tick);
            // 
            // timerClock
            // 
            this.timerClock.Interval = 125;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "The Wolf of Wallstreet - created by Lac Cam Chuong";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerDelay;
        private System.Windows.Forms.Timer timerAnim;
        private System.Windows.Forms.Timer timerClock;
    }
}

