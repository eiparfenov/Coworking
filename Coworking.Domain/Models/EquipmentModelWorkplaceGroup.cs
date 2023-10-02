namespace Coworking.Domain.Models;

public class EquipmentModelWorkplaceGroup
{
    public EquipmentModel? EquipmentModel { get; set; }
    public int EquipmentModelId { get; set; }

    public Workplace? Workplace { get; set; }
    public int WorkplaceId { get; set; }

    public int Order { get; set; }
}