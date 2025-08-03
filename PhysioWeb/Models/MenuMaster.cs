namespace PhysioWeb.Models
{
    public class MenuMaster
    {
        public List<DropDownSource> MenuList { get; set; }

        public MenuMaster()
        {
            MenuList = new List<DropDownSource>();
        }
    }
}
