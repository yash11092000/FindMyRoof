using PhysioWeb.Data;
using PhysioWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using System.Data.Common;

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
                string[] parametersName = { "UniquId", "PropertyCategory", "IsActive" , "UserID" };
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
                string[] parametersName = { "UniquId" ,"UserID"};
                object[] Values = { PropertyCategoryMaster.UniquId , 0};

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
                string[] parametersName = { "UniquId"};
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
                    PropertyTypeMaster PropertyTypeMaster = new PropertyTypeMaster(data,1);
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
                string[] parametersName = { "UniquId" ,"UserID" };
                object[] Values = { RentalTypeMaster.UniquId , RentalTypeMaster.AgencyId };

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
                string[] parametersName = { "UniquId", "FurnishingType",  "IsActive", "UserID" };
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


        public async Task<bool> SaveAmenityMaster(AmenityMaster AmenityMaster)
        {
            try
            {
                string[] parametersName = { "UniquId", "AmenityName", "IconImage", "IsActive", "UserID" };
                object[] Values = { AmenityMaster.UniquId,AmenityMaster.AmenityName,AmenityMaster.IconImage,
                AmenityMaster.IsActive ,0 };

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
                object[] Values = { AmenityMaster.UniquId, AmenityMaster.AgencyId };

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
        public async Task<AreaMaster> EditAreaMaster(int UniqueID, int UserID)
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
    "Pincode", "IsActive", "UserID"
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
                object[] Values = { AreaMaster.UniquId, AreaMaster.AgencyId };

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

        public async Task<List<DropDownSource>> GetAreaList(string searchTerm)
        {
            string[] parameterNames = new string[] { "@SearchTerm" };
            object[] parameterValues = new object[] { searchTerm ?? (object)DBNull.Value };

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

        public async Task<Dictionary<string, List<DropDownSource>>> GetPropertyDetails()
        {
            var result = new Dictionary<string, List<DropDownSource>>();

            using (var dataReader = await _dbHelper.GetDataReaderAsync("FMR_GetPropertyDetails", new string[] { }, new object[] { }))
            {
                var reader = (System.Data.Common.DbDataReader)dataReader;

                result["PropertyTypes"] = ReadDropDownList(reader);

                await reader.NextResultAsync();
                result["Bedrooms"] = ReadDropDownList(reader);

                await reader.NextResultAsync();
                result["Amenities"] = ReadDropDownList(reader);

                await reader.NextResultAsync();
                result["RentalTypes"] = ReadDropDownList(reader);

                await reader.NextResultAsync();
                result["PropertyCategories"] = ReadDropDownList(reader);
            }

            return result;
        }

        private List<DropDownSource> ReadDropDownList(DbDataReader reader)
        {
            var list = new List<DropDownSource>();
            while (reader.Read())
            {
                list.Add(new DropDownSource
                {
                    Value = reader["Value"].ToString(),
                    Text = reader["Text"].ToString()
                });
            }
            return list;
        }



    }
}