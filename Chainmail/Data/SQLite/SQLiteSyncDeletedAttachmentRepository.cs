using Chainmail.Model;
using Dapper;
namespace Chainmail.Data;

public class SQLiteSyncDeletedAttachmentRepository: SQLiteDB, ISyncDeletedAttachmentRepository
{
    public SQLiteSyncedDeletedAttachmentRepository(string dbPath) : base(dbPath)
    {
    }

    public SyncDeletedAttachment Get(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.QueryFirstOrDefault<SyncDeletedAttachment>("SELECT * FROM sync_deleted_attachments WHERE ROWID = @rowid", new { rowid });
    }

    public IEnumerable<SyncDeletedAttachment> GetAll()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException();
        }
        using var db = CreateConnection();
        return db.Query<SyncDeletedAttachment>("SELECT * FROM handle");
    }
}