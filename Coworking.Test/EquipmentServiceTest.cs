using Coworking.Logic.Interfaces;
using Coworking.Logic.Services;

namespace Coworking.Test;

[UsesVerify]
public class EquipmentServiceTest
{
    private readonly IEquipmentService _equipmentService;
    public EquipmentServiceTest()
    {
        var db = new TempDbContext();
        db.FillDb();
        _equipmentService = new EquipmentService(db);
    }

    [Fact]
    public async Task TestGetAllEquipmentModels()
    {
        var result = await _equipmentService.GetAllEquipmentModels(0);
        await Verify(result.Select(x => x.Name));
    }

    [Fact]
    public async Task TestGetEquipmentModelCount()
    {
        var result = await _equipmentService.GetEquipmentModelCount(2);
        Assert.Equal(3, result);
    }
}