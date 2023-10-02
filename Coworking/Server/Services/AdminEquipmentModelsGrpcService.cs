using Coworking.Logic.Interfaces;
using Coworking.Shared.Services;
using Mapster;
using ProtoBuf.Grpc;

namespace Coworking.Server.Services;

public class AdminEquipmentModelsGrpcService: IAdminEquipmentModelsGrpcService
{
    private readonly IAdminEquipmentModelsService _service;
    private readonly IDepartmentService _departmentService;
    public AdminEquipmentModelsGrpcService(IAdminEquipmentModelsService service, IDepartmentService departmentService)
    {
        _service = service;
        _departmentService = departmentService;
    }

    public async Task<GetAllEquipmentModelsResponse> GetAllEquipments(GetAllEquipmentModelsRequest request, CallContext context = default)
    {
        var departmentId = await _departmentService.GetDepartmentIdByName(request.DepartmentName);
        var eqModels = await _service.ReadAllEquipmentModels(departmentId);

        return new GetAllEquipmentModelsResponse()
        {
            EquipmentModels = eqModels
                .AsQueryable()
                .ProjectToType<EquipmentModelDto>()
                .ToList()
        };
    }
}