using FinalLabWeb.Models;
using FinalLabWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalLabWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStaffService _staffService;

        public HomeController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        public IActionResult Index(int status = 0)
        {
            switch (status)
            {
                case 1:
                    ViewBag.Message = "Staff added successfully.";
                    break;
                case -1:
                    ViewBag.Message = "Bad request";
                    break;
                case -2:
                    ViewBag.Message = "Error adding staff due to duplicated Staff ID";
                    break;
                case -3:
                    ViewBag.Message = "Invalid information";
                    break;
                case -4:
                    ViewBag.Message = "System error";
                    break;
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddStaff(int? id, string? name, string? email, string? phone, DateOnly? startingDate, string? photo)
        {
            name = name?.Trim();
            email = email?.Trim();
            phone = phone?.Trim();
            photo = photo?.Trim();
            if (id == null || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || startingDate == null || string.IsNullOrEmpty(photo))
            {
                return Redirect("/Home/Index?status=-2");
            }
            try
            {
                Dictionary<string, object> result = _staffService.AddStaff((int)id, name, email, phone, (DateOnly)startingDate, photo);
                if (!((bool)result["ok"]))
                {
                    return Redirect($"/Home/Index?status={(int)result["status"]}");
                }
                return Redirect("/Home/Index?status=1");
            }
            catch (Exception ex)
            {
                return Redirect("/Home/Index?status=-4");
            }
        }

        public IActionResult GetStaffList()
        {
            try
            {
                Dictionary<string, object> result = _staffService.GetStaffList();
                if (!((bool)result["ok"]))
                {
                    ViewBag.Message = "Unkown error";
                    return View();
                }
                ViewBag.StaffList = (List<Staff>)((Dictionary<string, object>)result["data"])["staffList"];
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "System error";
                return View();
            }
        }

        public IActionResult GetStaffInformation(int? id)
        {
            if (id == null)
            {
                ViewBag.Message = "Bad request";
                return View();
            }
            try
            {
                Dictionary<string, object> result = _staffService.GetStaffInformationByID((int)id);
                if (!((bool)result["ok"]))
                {
                    switch ((int)result["status"])
                    {
                        case -2:
                            ViewBag.Message = "Staff not found";
                            break;
                        default:
                            ViewBag.Message = "Unknown error";
                            break;
                    }
                    return View();
                }
                ViewBag.Staff = (Staff)((Dictionary<string, object>)result["data"])["staff"];
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "System error";
                return View();

            }
        }
    }
}