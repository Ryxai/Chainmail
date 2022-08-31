using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Chainmail.Model.SQL;
using LanguageExt;

namespace Chainmail.Model.Core;

/// <summary>
/// An internal representation of an Chat, used for serialization to other formats.
/// </summary>
public class CoreChatThread : ISerializable
{
    /// <summary>
    /// The identifier of the chat thread, whats formally used by the system
    /// </summary>
    public string ChatIdentifier { get; set; }
    /// <summary>
    /// A display name (the one that appears at the top of the conversation in Messages)
    /// </summary>
    public string DisplayName { get; set; }
    /// <summary>
    /// A list of handles involved in the Chat
    /// </summary>
    public IEnumerable<CoreHandle> Handles { get; set; }
    /// <summary>
    /// The messages that are part of the chat
    /// </summary>
    public IEnumerable<CoreMessage> Messages { get; set; }
    /// <summary>
    /// Whether or not the chat is deleted
    /// </summary>
    public bool IsDeleted { get; set; }
    
    /// <summary>
    /// Constructs a new CoreChatThread from a Chat and a list of Messages, Handles and a boolean
    /// </summary>
    /// <param name="chat">The chat to be constructed</param>
    /// <param name="handles">The handles involved in the chat</param>
    /// <param name="messages">The messages that are part of the chat</param>
    /// <param name="isDeleted">A boolean representing whether or not this chat has been deleted</param>
    public CoreChatThread(Chat chat, IEnumerable<CoreHandle> handles, IEnumerable<CoreMessage> messages, bool isDeleted)
    {
        ChatIdentifier = chat.chat_identifier;
        DisplayName = chat.display_name;
        Handles = handles;
        Messages = messages;
        IsDeleted = isDeleted;
    }
    
    /// <summary>
    /// Sorts the messages by the comparer provided
    /// </summary>
    /// <param name="comparer">A comparer object that shows how to compare Messages</param>
    public void SortMessages(IComparer<CoreMessage> comparer)
    {
        Messages = Messages.OrderBy(x => x, comparer);
    }
    
    
    /// <summary>
    /// Sorts the handle by the comparer provided
    /// </summary>
    /// <param name="comparer">A comparer object that shows how to compare Handles</param>
    public void SortHandles(IComparer<CoreHandle> comparer)
    {
        Handles = Handles.OrderBy(x => x, comparer);
    }

    /// <summary>
    /// Serializes the CoreChat into a formatted string array to be exported
    /// </summary>
    /// <returns>A single entry string array containing the formatted string representing the ChatThread</returns>
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
    

    /// <summary>
    /// Serializes the attachment into a JSON string
    /// </summary>
    /// <returns>A string containing the JSON formatted version of the object</returns>
    public string SerializeAsJson()
    {
        return JsonSerializer.Serialize(new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
    }
}