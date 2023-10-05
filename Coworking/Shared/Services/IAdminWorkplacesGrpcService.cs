using ProtoBuf;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Coworking.Shared.Services;


[Service("rtu-tc.admin.workplaces")]
public interface IAdminWorkplacesGrpcService
{
    Task<GetAllWorkplacesResponse> GetAllWorkplaces(GetAllWorkplacesRequest request, CallContext context = default!);
    Task<CreateWorkplaceResponse> CreateWorkplace(CreateWorkplaceRequest request, CallContext context = default!);
    Task<UpdateWorkplaceResponse> UpdateWorkplace(UpdateWorkplaceRequest request, CallContext context = default!);
    Task<DeleteWorkplaceResponse> DeleteWorkplace(DeleteWorkplaceRequest request, CallContext context = default!);
}

#region GetWorkplace

[ProtoContract]
public class GetAllWorkplacesRequest
{
    [ProtoMember(1)] public string? DepartmentName { get; set; }
}

[ProtoContract]
public class WorkplaceDto
{
    [ProtoMember(1)] public int Id { get; set; }
    [ProtoMember(2)] public string? Name { get; set; }
    [ProtoMember(3)] public List<int>? EquipmentModelIds { get; set; }
}

[ProtoContract]
public class GetAllWorkplacesResponse
{
    [ProtoMember(1)] public List<WorkplaceDto>? Workplaces { get; set; }
}
#endregion

#region CreateWorkplace

[ProtoContract]
public class CreateWorkplaceRequest
{
    [ProtoMember(1)] public string? DepartmentName { get; set; }
    [ProtoMember(2)] public string? Name { get; set; }
    [ProtoMember(3)] public List<int>? EquipmentModelIds { get; set; }
}

[ProtoContract]
public class CreateWorkplaceResponse
{
    [ProtoMember(1)] public int Id { get; set; }
}

#endregion

#region UpdateWorkplace

[ProtoContract]
public class UpdateWorkplaceRequest
{
    [ProtoMember(1)] public string? DepartmentName { get; set; }
    [ProtoMember(2)] public string? Name { get; set; }
    [ProtoMember(3)] public List<int>? EquipmentModelIds { get; set; }
    [ProtoMember(4)] public int Id { get; set; }
}

[ProtoContract]
public class UpdateWorkplaceResponse
{
    
}

#endregion

#region DeleteWorkplace

[ProtoContract]
public class DeleteWorkplaceRequest
{
    [ProtoMember(1)] public string? DepartmentName { get; set; }
    [ProtoMember(2)] public int Id { get; set; }
}

[ProtoContract]
public class DeleteWorkplaceResponse
{
    
}
#endregion