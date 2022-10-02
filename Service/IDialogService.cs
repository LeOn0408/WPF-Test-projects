namespace WPF_Test.Service;
public interface IDialogService
{
    ExportTypeData ExportType { get; set; }
    string? FilePath { get; set; }
    bool SaveFileDialog();
}