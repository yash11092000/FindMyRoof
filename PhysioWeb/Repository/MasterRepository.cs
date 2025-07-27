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

                var reader = await _dbHelper.GetDataReaderAsync("[FMR_DataListPropertyType]", parameterName,parameterValue);

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
    }
}