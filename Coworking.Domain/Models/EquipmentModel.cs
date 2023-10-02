namespace Coworking.Domain.Models;

public class EquipmentModel
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }

    public List<Equipment>? Equipments { get; set; }
    public List<Workplace>? Workplaces { get; set; }
    public Department? Department { get; set; }
    public int DepartmentId { get; set; }
}