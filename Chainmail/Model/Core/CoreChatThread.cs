using LanguageExt;

namespace Chainmail.Model.Core;

public class CoreChatThread
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
        Messages.Sort<CoreMessage>()
    }
    
    public void SortHandles(IComparer<CoreHandle> comparer)
    {
        Handles.Sort(comparer);
    }
}