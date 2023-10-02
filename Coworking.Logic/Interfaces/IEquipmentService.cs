using Coworking.Domain.Models;
using Coworking.Logic.Maths;

namespace Coworking.Logic.Interfaces;

public interface IEquipmentService
{
    Task<List<EquipmentModel>> GetAllEquipmentModels(int departmentId);
    Task<int> GetEquipmentModelCount(int equipmentModelId);
    Task<(List<TimePeriod> periods, GetEquipmentModelsFreeTimeResult result)> GetEquipmentModelsFreeTime(List<int> equipmentModelsId, DateOnly date);
}

public enum GetEquipmentModelsFreeTimeResult
{
    FoundFreeTime = 1,
    NoWorkplaceForSuchSet = 2,
    AllReserved = 3,
}
