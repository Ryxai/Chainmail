using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;

/// <summary>
/// Interface for querying about SyncDeletedChats from the database
/// </summary>
public interface ISyncDeletedChatRepository
{
    
    /// <summary>
    /// Gets a SyncDeletedChat by the rowid in the database
    /// </summary>
    /// <param name="rowid">The primary key of the SyncDeletedChat</param>
    /// <returns>Either a SyncDeletedChat if the rowid occurs or null</returns>
    public SyncDeletedChat GetSyncDeletedChat(int rowid);
    
    /// <summary>
    /// Gets all of the handles from the SyncDeletedChat table
    /// </summary>
    /// <returns>An enumerable containing all of the SyncDeletedChats or returns null if none in the table</returns>
    public IEnumerable<SyncDeletedChat> GetSyncDeletedChats();
}