namespace PhysioWeb.Models
{
    public class AreaMaster : CommanProp
    {
        public string AreaName { get; set; }
        public string Pincode { get; set; }
        public string LatitudeLongitude { get; set; }
        public bool IsActive { get; set; }
        public List<DropDownSource> State { get; set; }
        public List<DropDownSource> City { get; set; }
        public List<DropDownSource> Country { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }

    }
}
