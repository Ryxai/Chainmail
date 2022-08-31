using System.Text.Encodings.Web;
using System.Text.Json;

namespace Chainmail.Model.Core;

public class CoreDatabase : ISerializable
{
    public List<CoreChatThread> ChatThreads { get; set; }

    public CoreDatabase(IEnumerable<CoreChatThread> chatThreads)
    {
        ChatThreads = chatThreads.ToList();
    }

    public string[] Serialize()
    {
        return ChatThreads.Select(thread => thread.Serialize()).SelectMany(x => x).ToArray();
    }

    public string SerializeAsJson()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
    }
}