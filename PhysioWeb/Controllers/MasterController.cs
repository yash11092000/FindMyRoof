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
        
        public async Task<ActionResult> ListPropertyType(DataTablePara dataTablePara)
        {
            var result = await _masterRepository.ListPropertyType(dataTablePara);

            //return Json(result);
            return Json(new
            {
                data = result.daaa, // 👈 make sure this matches what DataTables expects
                recordsTotal = result.totalCount,
                recordsFiltered = result.totalCount
            });
        }

        #endregion

    }
}
