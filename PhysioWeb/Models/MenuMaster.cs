using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace PhysioWeb.Models
{
    public class MenuMaster : CommanProp
    {
        public List<MenuDetails> MenuList { get; set; }

        public MenuMaster()
        {
            MenuList = new List<MenuDetails>();
        }
    }

    public class MenuDetails
    {
        public int MenuId { get; set; }

        public string MenuName { get; set; }

        public string MenuUrl { get; set; }

        public string MenuIcon { get; set; }
        public MenuDetails()
        {

        }

        public MenuDetails(IDataReader Idr)
        {
            populateObject(this, Idr);
        }

        private void populateObject(MenuDetails obj, IDataReader rdr)
        {

            if (!rdr.IsDBNull(rdr.GetOrdinal("MenuId")))
            {
                obj.MenuId = rdr.GetInt32(rdr.GetOrdinal("MenuId"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("MenuName")))
            {
                obj.MenuName = rdr.GetString(rdr.GetOrdinal("MenuName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("MenuUrl")))
            {
                obj.MenuUrl = rdr.GetString(rdr.GetOrdinal("MenuUrl"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("MenuIcon")))
            {
                obj.MenuIcon = rdr.GetString(rdr.GetOrdinal("MenuIcon"));
            }

        }
    }
}
