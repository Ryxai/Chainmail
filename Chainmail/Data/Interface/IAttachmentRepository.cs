namespace Chainmail.Data;
using Chainmail.Model;

public interface IAttachmentRepository
{
    public Attachment GetAttachment(int rowid);
    public IEnumerable<Attachment> GetAttachments();
}