using System.Text.Encodings.Web;
using System.Text.Json;
using Chainmail.Model.SQL;

namespace Chainmail.Model.Core;

public class CoreHandle : ISerializable
{
    public string Id { get; set; }

    public CoreHandle(Handle? handle)
    {
        Id = handle.id;
    }

    public string[] Serialize()
    {
        return new[] {$@"Contact:\n\tID:{Id}"};
    }
    
    public string SerializeAsJson()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions {WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
    }
    
    
}