namespace Coworking.Domain.Models;

public class Reservation
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public User? User { get; set; }
    public int UserId { get; set; }

    public List<EquipmentModel>? EquipmentModels { get; set; }

    public string? UserComment { get; set; }
    public int Count { get; set; }
}