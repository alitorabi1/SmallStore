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

                // Creates a PDF document

                document = null;
                document = new Document(PageSize.A7, 0, 0, 15, 5);

                // Creates a PdfPTable with column count of the table equal to no of columns of the gridview or gridview datasource.
                iTextSharp.text.pdf.PdfPTable mainTable = new iTextSharp.text.pdf.PdfPTable(noOfColumns);

                // Sets the first 4 rows of the table as the header rows which will be repeated in all the pages.
                mainTable.HeaderRows = 4;

                // Creates a PdfPTable with 2 columns to hold the header in the exported PDF.
                iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(2);

                // Creates a phrase to hold the application name at the left hand side of the header.
                Phrase phApplicationName = new Phrase("SmallStore", FontFactory.GetFont("Arial", store, iTextSharp.text.Font.NORMAL));

                // Creates a PdfPCell which accepts a phrase as a parameter.
                PdfPCell clApplicationName = new PdfPCell(phApplicationName);
                // Sets the border of the cell to zero.
                clApplicationName.Border = PdfPCell.NO_BORDER;
                // Sets the Horizontal Alignment of the PdfPCell to left.
                clApplicationName.HorizontalAlignment = Element.ALIGN_CENTER;

                // Creates a phrase to show the current date at the right hand side of the header.
                Phrase phDate = new Phrase(DateTime.Now.Date.ToString("yyyy-MM-dd"), FontFactory.GetFont("Arial", store, iTextSharp.text.Font.NORMAL));

                // Creates a PdfPCell which accepts the date phrase as a parameter.
                PdfPCell clDate = new PdfPCell(phDate);
                // Sets the Horizontal Alignment of the PdfPCell to right.
                clDate.HorizontalAlignment = Element.ALIGN_RIGHT;
                // Sets the border of the cell to zero.
                clDate.Border = PdfPCell.NO_BORDER;

                // Adds the cell which holds the application name to the headerTable.
                headerTable.AddCell(clApplicationName);
                // Adds the cell which holds the date to the headerTable.
                headerTable.AddCell(clDate);
                // Sets the border of the headerTable to zero.
                headerTable.DefaultCell.Border = PdfPCell.NO_BORDER;

                // Creates a PdfPCell that accepts the headerTable as a parameter and then adds that cell to the main PdfPTable.
                PdfPCell cellHeader = new PdfPCell(headerTable);
                cellHeader.Border = PdfPCell.NO_BORDER;
                // Sets the column span of the header cell to noOfColumns.
                cellHeader.Colspan = noOfColumns;
                // Adds the above header cell to the table.
                mainTable.AddCell(cellHeader);
               
                // Creates a phrase which holds the file name.
                Phrase phHeader = new Phrase("Merci et a bientot ", FontFactory.GetFont("Arial", ReportNameSize, iTextSharp.text.Font.BOLD));
                PdfPCell clHeader = new PdfPCell(phHeader);
                clHeader.Colspan = noOfColumns;
                clHeader.Border = PdfPCell.NO_BORDER;
                clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                mainTable.AddCell(clHeader);

                // Creates a phrase for a new line.
                Phrase phSpace = new Phrase("\n");
                PdfPCell clSpace = new PdfPCell(phSpace);
                clSpace.Border = PdfPCell.NO_BORDER;
                clSpace.Colspan = noOfColumns;
                mainTable.AddCell(clSpace);

                // Sets the gridview column names as table headers.

                mainTable.AddCell(new Phrase("ProductName", FontFactory.GetFont("Arial", HeaderTextSize, iTextSharp.text.Font.NORMAL)));
                mainTable.AddCell(new Phrase("Qty", FontFactory.GetFont("Arial", HeaderTextSize, iTextSharp.text.Font.NORMAL)));
                mainTable.AddCell(new Phrase("Amount p/i", FontFactory.GetFont("Arial", HeaderTextSize, iTextSharp.text.Font.NORMAL)));
               // mainTable.AddCell(new Phrase("Total", FontFactory.GetFont("Arial", HeaderTextSize, iTextSharp.text.Font.BOLD)));

                // Reads the gridview rows and adds them to the mainTable
                foreach (var item in orderItems)
                {

                    {
                        //mainTable.AddCell(new Phrase(item.ProductId.ToString(), FontFactory.GetFont("Arial", ReportTextSize, iTextSharp.text.Font.NORMAL)));
                        mainTable.AddCell(new Phrase(item.ProductName, FontFactory.GetFont("Arial", ReportTextSize, iTextSharp.text.Font.NORMAL)));
                        mainTable.AddCell(new Phrase(item.NumberOfUnit.ToString(), FontFactory.GetFont("Arial", ReportTextSize, iTextSharp.text.Font.NORMAL)));
                        mainTable.AddCell(new Phrase(item.SalePricePerUnit.ToString(), FontFactory.GetFont("Arial", ReportTextSize, iTextSharp.text.Font.NORMAL)));

                    }
                    // Tells the mainTable to complete the row even if any cell is left incomplete.
                    mainTable.CompleteRow();
                }
                Phrase phFooter = new Phrase("Merci et a bientot ", FontFactory.GetFont("Arial", ReportNameSize, iTextSharp.text.Font.BOLD));
                PdfPCell clFooter = new PdfPCell(phFooter);
                clFooter.Colspan = 1;
                clFooter.Border = PdfPCell.NO_BORDER;
                clFooter.HorizontalAlignment = Element.ALIGN_LEFT;
                mainTable.AddCell(clFooter);
                // mainTable.CompleteRow();
               string fname = "recipet" + DateTime.Now.ToString("yyMMddhhmmss") + ".pdf";
                // Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                PdfWriter wri = PdfWriter.GetInstance(document, new System.IO.FileStream(fname, FileMode.Create));

                document.Open();//Open Document to write


                Paragraph paragraph = new Paragraph("data Exported From DataGridview!");

                document.Add(mainTable);
                //doc.Add(t1);
                document.Close(); //Close document
                //Ope the pdf file just created
                System.Diagnostics.Process.Start(fname);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The Pdf could not be created or it could not be opened", "Error PDF", MessageBoxButton.OK, MessageBoxImage.Error);
                throw (ex);
            }
        }
    }
}
