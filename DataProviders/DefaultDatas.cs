using WPF_Test.Models;

namespace WPF_Test.DataProviders
{
    public class DefaultDatas
    {
        public static Project project1 = new() { Id = 1, Name="Проект 1", AdditionalDescription="Первый тестовый проект"};
        public static Project project2 = new() { Id = 2,Name="Проект 2", AdditionalDescription="Второй тестовый проект"};
    }
}