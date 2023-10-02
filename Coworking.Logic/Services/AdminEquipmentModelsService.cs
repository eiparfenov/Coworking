using Coworking.Domain.Models;
using Coworking.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Logic.Services;

public class AdminEquipmentModelsService: IAdminEquipmentModelsService
{
    private readonly IApplicationDbContext _db;

    public AdminEquipmentModelsService(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<EquipmentModel>> ReadAllEquipmentModels(int departmentId)
    {
        var eqModels = _db.EquipmentModels.ToList();
        return eqModels;
    }

    public async Task<EquipmentModel> ReadEquipmentModel(int equipmentModelId, int departmentId)
    {
        var eqModel = await _db.EquipmentModels.FindAsync(equipmentModelId);

        if (eqModel == null)
        {
            throw new ArgumentException("No equipment model with such id", nameof(equipmentModelId));
        }

        if (eqModel.DepartmentId != departmentId)
        {
            throw new WrongDepartmentException();
        }

        return eqModel;
    }

    public async Task UpdateEquipmentModel(int equipmentModelId, int departmentId, string name, string description)
    {
        var eqModel = await ReadEquipmentModel(equipmentModelId, departmentId);
        
        eqModel.Name = name;
        eqModel.Description = description;

        await _db.SaveChangesAsync();
    }

    public async Task DeleteEquipmentModel(int equipmentModelId, int departmentId)
    {
        var eqModel = await ReadEquipmentModel(equipmentModelId ,departmentId);
        
        _db.EquipmentModels.Remove(eqModel);

        await _db.SaveChangesAsync();
    }

    public async Task<int> CreateEquipmentModel(string name, string description, int departmentId)
    {
        var eqModel = new EquipmentModel()
        {
            DepartmentId = departmentId,
            Name = name,
            Description = description,
        };

        _db.EquipmentModels.Add(eqModel);
        await _db.SaveChangesAsync();

        return eqModel.Id;
    }
}