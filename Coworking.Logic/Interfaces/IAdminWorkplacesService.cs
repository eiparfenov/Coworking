using Coworking.Domain.Models;

namespace Coworking.Logic.Interfaces;

public interface IAdminWorkplacesService
{
    Task<List<Workplace>> GetAllWorkplaces(int departmentId);
    Task<Workplace> GetWorkplace(int departmentId, int workplaceId);
    Task<int> CreateWorkplace(int departmentId, string name, List<int> equipmentModelIds);
    Task UpdateWorkplace(int departmentId, int workplaceId, string name, List<int> equipmentModelIds);
    Task DeleteWorkplace(int departmentId, int workplaceId);
}