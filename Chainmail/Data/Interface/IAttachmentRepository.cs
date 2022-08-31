using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;

/// <summary>
/// Interface for querying about attachments from the database
/// </summary>
public interface IAttachmentRepository
{
    /// <summary>
    /// Gets a single attachment by the the rowid
    /// </summary>
    /// <param name="rowid">An integer which is the primary key for the attachment table</param>
    /// <returns>A single attachment from the able whose row_id matches </returns>
    public Attachment GetAttachment(int rowid);
    /// <summary>
    /// Gets all the attachments in the attachment table
    /// </summary>
    /// <returns>Returns all the attachments in an enumerable, if there are none it returns null</returns>
    public IEnumerable<Attachment> GetAttachments();
}