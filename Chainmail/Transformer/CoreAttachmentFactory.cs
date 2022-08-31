using Chainmail.Model;
using Chainmail.Model.Core;
using Chainmail.Model.SQL;
using LanguageExt;

namespace Chainmail.Transformer;


public class CoreAttachmentFactory : IFactory<CoreAttachment>
{
    private Attachment? _attachment;
    private SyncDeletedAttachment? _syncDeletedAttachment;
    private LoadStateFlags _loadStateFlags;

    [Flags]
    private enum LoadStateFlags
    {
        None = 0,
        Attachment = 1,
        SyncDeletedAttachment = 2,
        All = Attachment | SyncDeletedAttachment
    }

    private void Initialize()
    {
        _attachment = null;
        _syncDeletedAttachment = null;
        _loadStateFlags = LoadStateFlags.None;
    }

    public CoreAttachmentFactory()
    {
        Initialize();
    }

    public bool Load(Attachment? attachment)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.Attachment))
            return false;
        _loadStateFlags |= LoadStateFlags.Attachment;
        _attachment = attachment;
        return true;
    }
    
    public bool Load(SyncDeletedAttachment? syncDeletedAttachment)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.SyncDeletedAttachment))
            return false;
        _loadStateFlags |= LoadStateFlags.SyncDeletedAttachment;
        _syncDeletedAttachment = syncDeletedAttachment;
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
    
    public Option<CoreAttachment> Assemble()
    {
        if (!IsReady())
            return Option<CoreAttachment>.None;
        var isDeleted = _syncDeletedAttachment != null && _syncDeletedAttachment.guid.Equals(_attachment?.guid);
        var results = new CoreAttachment(_attachment, isDeleted);
        Initialize();
        return results;
    }

}