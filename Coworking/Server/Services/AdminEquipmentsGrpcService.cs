using Coworking.Logic.Interfaces;
using Coworking.Shared.Services;
using Mapster;
using ProtoBuf.Grpc;

namespace Coworking.Server.Services;

public class AdminEquipmentsGrpcService: IAdminEquipmentsGrpcService
{
    private readonly IEquipmentModelsService _equipmentModelsService;
    private readonly IEquipmentsService _equipmentsService;
    private readonly IDepartmentService _departmentService;
    public AdminEquipmentsGrpcService(IEquipmentModelsService equipmentModelsService, IDepartmentService departmentService, IEquipmentsService equipmentsService)
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


    public async Task<GetEquipmentForModelResponse> GetEquipmentsForModel(GetEquipmentsForModelRequest request, CallContext context = default)
    {
        var departmentId = await GetDepartmentId(request.DepartmentName);
        var eqs = await _equipmentsService.ReadAllEquipments(departmentId, request.EquipmentModelId);

        return new GetEquipmentForModelResponse()
        {
            Equipments = eqs.AsQueryable()
                .ProjectToType<EquipmentDto>()
                .ToList()
        };
    }

    public async Task<DeleteEquipmentModelResponse> DeleteEquipmentModel(DeleteEquipmentModelRequest request, CallContext callContext = default)
    {
        var departmentId = await GetDepartmentId(request.DepartmentName);
        await _equipmentModelsService.DeleteEquipmentModel(request.EquipmentModelId, departmentId);

        return new DeleteEquipmentModelResponse();
    }
    public async Task<DeleteEquipmentResponse> DeleteEquipment(DeleteEquipmentRequest request, CallContext callContext = default)
    {
        var departmentId = await _departmentService.GetDepartmentIdByName(request.DepartmentName);
        await _equipmentsService.DeleteEquipment(request.EquipmentId, departmentId);

        return new DeleteEquipmentResponse();
    }

    public async Task<UpdateEquipmentModelResponse> UpdateEquipmentModel(UpdateEquipmentModelRequest request, CallContext callContext = default)
    {
        var departmentId = await GetDepartmentId(request.DepartmentName);
        await _equipmentModelsService.UpdateEquipmentModel(request.Id, departmentId, request.Name, request.Description);

        return new UpdateEquipmentModelResponse();
    }

    public async Task<UpdateEquipmentResponse> UpdateEquipment(UpdateEquipmentRequest request, CallContext callContext = default)
    {
        var departmentId = await GetDepartmentId(request.DepartmentName);
        await _equipmentsService.UpdateEquipment(request.Id, departmentId, request.InvNumber, request.Comment);

        return new UpdateEquipmentResponse();
    }

    public async Task<CreateEquipmentModelResponse> CreateEquipmentModel(CreateEquipmentModelRequest request, CallContext callContext = default)
    {
        var departmentId = await GetDepartmentId(request.DepartmentName);
        var id = await _equipmentModelsService.CreateEquipmentModel(request.Name, request.Description, departmentId);

        return new CreateEquipmentModelResponse() { Id = id };
    }

    public async Task<CreateEquipmentResponse> CreateEquipment(CreateEquipmentRequest request, CallContext callContext = default)
    {
        var departmentId = await GetDepartmentId(request.DepartmentName);
        var id = await _equipmentsService.CreateEquipment(request.InvNumber, request.Comment, request.EquipmentModelId, departmentId);

        return new CreateEquipmentResponse() { Id = id };
    }

    private Task<int> GetDepartmentId(string departmentName) => _departmentService.GetDepartmentIdByName(departmentName);
}