using Coworking.Domain.Models;

namespace Coworking.Logic.Interfaces;

public interface IAdminEquipmentModelsService
{
    Task<List<EquipmentModel>> ReadAllEquipmentModels(int departmentId);
    Task<EquipmentModel> ReadEquipmentModel(int equipmentModelId, int departmentId);
    Task UpdateEquipmentModel(int equipmentModelId, int departmentId, string name, string description);
    Task DeleteEquipmentModel(int equipmentModelId, int departmentId);
    Task<int> CreateEquipmentModel(string name, string description, int departmentId);
}