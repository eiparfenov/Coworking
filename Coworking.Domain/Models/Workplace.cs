namespace Coworking.Domain.Models;

public class Workplace
{
    public int Id { get; set; }
    
    public string? Name { get; set; }

    public List<EquipmentModel>? EquipmentModels { get; set; }
    
    public Department? Department { get; set; }
    public int DepartmentId { get; set; }
}