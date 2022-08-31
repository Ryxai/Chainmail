using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Chainmail.Model.SQL;
using LanguageExt;

namespace Chainmail.Model.Core;

public class CoreChatThread : ISerializable
{
    public string ChatIdentifier { get; set; }
    public string DisplayName { get; set; }
    public IEnumerable<CoreHandle> Handles { get; set; }
    public IEnumerable<CoreMessage> Messages { get; set; }
    public bool IsDeleted { get; set; }
    
    public CoreChatThread(Chat chat, IEnumerable<CoreHandle> handles, IEnumerable<CoreMessage> messages, bool isDeleted)
    {
        ChatIdentifier = chat.chat_identifier;
        DisplayName = chat.display_name;
        Handles = handles;
        Messages = messages;
        IsDeleted = isDeleted;
    }
    
    public void SortMessages(IComparer<CoreMessage> comparer)
    {
        Messages = Messages.OrderBy(x => x, comparer);
    }
    
    public void SortHandles(IComparer<CoreHandle> comparer)
    {
        Handles = Handles.OrderBy(x => x, comparer);
    }

    public string[] Serialize()
    {
        var handlesSB = new StringBuilder();
        var messagesSB = new StringBuilder();
        foreach(var handle in Handles)
        {
            handlesSB.Append(handle.Serialize());
        }
        foreach(var message in Messages)
        {
            messagesSB.Append(message.Serialize());
        }
        return new[] {$"Chat:\nChatIdentifier: {ChatIdentifier}\nDisplayName: {DisplayName}\nHandles: {handlesSB.ToString()}\nMessages: {messagesSB.ToString()}\nIsDeleted: {IsDeleted}", $"ChatIdentifier: {ChatIdentifier}\nDisplayName: {DisplayName}\nHandles: {handlesSB.ToString()}\nMessages: {messagesSB.ToString()}\nIsDeleted: {IsDeleted}"};
    }

    public string SerializeAsJson()
    {
        return JsonSerializer.Serialize(new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
    }
}