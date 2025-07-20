namespace PhysioWeb.Models
{
    public class AmenityMaster : CommanProp
    {
        public int AmenityID { get; set; }
        public string AmenityName { get; set; }
        public string IconImage { get; set; } 
        public bool IsActive { get; set; }
    }


}
