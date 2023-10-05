using Coworking.Domain.Models;
using Coworking.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Logic.Services;

public class AdminWorkplacesService: IAdminWorkplacesService
{
    private readonly IApplicationDbContext _db;

    public AdminWorkplacesService(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Workplace>> GetAllWorkplaces(int departmentId)
    {
        return await _db.Workplaces
            .Include(p => p.EquipmentModels)
            .Where(wp => wp.DepartmentId == departmentId)
            .ToListAsync();
    }

    public async Task<Workplace> GetWorkplace(int departmentId, int workplaceId)
    {
        var workplace = await _db.Workplaces
            .Include(wp => wp.EquipmentModels)
            .FirstOrDefaultAsync(wp => wp.Id == workplaceId);

        if (workplace == null) throw new ArgumentException("Workplace id does not exist", nameof(workplaceId));
        if (workplace.DepartmentId != departmentId) throw new WrongDepartmentException();

        return workplace;
    }

    public async Task<int> CreateWorkplace(int departmentId, string name, List<int> equipmentModelIds)
    {
        var workplace = new Workplace()
        {
            DepartmentId = departmentId,
            Name = name,
            EquipmentModels = new List<EquipmentModel>()
        };

        await FillWorkplaceWithEquipmentModels(departmentId, equipmentModelIds, workplace);

        await _db.Workplaces.AddAsync(workplace);
        await _db.SaveChangesAsync();

        return workplace.Id;
    }

    public async Task UpdateWorkplace(int departmentId, int workplaceId, string name, List<int> equipmentModelIds)
    {
        var workplace = await GetWorkplace(departmentId, workplaceId);

        workplace.Name = name;
        await FillWorkplaceWithEquipmentModels(departmentId, equipmentModelIds, workplace);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteWorkplace(int departmentId, int workplaceId)
    {
        var workplace = await GetWorkplace(departmentId, workplaceId);

        _db.Workplaces.Remove(workplace);

        await _db.SaveChangesAsync();
    }
    
    private async Task FillWorkplaceWithEquipmentModels(int departmentId, List<int> equipmentModelIds, Workplace workplace)
    {
        workplace.EquipmentModels!.Clear();
        foreach (var equipmentModelId in equipmentModelIds)
        {
            var eqModel = await _db.EquipmentModels.FirstOrDefaultAsync(eqModel => eqModel.Id == equipmentModelId);

            if (eqModel == null)
                throw new ArgumentException("Equipment model id does not exist", nameof(equipmentModelIds));
            if (eqModel.DepartmentId != departmentId) throw new WrongDepartmentException();

            workplace.EquipmentModels.Add(eqModel);
        }
    }
}