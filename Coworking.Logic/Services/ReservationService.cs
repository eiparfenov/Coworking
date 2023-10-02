using Coworking.Logic.Interfaces;
using Coworking.Logic.Maths;

namespace Coworking.Logic.Services;

public class ReservationService: IReservationService
{
    private readonly IApplicationDbContext _db;
    private readonly IEquipmentService _equipmentService;
    
    public ReservationService(IApplicationDbContext db, IEquipmentService equipmentService)
    {
        _db = db;
        _equipmentService = equipmentService;
    }

    public async Task<AddReservationResult> AddReservation(int userId, int equipmentModelId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        if (!await CheckUserHasNoReservations(userId, date, startTime, endTime)) // user already reserved something on this time
        {
            return new AddReservationResult()
            {
                ReservationId = null,
                Error = AddReservationResult.ErrorType.UserRentIntersect
            };
        }

        //var freeTime = await _equipmentService.GetEquipmentModelFreeTime(equipmentModelId, date);
        //
        //var reservationPeriod = new TimePeriod(startTime, endTime);
        //if (!freeTime.Any(freePeriod => freePeriod.Contains(reservationPeriod))) // somebody already rented equipment or workplace
        //{
        //    return new AddReservationResult()
        //    {
        //        ReservationId = null,
        //        Error = AddReservationResult.ErrorType.EquipmentWorkplaceRentIntersect
        //    };
        //}

        return default;
    }

    public async Task RemoveReservation(int reservationId, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CheckUserHasNoReservations(int userId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
    {
        var reservations = _db.Reservations
            .Where(res => res.Date == date)
            .Where(res => res.UserId == userId)
            .ToList();

        return reservations.Any(x => x.StartTime < startTime && startTime < x.EndTime
                                     || x.StartTime < endTime && endTime < x.EndTime);
    }
}