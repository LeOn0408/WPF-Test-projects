namespace WPF_Test.Models
{
    public class ProjectDocument
    {
        public Project Project { get; set;} = null!;
        public Stage Stage { get; set;} = null!;
        public Document Document  { get; set;} = null!;  
        public string Code { get; set;} = null!;    
        public int Standard { get; set;}
        public int Quantity { get; set;} = 0;
        public int Total { get; set;}
    }
}