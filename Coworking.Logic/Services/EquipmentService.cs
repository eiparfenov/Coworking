using Coworking.Domain.Models;
using Coworking.Logic.Interfaces;
using Coworking.Logic.Maths;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Logic.Services;

public class EquipmentService: IEquipmentService
{
    private readonly IApplicationDbContext _db;

    public EquipmentService(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<EquipmentModel>> GetAllEquipmentModels(int departmentId)
    {
        return _db.EquipmentModels.ToList();
    }

    public async Task<int> GetEquipmentModelCount(int equipmentModelId)
    {
        var model = await _db.EquipmentModels
            .Include(eq => eq.Equipments)
            .FirstAsync(eq => eq.Id == equipmentModelId);

        return model.Equipments?.Count ?? 0;
    }

    public async Task<(List<TimePeriod> periods, GetEquipmentModelsFreeTimeResult result)> GetEquipmentModelsFreeTime(List<int> equipmentModelsId, DateOnly date)
    {
        var todayReservations = _db.Reservations
            .Include(res => res.EquipmentModels)
            .Where(res => res.Date == date)
            .ToList();
        
        var modelsForReservation = _db.EquipmentModels
            .Where(eqModel => equipmentModelsId.Any(id => id == eqModel.Id))
            .Include(eqModel => eqModel.Workplaces)
            .Include(eqModel => eqModel.Equipments)
            .Include(eqModel => eqModel.Department)
            .ToList();

        var equipmentModelsFreePeriods = TimeManager.IntersectTimePeriods(
            modelsForReservation
                .SelectMany(eqModel => 
                    TimeManager.ReverseIntersectTimePeriods(
                        todayReservations
                            .Where(res => res.EquipmentModels!.Contains(eqModel))
                            .Select(res => new TimePeriod(res.StartTime, res.EndTime))
                            .ToList(),
                        eqModel.Department!.WorkDayStart,
                        eqModel.Department!.WorkDayEnd,
                        eqModel.Equipments!.Count
                    )
                )
                .ToList(),
            modelsForReservation.Count
        );
        
        return default;
    }
}