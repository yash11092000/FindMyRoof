using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IMasterRepository
    {
        Task<bool> SavePropCategory(PropertyCategoryMaster propertyCategoryMaster);
        Task<DataTableResult> ListPropertyCategory(DataTablePara dataTablePara);
        Task<PropertyCategoryMaster> EditPropertyCategory(int UniqueID, int UserID);
        Task<bool> DeletePropertyCategory(PropertyCategoryMaster PropertyCategoryMaster);
        Task<bool> SavePropType(PropertyTypeMaster PropertyTypeMaster);
        Task<bool> DeletePropertyType(PropertyTypeMaster PropertyTypeMaster);
        Task<DataTableResult> ListPropertyType(DataTablePara dataTablePara);
        Task<bool> SaveRentalType(RentalTypeMaster RentalTypeMaster);
        Task<PropertyTypeMaster> EditPropertyType(int UniqueID, int UserID);
    }
}
