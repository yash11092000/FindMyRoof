using System.Net.NetworkInformation;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Models;
using PhysioWeb.Repository;

namespace PhysioWeb.Controllers
{
    public class MasterController : Controller
    {
        private readonly IMasterRepository _masterRepository;
        public MasterController(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        #region Property Category Master
        [HttpGet]
        public async Task<ActionResult> PropertyCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SavePropCategory(PropertyCategoryMaster propertyCategoryMaster)
        {
            var result = await _masterRepository.SavePropCategory(propertyCategoryMaster);
            return Json(result);
        }

        #endregion

        #region Property Type Master
        [HttpGet]
        public async Task<ActionResult> PropertyType()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> SavePropertyType(PropertyTypeMaster propertyTypeMaster)
        {
            var result = await _masterRepository.SavePropType(propertyTypeMaster);
            return Json(result);
        }

        [HttpPost]

        public async Task<ActionResult> ListPropertyType()
        {
            var form = Request.Form;

            // ✅ Map DataTables default parameters
            var dataTablePara = new DataTablePara
            {
                iDisplayStart = Convert.ToInt32(form["start"]),
                iDisplayLength = Convert.ToInt32(form["length"]),
                iSortCol_0 = Convert.ToInt32(form["order[0][column]"]),
                sSortDir_0 = form["order[0][dir]"],
                sSearch = form["search[value]"]
            };

            // ✅ Map column filters dynamically (for first 10 columns)
            for (int i = 0; i < 30; i++)
            {
                string key = $"columns[{i}][search][value]";
                if (Request.Form.ContainsKey(key))
                {
                    typeof(DataTablePara)
                        .GetProperty($"sSearch_{i}")
                        ?.SetValue(dataTablePara, Request.Form[key].ToString());
                }
            }
            var result = await _masterRepository.ListPropertyType(dataTablePara);
            var requestForm = Request.Form;
            return Json(new
            {
                draw = requestForm["draw"],                     // Echo back the draw count
                recordsTotal = result.iTotalRecords,            // Total records in DB
                recordsFiltered = result.iTotalDisplayRecords,  // Total records after filtering
                data = result.aaData                            // Actual paged data
            });

        }

        #endregion

        #region Rental Type Master

        [HttpGet]
        public async Task<ActionResult> RentalType()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SaveRentalType(RentalTypeMaster RentalTypeMaster)
        {
            var result = await _masterRepository.SaveRentalType(RentalTypeMaster);
            return Json(result);
        }

        #endregion
    }
}
