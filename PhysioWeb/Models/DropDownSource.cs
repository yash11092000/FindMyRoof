using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace PhysioWeb.Models
{
    public class DropDownSource
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public DropDownSource()
        {

        }
        public DropDownSource(IDataReader Idr)
        {
            populateObject(this, Idr);
        }

        private void populateObject(DropDownSource obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("Url")))
            {
                obj.Value = rdr.GetString(rdr.GetOrdinal("Url"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Text")))
            {
                obj.Text = rdr.GetString(rdr.GetOrdinal("Text"));
            }

        }
    }
}
