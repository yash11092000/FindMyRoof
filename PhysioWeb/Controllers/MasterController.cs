using System.Net.NetworkInformation;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Models;
using PhysioWeb.Repository;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace PhysioWeb.Controllers
{
    [Authorize(Roles = "Agency")]
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


        [HttpPost]  
        public async Task<ActionResult> ListPropertyCategory()
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
            var result = await _masterRepository.ListPropertyCategory(dataTablePara);
            var requestForm = Request.Form;
            return Json(new
            {
                draw = requestForm["draw"],                     // Echo back the draw count
                recordsTotal = result.iTotalRecords,            // Total records in DB
                recordsFiltered = result.iTotalDisplayRecords,  // Total records after filtering
                data = result.aaData                            // Actual paged data
            });

        }

        [HttpPost]
        public async Task<ActionResult> EditPropertyCategory(int UniqueID)
        {
            try
            {
                int UserID = 0;
                var data = await _masterRepository.EditPropertyCategory(UniqueID, UserID);

                return Json(data);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult> DeletePropertyCategory(int UniqueID)
        {
            // propertyTypeMaster.AgencyId = 0;
            var PropertyCategoryMaster = new PropertyCategoryMaster
            {
                UniquId = UniqueID
            };

            var result = await _masterRepository.DeletePropertyCategory(PropertyCategoryMaster);
            return Json(new { success = result });
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
        public async Task<ActionResult> DeletePropertyType(int UniqueID)
        {
            // propertyTypeMaster.AgencyId = 0;
            var propertyTypeMaster = new PropertyTypeMaster
            {
                UniquId = UniqueID
            };

            var result = await _masterRepository.DeletePropertyType(propertyTypeMaster);
            return Json(new { success = result });
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

        [HttpPost]
        public async Task<ActionResult> EditPropertyType(int UniqueID)
        {
            try
            {
                int UserID = 0;
                var data = await _masterRepository.EditPropertyType(UniqueID, UserID);

                return Json(data);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
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
        [HttpPost]
        public async Task<ActionResult> ListRentalType()
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
            var result = await _masterRepository.ListRentalType(dataTablePara);
            var requestForm = Request.Form;
            return Json(new
            {
                draw = requestForm["draw"],                     // Echo back the draw count
                recordsTotal = result.iTotalRecords,            // Total records in DB
                recordsFiltered = result.iTotalDisplayRecords,  // Total records after filtering
                data = result.aaData                            // Actual paged data
            });

        }

        [HttpPost]
        public async Task<ActionResult> EditRentalType(int UniqueID)
        {
            try
            {
                int UserID = 0;
                var data = await _masterRepository.EditRentalType(UniqueID, UserID);

                return Json(data);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult> DeleteRentalType(int UniqueID)
        {
            // propertyTypeMaster.AgencyId = 0;
            var RentalTypeMaster = new RentalTypeMaster
            {
                UniquId = UniqueID
            };

            var result = await _masterRepository.DeleteRentalType(RentalTypeMaster);
            return Json(new { success = result });
        }

        #endregion

        #region Property Master
        public async Task<ActionResult> PropertyMaster()
        {
            return View();
        }

        public async Task<IActionResult> SaveProperty(string PropertyMaster, List<IFormFile> Images, List<IFormFile> Videos)
        {
            var property = JsonConvert.DeserializeObject<PropertyMaster>(PropertyMaster);

            return Json(new { success = false, message = "Invalid property data" });

        }
        #endregion



        #region Furnishing Type Master
        [HttpGet]
        public async Task<ActionResult> FurnishingType()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> SaveFurnishingType(FurnishingTypeMaster FurnishingTypeMaster)
        {
            var result = await _masterRepository.SaveFurnishingType(FurnishingTypeMaster);
            return Json(result);
        }


        [HttpPost]
        public async Task<ActionResult> DeleteFurnishingType(int UniqueID)
        {
            // propertyTypeMaster.AgencyId = 0;
            var FurnishingTypeMaster = new FurnishingTypeMaster
            {
                UniquId = UniqueID
            };

            var result = await _masterRepository.DeleteFurnishingType(FurnishingTypeMaster);
            return Json(new { success = result });
        }


        [HttpPost]

        public async Task<ActionResult> ListFurnishingType()
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
            var result = await _masterRepository.ListFurnishingType(dataTablePara);
            var requestForm = Request.Form;
            return Json(new
            {
                draw = requestForm["draw"],                     // Echo back the draw count
                recordsTotal = result.iTotalRecords,            // Total records in DB
                recordsFiltered = result.iTotalDisplayRecords,  // Total records after filtering
                data = result.aaData                            // Actual paged data
            });

        }

        [HttpPost]
        public async Task<ActionResult> EditFurnishingType(int UniqueID)
        {
            try
            {
                int UserID = 0;
                var data = await _masterRepository.EditFurnishingType(UniqueID, UserID);

                return Json(data);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Amenity Master
        [HttpGet]
        public async Task<ActionResult> Amenities()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> SaveAmenityMaster(AmenityMaster AmenityMaster)
        {
            var result = await _masterRepository.SaveAmenityMaster(AmenityMaster);
            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAmenityMaster(int UniqueID)
        {
            // propertyTypeMaster.AgencyId = 0;
            var AmenityMaster = new AmenityMaster
            {
                UniquId = UniqueID
            };

            var result = await _masterRepository.DeleteAmenityMaster(AmenityMaster);
            return Json(new { success = result });
        }


        [HttpPost]

        public async Task<ActionResult> ListAmenityMaster()
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
            var result = await _masterRepository.ListAmenityMaster(dataTablePara);
            var requestForm = Request.Form;
            return Json(new
            {
                draw = requestForm["draw"],                     // Echo back the draw count
                recordsTotal = result.iTotalRecords,            // Total records in DB
                recordsFiltered = result.iTotalDisplayRecords,  // Total records after filtering
                data = result.aaData                            // Actual paged data
            });

        }

        [HttpPost]
        public async Task<ActionResult> EditAmenityMaster(int UniqueID)
        {
            try
            {
                int UserID = 0;
                var data = await _masterRepository.EditAmenityMaster(UniqueID, UserID);

                return Json(data);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion

    }
}
