using PhysioWeb.Data;
using PhysioWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;


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
    }
}