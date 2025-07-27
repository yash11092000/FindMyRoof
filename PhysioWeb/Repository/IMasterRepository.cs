using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IMasterRepository
    {
        Task<bool> SavePropCategory(PropertyCategoryMaster propertyCategoryMaster);
        Task<bool> SavePropType(PropertyTypeMaster PropertyTypeMaster);
        Task<DataTableResult> ListPropertyType(DataTablePara dataTablePara);
        Task<bool> SaveRentalType(RentalTypeMaster RentalTypeMaster);
    }
}
