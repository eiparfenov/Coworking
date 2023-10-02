namespace Coworking.Domain.Models;

public class Rent
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public User? User { get; set; }
    public int UserId { get; set; }

    public Workplace? Workplace { get; set; }
    public int WorkplaceId { get; set; }

    public Equipment? Equipment { get; set; }
    public int EquipmentId { get; set; }

    public string? UserComment { get; set; }
    public string? Comment { get; set; }
}