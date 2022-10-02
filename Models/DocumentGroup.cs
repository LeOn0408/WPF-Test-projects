using System.Collections.Generic;

namespace WPF_Test.Models
{
    public class Document
    {
        public int Id {get;set;}
        public string Name { get; set;}=null!;  
        public Field Field { get; set;} = null!; 
    }

    public class Field
    {
        public int Id {get;set;}
        public string Name { get; set;}=null!;
        public string Code { get; set;}=null!;
    }
    
}