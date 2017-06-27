using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Data;

namespace pdftoimg
{

    // A delegate type for hooking up change notifications.
    public delegate void ProgressChangingEventHandler(object sender, string  e);


    /// <summary>
    /// Author:ESMAEEL ZENDEHDEL zendehdell@yahoo.com
    /// DATE: 88/11/17
    /// Description: A Class For Exporting Image From PDF Files
    /// License : Free For All
    /// </summary>
    public class PDFConvertor
    {
        public int pageCount = 0;
        Acrobat.CAcroPDDoc pdfDoc = new Acrobat.AcroPDDoc();
        Acrobat.CAcroPDPage pdfPage = null;
        Acrobat.CAcroRect pdfRect = new Acrobat.AcroRect();
        Acrobat.AcroPoint pdfPoint =new Acrobat.AcroPoint();

        

        public event ProgressChangingEventHandler  ExportProgressChanging;

        protected virtual void OnExportProgressChanging(string e)
        {
            Thread.SpinWait(100);
          if (ExportProgressChanging != null)
                ExportProgressChanging(this, e);
        }

        #region Convert
        /// <summary>
        /// Converting PDF Files TO Specified Image Format
        /// </summary>
        /// <param name="sourceFileName">Source PDF File Path</param>
        /// <param name="DestinationPath">Destination PDF File Path</param>
        /// <param name="outPutImageFormat">Type Of Exported Image</param>
        /// <returns>Returns Count Of Exported Images</returns>
        public int Convert(string sourceFileName, string DestinationPath, ImageFormat outPutImageFormat)
        {


            if (pdfDoc.Open(sourceFileName))
            {

                // pdfapp.Hide();
                pageCount = pdfDoc.GetNumPages();

                for (int i = 0; i < pageCount; i++)
                {
                    pdfPage = (Acrobat.CAcroPDPage)pdfDoc.AcquirePage(i);


                    pdfPoint = (Acrobat.AcroPoint)pdfPage.GetSize();
                    pdfRect.Left = 0;
                    pdfRect.right = pdfPoint.x;
                    pdfRect.Top = 0;
                    pdfRect.bottom = pdfPoint.y;

                    pdfPage.CopyToClipboard(pdfRect, 0, 0, 100);

                    string outimg = "";
                    string filename = sourceFileName.Substring(sourceFileName.LastIndexOf("\\")).Replace(".pdf", "");

                    //if (pageCount == 1)
                    //    outimg = DestinationPath + "\\" + filename + "." + outPutImageFormat.ToString();
                    //else
                        outimg = DestinationPath + "\\" + filename + "_" + (i+1).ToString() + "." + outPutImageFormat.ToString();
                    
                    Clipboard.GetImage().Save(outimg, outPutImageFormat);

                    ////////////Firing Progress Event 
                    OnExportProgressChanging(outimg);
                }

                  Dispose();
            }
            else
            {
                Dispose();
                throw new System.IO.FileNotFoundException(sourceFileName +" Not Found!");

            }
            return pageCount;
        }

        public DataTable ConvertToDatatable(string sourceFileName, string DestinationPath, ImageFormat outPutImageFormat)
        {
            DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("next_id", typeof(int));
            dt.Columns.Add("pathname");

            if (pdfDoc.Open(sourceFileName))
            {
                pageCount = pdfDoc.GetNumPages();

                for (int i = 0; i < pageCount; i++)
                {
                    pdfPage = (Acrobat.CAcroPDPage)pdfDoc.AcquirePage(i);

                    pdfPoint = (Acrobat.AcroPoint)pdfPage.GetSize();
                    pdfRect.Left = 0;
                    pdfRect.right = pdfPoint.x;
                    pdfRect.Top = 0;
                    pdfRect.bottom = pdfPoint.y;

                    pdfPage.CopyToClipboard(pdfRect, 0, 0, 100);

                    string outimg = "";
                    string filename = sourceFileName.Substring(sourceFileName.LastIndexOf("\\")).Replace(".pdf", "");

                    //if (pageCount == 1)
                    //    outimg = DestinationPath + "\\" + filename + "." + outPutImageFormat.ToString();
                    //else
                    outimg = DestinationPath +  filename + "_" + (i + 1).ToString() + "." + outPutImageFormat.ToString();

                    Clipboard.GetImage().Save(outimg, outPutImageFormat);
                    if (System.IO.File.Exists(outimg) == true) {
                        System.Data.DataRow dr = dt.NewRow();
                        dr["next_id"] = (i + 1);
                        dr["pathname"] = outimg;

                        dt.Rows.Add(dr);
                    }

                    ////////////Firing Progress Event 
                    OnExportProgressChanging(outimg);
                }

                Dispose();
            }
            else
            {
                Dispose();
                throw new System.IO.FileNotFoundException(sourceFileName + " Not Found!");

            }
            return dt;
        }
        #endregion

        #region Convert With Zoom
        /// <summary>
        /// Converting PDF Files TO Specified Image Format
        /// </summary>
        /// <param name="sourceFileName">Source PDF File Path</param>
        /// <param name="DestinationPath">Destination PDF File Path</param>
        /// <param name="outPutImageFormat">Type Of Exported Image</param>
        /// <param name="width">Width Of Exported Images</param>
        /// <param name="height">Heiht Of Exported Images</param>
        /// <param name="zoom">Zoom Percent</param>
        /// <returns>Returns Count Of Exported Images</returns>
        public int Convert(string sourceFileName, string DestinationPath, ImageFormat outPutImageFormat, short width, short height, short zoom)
        {



            if (pdfDoc.Open(sourceFileName))
            {

                // pdfapp.Hide();
                pageCount = pdfDoc.GetNumPages();

                for (int i = 0; i < pageCount; i++)
                {
                    pdfPage = (Acrobat.CAcroPDPage)pdfDoc.AcquirePage(i);


                    //  pdfPoint = (Acrobat.CAcroPoint)pdfPage.GetSize();
                    pdfRect.Left = 0;
                    pdfRect.right = width; //pdfPoint.x;
                    pdfRect.Top = 0;
                    pdfRect.bottom = height; //pdfPoint.y;

                    pdfPage.CopyToClipboard(pdfRect, 0, 0, zoom);

                    string outimg = "";
                    string filename = sourceFileName.Substring(sourceFileName.LastIndexOf("\\"));

                    if (pageCount == 1)
                        outimg = DestinationPath + "\\" + filename + "." + outPutImageFormat.ToString();
                    else
                        outimg = DestinationPath + "\\" + filename + "_" + i.ToString() + "." + outPutImageFormat.ToString();

                    
                    Clipboard.GetImage().Save(outimg, outPutImageFormat);
                 
                    ////////////Firing Progress Event 
                    OnExportProgressChanging(outimg);
                }
                Dispose();

            }
            else
            {
                Dispose();
                throw new System.IO.IOException("Specified File Not Found!");
            }
            return pageCount;
        }
        #endregion



    


        #region Destractor
        ~PDFConvertor()
        {
            GC.Collect();
            if (pdfPage!=null)
                Marshal.ReleaseComObject(pdfPage);
            Marshal.ReleaseComObject(pdfPoint);
            Marshal.ReleaseComObject(pdfRect);
            Marshal.ReleaseComObject(pdfDoc);
        }
        public void Dispose()
        {
            GC.Collect();
            if (pdfPage != null)
                Marshal.ReleaseComObject(pdfPage);
            Marshal.ReleaseComObject(pdfPoint);
            Marshal.ReleaseComObject(pdfRect);
            Marshal.ReleaseComObject(pdfDoc);
        }
        #endregion

    }
}
