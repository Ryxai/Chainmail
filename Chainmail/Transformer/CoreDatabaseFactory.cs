using System.CodeDom;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using Chainmail.Model.Core;
using Chainmail.Model.SQL;
using LanguageExt;

namespace Chainmail.Transformer;

public class CoreDatabaseFactory: IFactory<CoreDatabase>
{
    private Dictionary<string, Chat>? _chats;
    private Dictionary<string, SyncDeletedChat>? _deletedChats;
    private Dictionary<string, Handle>? _handles;
    private Dictionary<long, List<Handle>>? _handlesByChat;
    private Dictionary<string, Attachment>? _attachments;
    private Dictionary<long, List<Attachment>>? _attachmentsByMessage;
    private Dictionary<string, SyncDeletedAttachment>? _deletedAttachments;
    private Dictionary<string, Message>? _messages;
    private Dictionary<long, List<Message>>? _messagesByChat;
    private Dictionary<string, DeletedMessage>? _deletedMessages;
    private Dictionary<string, SyncDeletedMessage>? _syncDeletedMessages;
    private readonly CoreChatThreadFactory _chatThreadFactory;
    private readonly IReadOnlyList<MessageAttachmentJoin> _messageAttachmentJoin;
    private readonly IReadOnlyList<ChatHandleJoin> _chatHandleJoins;
    private readonly IReadOnlyList<ChatMessageJoin> _chatMessageJoins;
    private LoadStateFlags _loadStateFlags;

    [Flags]
    private enum LoadStateFlags
    {
        None = 0,
        Chats = 1,
        DeletedChats = 2,
        Handles = 4,
        Attachments = 8,
        DeletedAttachments = 16,
        Messages = 32,
        DeletedMessages = 64,
        SyncDeletedMessages = 128,
        All = Chats | DeletedChats | Handles | Attachments | DeletedAttachments | Messages | DeletedMessages | SyncDeletedMessages
    }

    private CoreDatabaseFactory(){}

    private void Initialize()
    {
        _chats = new Dictionary<string, Chat>();
        _deletedChats = new Dictionary<string, SyncDeletedChat>();
        _handles = new Dictionary<string, Handle>();
        _handlesByChat = new Dictionary<long, List<Handle>>();
        _attachments = new Dictionary<string, Attachment>();
        _attachmentsByMessage = new Dictionary<long, List<Attachment>>();
        _deletedAttachments = new Dictionary<string, SyncDeletedAttachment>();
        _messages = new Dictionary<string, Message>();
        _messagesByChat = new Dictionary<long, List<Message>>();
        _deletedMessages = new Dictionary<string, DeletedMessage>();
        _syncDeletedMessages = new Dictionary<string, SyncDeletedMessage>();
        _loadStateFlags = LoadStateFlags.None;
    }

    private void ArrangeMessagesByChat()
    {
        _messagesByChat= (from cmj in _chatMessageJoins
            group cmj by cmj.chat_id
            into cmjGroup
            orderby cmjGroup.Key
            select new
            {
                Id = cmjGroup.Key,
                Messages= cmjGroup
                    .Select(x => 
                        _messages.First(y
                            => y.Value.ROWID == x.message_id).Value).ToList()
            }).ToDictionary(
            x => x.Id
            , x => x.Messages.ToList());

        /*        
        foreach (var cmj in _chatMessageJoins)
        {
            if (messageIdByChatId.ContainsKey(cmj.chat_id))
                messageIdByChatId[cmj.chat_id].Add(cmj.message_id);
            else
                messageIdByChatId[cmj.chat_id] = new List<int> { cmj.message_id };
        }
        _messagesByChat = new Dictionary<int, List<Message>>();
        foreach (var kvp in messageIdByChatId)
        {
            var messages = kvp.Value.Select(x => _messages.First(y => y.Value.ROWID == x).Value);
            _messagesByChat.Add(kvp.Key, messages);
        }*/
    }

    private void ArrangeHandlesByChat()
    {
        _handlesByChat = (from chj in _chatHandleJoins
            group chj by chj.chat_id
            into chjGroup
            orderby chjGroup.Key
            select new
            {
                Id = chjGroup.Key,
                Handles = chjGroup
                    .Select(x => 
                        _handles.First(y => 
                            y.Value.ROWID == x.handle_id).Value).ToList()
            }).ToDictionary(
            x => x.Id
            , x => x.Handles.ToList());
    }
    
    private void ArrangeAttachmentsByMessage()
    {
        _attachmentsByMessage = (from maj in _messageAttachmentJoin
            group maj by maj.message_id
            into majGroup
            orderby majGroup.Key
            select new
            {
                Id = majGroup.Key,
                Attachments = majGroup
                    .Select(x => _attachments.
                        First(y => y.Value.ROWID == x.attachment_id).Value).ToList()
            }).ToDictionary(
            x => x.Id,
            x => x.Attachments);
    }

    public CoreDatabaseFactory(IReadOnlyList<MessageAttachmentJoin> messageAttachmentJoins,
                            IReadOnlyList<ChatHandleJoin> chatHandleJoins,
        IReadOnlyList<ChatMessageJoin> chatMessageJoins)
    {
        Initialize();
        _messageAttachmentJoin = messageAttachmentJoins;
        _chatHandleJoins = chatHandleJoins;
        _chatMessageJoins = chatMessageJoins;
        _chatThreadFactory = new CoreChatThreadFactory(messageAttachmentJoins);
    }
    
