namespace Chainmail.Data;
using Chainmail.Model;

public interface ISyncDeletedAttachmentRepository
{
    public SyncDeletedAttachment Get(int rowId);
    public IEnumerable<SyncDeletedAttachment> GetAll();
}