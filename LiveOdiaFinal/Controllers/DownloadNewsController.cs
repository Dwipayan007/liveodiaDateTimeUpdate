using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LiveOdiaFinal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace LiveOdiaFinal.Controllers
{
    public class DownloadNewsController : ApiController
    {
        // POST: api/DownloadNews
        public HttpResponseMessage Post(AdminModel value)
        {
            ////HttpResponse response = HttpContext.Current.Response;
            DataTable dt = null;
            string pdfname = "";
            dt = dbutility.getAllNews(value);
            pdfname = CreatePdfFile(dt, value);
            var path = System.Web.HttpContext.Current.Server.MapPath("~/NewsPdf/"+pdfname); ;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentLength = stream.Length;
            return result;


            //response = showFile(response, fs, fs.Length, "application/octet-stream", pdfname);
        }

        private string CreatePdfFile(DataTable dt, AdminModel val)
        {
            String retval = "Error";
            String msgstrng = "";
            bool res = false;
            int rcode = 0;
            try
            {
                string StoragePath = HttpContext.Current.Server.MapPath("~/NewsPdf/");
                rcode = 1;
                //-----------------------------------------------------------------------------------------//
                string pdfname = DateTime.Now.Millisecond.ToString() + ".pdf";
                res = dbutility.createPdfName(pdfname, val);
                if (res)
                {
                    retval = pdfname;

                    string directoryName = Path.GetDirectoryName(StoragePath);
                    if ((directoryName.Length > 0) && (!Directory.Exists(directoryName)))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    FileStream fs = new FileStream(StoragePath + pdfname, FileMode.Create);

                    // ...

                    //FileStream fs = new FileStream(StoragePath, FileMode.OpenOrCreate);
                    //File.Copy(LocalFileName, Path.Combine(StoragePath, fname));
                    Document document = new Document(PageSize.A4, 15, 15, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    //writer.CloseStream = true;
                    HeaderFooter PageEventHandler = new HeaderFooter();

                    writer.PageEvent = PageEventHandler;
                    // Define the page header
                    PageEventHandler.HeaderFont = FontFactory.GetFont("Calibri (Body)", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                    PageEventHandler.HeaderRight = "All Top News";

                    document.Open();


                    #region ****************PDFCONTENT***********************

                    //----------------------------------------NEW CODE-------------------------------------------
                    iTextSharp.text.Font font8B = FontFactory.GetFont("Calibri (Body)", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                    iTextSharp.text.Font font8N = FontFactory.GetFont("Calibri (Body)", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                    iTextSharp.text.Font font10B = FontFactory.GetFont("Calibri (Body)", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                    iTextSharp.text.Font font10N = FontFactory.GetFont("Calibri (Body)", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                    iTextSharp.text.Font font12B = FontFactory.GetFont("Calibri (Body)", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);


                    PdfPTable ptbl = new PdfPTable(3);
                    ptbl.HorizontalAlignment = iTextSharp.text.Table.ALIGN_CENTER;
                    float[] twidth = { 100, 300, 100 };
                    ptbl.WidthPercentage = 95;
                    ptbl.SetWidths(twidth);

                    PdfPCell cell = new PdfPCell();

                    try
                    {
                        rcode = 2;
                        foreach (DataRow dr in dt.Rows)
                        {


                            PdfPTable nestTBL = new PdfPTable(1);
                            nestTBL.WidthPercentage = 100;
                            nestTBL.HorizontalAlignment = iTextSharp.text.Table.ALIGN_LEFT;

                            cell = new PdfPCell(new Phrase(dr["ttitle"].ToString(), font12B));
                            cell.HorizontalAlignment = iTextSharp.text.Table.ALIGN_CENTER;
                            cell.Border = 0;
                            cell.Colspan = 1;
                            nestTBL.AddCell(cell);
                            cell = new PdfPCell(new Phrase(dr["tsub"].ToString(), font8N));
                            cell.HorizontalAlignment = iTextSharp.text.Table.ALIGN_CENTER;
                            cell.Border = 0;
                            cell.Colspan = 1;
                            nestTBL.AddCell(cell);

                            cell = new PdfPCell(new Phrase(dr["topnews"].ToString(), font8N));
                            cell.HorizontalAlignment = iTextSharp.text.Table.ALIGN_CENTER;
                            cell.Border = 0;
                            cell.Colspan = 1;
                            nestTBL.AddCell(cell);

                            cell = new PdfPCell(new Phrase(dr["timage"].ToString(), font8N));
                            cell.HorizontalAlignment = iTextSharp.text.Table.ALIGN_CENTER;
                            cell.Border = 0;
                            cell.Colspan = 1;
                            nestTBL.AddCell(cell);

                            cell = new PdfPCell(nestTBL);
                            cell.HorizontalAlignment = iTextSharp.text.Table.ALIGN_LEFT;
                            cell.Border = 0;
                            cell.Colspan = 1;
                            ptbl.AddCell(cell);

                            cell = new PdfPCell(new Phrase(""));
                            cell.HorizontalAlignment = iTextSharp.text.Table.ALIGN_CENTER;
                            cell.Border = 0;
                            cell.Colspan = 3;
                            ptbl.AddCell(cell);

                            cell = new PdfPCell(new Phrase(" "));
                            cell.HorizontalAlignment = iTextSharp.text.Table.ALIGN_CENTER;
                            cell.Border = 0;
                            cell.BorderWidthTop = 1f;
                            cell.Colspan = 3;
                            ptbl.AddCell(cell);

                            document.Add(ptbl);

                            PdfPTable nestTBL2 = new PdfPTable(4);
                            nestTBL2.HorizontalAlignment = iTextSharp.text.Table.ALIGN_CENTER;
                            float[] twidth44 = { 85, 100, 105, 110 };
                            nestTBL2.SetWidths(twidth44);
                            nestTBL2.WidthPercentage = 95;

                            cell = new PdfPCell(new Phrase(dr["timage"].ToString(), font10B));
                            cell.HorizontalAlignment = iTextSharp.text.Table.ALIGN_LEFT;
                            cell.Border = 0;
                            cell.Colspan = 1;
                            nestTBL2.AddCell(cell);

                            document.Add(nestTBL2);


                            #endregion
                        }

                    }
                    catch (Exception exc)
                    {
                        retval = "Error, PDFgen-" + exc.Message;
                    }

                    rcode = 6;
                    document.Close();
                }
            }
            catch (Exception ee)
            {
                retval = "Error, creating PDF document.";
            }

            return retval;

        }

        public class HeaderFooter : PdfPageEventHelper
        {
            public String msg;

            // This is the contentbyte object of the writer
            PdfContentByte cb;

            // we will put the final number of pages in a template
            PdfTemplate template;

            // this is the BaseFont we are going to use for the header / footer
            BaseFont bf = null;

            //// This keeps track of the creation time
            //DateTime PrintTime = DateTime.Now;


            #region Properties
            private string _HeaderRight;
            public string HeaderRight
            {
                get { return _HeaderRight; }
                set { _HeaderRight = value; }
            }

            private iTextSharp.text.Font _HeaderFont;
            public iTextSharp.text.Font HeaderFont
            {
                get { return _HeaderFont; }
                set { _HeaderFont = value; }
            }

            private iTextSharp.text.Font _FooterFont;
            public iTextSharp.text.Font FooterFont
            {
                get { return _FooterFont; }
                set { _FooterFont = value; }
            }

            #endregion

            // we override the onOpenDocument method
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    //PrintTime = DateTime.Now;
                    bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    cb = writer.DirectContent;
                    template = cb.CreateTemplate(50, 50);
                }
                catch (DocumentException de)
                {
                }
                catch (System.IO.IOException ioe)
                {
                }
            }

            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);

                iTextSharp.text.Rectangle pageSize = document.PageSize;

                if (HeaderRight != string.Empty)
                {
                    PdfPTable HeaderTable = new PdfPTable(2);
                    HeaderTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    HeaderTable.TotalWidth = pageSize.Width - 10;
                    HeaderTable.SetWidthPercentage(new float[] { 45, 45 }, pageSize);

                    PdfPCell HeaderRightCell = new PdfPCell(new Phrase(8, HeaderRight, HeaderFont));
                    HeaderRightCell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    HeaderRightCell.Border = 0;
                    HeaderRightCell.Colspan = 2;
                    HeaderTable.AddCell(HeaderRightCell);

                    cb.SetRGBColorFill(0, 0, 0);
                    HeaderTable.WriteSelectedRows(0, -1, pageSize.GetLeft(0), pageSize.GetTop(10), cb);
                }
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);

                int pageN = writer.PageNumber;
                String text = msg + "     Page " + pageN;
                float len = bf.GetWidthPoint(text, 8);

                iTextSharp.text.Rectangle pageSize = document.PageSize;

                cb.SetRGBColorFill(100, 100, 100);

                cb.BeginText();
                cb.SetFontAndSize(bf, 8);
                cb.SetColorFill(iTextSharp.text.Color.BLACK);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, text, 570, 15, 0);
                cb.EndText();

                cb.AddTemplate(template, pageSize.GetLeft(570) + len, pageSize.GetBottom(15));

            }

            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);

                template.BeginText();
                template.SetFontAndSize(bf, 8);
                template.SetTextMatrix(0, 0);
                template.ShowText("" + (writer.PageNumber - 1));
                template.EndText();
            }

        }

        public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        public static System.Drawing.Bitmap Base64ToImage(string base64String)
        {
            Byte[] bitmapData = new Byte[base64String.Length];
            bitmapData = Convert.FromBase64String(base64String);
            System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);
            System.Drawing.Bitmap bitImage = new System.Drawing.Bitmap((System.Drawing.Bitmap)System.Drawing.Image.FromStream(streamBitmap));
            bitImage.MakeTransparent(System.Drawing.Color.Black);
            return bitImage;

        }

        public HttpResponse showFile(HttpResponse response, Stream inputStream, long contentLength, string mimeType, string fileName)
        {

            string clength = contentLength.ToString(CultureInfo.InvariantCulture);

            response.ContentType = mimeType;
            response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            if (contentLength != -1) response.AddHeader("Content-Length", clength);
            response.ClearContent();
            inputStream.CopyTo(response.OutputStream);
            //inputStream.Close();
            //inputStream.Dispose();
            response.OutputStream.Flush();
            response.End();

            return response;

            //return response;
        }


        // PUT: api/DownloadNews/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DownloadNews/5
        public void Delete(int id)
        {
        }
        public DataTable Get()
        {
            return dbutility.getPriorityNews();
        }
    }
}
