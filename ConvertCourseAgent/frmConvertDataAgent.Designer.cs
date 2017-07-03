namespace ConvertCourseWindowsService
{
    partial class frmConvertDataAgent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConvertDataAgent));
            this.timerConvertPDF = new System.Windows.Forms.Timer(this.components);
            this.timerConvertCourse = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // timerConvertPDF
            // 
            this.timerConvertPDF.Enabled = true;
            this.timerConvertPDF.Interval = 10000;
            this.timerConvertPDF.Tick += new System.EventHandler(this.timerConvertPDF_Tick);
            // 
            // timerConvertCourse
            // 
            this.timerConvertCourse.Enabled = true;
            this.timerConvertCourse.Interval = 5000;
            this.timerConvertCourse.Tick += new System.EventHandler(this.timerConvertCourse_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // frmConvertDataAgent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConvertDataAgent";
            this.Text = "frmTestForm";
            this.Shown += new System.EventHandler(this.frmConvertDataAgent_Shown);
            this.Resize += new System.EventHandler(this.frmConvertDataAgent_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerConvertPDF;
        private System.Windows.Forms.Timer timerConvertCourse;
        protected System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}