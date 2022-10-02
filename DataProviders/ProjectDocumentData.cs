using System;
using System.Collections.ObjectModel;
using System.Linq;
using WPF_Test.Models;

namespace WPF_Test.DataProviders
{
    public class ProjectDocumentData
    {
        public ObservableCollection<ProjectDocument> GetData(bool onlyFilled, int selectProjectId, int selectStageId, int selectField)
        {
            ////заполним дефолтными данными если данные пусты           
            if (projects.Count == 0)
            {
                FillDefaultData();
            }
            int quantity = onlyFilled ? 1 : 0;
            var documents = projects.Where(pr => pr?.Quantity >= quantity
                                            && pr?.Project.Id == selectProjectId
                                            && pr?.Stage.Id == selectStageId).ToList();
            if (selectField != 0)
            {
                documents = documents.Where(pr => pr?.Document.Field.Id == selectField).ToList();
            }
            return new ObservableCollection<ProjectDocument>(documents);
        }
        private void FillDefaultData()
        {
            foreach(var document in new DocumentGroup().Documents)
            {
                Random rnd = new Random();
                int standard = rnd.Next(1, 3);

                foreach(var project in new ProjectData().Projects)
                {
                    foreach(var stage in new StageData().Stages)
                    {
                        projects.Add(new(){
                            Project = project,
                            Stage = stage,
                            Document = document,
                            Standard = standard,
                            Code = $"{document.Field.Code}-{document.Id}"
                        });
                    }
                }
            }
        }

        

        private static ObservableCollection<ProjectDocument> projects = new();
    }
}