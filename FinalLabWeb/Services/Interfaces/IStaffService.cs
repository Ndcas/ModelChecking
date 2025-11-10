namespace FinalLabWeb.Services.Interfaces
{
    public interface IStaffService
    {
        Dictionary<string, object> GetStaffList();

        Dictionary<string, object> GetStaffInformationByID(int id);

        Dictionary<string, object> AddStaff(int ID, string name, string email, string phone, DateOnly startingDate, string photo);
    }
}
