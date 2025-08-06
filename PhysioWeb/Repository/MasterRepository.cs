using Microsoft.AspNetCore.Components;
using PhysioWeb.Data;
using PhysioWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


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
            try
            {
                string[] parametersName = { "UniquId", "PropertyCategory", "IsActive", "UserID" };
                object[] Values = { propertyCategoryMaster.UniquId, propertyCategoryMaster.CategoryName,
                    propertyCategoryMaster.IsActive,propertyCategoryMaster.AgencyId };

                string Sp = "FMR_SavePropertyCategory";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }
        public async Task<DataTableResult> ListPropertyCategory(DataTablePara dataTablePara)
        {
            try
            {
                string[] parameterName = new string[]
                {
                    "DisplayLength", "DisplayStart", "SortCol", "SortDir", "Search",
                    "PropertyCategory",  "IsActive", "CreatedBy", "AgencyId"
                };

                object[] parameterValue = new object[]
                {
                    dataTablePara.iDisplayLength,dataTablePara.iDisplayStart,dataTablePara.iSortCol_0,
                    dataTablePara.sSortDir_0,dataTablePara.sSearch,dataTablePara.sSearch_0,
                    dataTablePara.sSearch_1,dataTablePara.sSearch_2,dataTablePara.AgencyId
                };

                var reader = await _dbHelper.GetDataReaderAsync("[FMR_DataListPropertyCategory]", parameterName, parameterValue);

                var result = new DataTableResult();
                var list = new List<PropertyCategoryMaster>();

                while (reader.Read())
                {
                    list.Add(new PropertyCategoryMaster(reader));
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        result.iTotalRecords = Convert.ToInt32(reader[0]);
                    }
                }

                result.iTotalDisplayRecords = result.iTotalRecords;
                result.aaData = list;

                return result;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<PropertyCategoryMaster> EditPropertyCategory(int UniqueID, int UserID)
        {
            //why there is no try catch??
            try
            {
                string[] parameterNames = { "UniqueID", "UserID" };
                object[] parameterValues = { UniqueID, UserID };


                string Sp = "FMR_EditPropertyCategory";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
                while (data.Read())
                {
                    PropertyCategoryMaster PropertyCategoryMaster = new PropertyCategoryMaster(data, 1);
                    return PropertyCategoryMaster;
                }
                return null;

                //bind 
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> DeletePropertyCategory(PropertyCategoryMaster PropertyCategoryMaster)
        {
            try
            {
                string[] parametersName = { "UniquId", "UserID" };
                object[] Values = { PropertyCategoryMaster.UniquId, 0 };

                string Sp = "FMR_DeletePropertyCategory";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }
        public async Task<bool> SavePropType(PropertyTypeMaster PropertyTypeMaster)
        {
            try
            {
                string[] parametersName = { "UniquId", "PropertyType", "Description", "IsActive", "UserID" };
                object[] Values = { PropertyTypeMaster.UniquId,PropertyTypeMaster.PropertyType, PropertyTypeMaster.Description,
                PropertyTypeMaster.IsActive ,0 };

                string Sp = "FMR_SavePropertyType";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }
        public async Task<bool> DeletePropertyType(PropertyTypeMaster PropertyTypeMaster)
        {
            try
            {
                string[] parametersName = { "UniquId" };
                object[] Values = { PropertyTypeMaster.UniquId };

                string Sp = "FMR_DeletePropertyType";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }
        public async Task<DataTableResult> ListPropertyType(DataTablePara dataTablePara)
        {
            try
            {
                string[] parameterName = new string[]
                {
                    "DisplayLength", "DisplayStart", "SortCol", "SortDir", "Search",
                    "PropertyType", "Description", "IsActive", "CreatedBy", "AgencyId"
                };

                object[] parameterValue = new object[]
                {
                    dataTablePara.iDisplayLength,dataTablePara.iDisplayStart,dataTablePara.iSortCol_0,
                    dataTablePara.sSortDir_0,dataTablePara.sSearch,dataTablePara.sSearch_0,
                    dataTablePara.sSearch_1,dataTablePara.sSearch_2,dataTablePara.sSearch_3,dataTablePara.AgencyId
                };

                var reader = await _dbHelper.GetDataReaderAsync("[FMR_DataListPropertyType]", parameterName, parameterValue);

                var result = new DataTableResult();
                var list = new List<PropertyTypeMaster>();

                while (reader.Read())
                {
                    list.Add(new PropertyTypeMaster(reader));
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        result.iTotalRecords = Convert.ToInt32(reader[0]);
                    }
                }

                result.iTotalDisplayRecords = result.iTotalRecords;
                result.aaData = list;

                return result;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<bool> SaveRentalType(RentalTypeMaster RentalTypeMaster)
        {
            try
            {
                string[] parametersName = { "UniquId", "RentalType", "Description", "IsActive", "UserID" };
                object[] Values = { RentalTypeMaster.UniquId, RentalTypeMaster.RentalType, RentalTypeMaster.Description,
                    RentalTypeMaster.IsActive, RentalTypeMaster.AgencyId };

                string Sp = "FMR_SaveRentalType";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }

        }
        public async Task<PropertyTypeMaster> EditPropertyType(int UniqueID, int UserID)
        {
            //why there is no try catch??
            try
            {
                string[] parameterNames = { "UniqueID", "UserID" };
                object[] parameterValues = { UniqueID, UserID };


                string Sp = "FMR_EditPropertyType";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
                while (data.Read())
                {
                    PropertyTypeMaster PropertyTypeMaster = new PropertyTypeMaster(data, 1);
                    return PropertyTypeMaster;
                }
                return null;

                //bind 
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<DataTableResult> ListRentalType(DataTablePara dataTablePara)
        {
            try
            {
                string[] parameterName = new string[]
                {
                    "DisplayLength", "DisplayStart", "SortCol", "SortDir", "Search",
                    "RentalType", "Description", "IsActive", "CreatedBy", "AgencyId"
                };

                object[] parameterValue = new object[]
                {
                    dataTablePara.iDisplayLength,dataTablePara.iDisplayStart,dataTablePara.iSortCol_0,
                    dataTablePara.sSortDir_0,dataTablePara.sSearch,dataTablePara.sSearch_0,
                    dataTablePara.sSearch_1,dataTablePara.sSearch_2,dataTablePara.sSearch_3,dataTablePara.AgencyId
                };

                var reader = await _dbHelper.GetDataReaderAsync("[FMR_DataListRentalType]", parameterName, parameterValue);

                var result = new DataTableResult();
                var list = new List<RentalTypeMaster>();

                while (reader.Read())
                {
                    list.Add(new RentalTypeMaster(reader));
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        result.iTotalRecords = Convert.ToInt32(reader[0]);
                    }
                }

                result.iTotalDisplayRecords = result.iTotalRecords;
                result.aaData = list;

                return result;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }


        public async Task<RentalTypeMaster> EditRentalType(int UniqueID, int UserID)
        {

            try
            {
                string[] parameterNames = { "UniqueID", "UserID" };
                object[] parameterValues = { UniqueID, UserID };


                string Sp = "FMR_EditRentalType";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
                while (data.Read())
                {
                    RentalTypeMaster RentalTypeMaster = new RentalTypeMaster(data, 1);
                    return RentalTypeMaster;
                }
                return null;

                //bind 
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteRentalType(RentalTypeMaster RentalTypeMaster)
        {
            try
            {
                string[] parametersName = { "UniquId", "UserID" };
                object[] Values = { RentalTypeMaster.UniquId, RentalTypeMaster.AgencyId };

                string Sp = "FMR_DeleteRentalType";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<bool> SaveFurnishingType(FurnishingTypeMaster FurnishingTypeMaster)
        {
            try
            {
                string[] parametersName = { "UniquId", "FurnishingType", "IsActive", "UserID" };
                object[] Values = { FurnishingTypeMaster.UniquId,FurnishingTypeMaster.FurnishingType,
                FurnishingTypeMaster.IsActive ,0 };

                string Sp = "FMR_SaveFurnishingType";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }
        public async Task<bool> DeleteFurnishingType(FurnishingTypeMaster FurnishingTypeMaster)
        {
            try
            {
                string[] parametersName = { "UniquId", "UserID" };
                object[] Values = { FurnishingTypeMaster.UniquId, FurnishingTypeMaster.AgencyId };

                string Sp = "FMR_DeleteFurnishingType";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<DataTableResult> ListFurnishingType(DataTablePara dataTablePara)
        {
            try
            {
                string[] parameterName = new string[]
                {
                    "DisplayLength", "DisplayStart", "SortCol", "SortDir", "Search",
                    "FurnishingType", "IsActive", "CreatedBy", "AgencyId"
                };

                object[] parameterValue = new object[]
                {
                    dataTablePara.iDisplayLength,dataTablePara.iDisplayStart,dataTablePara.iSortCol_0,
                    dataTablePara.sSortDir_0,dataTablePara.sSearch,dataTablePara.sSearch_0,
                    dataTablePara.sSearch_1,dataTablePara.sSearch_2,dataTablePara.AgencyId
                };

                var reader = await _dbHelper.GetDataReaderAsync("[FMR_DataListFurnishingType]", parameterName, parameterValue);

                var result = new DataTableResult();
                var list = new List<FurnishingTypeMaster>();

                while (reader.Read())
                {
                    list.Add(new FurnishingTypeMaster(reader));
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        result.iTotalRecords = Convert.ToInt32(reader[0]);
                    }
                }

                result.iTotalDisplayRecords = result.iTotalRecords;
                result.aaData = list;

                return result;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }
        public async Task<FurnishingTypeMaster> EditFurnishingType(int UniqueID, int UserID)
        {

            try
            {
                string[] parameterNames = { "UniqueID", "UserID" };
                object[] parameterValues = { UniqueID, UserID };


                string Sp = "FMR_EditFurnishingType";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
                while (data.Read())
                {
                    FurnishingTypeMaster FurnishingTypeMaster = new FurnishingTypeMaster(data, 1);
                    return FurnishingTypeMaster;
                }
                return null;

                //bind 
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> SaveProperty(PropertyMaster propertyMaster)
        {
            try
            {

                string[] parameterNames = { "UniquID", "PropertyName", "Description", "PropertyType", "Bedrooms", "Bathrooms", "CarpetArea", "BuiltUpArea", "Address", "City", "State", "PinCode", "MinPrice", "MaxPrice", "FurnishingStatus", "PossessionDate", "IsActive" };
                object[] parameterValues = { propertyMaster.UniquId, propertyMaster.Title, propertyMaster.Description, propertyMaster.PropertyType, propertyMaster.Bedrooms, propertyMaster.Bathrooms, propertyMaster.CarpetArea, propertyMaster.BuiltUpArea, propertyMaster.Address, propertyMaster.City, propertyMaster.State, propertyMaster.PinCode, propertyMaster.BudgetMin, propertyMaster.BudgetMax, propertyMaster.FurnishingStatus, propertyMaster.PossessionDate.HasValue ? propertyMaster.PossessionDate.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value, propertyMaster.IsActive };
                string Sp = "FMR_SavePropertyDetails";
                var data = await _dbHelper.ExecuteScalarAsync(Sp, parameterNames, parameterValues);
                return Convert.ToInt32(data);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> SavePropertyMedia(DataTable mediaTable, int propertyId)
        {
            try
            {
                string[] parametersName = { "PropertyId", "MediaFiles" };
                object[] Values = { propertyId, mediaTable };
                SqlDbType[] paramTypes = { SqlDbType.Int, SqlDbType.Structured };
                string[] tvpTypeNames = { null, "dbo.PropertyImageType" };

                string Sp = "FMR_InsertPropertyMedia";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values, paramTypes, tvpTypeNames);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<PropertyMaster> PropertyMasterDropDown()
        {
            try
            {
                string[] parameterNames = { };
                object[] parameterValues = { };


                string Sp = "FMR_PropertyMasterDropDown";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
                PropertyMaster propertyMaster = new PropertyMaster();
                while (data.Read())
                {
                    propertyMaster.CountryList.Add(new DropDownSource(data));
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.StateList.Add(new DropDownSource(data));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.CityList.Add(new DropDownSource(data));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.PropertyCategoryList.Add(new DropDownSource(data));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.AreaList.Add(new DropDownSource(data));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.PropertyTypeList.Add(new DropDownSource(data));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.FurnishingTypeList.Add(new DropDownSource(data));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.RentalTypeList.Add(new DropDownSource(data));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.BedRoomList.Add(new DropDownSource(data));
                    }
                }
                return propertyMaster;


            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}