using Microsoft.AspNetCore.Components;
using PhysioWeb.Data;
using PhysioWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System.Threading.Tasks;
using System.Data.Common;
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
                string[] parametersName = { "UniquId", "PropertyCategory", "IsActive", "UserID", "AgencyId" };
                object[] Values = { propertyCategoryMaster.UniquId, propertyCategoryMaster.CategoryName,
                    propertyCategoryMaster.IsActive,propertyCategoryMaster.UserID ,propertyCategoryMaster.AgencyId};

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

        public async Task<PropertyCategoryMaster> EditPropertyCategory(int UniqueID, string AgencyID)
        {
            //why there is no try catch??
            try
            {
                string[] parameterNames = { "UniqueID", "AgencyID" };
                object[] parameterValues = { UniqueID, AgencyID };


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
                object[] Values = { PropertyCategoryMaster.UniquId, PropertyCategoryMaster.UserID };

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
                string[] parametersName = { "UniquId", "PropertyType", "Description", "IsActive", "AgencyID", "UserID" };
                object[] Values = { PropertyTypeMaster.UniquId,PropertyTypeMaster.PropertyType, PropertyTypeMaster.Description,
                PropertyTypeMaster.IsActive ,PropertyTypeMaster.AgencyId ,PropertyTypeMaster.UserID };

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
                string[] parametersName = { "UniquId", "UserID" };
                object[] Values = { PropertyTypeMaster.UniquId, PropertyTypeMaster.UserID };

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
                string[] parametersName = { "UniquId", "RentalType", "Description", "IsActive", "UserID", "AgencyId" };
                object[] Values = { RentalTypeMaster.UniquId, RentalTypeMaster.RentalType, RentalTypeMaster.Description,
                    RentalTypeMaster.IsActive,RentalTypeMaster.UserID ,RentalTypeMaster.AgencyId };

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
        public async Task<PropertyTypeMaster> EditPropertyType(int UniqueID, string UserID)
        {
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


        public async Task<RentalTypeMaster> EditRentalType(int UniqueID, string UserID)
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
                object[] Values = { RentalTypeMaster.UniquId, RentalTypeMaster.UserID };

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
                string[] parametersName = { "UniquId", "FurnishingType", "IsActive", "UserID", "AgencyID" };
                object[] Values = { FurnishingTypeMaster.UniquId,FurnishingTypeMaster.FurnishingType,
                FurnishingTypeMaster.IsActive ,FurnishingTypeMaster.UserID ,FurnishingTypeMaster.AgencyId};

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
                object[] Values = { FurnishingTypeMaster.UniquId, FurnishingTypeMaster.UserID };

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

        public async Task<bool> SaveAmenityMaster(AmenityMaster AmenityMaster)
        {
            try
            {
                string[] parametersName = { "UniquId", "AmenityName", "IconImage", "IsActive",
                    "UserID" ,"AgencyID"};
                object[] Values = { AmenityMaster.UniquId,AmenityMaster.AmenityName,AmenityMaster.IconImage,
                AmenityMaster.IsActive ,AmenityMaster.UserID , AmenityMaster.AgencyId };

                string Sp = "FMR_SaveAmenityMaster";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }
        public async Task<bool> DeleteAmenityMaster(AmenityMaster AmenityMaster)
        {
            try
            {
                string[] parametersName = { "UniquId", "UserID" };
                object[] Values = { AmenityMaster.UniquId, AmenityMaster.UserID };

                string Sp = "FMR_DeleteAmenityMaster";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<DataTableResult> ListAmenityMaster(DataTablePara dataTablePara)
        {
            try
            {
                string[] parameterName = new string[]
                {
                    "DisplayLength", "DisplayStart", "SortCol", "SortDir", "Search",
                    "AmenityName", "IsActive", "CreatedBy", "AgencyId"
                };

                object[] parameterValue = new object[]
                {
                    dataTablePara.iDisplayLength,dataTablePara.iDisplayStart,dataTablePara.iSortCol_0,
                    dataTablePara.sSortDir_0,dataTablePara.sSearch,dataTablePara.sSearch_0,
                    dataTablePara.sSearch_1,dataTablePara.sSearch_2,dataTablePara.AgencyId
                };

                var reader = await _dbHelper.GetDataReaderAsync("[FMR_DataListAmenityMaster]", parameterName, parameterValue);

                var result = new DataTableResult();
                var list = new List<AmenityMaster>();

                while (reader.Read())
                {
                    list.Add(new AmenityMaster(reader));
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
        public async Task<AmenityMaster> EditAmenityMaster(int UniqueID, int UserID)
        {

            try
            {
                string[] parameterNames = { "UniqueID", "UserID" };
                object[] parameterValues = { UniqueID, UserID };


                string Sp = "FMR_EditAmenityMaster";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
                while (data.Read())
                {
                    AmenityMaster AmenityMaster = new AmenityMaster(data, 1);
                    return AmenityMaster;
                }
                return null;

                //bind 
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<AreaMaster> EditAreaMaster(int UniqueID, string UserID)
        {

            try
            {
                string[] parameterNames = { "UniqueID", "UserID" };
                object[] parameterValues = { UniqueID, UserID };


                string Sp = "FMR_EditAreaMaster";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
                while (data.Read())
                {
                    AreaMaster AreaMaster = new AreaMaster(data, 1);
                    return AreaMaster;
                }
                return null;

                //bind 
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<DataTableResult> ListAreaMaster(DataTablePara dataTablePara)
        {
            try
            {
                string[] parameterName = new string[]
                {
                    "DisplayLength", "DisplayStart", "SortCol", "SortDir", "Search",
                    "AreaName","SubAreaName", "City","IsActive", "CreatedBy", "AgencyId"
                };

                object[] parameterValue = new object[]
                {
                    dataTablePara.iDisplayLength,dataTablePara.iDisplayStart,dataTablePara.iSortCol_0,
                    dataTablePara.sSortDir_0,dataTablePara.sSearch,dataTablePara.sSearch_0,
                    dataTablePara.sSearch_1,dataTablePara.sSearch_2,dataTablePara.sSearch_3,
                    dataTablePara.sSearch_4,dataTablePara.AgencyId
                };


                var reader = await _dbHelper.GetDataReaderAsync("[FMR_DataListAreaMaster]", parameterName, parameterValue);

                var result = new DataTableResult();
                var list = new List<AreaMaster>();

                while (reader.Read())
                {
                    list.Add(new AreaMaster(reader));
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

        public async Task<bool> SaveAreaMaster(AreaMaster AreaMaster)
        {
            try
            {
                string[] parametersName = {
                "UniquId", "AreaName", "SubAreaName", "City", "State", "Country",
                "Pincode", "IsActive", "UserID" , "AgencyID"
            };

                object[] Values = {
                        AreaMaster.UniquId,
                        AreaMaster.AreaName,
                        AreaMaster.SubAreaName,
                        AreaMaster.City,
                        AreaMaster.State,
                        AreaMaster.Country,
                        AreaMaster.Pincode,
                        AreaMaster.IsActive,
                        AreaMaster.UserID,
                        AreaMaster.AgencyId
                    };


                string Sp = "FMR_SaveAreaMaster";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }
        public async Task<bool> DeleteAreaMaster(AreaMaster AreaMaster)
        {
            try
            {
                string[] parametersName = { "UniquId", "UserID" };
                object[] Values = { AreaMaster.UniquId, AreaMaster.UserID };

                string Sp = "FMR_DeleteAreaMaster";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<List<DropDownSource>> GetCountryList()
        {
            string[] parameterNames = new string[] { };
            object[] parameterValues = new object[] { };

            var list = new List<DropDownSource>();

            using (var reader = await _dbHelper.GetDataReaderAsync("FMR_GetCountryList", parameterNames, parameterValues))
            {
                while (reader.Read())
                {
                    list.Add(new DropDownSource
                    {
                        Value = reader["Value"].ToString(),
                        Text = reader["Text"].ToString()
                    });
                }
            }

            return list;
        }

        public async Task<List<DropDownSource>> GetStateList(string countryId)
        {
            string[] parameterNames = new string[] { "countryId" };
            object[] parameterValues = new object[] { countryId };


            string Sp = "FMR_GetStateList";
            var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
            var list = new List<DropDownSource>();
            while (data.Read())
            {
                list.Add(new DropDownSource
                {
                    Value = data["Value"].ToString(),
                    Text = data["Text"].ToString()
                });
            }
            return list;

        }
        public async Task<List<DropDownSource>> GetCityList(string stateId)
        {
            string[] parameterNames = new string[] { "stateId" };
            object[] parameterValues = new object[] { stateId };

            var list = new List<DropDownSource>();

            using (var reader = await _dbHelper.GetDataReaderAsync("FMR_GetCityList", parameterNames, parameterValues))
            {
                while (reader.Read())
                {
                    list.Add(new DropDownSource
                    {
                        Value = reader["Value"].ToString(),
                        Text = reader["Text"].ToString()
                    });
                }
            }

            return list;
        }

        public async Task<List<DropDownSource>> GetAreaList(string searchTerm, string AgencyID)
        {
            string[] parameterNames = new string[] { "@SearchTerm", "AgencyID" };
            object[] parameterValues = new object[] { searchTerm ?? (object)DBNull.Value, AgencyID };

            var list = new List<DropDownSource>();

            using (var reader = await _dbHelper.GetDataReaderAsync("FMR_GetGetAreaList", parameterNames, parameterValues))
            {
                while (reader.Read())
                {
                    list.Add(new DropDownSource
                    {
                        Value = reader["Value"].ToString(),
                        Text = reader["Text"].ToString()
                    });
                }
            }

            return list;
        }

        public async Task<int> SaveProperty(PropertyMaster propertyMaster)
        {
            try
            {
                string[] parameterNames = { "UniquID", "PropertyName", "Description",
                    "PropertyType", "Bedrooms", "Bathrooms", "CarpetArea", "BuiltUpArea",
                    "Address", "City", "State", "PinCode", "MinPrice", "MaxPrice",
                    "FurnishingStatus", "PossessionDate", "IsActive", "TransactionType",
                    "Floor", "ContactPersonName", "ContactPersonNo", "AlternateNo", "Area",
                    "SubArea", "Country", "Amenities","UserID" ,"AgencyID" ,"Vastu" , "YearOfConstruction" , "PropertyCategory",
                    "PreferedBuyerType" , "AmountUnitMinPrice" , "AmountUnitMaxPrice" ,"ConvertedActualPrice" , "ConvertedNegotiablePrice",
                    "TotalFloorBuilding" ,"IsNegotiable"
                };

                object[] parameterValues = { propertyMaster.UniquId, propertyMaster.Title,
                    propertyMaster.Description, propertyMaster.PropertyType,
                    propertyMaster.Bedrooms, propertyMaster.Bathrooms, propertyMaster.CarpetArea,
                    propertyMaster.BuiltUpArea, propertyMaster.Address, propertyMaster.City,
                    propertyMaster.State, propertyMaster.PinCode, propertyMaster.BudgetMin,
                    propertyMaster.BudgetMax, propertyMaster.FurnishingStatus,
                    propertyMaster.PossessionDate.HasValue ? propertyMaster.PossessionDate.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value,
                    propertyMaster.IsActive, propertyMaster.TransactionType, propertyMaster.Floor,
                    propertyMaster.ContactPersonName, propertyMaster.ContactPersonPhone,
                    propertyMaster.ContactPersonAlternatePhone, propertyMaster.Area, propertyMaster.SubArea,
                    propertyMaster.Country ,propertyMaster.Amenities, propertyMaster.UserID,  propertyMaster.AgencyId,
                    propertyMaster.Vastu , propertyMaster.YearOfConstruction , propertyMaster.PropertyCategory,
                    propertyMaster.PreferedBuyerType ,propertyMaster.AmountUnitMinPrice ,propertyMaster.AmountUnitMaxPrice,
                    propertyMaster.ConvertedActualPrice ,propertyMaster.ConvertedNegotiablePrice,
                    propertyMaster.TotalFloorBuilding,propertyMaster.IsNegotiable
                };

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

        public async Task<PropertyMaster> PropertyMasterDropDown(string AgencyID)
        {
            try
            {
                string[] parameterNames = { "AgencyID" };
                object[] parameterValues = { AgencyID };

                string Sp = "FMR_PropertyMasterDropDown";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);

                PropertyMaster propertyMaster = new PropertyMaster();

                while (data.Read())
                {
                    propertyMaster.CountryList.Add(new DropDownSource(data, true));
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.StateList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.CityList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.PropertyCategoryList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.AreaList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.PropertyTypeList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.FurnishingTypeList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.RentalTypeList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.BedRoomList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.AmenityList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.AmountUnitList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        propertyMaster.PreferedBuyerTypeList.Add(new DropDownSource(data, true));
                    }
                }
                return propertyMaster;


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PropertyMaster> GetAreaMasterData(int AreaID, string UserID)
        {
            try
            {
                string[] parameterNames = { "AreaID", "UserID" };
                object[] parameterValues = { AreaID, UserID };

                string Sp = "FMR_GetAreaMasterData";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);

                while (data.Read())
                {
                    PropertyMaster PropertyMaster = new PropertyMaster(data, 1);
                    return PropertyMaster;
                }

                return null;
                //bind 
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<DataTableResult> ListPropertyMaster(DataTablePara dataTablePara)
        {
            try
            {
                string[] parameterName = new string[]
                {
                    "DisplayLength", "DisplayStart", "SortCol", "SortDir", "Search",
                    "PropertyName", "PropertyType","TransactionType","City","ContactPersonName",
                    "IsActive", "CreatedBy", "AgencyId"
                };

                object[] parameterValue = new object[]
                {
                    dataTablePara.iDisplayLength,dataTablePara.iDisplayStart,dataTablePara.iSortCol_0,
                    dataTablePara.sSortDir_0,dataTablePara.sSearch,dataTablePara.sSearch_0,
                    dataTablePara.sSearch_1,dataTablePara.sSearch_2,dataTablePara.sSearch_3,
                    dataTablePara.sSearch_4,dataTablePara.sSearch_5,dataTablePara.sSearch_6,
                    dataTablePara.AgencyId
                };

                var reader = await _dbHelper.GetDataReaderAsync("[FMR_DataListPropertyMaster]", parameterName, parameterValue);

                var result = new DataTableResult();
                var list = new List<PropertyMaster>();

                while (reader.Read())
                {
                    list.Add(new PropertyMaster(reader, 2));
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

        public async Task<HomeDashboard> SearchProperties(string location, string propertyType, string Bedroom, string rentalType, string propertyCategory, string amenities, string minPrice, string maxPrice, int pageNo, int pageSize)
        {
            try
            {
                string[] parametersName = { "Location", "PropertyType", "Bedrooms", "RentalType", "PropertyCategory", "Amenities", "MinPrice", "MaxPrice", "PageNo", "PageSize" };
                object[] Values = { location, propertyType, Bedroom, rentalType, propertyCategory, amenities, minPrice, maxPrice, pageNo, pageSize };

                string Sp = "FMR_SearchProperties";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parametersName, Values);
                HomeDashboard home = new HomeDashboard();
                while (data.Read())
                {
                    home.PropertyDetails.Add(new PropertyDetails(data, 1));
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        home.PropertyTypeList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        home.BedroomList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        home.AmenityList.Add(new DropDownSource(data, true));
                    }
                }

                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        home.RentalTypeList.Add(new DropDownSource(data, true));
                    }
                }

                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        home.PropertyCategoryList.Add(new DropDownSource(data, true));
                    }
                }

                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        home.TotalCount = Convert.ToInt32(data[0]);
                        home.CurrentPage = pageNo;
                    }
                }
                return home;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteProperty(PropertyMaster PropertyMaster)
        {
            try
            {
                string[] parametersName = { "UniquId", "UserID" };
                object[] Values = { PropertyMaster.UniquId, PropertyMaster.UserID };

                string Sp = "FMR_DeletePropertyMaster";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<PropertyMaster> EditProperty(int UniqueID, string UserID)
        {
            try
            {
                string[] parameterNames = { "UniqueID", "UserID" };
                object[] parameterValues = { UniqueID, UserID };

                string Sp = "FMR_EditPropertyMaster";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
                var PropertyMaster = new PropertyMaster();

                while (data.Read())
                {
                    PropertyMaster = new PropertyMaster(data, 3);
                    if (data.NextResult())
                    {
                        while (data.Read())
                        {
                            PropertyMaster.Amenities = Convert.ToString(data.GetValue(0));
                        }
                    }
                    if (data.NextResult())
                    {
                        while (data.Read())
                        {
                            PropertyMaster.Images.Add(new DropDownSource(data));
                        }
                    }

                    if (data.NextResult())
                    {
                        while (data.Read())
                        {
                            PropertyMaster.Videos.Add(new DropDownSource(data));

                        }
                    }

                    return PropertyMaster;
                }

                return PropertyMaster;
                //bind 
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<DataTableResult> ListAgents(DataTablePara dataTablePara)
        {
            try
            {
                string[] parameterName = new string[]
                {
                    "DisplayLength", "DisplayStart", "SortCol", "SortDir", "Search",
                    "FullName", "Email","Phone","CreatedBy","IsActive","AgencyId"
                };

                object[] parameterValue = new object[]
                {
                    dataTablePara.iDisplayLength,dataTablePara.iDisplayStart,dataTablePara.iSortCol_0,
                    dataTablePara.sSortDir_0,dataTablePara.sSearch,
                    dataTablePara.sSearch_1,dataTablePara.sSearch_2,dataTablePara.sSearch_3,
                    dataTablePara.sSearch_4,dataTablePara.sSearch_5,dataTablePara.AgencyId
                };

                var reader = await _dbHelper.GetDataReaderAsync("[FMR_DataListAgent]", parameterName, parameterValue);

                var result = new DataTableResult();
                var list = new List<Agent>();

                while (reader.Read())
                {
                    list.Add(new Agent(reader, 0));
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

        public async Task<int> SaveAgent(Agent agent)
        {
            try
            {
                string[] parametersName = {
                "UniquId", "UserName", "FirstName", "MiddleName", "LastName", "Email",
                "Phone", "AlternatePhone","IsActive","ProfileImage","AgencyID","Password"
            };

                object[] Values = {
                        agent.UniquId,
                        agent.UserName,
                        agent.FirstName,
                        agent.MiddleName,
                        agent.LastName,
                        agent.Email,
                        agent.Phone,
                        agent.AlternatePhone,
                        agent.IsActive,
                        agent.ProfileImageFilePath,
                        agent.AgencyId,
                        agent.Password
                    };


                string Sp = "FMR_SaveAgent";
                var data = await _dbHelper.ExecuteScalarAsync(Sp, parametersName, Values);
                return Convert.ToInt32(data);
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<bool> DeleteAgent(Agent agent)
        {
            try
            {
                string[] parametersName = { "UniquId", "UserID" };
                object[] Values = { agent.UniquId, agent.UserID };

                string Sp = "FMR_DeleteAgent";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<Agent> EditAgent(int uniqueID, int UserID)
        {
            try
            {
                string[] parameterNames = { "UniqueID", "UserID" };
                object[] parameterValues = { uniqueID, UserID };

                string Sp = "FMR_EditAgent";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
                while (data.Read())
                {
                    Agent Agent = new Agent(data, 1);
                    return Agent;
                }
                return null;

                //bind 
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
