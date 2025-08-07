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
        public DropDownSource(IDataReader Idr, bool ForPages = false)
        {
            if (ForPages)
            {
                populateObjectForPages(this, Idr);
            }
            else
            {
                populateObject(this, Idr);
            }
        }

        private void populateObjectForPages(DropDownSource obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("Value")))
            {
                obj.Value = rdr.GetString(rdr.GetOrdinal("Value"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Text")))
            {
                obj.Text = rdr.GetString(rdr.GetOrdinal("Text"));
            }
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
