using Chainmail.Model;
using Chainmail.Model.SQL;
using Dapper;
namespace Chainmail.Data;

/// <summary>
/// An implementation of an SQLite database query provider and a Query Layer for SyncDeletedAttachments
/// </summary>
public class SQLiteSyncDeletedAttachmentRepository: SQLiteDB, ISyncDeletedAttachmentRepository
{
    /// <summary>
    /// Builds a new instance of the SQLiteSyncDeletedAttachedRepository class
    /// </summary>
    /// <param name="dbPath">The path of the SQLite database file</param>
    public SQLiteSyncDeletedAttachmentRepository(string dbPath) : base(dbPath)
    {
    }

    /// <summary>
    /// Gets a single SyncDeletedAttachment by its ID
    /// </summary>
    /// <param name="rowid">The primary key of the SyncDeletedAttachment</param>
    /// <returns>A single attachment if its key exists or null</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public SyncDeletedAttachment GetSyncDeletedAttachment(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.QueryFirstOrDefault<SyncDeletedAttachment>("SELECT * FROM sync_deleted_attachments WHERE ROWID = @rowid", new { rowid });
    }

    /// <summary>
    /// Gets all of the SyncDeletedAttachments from the SyncDeletedAttachment table
    /// </summary>
    /// <returns>An enumerable containing all the Attachments or null if the table is empty</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public IEnumerable<SyncDeletedAttachment> GetSyncDeletedAttachments()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException();
        }
        using var db = CreateConnection();
        return db.Query<SyncDeletedAttachment>("SELECT * FROM sync_deleted_attachments") ?? new List<SyncDeletedAttachment>();
    }
}