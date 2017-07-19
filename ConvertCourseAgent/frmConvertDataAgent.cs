using System;
using Engine;
using System.Windows.Forms;


namespace ConvertCourseWindowsService
{
    public partial class frmConvertDataAgent : Form
    {
        public frmConvertDataAgent()
        {
            InitializeComponent();
        }

        private void timerConvertPDF_Tick(object sender, EventArgs e)
        {
            timerConvertPDF.Enabled = false;
            LogFileENG.CreateHartbeat("tmConvertPDF");
            ConvertPDFFileENG.lblProgressStatus = lblProgressStatus;
            ConvertPDFFileENG.lblTime = lblTime;
            ConvertPDFFileENG.pb = progressBar1;
            ConvertPDFFileENG.NotiIcon = notifyIcon1;
            ConvertPDFFileENG.ConvertFilePDF();
            
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
            Application.DoEvents();

            timerConvertPDF.Enabled = true;
        }

        //private void timerConvertCourse_Tick(object sender, EventArgs e)
        //{
        //    timerConvertCourse.Enabled = false;
        //    LogFileENG.CreateHartbeat("tmConvertCourse");
        //    ConvertCourseENG.ConvertCourseData();
        //    timerConvertCourse.Enabled = true;
        //}

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void frmConvertDataAgent_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.Text = "Convert Data Agent";
            }
            else {
                notifyIcon1.Visible = false;
            }
        }

        private void frmConvertDataAgent_Shown(object sender, EventArgs e)
        {
            this.Text = "Form Convert Data Agent";
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "Convert Data Agent";
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
