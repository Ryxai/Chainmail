using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;

/// <summary>
/// Interface for querying about SyncDeletedAttachments from the database
/// </summary>
public interface ISyncDeletedAttachmentRepository
{
    
    /// <summary>
    /// Gets a SyncDeletedAttachment by the rowid in the database
    /// </summary>
    /// <param name="rowid">The primary key of the SyncDeletedAttachment</param>
    /// <returns>Either a SyncDeletedAttachment if the rowid occurs or null</returns>
    public SyncDeletedAttachment GetSyncDeletedAttachment(int rowid);
    
    /// <summary>
    /// Gets all of the SyncDeletedAttachments from the SyncDeletedAttachment table
    /// </summary>
    /// <returns>An enumerable containing all of the SyncDeletedAttachments or returns null if none in the table</returns>
    public IEnumerable<SyncDeletedAttachment> GetSyncDeletedAttachments();
}