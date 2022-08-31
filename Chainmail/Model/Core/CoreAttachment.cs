using System.Text.Encodings.Web;
using Chainmail.Model.SQL;
using System.Text.Json;
namespace Chainmail.Model.Core;

/// <summary>
/// An internal representation of an Attachment, used for serialization to other formats.
/// </summary>
public class CoreAttachment : ISerializable
{
    /// <summary>
    /// The filename of the attachment.
    /// </summary>
    public string FileName { get; set; }
    /// <summary>
    /// The location of the attachment in the filesystem.
    /// </summary>
    public string Path { get; set; }
    /// <summary>
    /// The type of content (e.g. "image/png").
    /// </summary>
    public string ContentType { get; set; }
    /// <summary>
    /// Whether or not the attachment has been deleted
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Creates a CoreAttachment from an Attachment.
    /// </summary>
    /// <param name="attachment">An attachment instance</param>
    /// <param name="isDeleted">A boolean representing if the attachment is considered deleted by the database</param>
    public CoreAttachment(Attachment? attachment, bool isDeleted)
    {
        FileName = attachment.transfer_name;
        Path = attachment.filename;
        ContentType = attachment.mime_type ?? attachment.uti;
        IsDeleted = isDeleted;
    }

    /// <summary>
    /// Serializes the CoreAttachment into a formatted string array to be exported
    /// </summary>
    /// <returns>A single entry string array containing the formatted string representing the attachment</returns>
    public string[] Serialize()
    {
        return new[] {$@"Attachment:\n\tFilename: {FileName}\n\tPath: {Path}\n\tMIME-Type: {ContentType}\n\tWas-Deleted:{IsDeleted}".ToString()};
    }
    
    /// <summary>
    /// Serializes the attachment into a JSON string
    /// </summary>
    /// <returns>A string containing the JSON formatted version of the object</returns>
    public string SerializeAsJson()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
    }
}