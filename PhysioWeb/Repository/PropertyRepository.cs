using PhysioWeb.Data;
using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DbHelper _dbHelper;

        public PropertyRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<PropertyMaster> GetPropertyDetails(int propertyId)
        {
            try
            {
                string[] parameterNames = { "PropertyId" };
                object[] parameterValues = { propertyId };


                string Sp = "FMR_GetPropertyDetailsById";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
                PropertyMaster Property;
                while (data.Read())
                {
                    Property = new PropertyMaster(data);
                    if (data.NextResult())
                    {
                        while (data.Read())
                        {

                            Property.Images.Add(new DropDownSource(data, true));
                        }
                    }
                    if (data.NextResult())
                    {
                        while (data.Read())
                        {

                            Property.Videos.Add(new DropDownSource(data, true));
                        }
                    }
                    if (data.NextResult())
                    {
                        while (data.Read())
                        {
                            Property.AmenityList.Add(new DropDownSource(data, true));
                        }
                    }

                    return Property;
                }


                return new PropertyMaster();


            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
