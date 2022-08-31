using Chainmail.Model;
using Chainmail.Model.SQL;
using Dapper;
namespace Chainmail.Data;

public class SQLiteSyncDeletedAttachmentRepository: SQLiteDB, ISyncDeletedAttachmentRepository
{
    public SQLiteSyncDeletedAttachmentRepository(string dbPath) : base(dbPath)
    {
    }

    public SyncDeletedAttachment GetSyncDeletedAttachment(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.QueryFirstOrDefault<SyncDeletedAttachment>("SELECT * FROM sync_deleted_attachments WHERE ROWID = @rowid", new { rowid });
    }

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