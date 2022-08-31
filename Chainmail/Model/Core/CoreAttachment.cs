using System.Text.Encodings.Web;
using Chainmail.Model.SQL;
using System.Text.Json;
namespace Chainmail.Model.Core;

public class CoreAttachment : ISerializable
{
    public string FileName { get; set; }
    public string Path { get; set; }
    public string ContentType { get; set; }
    public bool IsDeleted { get; set; }

    public CoreAttachment(Attachment? attachment, bool isDeleted)
    {
        FileName = attachment.transfer_name;
        Path = attachment.filename;
        ContentType = attachment.mime_type ?? attachment.uti;
        IsDeleted = isDeleted;
    }

    public string[] Serialize()
    {
        return new[] {$@"Attachment:\n\tFilename: {FileName}\n\tPath: {Path}\n\tMIME-Type: {ContentType}\n\tWas-Deleted:{IsDeleted}".ToString()};
    }
    
    public string SerializeAsJson()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
    }
}