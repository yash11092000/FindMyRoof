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
    }
}
