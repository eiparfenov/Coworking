using ProtoBuf;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Coworking.Shared.Services;


[Service("rtu-tc.admin.equipments")]
public interface IAdminEquipmentsGrpcService
{
    Task<GetAllEquipmentModelsResponse> GetAllEquipmentModels(GetAllEquipmentModelsRequest request, CallContext context = default!);
    Task<GetEquipmentForModelResponse> GetEquipmentsForModel(GetEquipmentsForModelRequest request, CallContext context = default!);
    Task<DeleteEquipmentModelResponse> DeleteEquipmentModel(DeleteEquipmentModelRequest request, CallContext callContext = default!);
    Task<DeleteEquipmentResponse> DeleteEquipment(DeleteEquipmentRequest request, CallContext callContext = default!);
    Task<UpdateEquipmentModelResponse> UpdateEquipmentModel(UpdateEquipmentModelRequest request, CallContext callContext = default!);
    Task<UpdateEquipmentResponse> UpdateEquipment(UpdateEquipmentRequest request, CallContext callContext = default!);
    Task<CreateEquipmentModelResponse> CreateEquipmentModel(CreateEquipmentModelRequest request, CallContext callContext = default!);
    Task<CreateEquipmentResponse> CreateEquipment(CreateEquipmentRequest request, CallContext callContext = default!);
    
}

#region GetAllEquipmentModels

[ProtoContract]
public class GetAllEquipmentModelsRequest
{
    [ProtoMember(1)] public string? DepartmentName { get; set; }
}

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
public class GetEquipmentsForModelRequest
{
    [ProtoMember(1)] public string? DepartmentName { get; set; }

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
public class DeleteEquipmentModelRequest
{
    [ProtoMember(1)] public string? DepartmentName { get; set; }
    [ProtoMember(2)] public int EquipmentModelId { get; set; }
}

[ProtoContract]
public class DeleteEquipmentModelResponse
{
    
}

#endregion

#region DeletEqipment
[ProtoContract]
public class DeleteEquipmentRequest
{
    [ProtoMember(1)] public string? DepartmentName { get; set; }
    [ProtoMember(2)] public int EquipmentId { get; set; }
}

[ProtoContract]
public class DeleteEquipmentResponse
{
    
}

#endregion

#region UpdateEquipmentModel

[ProtoContract]
public class UpdateEquipmentModelRequest
{
    [ProtoMember(1)] public string? DepartmentName { get; set; }
    [ProtoMember(2)] public int Id { get; set; }
    [ProtoMember(3)] public string? Name { get; set; }
    [ProtoMember(4)] public string? Description { get; set; }
}

[ProtoContract]
public class UpdateEquipmentModelResponse
{
    
}

#endregion

#region UpdateEquipment

[ProtoContract]
public class UpdateEquipmentRequest
{
    [ProtoMember(1)] public string? DepartmentName { get; set; }

    [ProtoMember(2)] public int Id { get; set; }
    [ProtoMember(3)] public string? InvNumber { get; set; }
    [ProtoMember(4)] public string? Comment { get; set; }
}

[ProtoContract]
public class UpdateEquipmentResponse
{
    
}

#endregion

#region CreateEquipmentModel

[ProtoContract]
public class CreateEquipmentModelRequest
{
    [ProtoMember(1)] public string? DepartmentName { get; set; }
    [ProtoMember(2)] public int Id { get; set; }
    [ProtoMember(3)] public string? Name { get; set; }
    [ProtoMember(4)] public string? Description { get; set; }
}

[ProtoContract]
public class CreateEquipmentModelResponse
{
    [ProtoMember(1)] public int Id { get; set; }
}

#endregion

#region CreateEquipment

[ProtoContract]
public class CreateEquipmentRequest
{
    [ProtoMember(1)] public string? DepartmentName { get; set; }
    [ProtoMember(2)] public string? InvNumber { get; set; }
    [ProtoMember(3)] public string? Comment { get; set; }
    [ProtoMember(4)] public int EquipmentModelId { get; set; }
}

[ProtoContract]
public class CreateEquipmentResponse
{
    [ProtoMember(1)] public int Id { get; set; }
}

#endregion
