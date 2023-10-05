using ProtoBuf;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Coworking.Shared.Services;


[Service("rtu-tc.admin.equipment-models")]
public interface IAdminEquipmentsGrpcService
{
    Task<GetAllEquipmentModelsResponse> GetAllEquipmentModels(GetAllEquipmentModelsRequest request, CallContext context = default!);
    Task<GetEquipmentForModelResponse> GetEquipmentsForModel(GetEquipmentsForModelRequest request, CallContext context = default!);
    Task<DeleteEquipmentModelResponse> DeleteEquipmentModel(DeleteEquipmentModelRequest request, CallContext callContext = default!);
    Task<DeleteEquipmentResponse> DeleteEquipment(DeleteEquipmentRequest request, CallContext callContext = default!);
}
#region GetAllEquipmentModels
[ProtoContract]
public class GetAllEquipmentModelsRequest: DepartmentMatchedRequest { }

[ProtoContract]
public class EquipmentModelDto
{
    [ProtoMember(1)] public int Id { get; set; }
    [ProtoMember(2)] public string Name { get; set; } = null!;
    [ProtoMember(3)] public string? Description { get; set; }

    public List<EquipmentDto>? Equipments { get; set; }
}

[ProtoContract]
public class GetAllEquipmentModelsResponse
{
    [ProtoMember(1)] public List<EquipmentModelDto>? EquipmentModels { get; set; } 
}
#endregion

#region GetEquipmentsForModel
[ProtoContract]
public class GetEquipmentsForModelRequest : DepartmentMatchedRequest
{
    [ProtoMember(2)] public int EquipmentModelId { get; set; }
}

[ProtoContract]
public class EquipmentDto
{
    [ProtoMember(1)] public int Id { get; set; }
    [ProtoMember(2)] public string? InvNumber { get; set; }
    [ProtoMember(3)] public string? Comment { get; set; }
    [ProtoMember(4)] public int EquipmentModelId { get; set; }
}

[ProtoContract]
public class GetEquipmentForModelResponse
{
    [ProtoMember(1)] public List<EquipmentDto>? Equipments { get; set; }
}
#endregion

#region DeleteEqipmentModel

[ProtoContract]
public class DeleteEquipmentModelRequest : DepartmentMatchedRequest
{
    [ProtoMember(2)] public int EquipmentModelId { get; set; }
}

[ProtoContract]
[ProtoInclude(2, typeof(DeleteEquipmentModelResponseFailed))]
[ProtoInclude(3, typeof(DeleteEquipmentModelResponseSuccess))]
public class DeleteEquipmentModelResponse
{
    
}

[ProtoContract]
public class DeleteEquipmentModelResponseSuccess: DeleteEquipmentModelResponse
{
    
}

[ProtoContract]
public class DeleteEquipmentModelResponseFailed: DeleteEquipmentModelResponse
{
    [ProtoMember(1)] public string? ErrorMessage { get; set; }
}

#endregion

#region DeletEqipment
[ProtoContract]
public class DeleteEquipmentRequest : DepartmentMatchedRequest
{
    [ProtoMember(2)] public int EquipmentId { get; set; }
}

[ProtoContract]
[ProtoInclude(2, typeof(DeleteEquipmentResponseFailed))]
[ProtoInclude(3, typeof(DeleteEquipmentResponseSuccess))]
public class DeleteEquipmentResponse
{
    
}

[ProtoContract]
public class DeleteEquipmentResponseSuccess: DeleteEquipmentResponse
{
    
}

[ProtoContract]
public class DeleteEquipmentResponseFailed: DeleteEquipmentResponse
{
    [ProtoMember(1)] public string? ErrorMessage { get; set; }
}
#endregion

