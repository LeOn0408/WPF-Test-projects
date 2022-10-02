using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using WPF_Test.Models;

namespace WPF_Test
{
    public class DataTableConverter
    {
        public DataTable ToDataTable(ObservableCollection<ProjectDocument> items)
        {
            var tb = new DataTable();

            tb.Columns.Add("Направление");
            tb.Columns.Add("Код");
            tb.Columns.Add("Документ");
            tb.Columns.Add("Норматив чел./час");
            tb.Columns.Add("Кол-во");
            tb.Columns.Add("Итого(все)");
            
            
            foreach (ProjectDocument item in items)
            {
                var values = new object[6];
                values[0] = item.Document.Field.Name;
                values[1] = item.Code;
                values[2] = item.Document.Name;
                values[3] = item.Standard;
                values[4] = item.Quantity;
                values[5] = item.Total;

                tb.Rows.Add(values); 
            }

            return tb;
        }
    }
}