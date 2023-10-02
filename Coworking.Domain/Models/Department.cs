namespace Coworking.Domain.Models;

public class Department
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public TimeOnly WorkDayStart { get; set; }
    public TimeOnly WorkDayEnd { get; set; }
    
    public List<EquipmentModel>? EquipmentModels { get; set; }
}