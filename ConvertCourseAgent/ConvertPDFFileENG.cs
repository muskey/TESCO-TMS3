using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LinqDB.ConnectDB;
using LinqDB.TABLE;
using System.Data;
using System.IO;
using System.Net;
using Engine;

namespace ConvertCourseWindowsService
{
    public class ConvertPDFFileENG
    {
        #region "Convert File PDF"
        public static void ConvertFilePDF() {
            try { 
                IniFile ini = new IniFile(@"C:\Windows\TESCO-TMS3.ini");
                string TempFolder = ini.GetKeyValue("FolderSetting", "TempFolder");
                if (Directory.Exists(TempFolder) == false)
                    Directory.CreateDirectory(TempFolder);

                TbUserCourseDocumentFileLinqDB lnq = new TbUserCourseDocumentFileLinqDB();
                DataTable dt = lnq.GetDataList("is_convert='N'", "", null, null);
                if (dt.Rows.Count > 0)
                {
                    LogFileENG.LogTrans("พบข้อมูล " + dt.Rows.Count.ToString() + " รายการ");
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.IsDBNull(dr["file_url"]) == false)
                        {
                            string DocumentFolder = ini.GetKeyValue("FolderSetting", "DocumentFolder") + dr["user_id"].ToString() + "\\";
                            if (Directory.Exists(DocumentFolder) == false)
                                Directory.CreateDirectory(DocumentFolder);

                            string OutputFile = TempFolder + Path.GetFileName(dr["file_url"].ToString());

                            LogFileENG.LogTrans("เริ่ม Download File " + OutputFile);

                            //Download PDF File Form Backend
                            if (DownloadFileFromURL(dr["file_url"].ToString(), OutputFile) == true)
                            {

                                LogFileENG.LogTrans("เริ่ม Convert File " + OutputFile);
                                PDFConvertor pdf = new PDFConvertor();
                                DataTable fileDt = pdf.ConvertToDatatable(OutputFile, DocumentFolder, System.Drawing.Imaging.ImageFormat.Jpeg);
                                if (fileDt.Rows.Count > 0)
                                {
                                    LogFileENG.LogTrans("Convert File " + OutputFile + " สำเร็จ จำนวน " + fileDt.Rows.Count + " หน้า");
                                    lnq = new TbUserCourseDocumentFileLinqDB();
                                    lnq.GetDataByPK(Convert.ToInt64(dr["id"]), null);
                                    lnq.PDF_PAGE = fileDt.Rows.Count;
                                    lnq.IS_CONVERT = 'Y';

                                    TransactionDB trans = new TransactionDB();
                                    ExecuteDataInfo ret = lnq.UpdateData("ConvertPDFService", trans.Trans);
                                    if (ret.IsSuccess == true)
                                    {
                                        trans.CommitTransaction();
                                        LogFileENG.LogTrans("Update IS_CONVERT ของไฟล์ " + OutputFile + " สำเร็จ");
                                    }
                                    else
                                    {
                                        trans.RollbackTransaction();
                                        LogFileENG.LogError("Update IS_CONVERT ของไฟล์ " + OutputFile + " ไม่สำเร็จ");
                                    }
                                }
                                else {
                                    LogFileENG.LogError("Convert File " + OutputFile + " ไม่สำเร็จ");
                                }
                            }
                        }
                    }
                }
                
                dt.Dispose();
            }
            catch (Exception ex) {
                LogFileENG.LogException(ex.Message, ex.StackTrace);
            }
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
                client.Dispose();

                FileStream fs = new FileStream(OutputFile, FileMode.Create);
                fs.Write(b, 0, b.Length);
                fs.Close();

                if (File.Exists(OutputFile) == true)
                {
                    ret = true;
                    LogFileENG.LogTrans("ดาวน์โหลดไพล์ " + URL + " สำเร็จ");
                }
                else {
                    LogFileENG.LogError("Error : ดาวน์โหลดไพล์ " + URL + " ไม่สำเร็จ");
                }
            }
            catch (Exception ex)
            {
                ret = false;
                LogFileENG.LogException("ดาวน์โหลดไพล์ " + URL + " ไม่สำเร็จ Exception Error:" + ex.Message, ex.StackTrace);
            }

            return ret;
        }
        #endregion
    }
}
