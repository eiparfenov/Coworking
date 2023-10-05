using Coworking.Domain.Models;
using Coworking.Logic.Interfaces;
using Coworking.Shared.Services;
using Mapster;
using ProtoBuf.Grpc;

namespace Coworking.Server.Services;

public class AdminWorkplacesGrpcService: IAdminWorkplacesGrpcService
{
    private readonly IAdminWorkplacesService _adminService;
    private readonly IDepartmentService _departmentService;

    public AdminWorkplacesGrpcService(IAdminWorkplacesService adminService, IDepartmentService departmentService)
    {
        _adminService = adminService;
        _departmentService = departmentService;
    }

    public async Task<GetAllWorkplacesResponse> GetAllWorkplaces(GetAllWorkplacesRequest request, CallContext context = default)
    {
        var departmentId = await _departmentService.GetDepartmentIdByName(request.DepartmentName!);
        var workplaces = await _adminService.GetAllWorkplaces(departmentId);

        return new GetAllWorkplacesResponse()
        {
            Workplaces = workplaces
                .Select(wp => new WorkplaceDto()
                {
                    Id = wp.Id,
                    Name = wp.Name,
                    EquipmentModelIds = wp.EquipmentModels!
                        .Select(eqModel => eqModel.Id)
                        .ToList()
                })
                .ToList()
        };
    }

    public async Task<CreateWorkplaceResponse> CreateWorkplace(CreateWorkplaceRequest request, CallContext context = default)
    {
        var departmentId = await _departmentService.GetDepartmentIdByName(request.DepartmentName!);
        var workplaceId = await _adminService.CreateWorkplace(departmentId, request.Name!, request.EquipmentModelIds!);

        return new CreateWorkplaceResponse()
        {
            Id = workplaceId
        };
    }

    public async Task<UpdateWorkplaceResponse> UpdateWorkplace(UpdateWorkplaceRequest request, CallContext context = default)
    {
        var departmentId = await _departmentService.GetDepartmentIdByName(request.DepartmentName!);
        await _adminService.UpdateWorkplace(departmentId, request.Id, request.Name!, request.EquipmentModelIds!);

        return new UpdateWorkplaceResponse();
    }

    public async Task<DeleteWorkplaceResponse> DeleteWorkplace(DeleteWorkplaceRequest request, CallContext context = default)
    {
        var departmentId = await _departmentService.GetDepartmentIdByName(request.DepartmentName!);
        await _adminService.DeleteWorkplace(departmentId, request.Id);

        return new DeleteWorkplaceResponse();
    }
}