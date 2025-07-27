namespace PhysioWeb.Models
{
    public class DataTableResult :CommanProp
    {
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public object aaData { get; set; }
    }
}
