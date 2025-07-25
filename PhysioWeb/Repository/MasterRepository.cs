using PhysioWeb.Data;
using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public class MasterRepository : IMasterRepository
    {
        private readonly DbHelper _dbHelper;

        public MasterRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;

        }
        public async Task<bool> SavePropCategory(PropertyCategoryMaster propertyCategoryMaster)
        {
            string[] parametersName = { "UniquId","PropertyCategory", "IsActive" };
            object[] Values = { propertyCategoryMaster.UniquId,propertyCategoryMaster.CategoryName, propertyCategoryMaster.IsActive };

            string Sp = "FMR_SavePropertyCategory";
            int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
            return RecordAffected > 0;
        }
        public async Task<bool> SavePropType(PropertyTypeMaster PropertyTypeMaster)
        {
            string[] parametersName = { "UniquId", "PropertyType", "Description", "IsActive" };
            object[] Values = { PropertyTypeMaster.UniquId,PropertyTypeMaster.PropertyType, PropertyTypeMaster.Description,
                PropertyTypeMaster.IsActive };

            string Sp = "FMR_SavePropertyType";
            int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
            return RecordAffected > 0;
        }
    }
}
