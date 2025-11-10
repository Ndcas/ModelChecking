using FinalLabWeb.Models;
using FinalLabWeb.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace FinalLabWeb.Services.Implementations
{
    public class StaffService : IStaffService
    {
        private readonly ModelCheckingContext _context;

        public StaffService(ModelCheckingContext context)
        {
            _context = context;
        }

        public Dictionary<string, object> GetStaffList()
        {
            try
            {
                List<Staff> staffList = _context.Staff.ToList();
                Dictionary<string, object> result = new();
                result["ok"] = true;
                result["data"] = new Dictionary<string, object>();
                ((Dictionary<string, object>)result["data"])["staffList"] = staffList;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving staff list\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception("System error");
            }
        }

        public Dictionary<string, object> GetStaffInformationByID(int id)
        {
            try
            {
                Dictionary<string, object> result = new();
                Staff staff = _context.Staff.Find(id);
                if (staff == null)
                {
                    result["ok"] = false;
                    result["status"] = -2;
                    return result;
                }
                result["ok"] = true;
                result["data"] = new Dictionary<string, object>();
                ((Dictionary<string, object>)result["data"])["staff"] = staff;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving staff list\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception("System error");
            }
        }

        public Dictionary<string, object> AddStaff(int ID, string name, string email, string phone, DateOnly startingDate, string photo)
        {
            try
            {
                Dictionary<string, object> result = new();
                Staff staff = _context.Staff.Find(ID);
                if (staff != null)
                {
                    result["ok"] = false;
                    result["status"] = -2;
                    return result;
                }
                Staff newStaff = new();
                newStaff.Id = ID;
                newStaff.Name = name;
                newStaff.Email = email;
                newStaff.Phone = phone;
                newStaff.StartingDate = startingDate;
                newStaff.Photo = photo;
                List<ValidationResult> validationResults = new();
                ValidationContext validationContext = new(newStaff);
                if (!Validator.TryValidateObject(newStaff, validationContext, validationResults, true))
                {
                    result["ok"] = false;
                    result["status"] = -3;
                    return result;
                }
                _context.Staff.Add(newStaff);
                _context.SaveChanges();
                result["ok"] = true;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving staff list\n{ex.Message}\n{ex.StackTrace}");
                throw new Exception("System error");
            }
        }
    }
}