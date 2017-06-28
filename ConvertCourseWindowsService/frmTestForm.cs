using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ConvertCourseWindowsService
{
    public partial class frmTestForm : Form
    {
        public frmTestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Engine.ConvertCourseENG.ConvertCourseData();
            ConvertPDFFileENG.ConvertFilePDF();
             
        }
    }
}
