using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Chainmail.Model.SQL;

namespace Chainmail.Model.Core;

/// <summary>
/// An internal representation of an Message, used for serialization to other formats.
/// </summary>
public class CoreMessage : ISerializable
{
    /// <summary>
    /// The text of the message
    /// </summary>
    public string Text { get; set; }
    /// <summary>
    /// The date the message was sent
    /// </summary>
    public DateTime  Date { get; set; }
    /// <summary>
    /// A list of attachments to the message
    /// </summary>
    public List<CoreAttachment> Attachments { get; set; }
    /// <summary>
    /// If the person who the database belongs to sent the message
    /// </summary>
    public bool IsSentFromMe { get; set; }
    /// <summary>
    /// Whether or not the message has been deleted
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Builds a CoreMessage from a Message
    /// </summary>
    /// <param name="message">A message</param>
    /// <param name="isDeleted">A boolean representing whether the message has been deleted</param>
    /// <param name="attachments">A list of attachments</param>
    public CoreMessage(Message message, bool isDeleted, IEnumerable<CoreAttachment> attachments)
    {
        Text = message.text;
        Date = new DateTime(message.date);
        Attachments = attachments.ToList();
        IsSentFromMe = message.is_from_me == 1;
        IsDeleted = isDeleted;
    }

    /// <summary>
    /// Serializes the CoreMessage into a formatted string array to be exported
    /// </summary>
    /// <returns>A single entry string array containing the formatted string representing the message</returns>
    public string[] Serialize()
    {
        var sb = new StringBuilder();
        foreach(var attachment in Attachments)
        {
            sb.AppendLine(attachment.ToString());
        }

        return new[] { $"Message:\nText:${Text}\nDate:{Date.ToString()}\nAttachments:\n{sb.ToString()}\nIs Sent From Me:{IsSentFromMe}\nIsDeleted:{IsDeleted}" };
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
