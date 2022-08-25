namespace Chainmail.Data;
using Chainmail.Model;

public interface ISyncDeletedAttachmentRepository
{
    public SyncDeletedAttachment GetSyncDeletedAttachment(int rowid);
    public IEnumerable<SyncDeletedAttachment> GetSyncDeletedAttachments();
}