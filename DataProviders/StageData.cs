using System.Collections.ObjectModel;
using WPF_Test.Models;

namespace WPF_Test.DataProviders
{
    public class StageData
    {
        public ObservableCollection<Stage> Stages { get; } = null!;
        public StageData()
        {
            Stages = new()
            {
                new Stage { Id = 1, Name="ОПР", Explanation=""},
                new Stage { Id = 2, Name="ППР", Explanation=""},
                new Stage { Id = 3, Name="ПД", Explanation=""},
                new Stage { Id = 4, Name="РД", Explanation=""},
            };
        }
    }
}