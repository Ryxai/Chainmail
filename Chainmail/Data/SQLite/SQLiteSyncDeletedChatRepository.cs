using Chainmail.Model;
using Chainmail.Model.SQL;
using Dapper;
namespace Chainmail.Data;

/// <summary>
/// An implementation of an SQLite database query provider and a Query Layer for SyncDeletedChats
/// </summary>
public class SQLiteSyncDeletedChatRepository : SQLiteDB, ISyncDeletedChatRepository
{
    /// <summary>
    /// Builds a new instance of the SQLiteSyncDeletedChatRepository class
    /// </summary>
    /// <param name="dbPath">The path of the SQLite database file</param>
    public SQLiteSyncDeletedChatRepository(string dbPath) : base(dbPath)
    {
    }

    /// <summary>
    /// Gets a single SyncDeletedChat by its ID
    /// </summary>
    /// <param name="rowid">The primary key of the SyncDeletedChat</param>
    /// <returns>A single attachment if its key exists or null</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public SyncDeletedChat GetSyncDeletedChat(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.QueryFirstOrDefault<SyncDeletedChat>("SELECT * FROM sync_deleted_chats ROWID = @rowid", new { rowid });
    }

    /// <summary>
    /// Gets all of the SyncDeletedChat from the SyncDeletedChat table
    /// </summary>
    /// <returns>An enumerable containing all the SyncDeletedChats or null if the table is empty</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public IEnumerable<SyncDeletedChat> GetSyncDeletedChats()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException();
        }
        using var db = CreateConnection();
        return db.Query<SyncDeletedChat>("SELECT * FROM sync_deleted_chats") ?? new List<SyncDeletedChat>();
    }
}