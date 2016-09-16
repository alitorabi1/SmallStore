using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows;

namespace SmallStore
{
    public class Receipt
    {
        private Document document;
        public Receipt(List<OrderItem> orderItems, decimal TotalPrice, decimal TotalAndTax, decimal Discount, decimal Tax)
        {
            try
            {
                int noOfColumns = 0, noOfRows = 0;
                noOfColumns = 3;
                noOfRows = orderItems.Count;

                float HeaderTextSize = 6;
                float ReportNameSize = 7;
                float ReportTextSize = 6;
                float store = 7;

               

                document = null;
                document = new Document(PageSize.A7, 0, 0, 15, 5);

                
                iTextSharp.text.pdf.PdfPTable mainTable = new iTextSharp.text.pdf.PdfPTable(noOfColumns);

                
                mainTable.HeaderRows = 4;

                
                iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);

                Phrase phApplicationName = new Phrase("SmallStore", FontFactory.GetFont("Arial", store, iTextSharp.text.Font.NORMAL));

               
                PdfPCell clApplicationName = new PdfPCell(phApplicationName);
              
                clApplicationName.Border = PdfPCell.NO_BORDER;
             
                clApplicationName.HorizontalAlignment = Element.ALIGN_CENTER;

                Phrase phDate = new Phrase(DateTime.Now.Date.ToString("yyyy-MM-dd"), FontFactory.GetFont("Arial", store, iTextSharp.text.Font.NORMAL));

             
                PdfPCell clDate = new PdfPCell(phDate);
                
                clDate.HorizontalAlignment = Element.ALIGN_RIGHT;
              
                clDate.Border = PdfPCell.NO_BORDER;

                
                headerTable.AddCell(clApplicationName);
             
                headerTable.AddCell(clDate);
              
                headerTable.DefaultCell.Border = PdfPCell.NO_BORDER;

                
                PdfPCell cellHeader = new PdfPCell(headerTable);
                cellHeader.Border = PdfPCell.NO_BORDER;
               
                cellHeader.Colspan = noOfColumns;
               
                mainTable.AddCell(cellHeader);
               
                
                Phrase phHeader = new Phrase("Merci et à bientôt ", FontFactory.GetFont("Arial", ReportNameSize, iTextSharp.text.Font.BOLD));
                PdfPCell clHeader = new PdfPCell(phHeader);
                clHeader.Colspan = noOfColumns;
                clHeader.Border = PdfPCell.NO_BORDER;
                clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.AddCell(clHeader);

               
                Phrase phSpace = new Phrase("\n");
                PdfPCell clSpace = new PdfPCell(phSpace);
                clSpace.Border = PdfPCell.NO_BORDER;
                clSpace.Colspan = noOfColumns;
                mainTable.AddCell(clSpace);

               

                mainTable.AddCell(new Phrase("ProductName", FontFactory.GetFont("Arial", HeaderTextSize, iTextSharp.text.Font.NORMAL)));
                mainTable.AddCell(new Phrase("Qty", FontFactory.GetFont("Arial", HeaderTextSize, iTextSharp.text.Font.NORMAL)));
                mainTable.AddCell(new Phrase("Amount p/i", FontFactory.GetFont("Arial", HeaderTextSize, iTextSharp.text.Font.NORMAL)));
               

                
                foreach (var item in orderItems)
                {

                    {
                        //mainTable.AddCell(new Phrase(item.ProductId.ToString(), FontFactory.GetFont("Arial", ReportTextSize, iTextSharp.text.Font.NORMAL)));
                        mainTable.AddCell(new Phrase(item.ProductName, FontFactory.GetFont("Arial", ReportTextSize, iTextSharp.text.Font.NORMAL)));
                        mainTable.AddCell(new Phrase(String.Format("{0:0.00 }", (item.NumberOfUnit.ToString())), FontFactory.GetFont("Arial", ReportTextSize, iTextSharp.text.Font.NORMAL)));
                        mainTable.AddCell(new Phrase(String.Format("{0:0.00 }", item.SalePricePerUnit.ToString()), FontFactory.GetFont("Arial", ReportTextSize, iTextSharp.text.Font.NORMAL)));

                    }
                  
                   // mainTable.CompleteRow();
                }
               
                Phrase phFooter = new Phrase("price"+ String.Format("{0:0.00 }", TotalPrice.ToString()), FontFactory.GetFont("Arial", ReportNameSize, iTextSharp.text.Font.NORMAL));
                PdfPCell clFooter = new PdfPCell(phFooter);
                clFooter.Colspan = noOfColumns;
                clFooter.Border = PdfPCell.NO_BORDER;
                clFooter.HorizontalAlignment = Element.ALIGN_RIGHT;
                mainTable.AddCell(clFooter);
                Phrase phFooter2 = new Phrase("-"+ String.Format("{0:0.00 }", Discount.ToString()), FontFactory.GetFont("Arial", ReportNameSize, iTextSharp.text.Font.NORMAL));
                PdfPCell clFooter2 = new PdfPCell(phFooter2);
                clFooter2.Colspan = noOfColumns;
                clFooter2.Border = PdfPCell.NO_BORDER;
                clFooter2.HorizontalAlignment = Element.ALIGN_RIGHT;
                mainTable.AddCell(clFooter2);
                Phrase phFooter3 = new Phrase("%"+ String.Format("{0:0.00 }", Tax.ToString()), FontFactory.GetFont("Arial", ReportNameSize, iTextSharp.text.Font.NORMAL));
                PdfPCell clFooter3 = new PdfPCell(phFooter3);
                clFooter3.Colspan = noOfColumns;
                clFooter3.Border = PdfPCell.NO_BORDER;
                clFooter3.HorizontalAlignment = Element.ALIGN_RIGHT;
                mainTable.AddCell(clFooter3);
                Phrase phFooter4 = new Phrase("Total"+ String.Format("{0:0.00 }", TotalAndTax.ToString()), FontFactory.GetFont("Arial", ReportNameSize, iTextSharp.text.Font.NORMAL));
                PdfPCell clFooter4 = new PdfPCell(phFooter4);
                clFooter4.Colspan = noOfColumns;
                clFooter4.Border = PdfPCell.NO_BORDER;
                clFooter4.HorizontalAlignment = Element.ALIGN_RIGHT;
                mainTable.AddCell(clFooter4);
                //    mainTable.CompleteRow();
                string fname = "recipet" + DateTime.Now.ToString("yyMMddhhmmss") + ".pdf";
                // Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                PdfWriter wri = PdfWriter.GetInstance(document, new System.IO.FileStream(fname, FileMode.Create));

                document.Open();//Open Document to write


             //   Paragraph paragraph = new Paragraph("data Exported From DataGridview!");

                document.Add(mainTable);
               
                document.Close(); 
                
                System.Diagnostics.Process.Start(fname);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Open PDF File has a problme ", "Error PDF", MessageBoxButton.OK, MessageBoxImage.Error);
                throw (ex);
            }
        }
    }
}
