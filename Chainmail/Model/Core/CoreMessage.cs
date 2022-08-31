using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Chainmail.Model.SQL;

namespace Chainmail.Model.Core;

public class CoreMessage : ISerializable
{
    public string Text { get; set; }
    public DateTime  Date { get; set; }
    public List<CoreAttachment> Attachments { get; set; }
    public bool IsSentFromMe { get; set; }
    public bool IsDeleted { get; set; }

    public CoreMessage(Message message, bool isDeleted, IEnumerable<CoreAttachment> attachments)
    {
        Text = message.text;
        Date = new DateTime(message.date);
        Attachments = attachments.ToList();
        IsSentFromMe = message.is_from_me == 1;
        IsDeleted = isDeleted;
    }

    public string[] Serialize()
    {
        var sb = new StringBuilder();
        foreach(var attachment in Attachments)
        {
            sb.AppendLine(attachment.ToString());
        }

        return new[] { $"Message:\nText:${Text}\nDate:{Date.ToString()}\nAttachments:\n{sb.ToString()}\nIs Sent From Me:{IsSentFromMe}\nIsDeleted:{IsDeleted}" };
    }
    
    public string SerializeAsJson()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
    }

}
