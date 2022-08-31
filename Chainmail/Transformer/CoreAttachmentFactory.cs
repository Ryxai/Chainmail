using Chainmail.Model;
using Chainmail.Model.Core;
using Chainmail.Model.SQL;
using LanguageExt;

namespace Chainmail.Transformer;

/// <summary>
/// A factory to assemble a CoreAttachment
/// </summary>
public class CoreAttachmentFactory : IFactory<CoreAttachment>
{
    /// <summary>
    /// An attachment to be assembled
    /// </summary>
    private Attachment? _attachment;
    /// <summary>
    /// A SyncDeletedAttachment which is used for the deleted status
    /// </summary>
    private SyncDeletedAttachment? _syncDeletedAttachment;
    /// <summary>
    /// A set of flags used to determine if the factory has been loaded appropriately
    /// </summary>
    private LoadStateFlags _loadStateFlags;

    /// <summary>
    /// Flags for each of the core load methods
    /// </summary>
    [Flags]
    private enum LoadStateFlags
    {
        None = 0,
        Attachment = 1,
        SyncDeletedAttachment = 2,
        All = Attachment | SyncDeletedAttachment
    }

    /// <summary>
    /// Initializes the factory to a default state
    /// </summary>
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