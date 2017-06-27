using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using LinqDB.ConnectDB;
using LinqDB.TABLE;

namespace ConvertPDFWindowsService
{
    public partial class ConvertPDFService : ServiceBase
    {
        public ConvertPDFService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            tmConvertPDF = new System.Timers.Timer();
            tmConvertPDF.Interval = 10000;   //10 วินาที
            tmConvertPDF.Elapsed += new System.Timers.ElapsedEventHandler(tmConvertPDF_Tick);
            tmConvertPDF.Start();
            tmConvertPDF.Enabled = true;
        }

        private System.Timers.Timer tmConvertPDF;
        private void tmConvertPDF_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            tmConvertPDF.Enabled = false;
            ConvertPDFServiceENG.ConvertFilePDF();
            tmConvertPDF.Enabled = true;
        }

        

        protected override void OnStop()
        {
        }
    }
}
