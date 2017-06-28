using System.ServiceProcess;
using Engine;

namespace ConvertCourseWindowsService
{
    public partial class ConvertDataService : ServiceBase
    {
        public ConvertDataService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //tmConvertPDF = new System.Timers.Timer();
            //tmConvertPDF.Interval = 10000;   //10 วินาที
            //tmConvertPDF.Elapsed += new System.Timers.ElapsedEventHandler(tmConvertPDF_Tick);
            //tmConvertPDF.Start();
            //tmConvertPDF.Enabled = true;

            tmConvertCourse = new System.Timers.Timer();
            tmConvertCourse.Interval = 5000;   //5 วินาที
            tmConvertCourse.Elapsed += new System.Timers.ElapsedEventHandler(tmConvertCourse_Tick);
            tmConvertCourse.Start();
            tmConvertCourse.Enabled = true;
        }

        private System.Timers.Timer tmConvertPDF;
        private void tmConvertPDF_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            tmConvertPDF.Enabled = false;
            LogFileENG.CreateHartbeat("tmConvertPDF");
            ConvertPDFFileENG.ConvertFilePDF();
            tmConvertPDF.Enabled = true;
        }

        private System.Timers.Timer tmConvertCourse;
        private void tmConvertCourse_Tick(object sender, System.Timers.ElapsedEventArgs e) {
            tmConvertCourse.Enabled = false;
            LogFileENG.CreateHartbeat("tmConvertCourse");
            ConvertCourseENG.ConvertCourseData();
            tmConvertCourse.Enabled = true;
        }
        

        protected override void OnStop()
        {
        }
    }
}
