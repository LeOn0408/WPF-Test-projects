using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using WPF_Test.Models;
using Text = DocumentFormat.OpenXml.Spreadsheet.Text;

namespace WPF_Test.ExcelService
{
    public class ExcelService
    {
        internal MemoryStream GetDocument(ObservableCollection<ProjectDocument> projectDocuments)
        {
            var table = new DataTableConverter().ToDataTable(projectDocuments);
            
            using MemoryStream stream = new ();
            SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);

            WorkbookPart workbookpart = spreadSheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            Sheets? sheets = spreadSheetDocument.WorkbookPart.Workbook.
                AppendChild(new Sheets());

            Sheet sheet = new()
            {
                Id = spreadSheetDocument.WorkbookPart.
                GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Projects"
            };
            sheets.Append(sheet);

            ///
            SheetData? data = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            Row header = new();
            header.RowIndex = (uint)1;

            foreach (DataColumn column in table.Columns)
            {
                Cell headerCell = CreateTextCell(
                    table.Columns.IndexOf(column) + 1,
                    1,
                    column.ColumnName);

                header.AppendChild(headerCell);
            }
            data?.AppendChild(header);

            DataRow contentRow;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                contentRow = table.Rows[i];
                data?.AppendChild(CreateContentRow(contentRow, i + 2));
            }

            workbookpart.Workbook.Save();

            spreadSheetDocument.Close();

            return stream;
        }
        private Cell CreateTextCell(int columnIndex, int rowIndex, object cellValue)
        {
                    Cell cell = new();

                    cell.DataType = CellValues.InlineString;
                    cell.CellReference = GetColumnName(columnIndex) + rowIndex;

                    InlineString inlineString = new();
                    Text? t = new();

                    t.Text = cellValue?.ToString();
                    inlineString.AppendChild(t);
                    cell.AppendChild(inlineString);

                    return cell;
        }
        private Row CreateContentRow(DataRow dataRow,int rowIndex)
        {
            Row row = new()
            {
                RowIndex = (uint)rowIndex
            };

            for (int i = 0; i < dataRow.Table.Columns.Count; i++)
            {
                Cell dataCell = CreateTextCell(i + 1, rowIndex, dataRow[i]);
                row.AppendChild(dataCell);
            }
            return row;
        }
        private string GetColumnName(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = string.Empty;
            int modifier;

            while (dividend > 0)
            {
                modifier = (dividend - 1) % 26;
                columnName =
                    Convert.ToChar(65 + modifier).ToString() + columnName;
                dividend = (dividend - modifier) / 26;
            }

            return columnName;
        }

    }
}