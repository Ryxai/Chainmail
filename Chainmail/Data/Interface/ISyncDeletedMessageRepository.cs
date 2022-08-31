using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;

/// <summary>
/// Interface for querying about SyncDeletedMessages from the database
/// </summary>
public interface ISyncDeletedMessageRepository
{
    
    /// <summary>
    /// Gets a SyncDeletedMessage by the rowid in the database
    /// </summary>
    /// <param name="rowid">The primary key of the SyncDeletedMessage</param>
    /// <returns>Either a SyncDeletedMessage if the rowid occurs or null</returns>
    public SyncDeletedMessage GetSyncDeletedMessage(int rowid);
    
    /// <summary>
    /// Gets all of the handles from the SyncDeletedMessage table
    /// </summary>
    /// <returns>An enumerable containing all of the SyncDeletedMessages or returns null if none in the table</returns>
    public IEnumerable<SyncDeletedMessage> GetSyncDeletedMessages();
}