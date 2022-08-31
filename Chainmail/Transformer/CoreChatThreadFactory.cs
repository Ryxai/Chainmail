using Chainmail.Model;
using Chainmail.Model.Core;
using LanguageExt;
using System;
using Chainmail.Model.SQL;

namespace Chainmail.Transformer;

public class CoreChatThreadFactory : IFactory<CoreChatThread>
{
    private Chat? _chat;
    private IEnumerable<Handle>? _handles;
    private Dictionary<string, Attachment>? _attachments;
    private Dictionary<string, SyncDeletedAttachment>? _deletedAttachments;
    private Dictionary<string, Message>? _messages;
    private Dictionary<long, List<Attachment>> _attachmentsByMessage;
    private Dictionary<string, DeletedMessage>? _deletedMessages;
    private Dictionary<string, SyncDeletedMessage>? _syncDeletedMessages;
    private SyncDeletedChat? _syncDeletedChat;
    private readonly CoreHandleFactory _handleFactory;
    private readonly CoreMessageFactory _messageFactory;
    private readonly IReadOnlyList<MessageAttachmentJoin> _messageAttachmentJoin;
    private LoadStateFlags _loadStateFlags = LoadStateFlags.None;

    [Flags]
    private enum LoadStateFlags
    {
        None = 0,
        Chat = 1,
        Handles = 2,
        Attachments = 4,
        DeletedAttachments = 8,
        Messages = 16,
        DeletedMessages = 32,
        SyncDeletedMessages = 64,
        SyncDeletedChat = 128,
        All = Chat | Handles | Attachments | DeletedAttachments | Messages | DeletedMessages | SyncDeletedMessages | SyncDeletedChat
    }

    private CoreChatThreadFactory()
    {
        _handleFactory = new CoreHandleFactory();
        _messageFactory = new CoreMessageFactory();
    }

    public void Initialize()
    {
        _chat = null;
        _handles = new List<Handle>();
        _attachments = new Dictionary<string, Attachment>();
        _deletedAttachments = new Dictionary<string, SyncDeletedAttachment>();
        _messages = new Dictionary<string, Message>();
        _deletedMessages = new Dictionary<string, DeletedMessage>();
        _syncDeletedMessages = new Dictionary<string, SyncDeletedMessage>();
        _syncDeletedChat = null;
        _attachmentsByMessage = new Dictionary<long, List<Attachment>>();
        _loadStateFlags = LoadStateFlags.None;
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
                        FirstOrDefault(y => y.Value.ROWID == x.attachment_id).Value).ToList()
            }).ToDictionary(
            x => x.Id,
            x => x.Attachments);
    }

    public CoreChatThreadFactory(IEnumerable<MessageAttachmentJoin> messageAttachmentJoin)
    {
        Initialize();
        _messageAttachmentJoin = (IReadOnlyList<MessageAttachmentJoin>)messageAttachmentJoin;
        _handleFactory = new CoreHandleFactory();
        _messageFactory = new CoreMessageFactory();
    }
    
    public bool Load(Chat? chat)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.Chat))
            return false;
        _loadStateFlags |= LoadStateFlags.Chat;
        _chat = chat;
        return true;
    }

    public bool Load(IEnumerable<Handle>? handles)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.Handles))
            return false;
        _loadStateFlags |= LoadStateFlags.Handles;
        if (handles.Length() == 0)
            return true;
        _handles = handles;
        return true;
    }

    public bool Load(IEnumerable<Attachment> attachments)
    {     
        if (_loadStateFlags.HasFlag(LoadStateFlags.Attachments))
            return false;
        _loadStateFlags |= LoadStateFlags.Attachments;
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
    

    public bool Load(IEnumerable<Message>? messages)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.Messages))
            return false;
        _loadStateFlags |= LoadStateFlags.Messages;
        if (messages.Length() == 0)
            return true;
        _messages = messages!.ToDictionary(
            x => x.guid,
            x => x
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
            x => x
            );
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
            x => x
            );
        return true;
    }
    
    public bool Load(SyncDeletedChat? syncDeletedChat)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.SyncDeletedChat))
            return false;
        _loadStateFlags |= LoadStateFlags.SyncDeletedChat;
        _syncDeletedChat = syncDeletedChat;
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

    public Option<CoreChatThread> Assemble()
    {
        if (!IsReady())
            return Option<CoreChatThread>.None;
        ArrangeAttachmentsByMessage();
        var processedHandles = _handles!.Select(x =>
        {
            _handleFactory.Load(x);
            _handleFactory.ForceReadyState();
            return _handleFactory.Assemble();
        }).Select(x => x.AsEnumerable()).SelectMany(x => x);
        var processedMessages = _messages!.Select(x =>
        {
            _messageFactory.Load(x.Value);
            if (_attachmentsByMessage.ContainsKey(x.Value.ROWID))
            {
                _messageFactory.Load(_attachmentsByMessage[x.Value.ROWID]);
                _messageFactory.Load(_attachmentsByMessage[x.Value.ROWID].
                    Where(y => _deletedAttachments.ContainsKey(y.guid)).Select(y => _deletedAttachments![y.guid]));
            }
            if (_deletedMessages!.ContainsKey(x.Value.guid))
                _messageFactory.Load(_deletedMessages![x.Value.guid]);
            if (_syncDeletedMessages.ContainsKey(x.Value.guid))
                _messageFactory.Load(_syncDeletedMessages![x.Value.guid]);
            _messageFactory.ForceReadyState();
            return _messageFactory.Assemble();
        }).Select(x => x.AsEnumerable()).SelectMany(x => x);
        var isDeleted = _syncDeletedChat != null && _syncDeletedChat.guid.Equals(_chat!.guid);
        var res = new CoreChatThread(_chat!, processedHandles, processedMessages, isDeleted);
        Initialize();
        return res;
    }
}
