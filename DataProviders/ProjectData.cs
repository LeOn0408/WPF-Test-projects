using System.Collections.ObjectModel;
using WPF_Test.Models;

namespace WPF_Test.DataProviders
{
    public class ProjectData
    {
        public ProjectData()
        {
            Projects = new()
            {
                new Project { Id = 1, Name="Проект 1", AdditionalDescription="Первый тестовый проект"},
                new Project { Id = 2,Name="Проект 2", AdditionalDescription="Второй тестовый проект"},
            };
        }

        public ObservableCollection<Project> Projects { get;} = null!;
        

    }
}