using ProtoBuf;

namespace Coworking.Shared.Services;

[ProtoContract]
public class DepartmentMatchedRequest
{
    [ProtoMember(20)]
    public string DepartmentName { get; set; }
}