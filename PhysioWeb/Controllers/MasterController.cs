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


        //public String GetMacAddress()
        //{
        //    const int MIN_MAC_ADDR_LENGTH = 12;
        //    string macAddress = string.Empty;
        //    long maxSpeed = -1;

        //    foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        //    {
        //        //log.Debug(
        //        //    "Found MAC Address: " + nic.GetPhysicalAddress() +
        //        //    " Type: " + nic.NetworkInterfaceType);

        //        string tempMac = nic.GetPhysicalAddress().ToString();
        //        if (nic.Speed > maxSpeed &&
        //            !string.IsNullOrEmpty(tempMac) &&
        //            tempMac.Length >= MIN_MAC_ADDR_LENGTH)
        //        {
        //            //log.Debug("New Max Speed = " + nic.Speed + ", MAC: " + tempMac);
        //            maxSpeed = nic.Speed;
        //            macAddress = tempMac;
        //        }
        //    }

        //    return macAddress;
        //}

        //public String GetIPAddress()
        //{
        //    String address = "";
        //    try
        //    {
        //        WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
        //        using (WebResponse response = request.GetResponse())
        //        using (StreamReader stream = new StreamReader(response.GetResponseStream()))
        //        {
        //            address = stream.ReadToEnd();
        //        }

        //        int first = address.IndexOf("Address: ") + 9;
        //        int last = address.LastIndexOf("</body>");
        //        address = address.Substring(first, last - first);


        //    }
        //    catch (Exception)
        //    { }

        //    return address;
        //}

        //public String GetDeviceName()
        //{
        //    String DeviceName = "";
        //    DeviceName = Environment.UserName.ToString();

        //    DeviceName = DeviceName + "[" + Environment.MachineName.ToString() + "]";
        //    return DeviceName;
        //}

    }
}
