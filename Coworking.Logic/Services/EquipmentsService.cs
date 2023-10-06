using Coworking.Domain.Models;
using Coworking.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Logic.Services;

public class EquipmentsService: IEquipmentsService
{
    private readonly IApplicationDbContext _db;
    private readonly IEquipmentModelsService _equipmentModelsService;

    public EquipmentsService(IApplicationDbContext db, IEquipmentModelsService equipmentModelsService)
    {
        _db = db;
        _equipmentModelsService = equipmentModelsService;
    }

    public async Task<List<Equipment>> ReadAllEquipments(int departmentId, int equipmentModelId)
    {
        var eqModel = await _db.EquipmentModels
            .Include(eqModel => eqModel.Equipments)
            .FirstOrDefaultAsync(eqModel => eqModel.Id == equipmentModelId);

        if (eqModel == null)
        {
            throw new ArgumentException($"No equipment model with id {equipmentModelId}", nameof(equipmentModelId));
        }

        if (eqModel.DepartmentId != departmentId)
        {
            throw new WrongDepartmentException();
        }

        return eqModel.Equipments!;
    }

    public async Task<Equipment> ReadEquipment(int equipmentId, int departmentId)
    {
        var eq = await _db.Equipments
            .Include(eq => eq.Model)
            .FirstOrDefaultAsync(eq => eq.Id == equipmentId);

        if (eq == null)
        {
            throw new ArgumentException($"No equipment with id {equipmentId}", nameof(equipmentId));
        }

        if (eq.Model!.DepartmentId != departmentId)
        {
            throw new WrongDepartmentException();
        }

        return eq;
    }

    public async Task UpdateEquipment(int equipmentId, int departmentId, string invNumber, string comment)
    {
        var eq = await ReadEquipment(equipmentId, departmentId);

        eq.Comment = comment;
        eq.InvNumber = invNumber;
        
        await _db.SaveChangesAsync();
    }

    public async Task DeleteEquipment(int equipmentModelId, int departmentId)
    {
        var eq = await ReadEquipment(equipmentModelId, departmentId);

        _db.Equipments.Remove(eq);

        await _db.SaveChangesAsync();
    }

    public async Task<int> CreateEquipment(string invNumber, string comment, int equipmentModelId, int departmentId)
    {
        var eqModel = await _equipmentModelsService.ReadEquipmentModel(equipmentModelId, departmentId);

        var eq = new Equipment()
        {
            Comment = comment,
            Model = eqModel,
            InvNumber = invNumber
        };

        await _db.Equipments.AddAsync(eq);

        await _db.SaveChangesAsync();

        return eq.Id;
    }
}