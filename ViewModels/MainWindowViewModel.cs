using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using WPF_Test.DataProviders;
using WPF_Test.ExcelService;
using WPF_Test.Models;
using WPF_Test.Service;

namespace WPF_Test.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<Project>? projects;
        private ObservableCollection<Stage>? stages;
        private ObservableCollection<Field>? fields;
        private ObservableCollection<ProjectDocument>? projectsDocuments=new();
        private int selectProjectId = 0;
        private int selectStageId;
        private int selectField;
        private bool onlyFilled;
        private RelayCommand? exportData;
        private IDialogService? dialogService;

        public MainWindowViewModel()
        {
            Projects = new ProjectData().Projects;
            Stages = new StageData().Stages;
            Fields = new DocumentGroup().Fields;
            Fields.Add(new Field() { Id=0, Name="All"});
        }
        

        private void ProjectDocumentsFilter()
        {
            ProjectDocuments = new ProjectDocumentData().GetData(onlyFilled,selectProjectId,selectStageId, selectField);
        }

        public RelayCommand? ExportData
        {
            get
            {
                return exportData ??
                    (exportData = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (ProjectDocuments is null && ProjectDocuments?.Count() == 0)
                            {
                                return;
                            }

                            dialogService = new DialogExportService();
                            if (dialogService.SaveFileDialog()) 
                            {
                                ExportTypeData exportType = dialogService.ExportType;
                                MemoryStream stream;
                                switch (exportType)
                                {
                                    case ExportTypeData.Excel:
                                        stream = new ExcelService.ExcelService().GetDocument(ProjectDocuments);
                                        File.WriteAllBytes(dialogService.FilePath, stream.ToArray());
                                        break;
                                    case ExportTypeData.Pdf:
                                        stream = new PdfService().GetPdf(ProjectDocuments);
                                        File.WriteAllBytes(dialogService.FilePath, stream.ToArray());
                                        break;

                                }
                                
                            }
                        }
                        catch
                        {
                            // dialogService.ShowMessage(ex.Message);
                        }
                    }));
            }
        }

        public bool OnlyFilled
        {
            get => onlyFilled;
            set
            {
                onlyFilled = value;
                OnPropertyChanged("OnlyNotFilled");
                ProjectDocumentsFilter();
            }
        }



        public int SelectProjectId
        {
            get => selectProjectId;
            set
            {
                selectProjectId = value;
                ProjectDocumentsFilter();
                OnPropertyChanged("SelectProjectId");
            }
        }

        public int SelectField
        {
            get => selectField;
            set
            {
                selectField = value;
                ProjectDocumentsFilter();
                OnPropertyChanged("SelectField");
            }
        }


        public int SelectStageId
        {
            get => selectStageId;
            set
            {
                selectStageId = value;
                ProjectDocumentsFilter();
                OnPropertyChanged("SelectStageId");
            }
        }


        public ObservableCollection<Project>? Projects
        {
            get => projects;
            set
            {
                projects = value;
                OnPropertyChanged("Projects");
            }
        }

        public ObservableCollection<ProjectDocument>? ProjectDocuments
        {
            get => projectsDocuments;
            set
            {
                projectsDocuments = value;
                OnPropertyChanged("ProjectDocuments");
            }
        }


        public ObservableCollection<Stage>? Stages
        {
            get => stages;
            set
            {
                stages = value;
                OnPropertyChanged("Stages");
            }
        }

        public ObservableCollection<Field>? Fields
        {
            get => fields;
            set
            {
                fields = value;
                OnPropertyChanged("Fields");
            }
        }
    }
}