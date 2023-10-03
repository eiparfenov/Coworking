using ProtoBuf;

namespace Coworking.Shared.Services;

[ProtoContract]
public class DepartmentMatchedRequest
{
    [ProtoMember(1)] public string DepartmentName { get; set; } = null!;
}