using System.Collections.ObjectModel;
using WPF_Test.Models;

namespace WPF_Test.DataProviders
{
    public class DocumentGroup
    {
        public ObservableCollection<Field> Fields { get;}
        public ObservableCollection<Document> Documents { get;}
        public DocumentGroup()
        {
            Field  technology = new(){Id=1, Name = "Технология", Code="PR"};
            Field  electric = new(){Id=2, Name = "Электрика", Code="EL"};

            Fields = new(){ technology, electric};

            Documents = new ()
            {
                new Document{ Id = 1, Name="Схема сетей", Field=technology},
                new Document { Id = 2, Name="Расчет нагрузки", Field=technology},
                new Document { Id = 3, Name="Расчет мощности", Field=electric},
                new Document { Id = 4, Name="Схема цепи", Field=electric},
            };
        }
    }
}