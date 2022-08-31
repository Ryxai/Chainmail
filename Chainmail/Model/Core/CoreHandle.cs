using System.Text.Encodings.Web;
using System.Text.Json;
using Chainmail.Model.SQL;

namespace Chainmail.Model.Core;

/// <summary>
/// An internal representation of an Handle, used for serialization to other formats.
/// </summary>
public class CoreHandle : ISerializable
{
    /// <summary>
    /// The Handle's ID (e,g, phone number or email address for icloud etc)
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Constructs a CoreHandle from a Handle.
    /// </summary>
    /// <param name="handle">A handle</param>
    public CoreHandle(Handle? handle)
    {
        Id = handle.id;
    }

    /// <summary>
    /// Serializes the CoreHandle into a formatted string array to be exported
    /// </summary>
    /// <returns>A single entry string array containing the formatted string representing the handle</returns>
    public string[] Serialize()
    {
        return new[] {$@"Contact:\n\tID:{Id}"};
    }
    
    public string SerializeAsJson()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions {WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
    }
    
    
}