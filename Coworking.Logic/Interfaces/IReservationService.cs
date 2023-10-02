namespace Coworking.Logic.Interfaces;

public interface IReservationService
{
    Task<AddReservationResult> AddReservation(int userId, int equipmentModelId, DateOnly date, TimeOnly startTime, TimeOnly endTime);
    Task RemoveReservation(int reservationId, int userId);
    Task<bool> CheckUserHasNoReservations(int userId, DateOnly date, TimeOnly startTime, TimeOnly endTime);
}

public class AddReservationResult
{
    public int? ReservationId { get; set; }
    public ErrorType Error { get; set; }
    
    public enum ErrorType
    {
        None = 1,
        UserRentIntersect = 2,
        EquipmentWorkplaceRentIntersect = 3
    }
}