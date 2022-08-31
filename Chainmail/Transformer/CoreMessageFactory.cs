using Chainmail.Model;
using Chainmail.Model.Core;
using System;
using Chainmail.Model.SQL;
using LanguageExt;

namespace Chainmail.Transformer;

public class CoreMessageFactory : IFactory<CoreMessage>
{
    private Message? _message;
    private Dictionary<string, Attachment>? _attachments;
    private Dictionary<string, SyncDeletedAttachment>? _deletedAttachments;
    private DeletedMessage? _deletedMessage;
    private SyncDeletedMessage? _syncDeletedMessage;
    private readonly CoreAttachmentFactory _attachmentFactory;
    private LoadStateFlags _loadStateFlags;
    
    [Flags]
    private enum LoadStateFlags
    {
        None = 0,
        Message = 1,
        Attachments = 2,
        DeletedAttachments = 4,
        DeletedMessage = 8,
        SyncDeletedMessage = 16,
        All = Message | Attachments | DeletedAttachments | DeletedMessage | SyncDeletedMessage
    }

    private void Initialize()
    {
        _message = null;
        _attachments = new Dictionary<string, Attachment>();
        _deletedAttachments = new Dictionary<string, SyncDeletedAttachment>();
        _deletedMessage = null;
        _syncDeletedMessage = null;
        _loadStateFlags = LoadStateFlags.None;
    }
    
    public CoreMessageFactory()
    {
        Initialize();
        _attachmentFactory = new CoreAttachmentFactory();
    }
    
    public bool Load(Message? message)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.Message))
            return false;
        _loadStateFlags |= LoadStateFlags.Message;
        _message = message;
        return true;
    }
    
    //A load method for attachments that returns true if the attachments were loaded successfully.
    public bool Load(IEnumerable<Attachment> attachments)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.Attachments))
            return false;
        _loadStateFlags |= LoadStateFlags.Attachments;
        if (attachments.Length() == 0)
            return true;
        _attachments = attachments.ToDictionary(
            x => x.guid,
            x => x);
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
            x => x);
        return true;
    }
    
    public bool Load(DeletedMessage deletedMessage)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.DeletedMessage))
            return false;
        _loadStateFlags |= LoadStateFlags.DeletedMessage;
        _deletedMessage = deletedMessage;
        return true;
    }
    
    public bool Load(SyncDeletedMessage syncDeletedMessage)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.SyncDeletedMessage))
            return false;
        _loadStateFlags |= LoadStateFlags.SyncDeletedMessage;
        _syncDeletedMessage = syncDeletedMessage;
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

    public Option<CoreMessage> Assemble()
    {
        if (!IsReady())
            return Option<CoreMessage>.None;
        var processedAttachments = _attachments!.Select(x =>
        {
            _attachmentFactory.Load(x.Value);
            if (_deletedAttachments!.ContainsKey(x.Key))
                _attachmentFactory.Load(_deletedAttachments![x.Key]);
            _attachmentFactory.ForceReadyState();
            return _attachmentFactory.Assemble();
        }).Select(x => x.AsEnumerable()).
            SelectMany(x => x);
        var isDeleted = _deletedMessage != null && _deletedMessage.guid.Equals(_message?.guid);
        var results = new CoreMessage(_message!, isDeleted, processedAttachments);
        Initialize();
        return results;
    }
}