using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using PhysioWeb.Hubs;
using PhysioWeb.Models;
using PhysioWeb.Repository;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace PhysioWeb.Controllers
{
    [Authorize(Roles = "Agency")]
    public class MasterController : Controller
    {
        private readonly IMasterRepository _masterRepository;
        private readonly IHubContext<NotificationHub> _hubContext;

        public MasterController(IMasterRepository masterRepository, IHubContext<NotificationHub> hubContext)
        {
            _masterRepository = masterRepository;
            _hubContext = hubContext;

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
            propertyCategoryMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            propertyCategoryMaster.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
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
            dataTablePara.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            dataTablePara.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
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
                string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
                string AgencyID = User.FindFirst(ClaimTypes.GroupSid)?.Value;

                var data = await _masterRepository.EditPropertyCategory(UniqueID, AgencyID);

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
            var PropertyCategoryMaster = new PropertyCategoryMaster
            {
                UniquId = UniqueID
            };
            PropertyCategoryMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
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
            propertyTypeMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            propertyTypeMaster.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
            var result = await _masterRepository.SavePropType(propertyTypeMaster);
            return Json(result);
        }


        [HttpPost]
        public async Task<ActionResult> DeletePropertyType(int UniqueID)
        {
            var propertyTypeMaster = new PropertyTypeMaster
            {
                UniquId = UniqueID
            };
            propertyTypeMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
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
            dataTablePara.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            dataTablePara.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
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
                string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
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
            RentalTypeMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            RentalTypeMaster.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
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
            dataTablePara.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            dataTablePara.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
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
                string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;

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
            var RentalTypeMaster = new RentalTypeMaster
            {
                UniquId = UniqueID
            };
            RentalTypeMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            var result = await _masterRepository.DeleteRentalType(RentalTypeMaster);
            return Json(new { success = result });
        }

        #endregion

        #region Property Master
        public async Task<ActionResult> PropertyMaster()
        {
            string AgencyID = User.FindFirst(ClaimTypes.GroupSid)?.Value;
            var PropertyMasterDropDown = await _masterRepository.PropertyMasterDropDown(AgencyID);
            return View(PropertyMasterDropDown);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProperty([FromBody] PropertyMaster PropertyMaster)
        {
            if (PropertyMaster == null)
                return Json(new { success = false, message = "Model binding failed!" });

            PropertyMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            PropertyMaster.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;

            var result = await _masterRepository.SaveProperty(PropertyMaster);

            // 2. Send notification to SuperAdmin group
            await _hubContext.Clients.Group("SuperAdmin")
                .SendAsync("ReceiveNotification", $"Property Added : {PropertyMaster.Title}");

            return Json(new { success = true, propertyId = result });
        }

        [HttpPost]
        public async Task<IActionResult> UploadPropertyMedia()
        {

            int PropertyId = int.Parse(Request.Form["PropertyId"]);
            var Images = Request.Form.Files.Where(f => f.Name.StartsWith("Images")).ToList();
            var Videos = Request.Form.Files.Where(f => f.Name.StartsWith("Videos")).ToList();

            try
            {
                DataTable mediaTable = new DataTable();
                mediaTable.Columns.Add("TempId", typeof(Guid));
                mediaTable.Columns.Add("FilePath", typeof(string));
                mediaTable.Columns.Add("FileName", typeof(string));
                mediaTable.Columns.Add("FileType", typeof(int));

                if (PropertyId <= 0)
                    return Json(new { success = false, message = "Invalid PropertyId" });


                string basePath = Path.Combine(Directory.GetCurrentDirectory(), "secure-images", "Property", PropertyId.ToString());
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);


                string ImagesDir = Path.Combine(basePath, "Images");
                if (!Directory.Exists(ImagesDir)) Directory.CreateDirectory(ImagesDir);

                // ✅ Save Images
                foreach (var img in Images)
                {
                    if (img != null && img.Length > 0)
                    {
                        string fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                        string filePath = Path.Combine(ImagesDir, fileName);

                        var relativePath = Path.Combine("Property", PropertyId.ToString(), "Images", fileName).Replace("\\", "/");
                        mediaTable.Rows.Add(Guid.NewGuid(), relativePath, fileName, 1);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await img.CopyToAsync(stream);
                        }
                    }
                }

                string VideosDir = Path.Combine(basePath, "Videos");
                if (!Directory.Exists(VideosDir)) Directory.CreateDirectory(VideosDir);


                // ✅ Save Videos
                foreach (var vid in Videos)
                {
                    if (vid != null && vid.Length > 0)
                    {
                        string fileName = Guid.NewGuid() + Path.GetExtension(vid.FileName);
                        string filePath = Path.Combine(VideosDir, fileName);

                        var relativePath = Path.Combine("Property", PropertyId.ToString(), "Videos", fileName).Replace("\\", "/");
                        mediaTable.Rows.Add(Guid.NewGuid(), relativePath, fileName, 2);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await vid.CopyToAsync(stream);
                        }

                    }
                }
                var result = await _masterRepository.SavePropertyMedia(mediaTable, PropertyId);
                return Json(new { success = true, message = "Media uploaded successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        [HttpPost]
        public async Task<ActionResult> ListPropertyMaster()
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
            dataTablePara.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            dataTablePara.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
            var result = await _masterRepository.ListPropertyMaster(dataTablePara);
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
        public async Task<ActionResult> DeleteProperty(int UniqueID)
        {
            var PropertyMaster = new PropertyMaster
            {
                UniquId = UniqueID
            };
            PropertyMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            var result = await _masterRepository.DeleteProperty(PropertyMaster);
            return Json(new { success = result });
        }


        [HttpPost]
        public async Task<ActionResult> EditProperty(int UniqueID)
        {
            try
            {
                string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
                var data = await _masterRepository.EditProperty(UniqueID, UserID);

                return Json(data);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
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
            FurnishingTypeMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            FurnishingTypeMaster.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;

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
            FurnishingTypeMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
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
            dataTablePara.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            dataTablePara.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
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
                string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
                var data = await _masterRepository.EditFurnishingType(UniqueID, Convert.ToInt32(UserID));

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
            AmenityMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            AmenityMaster.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
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
            AmenityMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
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
            dataTablePara.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            dataTablePara.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
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
                string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
                var data = await _masterRepository.EditAmenityMaster(UniqueID, Convert.ToInt32(UserID));

                return Json(data);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Area Master
        [HttpGet]
        public async Task<IActionResult> Area()
        {
            var model = new AreaMaster();

            model.CountryList = await _masterRepository.GetCountryList();
            model.StateList = new List<DropDownSource>(); // ✅ Prevent null error
            model.CityList = new List<DropDownSource>();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> SaveAreaMaster(AreaMaster AreaMaster)
        {
            AreaMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            AreaMaster.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
            var result = await _masterRepository.SaveAreaMaster(AreaMaster);
            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAreaMaster(int UniqueID)
        {
            // propertyTypeMaster.AgencyId = 0;
            var AreaMaster = new AreaMaster
            {
                UniquId = UniqueID
            };
            AreaMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;

            var result = await _masterRepository.DeleteAreaMaster(AreaMaster);
            return Json(new { success = result });
        }


        [HttpPost]
        public async Task<ActionResult> ListAreaMaster()
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
            dataTablePara.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            dataTablePara.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
            var result = await _masterRepository.ListAreaMaster(dataTablePara);
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
        public async Task<ActionResult> EditAreaMaster(int UniqueID)
        {
            try
            {
                string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;

                var data = await _masterRepository.EditAreaMaster(UniqueID, UserID);

                return Json(data);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetStatesByCountry(string countryId)
        {
            var states = await _masterRepository.GetStateList(countryId);
            return Json(states);
        }

        [HttpGet]
        public async Task<JsonResult> GetCitiesByState(string stateId)
        {
            var cities = await _masterRepository.GetCityList(stateId);
            return Json(cities);
        }

        #endregion

        #region Agent
        public async Task<ActionResult> Agent()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> ListAgents()
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
            dataTablePara.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            dataTablePara.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
            var result = await _masterRepository.ListAgents(dataTablePara);
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
        public async Task<ActionResult> SaveAgent(Agent agent)
        {
            agent.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            agent.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;

            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "secure-images", "Agencies", agent.AgencyId, "Agents");
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);


            string ImagesDir = Path.Combine(basePath, agent.Phone);
            if (!Directory.Exists(ImagesDir)) Directory.CreateDirectory(ImagesDir);


            if (agent.AgentProfile != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(agent.AgentProfile.FileName);
                string filePath = Path.Combine(ImagesDir, fileName);

                var relativePath = Path.Combine("Agencies", agent.AgencyId, "Agents", agent.Phone, fileName).Replace("\\", "/");
                agent.ProfileImageFilePath = relativePath;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await agent.AgentProfile.CopyToAsync(stream);
                }
            }

            if (agent.UniquId == 0 || agent.Password != "")
            {
                string hashed = BCrypt.Net.BCrypt.HashPassword(agent.Password);
                agent.Password = hashed;
            }
            else
            {
                agent.Password = agent.OldPassword;
            }

            var result = await _masterRepository.SaveAgent(agent);
            return Json(result);
        }

        public async Task<ActionResult> DeleteAgent(int UniqueID)
        {
            var Agent = new Agent
            {
                UniquId = UniqueID
            };
            Agent.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            var result = await _masterRepository.DeleteAgent(Agent);
            return Json(new { success = result });

        }

        [HttpPost]
        public async Task<ActionResult> EditAgent(int UniqueID)
        {
            try
            {
                string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
                var data = await _masterRepository.EditAgent(UniqueID, Convert.ToInt32(UserID));

                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region Comman Things
        [HttpGet]
        public async Task<IActionResult> GetAreas(string searchTerm)
        {
            string AgencyID = User.FindFirst(ClaimTypes.GroupSid)?.Value;
            var areas = await _masterRepository.GetAreaList(searchTerm, AgencyID);
            return Json(areas.Select(a => a.Text).ToList());
        }

        //GetAreaMasterData
        [HttpPost]
        public async Task<ActionResult> GetAreaMasterData(int AreaID)
        {
            try
            {
                string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;

                var data = await _masterRepository.GetAreaMasterData(AreaID, UserID);

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
