namespace Coworking.Domain.Models;

public class Equipment
{
    public int Id { get; set; }
    
    public string? InvNumber { get; set; }
    public string? Comment { get; set; }
    
    public EquipmentModel? Model { get; set; }
    public int EquipmentModelId { get; set; }
}