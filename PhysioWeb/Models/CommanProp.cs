namespace PhysioWeb.Models
{
    public class CommanProp
    {
        public int UniquId { get; set; }
        public string AgencyId { get; set; }
        public string UserID { get; set; }
        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public int TotalCount { get; set; }
        public int RowNo { get; set; }
    }
}
