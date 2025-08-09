using System.Data;
using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IMasterRepository
    {
        Task<bool> SavePropCategory(PropertyCategoryMaster propertyCategoryMaster);
        Task<DataTableResult> ListPropertyCategory(DataTablePara dataTablePara);
        Task<PropertyCategoryMaster> EditPropertyCategory(int UniqueID, string AgencyID);
        Task<bool> DeletePropertyCategory(PropertyCategoryMaster PropertyCategoryMaster);
        Task<bool> SavePropType(PropertyTypeMaster PropertyTypeMaster);
        Task<bool> DeletePropertyType(PropertyTypeMaster PropertyTypeMaster);
        Task<DataTableResult> ListPropertyType(DataTablePara dataTablePara);
        Task<bool> SaveRentalType(RentalTypeMaster RentalTypeMaster);
        Task<PropertyTypeMaster> EditPropertyType(int UniqueID, string UserID);
        Task<DataTableResult> ListRentalType(DataTablePara dataTablePara);
        Task<RentalTypeMaster> EditRentalType(int UniqueID, int UserID);
        Task<bool> DeleteRentalType(RentalTypeMaster RentalTypeMaster);
        Task<bool> SaveFurnishingType(FurnishingTypeMaster FurnishingTypeMaster);
        Task<bool> DeleteFurnishingType(FurnishingTypeMaster FurnishingTypeMaster);
        Task<DataTableResult> ListFurnishingType(DataTablePara dataTablePara);
        Task<FurnishingTypeMaster> EditFurnishingType(int UniqueID, int UserID);
        Task<bool> SaveAmenityMaster(AmenityMaster AmenityMaster);
        Task<bool> DeleteAmenityMaster(AmenityMaster AmenityMaster);
        Task<DataTableResult> ListAmenityMaster(DataTablePara dataTablePara);
        Task<AmenityMaster> EditAmenityMaster(int UniqueID, int UserID);
        Task<AreaMaster> EditAreaMaster(int UniqueID, int UserID);
        Task<DataTableResult> ListAreaMaster(DataTablePara dataTablePara);
        Task<bool> SaveAreaMaster(AreaMaster AreaMaster);
        Task<bool> DeleteAreaMaster(AreaMaster AreaMaster);
        Task<List<DropDownSource>> GetCountryList();
        Task<List<DropDownSource>> GetStateList(string countryId);
        Task<List<DropDownSource>> GetCityList(string stateId);
        Task<int> SaveProperty(PropertyMaster propertyMaster);
        Task<bool> SavePropertyMedia(DataTable mediaTable, int propertyId);
        Task<PropertyMaster> PropertyMasterDropDown();
        Task<List<DropDownSource>> GetAreaList(string searchTerm);
    
    }
}
