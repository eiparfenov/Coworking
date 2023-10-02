namespace Coworking.Logic.Interfaces;

public interface IDepartmentService
{
    Task<int> GetDepartmentIdByName(string name);
}