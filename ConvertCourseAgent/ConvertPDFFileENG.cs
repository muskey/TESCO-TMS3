﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LinqDB.ConnectDB;
using LinqDB.TABLE;
using System.Data;
using System.IO;
using System.Net;
using Engine;
using System.Windows.Forms;

namespace ConvertCourseWindowsService
{
    public class ConvertPDFFileENG
    {
        static Label _lblProgressStatus;
        static Label _lblTime;
        static ProgressBar _pb;
        static NotifyIcon _NotifyIcon;

        public static Label lblProgressStatus {
            set { _lblProgressStatus = value; }
        }
        public static Label lblTime {
            set { _lblTime = value; }
        }
        public static ProgressBar pb {
            set { _pb = value; }
        }
        public static NotifyIcon NotiIcon {
           set { _NotifyIcon = value; }
        }

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
                    _pb.Maximum = dt.Rows.Count;
                    _pb.Value = 0;
                    SetTextProgress("Start Convert " + dt.Rows.Count.ToString() + " File(s)");
                    LogFileENG.LogTrans("พบข้อมูล " + dt.Rows.Count.ToString() + " รายการ");

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.IsDBNull(dr["file_url"]) == false)
                        {
                            //string DocumentFolder = ini.GetKeyValue("FolderSetting", "DocumentFolder") + dr["user_id"].ToString() + "\\";
                            string DocumentFolder = ini.GetKeyValue("FolderSetting", "DocumentFolder") + "FILE_ID_" + dr["document_file_id"].ToString() + "\\";
                            if (Directory.Exists(DocumentFolder) == false)
                                Directory.CreateDirectory(DocumentFolder);

                            string UserFolder = ini.GetKeyValue("FolderSetting", "DocumentFolder") + dr["user_id"].ToString() + "\\";
                            if (Directory.Exists(UserFolder) == false)
                                Directory.CreateDirectory(UserFolder);

                            string OutputFile = TempFolder + Path.GetFileName(dr["file_url"].ToString());

                            LogFileENG.LogTrans("เริ่ม Download File " + OutputFile);

                            //Download PDF File Form Backend
                            SetTextProgress("Download File " + dr["file_url"].ToString());
                            Application.DoEvents();

                            if (DownloadFileFromURL(dr["file_url"].ToString(), OutputFile) == true)
                            {
                                SetTextProgress("Download File " + dr["file_url"].ToString() + " Success");
                                
                                LogFileENG.LogTrans("เริ่ม Convert File " + OutputFile);
                                SetTextProgress("Convert File " + OutputFile);
                                
                                PDFConvertor pdf = new PDFConvertor();
                                Int32 pageCount = pdf.ConvertAndCountFile(OutputFile, DocumentFolder, System.Drawing.Imaging.ImageFormat.Jpeg);
                                if (pageCount > 0)
                                {
                                    LogFileENG.LogTrans("Convert File " + OutputFile + " สำเร็จ จำนวน " + pageCount + " หน้า");
                                    SetTextProgress("Convert File " + OutputFile + " Success " + pageCount.ToString() + " Page(s)");

                                    try {
                                        Directory.Delete(UserFolder, true);
                                        if (Directory.Exists(UserFolder)==false)
                                            Directory.CreateDirectory(UserFolder);

                                        foreach (string f in Directory.GetFiles(DocumentFolder)) {
                                            FileInfo fInfo = new FileInfo(f);
                                            File.Copy(f, UserFolder + fInfo.Name);
                                        }

                                        lnq = new TbUserCourseDocumentFileLinqDB();
                                        lnq.GetDataByPK(Convert.ToInt64(dr["id"]), null);
                                        lnq.PDF_PAGE = pageCount;
                                        lnq.IS_CONVERT = 'Y';

                                        TransactionDB trans = new TransactionDB();
                                        ExecuteDataInfo ret = lnq.UpdateData("ConvertPDFService", trans.Trans);
                                        if (ret.IsSuccess == true)
                                        {
                                            trans.CommitTransaction();

                                            LogFileENG.LogTrans("Update IS_CONVERT ของไฟล์ " + OutputFile + " สำเร็จ");
                                            SetTextProgress("Update IS_CONVERT ของไฟล์ " + OutputFile + " สำเร็จ");
                                        }
                                        else
                                        {
                                            trans.RollbackTransaction();
                                            LogFileENG.LogError("Update IS_CONVERT ของไฟล์ " + OutputFile + " ไม่สำเร็จ");
                                        }
                                    }
                                    catch (Exception ex) {
                                        LogFileENG.LogError("Exception : Convert File " + OutputFile + " ไม่สำเร็จ");
                                        LogFileENG.LogException(ex.Message, ex.StackTrace);
                                        SetTextProgress("Convert File " + OutputFile + " Fail");
                                    }
                                }
                                else
                                {
                                    LogFileENG.LogError("Convert File " + OutputFile + " ไม่สำเร็จ");
                                    SetTextProgress("Convert File " + OutputFile + " Fail");
                                }
                            }
                            else {
                                SetTextProgress("Download File " + dr["file_url"].ToString() + " Fail");
                            }
                        }
                        _pb.Value += 1;
                        _lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
                        Application.DoEvents();
                    }

                    _pb.Value = 0;
                    _lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
                    SetTextProgress("Ready");
                    _NotifyIcon.Text = "Convert Data Agent (Ready)";
                    Application.DoEvents();
                }
                dt.Dispose();
            }
            catch (Exception ex) {
                LogFileENG.LogException(ex.Message, ex.StackTrace);
            }
        }

        private static void SetTextProgress(string ProgressStatus) {
            _lblProgressStatus.Text = ProgressStatus;
            _NotifyIcon.Text = "Processing " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
            _lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
            Application.DoEvents();
        }

        private static bool DownloadFileFromURL(string URL, string OutputFile)
        {
            bool ret = false;
            try
            {
                if (File.Exists(OutputFile) == false)
                {
                    //ถ้ายังไม่มีไฟล์นี้ค่อย Download
                    WebClient client = new WebClient();
                    client.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    byte[] b = client.DownloadData(URL);
                    client.Dispose();

                    FileStream fs = new FileStream(OutputFile, FileMode.Create);
                    fs.Write(b, 0, b.Length);
                    fs.Close();
                }

                if (File.Exists(OutputFile) == true)
                {
                    //ถ้ามีไฟล์อยู่แล้วก็ไม่ต้องดาวน์โหลดใหม่
                    ret = true;
                    LogFileENG.LogTrans("ดาวน์โหลดไพล์ " + URL + " สำเร็จ");
                }
                else
                {
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
