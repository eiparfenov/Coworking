using Coworking.Logic.Interfaces;
using Coworking.Shared.Services;
using Mapster;
using ProtoBuf.Grpc;

namespace Coworking.Server.Services;

public class AdminEquipmentsGrpcService: IAdminEquipmentsGrpcService
{
    private readonly IAdminEquipmentModelsService _equipmentModelsService;
    private readonly IAdminEquipmentsService _equipmentsService;
    private readonly IDepartmentService _departmentService;
    public AdminEquipmentsGrpcService(IAdminEquipmentModelsService equipmentModelsService, IDepartmentService departmentService, IAdminEquipmentsService equipmentsService)
    {
        _equipmentModelsService = equipmentModelsService;
        _departmentService = departmentService;
        _equipmentsService = equipmentsService;
    }

    public async Task<GetAllEquipmentModelsResponse> GetAllEquipmentModels(GetAllEquipmentModelsRequest request, CallContext context = default)
    {
        var departmentId = await _departmentService.GetDepartmentIdByName(request.DepartmentName);;
        var eqModels = await _equipmentModelsService.ReadAllEquipmentModels(departmentId);
        var dtos = eqModels
            .Select(eqModel => eqModel.Adapt<EquipmentModelDto>())
            .ToList();
        return new GetAllEquipmentModelsResponse()
        {
            EquipmentModels = dtos
        };
    }

    public async Task<DeleteEquipmentModelResponse> DeleteEquipmentModel(DeleteEquipmentModelRequest request, CallContext callContext = default)
    {
        var departmentId = await GetDepartmentId(request);
        await _equipmentModelsService.DeleteEquipmentModel(request.EquipmentModelId, departmentId);

        return new DeleteEquipmentModelResponseSuccess();
    }

    public async Task<GetEquipmentForModelResponse> GetEquipmentsForModel(GetEquipmentsForModelRequest request, CallContext context = default)
    {
        var departmentId = await GetDepartmentId(request);
        var eqs = await _equipmentsService.ReadAllEquipments(departmentId, request.EquipmentModelId);

        return new GetEquipmentForModelResponse()
        {
            Equipments = eqs.AsQueryable()
                .ProjectToType<EquipmentDto>()
                .ToList()
        };
    }

    public async Task<DeleteEquipmentResponse> DeleteEquipment(DeleteEquipmentRequest request, CallContext callContext = default)
    {
        var departmentId = await _departmentService.GetDepartmentIdByName(request.DepartmentName);
        await _equipmentsService.DeleteEquipment(request.EquipmentId, departmentId);

        return new DeleteEquipmentResponseSuccess();
    }

    private Task<int> GetDepartmentId(DepartmentMatchedRequest request)
    {
        return _departmentService.GetDepartmentIdByName(request.DepartmentName);
    }
}