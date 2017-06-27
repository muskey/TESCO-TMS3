using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LinqDB.ConnectDB;
using LinqDB.TABLE;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConvertPDFWindowsService
{
    public class ConvertPDFServiceENG
    {
        public static void ConvertFilePDF() {
            IniFile ini = new IniFile(Application.StartupPath + @"\Config.ini");
            string TempFolder = ini.GetKeyValue("Setting", "TempFolder");
            string DocumentFolder = ini.GetKeyValue("Setting", "DocumentDocument");

            TbUserCourseDocumentFileLinqDB lnq = new TbUserCourseDocumentFileLinqDB();
            DataTable dt = lnq.GetDataList("is_convert='N'", "", null, null);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.IsDBNull(dr["file_url"]) == false)
                    {
                        string OutputFile = TempFolder + Path.GetFileName(dr["file_url"].ToString());

                        if (DownloadFileFromURL(dr["file_url"].ToString(), TempFolder) == true)
                        {
                            pdftoimg.PDFConvertor pdf = new pdftoimg.PDFConvertor();
                            DataTable fileDt = pdf.ConvertToDatatable(OutputFile, DocumentFolder, System.Drawing.Imaging.ImageFormat.Jpeg);
                            if (fileDt.Rows.Count > 0)
                            {
                                lnq = new TbUserCourseDocumentFileLinqDB();
                                lnq.GetDataByPK(Convert.ToInt64(dr["id"]), null);

                                TransactionDB trans = new TransactionDB();
                                ExecuteDataInfo ret = lnq.UpdateData("ConvertPDFService", trans.Trans);
                                if (ret.IsSuccess == true)
                                {
                                    trans.CommitTransaction();
                                }
                                else
                                {
                                    trans.RollbackTransaction();
                                }
                            }
                        }
                    }
                }
            }
            dt.Dispose();
        }


        private static bool DownloadFileFromURL(string URL, string OutputFile)
        {
            bool ret = false;
            try
            {
                if (File.Exists(OutputFile) == true)
                {
                    File.SetAttributes(OutputFile, FileAttributes.Normal);
                    File.Delete(OutputFile);
                }

                WebClient client = new WebClient();
                client.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                byte[] b = client.DownloadData(URL);

                FileStream fs = new FileStream(OutputFile, FileMode.Create);
                fs.Write(b, 0, b.Length);
                fs.Close();

                if (File.Exists(OutputFile) == true)
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                ret = false;
            }

            return ret;
        }
    }
}
