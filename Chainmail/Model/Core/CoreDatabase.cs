using System.Text.Encodings.Web;
using System.Text.Json;

namespace Chainmail.Model.Core;

/// <summary>
/// An internal representation of a Database, used for serialization to other formats.
/// </summary>
public class CoreDatabase : ISerializable
{
    /// <summary>
    /// A list of chat threads
    /// </summary>
    public List<CoreChatThread> ChatThreads { get; set; }

    /// <summary>
    /// Constructs a database from a list of chat threads.
    /// </summary>
    /// <param name="chatThreads">A list of chats</param>
    public CoreDatabase(IEnumerable<CoreChatThread> chatThreads)
    {
        ChatThreads = chatThreads.ToList();
    }

    
    /// <summary>
    /// Serializes the CoreDatabase into a formatted string array to be exported
    /// </summary>
    /// <returns>A string array containing the formatted string representing the database</returns>
    public string[] Serialize()
    {
        return ChatThreads.Select(thread => thread.Serialize()).SelectMany(x => x).ToArray();
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