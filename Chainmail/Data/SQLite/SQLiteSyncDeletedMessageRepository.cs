using Chainmail.Model;
using Chainmail.Model.SQL;
using Dapper;
namespace Chainmail.Data;

/// <summary>
/// An implementation of an SQLite database query provider and a Query Layer for SyncDeletedMessages
/// </summary>
public class SQLiteSyncDeletedMessageRepository : SQLiteDB, ISyncDeletedMessageRepository
{
    /// <summary>
    /// Builds a new instance of the SQLiteSyncDeletedMessageRepository class
    /// </summary>
    /// <param name="dbPath">The path of the SQLite database file</param>
    public SQLiteSyncDeletedMessageRepository(string dbPath) : base(dbPath)
    {
    }

    /// <summary>
    /// Gets a single SyncDeletedMessage by its ID
    /// </summary>
    /// <param name="rowid">The primary key of the SyncDeletedMessage</param>
    /// <returns>A single attachment if its key exists or null</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public SyncDeletedMessage GetSyncDeletedMessage(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.QueryFirstOrDefault<SyncDeletedMessage>("SELECT * FROM sync_deleted_messages WHERE ROWID = @rowid", new { rowid });
    }

    /// <summary>
    /// Gets all of the SyncDeletedMessage from the SyncDeletedMessage table
    /// </summary>
    /// <returns>An enumerable containing all the SyncDeletedMessages or null if the table is empty</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public IEnumerable<SyncDeletedMessage> GetSyncDeletedMessages()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.Query<SyncDeletedMessage>("SELECT * FROM attachment") ?? new List<SyncDeletedMessage>();
    }
}