    public bool Load(IEnumerable<Chat> chats)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.Chats))
            return false;
        _loadStateFlags |= LoadStateFlags.Chats;
        if (chats.Length() == 0)
            return true;
        _chats = chats.ToDictionary(
            x => x.guid,
            x => x);
        return true;
    }

    public bool Load(IEnumerable<SyncDeletedChat> deletedChats)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.DeletedChats))
            return false;
        _loadStateFlags |= LoadStateFlags.DeletedChats;
        if (deletedChats.Length() == 0)
            return true;
        _deletedChats = deletedChats.ToDictionary(
            x => x.guid,
            x => x
        );
        return true;
    }
    
    public bool Load(IEnumerable<Handle> handles)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.Handles))
            return false;
        _loadStateFlags |= LoadStateFlags.Handles;
        if (handles.Length() == 0)
            return true;
        _handles = handles.ToDictionary(
            x => x.id,
            x => x
        );
        return true;
    }
    
    public bool Load(IEnumerable<Attachment> attachments)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.Attachments))
            return false;
        _loadStateFlags |= LoadStateFlags.Attachments;
        if (attachments.Length() == 0)
            return true;
        _attachments = attachments.ToDictionary(
            x => x.guid,
            x => x
        );
        return true;
    }
    
    public bool Load(IEnumerable<SyncDeletedAttachment> deletedAttachments)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.DeletedAttachments))
            return false;
        _loadStateFlags |= LoadStateFlags.DeletedAttachments;
        if (deletedAttachments.Length() == 0)
            return true;
        _deletedAttachments = deletedAttachments.ToDictionary(
            x => x.guid,
            x => x
        );
        return true;
    }
    
    public bool Load(IEnumerable<Message> messages)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.Messages))
            return false;
        _loadStateFlags |= LoadStateFlags.Messages;
        if (messages.Length() == 0)
            return true;
        _messages = messages.ToDictionary(
            x => x.guid, x => x
        );
        return true;
    }
    
    public bool Load(IEnumerable<DeletedMessage> deletedMessages)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.DeletedMessages))
            return false;
        _loadStateFlags |= LoadStateFlags.DeletedMessages;
        if (deletedMessages.Length() == 0)
            return true;
        _deletedMessages = deletedMessages.ToDictionary(
            x => x.guid,
            x => x);
        return true;
    }
    
    public bool Load(IEnumerable<SyncDeletedMessage> syncDeletedMessages)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.SyncDeletedMessages))
            return false;
        _loadStateFlags |= LoadStateFlags.SyncDeletedMessages;
        if (syncDeletedMessages.Length() == 0)
            return true;
        _syncDeletedMessages = syncDeletedMessages.ToDictionary(
            x => x.guid,
            x => x);
        return true;
    }
    
    public bool IsReady()
    {
        return _loadStateFlags == LoadStateFlags.All;
    }

    public void ForceReadyState()
    {
        _loadStateFlags = LoadStateFlags.All;
    }

    public Option<CoreDatabase> Assemble()
    {
        if (!IsReady())
            return Option<CoreDatabase>.None;
        ArrangeHandlesByChat();
        ArrangeMessagesByChat();
        ArrangeAttachmentsByMessage();
        var chats = _chats!.Values.Select(x =>
        {
            _chatThreadFactory.Load(x);
            if (_handlesByChat.ContainsKey(x.ROWID))
                _chatThreadFactory.Load(_handlesByChat![x.ROWID]);
            if (_messagesByChat.ContainsKey(x.ROWID))
            {
                _chatThreadFactory.Load(_messagesByChat![x.ROWID]);
                _chatThreadFactory.Load(_messagesByChat[x.ROWID].Where(y => _deletedMessages.ContainsKey(y.guid)).Select(y => _deletedMessages![y.guid]));
                _chatThreadFactory.Load(_messagesByChat[x.ROWID].Where(y => _syncDeletedMessages.ContainsKey(y.guid)).Select(y => _syncDeletedMessages![y.guid]));
                _chatThreadFactory.Load(_messagesByChat[x.ROWID].Where(y => _attachmentsByMessage.ContainsKey(y.ROWID)).SelectMany(y => _attachmentsByMessage![y.ROWID]));
                _chatThreadFactory.Load(_messagesByChat[x.ROWID].Where(y => _attachmentsByMessage.ContainsKey(y.ROWID)).SelectMany(y => _attachmentsByMessage![y.ROWID])
                    .Where(y => _deletedAttachments.ContainsKey(y.guid)).Select(y => _deletedAttachments![y.guid]));
            }
            if (_deletedMessages.ContainsKey(x.guid))
                _chatThreadFactory.Load(_deletedChats![x.guid]);
            _chatThreadFactory.ForceReadyState();
            return _chatThreadFactory.Assemble();
        }).Select(x => x.AsEnumerable()).SelectMany(x => x).ToList();
        var res = new CoreDatabase(chats);
        Initialize();
        return res;
    }
}