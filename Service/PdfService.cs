using DocumentFormat.OpenXml.Office2010.PowerPoint;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Test.Models;
using Document = iTextSharp.text.Document;

namespace WPF_Test.Service
{
    internal class PdfService
    {
        public PdfService()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public MemoryStream GetPdf(ObservableCollection<ProjectDocument> projectDocuments)
        {
            var dt = new DataTableConverter().ToDataTable(projectDocuments);

            using MemoryStream stream = new();

            Document document = new();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            Font font = CreateFont();
            PdfPTable table = CreateTable(dt);
            PdfPCell cell = new(new Phrase("Projects"))
            {
                Colspan = dt.Columns.Count
            };

            CreateHead(dt, font, table);
            FillTable(dt, font, table);

            document.Add(table);
            document.Close();

            return stream;
        }

        private static void FillTable(DataTable dt, Font font, PdfPTable table)
        {
            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int h = 0; h < dt.Columns.Count; h++)
                    {
                        table.AddCell(new Phrase(r[h].ToString(), font));
                    }
                }
            }
        }

        private static void CreateHead(DataTable dt, Font font, PdfPTable table)
        {
            foreach (DataColumn c in dt.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, font));
            }
        }

        private static PdfPTable CreateTable(DataTable dt)
        {
            PdfPTable table = new(dt.Columns.Count);

            float[] widths = new float[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
                widths[i] = 4f;

            table.SetWidths(widths);

            table.WidthPercentage = 100;
            return table;
        }

        private static Font CreateFont()
        {
            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new Font(baseFont, Font.DEFAULTSIZE, Font.NORMAL);
            return font;
        }
    }
}
