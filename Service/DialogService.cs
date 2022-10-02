using System.IO;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls.Primitives;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using Microsoft.Win32;

namespace WPF_Test.Service
{
    public class DialogExportService : IDialogService
    {
        public string? FilePath { get; set; }
        public ExportTypeData ExportType { get; set; }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new ();
            saveFileDialog.Filter = " Excel(*.xls;*.xlsx)| *.xls;*.xlsx| PDF(*.pdf)|*.pdf ";

            if (saveFileDialog.ShowDialog() == true)
            {
                ExportType = (ExportTypeData)saveFileDialog.FilterIndex;
                FilePath = saveFileDialog. FileName;
                return true;
            }
            return false;
        }

        
    }
    public enum ExportTypeData
    {
        Excel =1,
        Pdf
    }
}