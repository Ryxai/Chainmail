namespace Chainmail.Model.Core;

public class CoreAttachment
{
    public string FileName { get; set; }
    public string Path { get; set; }
    public string ContentType { get; set; }

    public CoreAttachment(Attachment? attachment)
    {
        FileName = attachment.transfer_name;
        Path = attachment.filename;
        ContentType = attachment.mime_type ?? attachment.uti;
    }
}