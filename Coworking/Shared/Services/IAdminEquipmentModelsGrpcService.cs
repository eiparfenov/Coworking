using ProtoBuf;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Coworking.Shared.Services;


[Service("rtu-tc.admin.equipment-models")]
public interface IAdminEquipmentModelsGrpcService
{
    Task<GetAllEquipmentModelsResponse> GetAllEquipments(GetAllEquipmentModelsRequest request, CallContext context = default!);
}

[ProtoContract]
public class GetAllEquipmentModelsRequest: DepartmentMatchedRequest { }

[ProtoContract]
public class EquipmentModelDto
{
    [ProtoMember(1)] public int Id { get; set; }
    [ProtoMember(2)] public string Name { get; set; } = null!;
    [ProtoMember(3)] public string? Description { get; set; }
}

[ProtoContract]
public class GetAllEquipmentModelsResponse
{
    [ProtoMember(1)] public List<EquipmentModelDto>? EquipmentModels { get; set; } 
}