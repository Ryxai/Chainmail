using Chainmail.Model;
using Chainmail.Model.SQL;
using Dapper;
namespace Chainmail.Data;

public class SQLiteSyncDeletedMessageRepository : SQLiteDB, ISyncDeletedMessageRepository
{
    public SQLiteSyncDeletedMessageRepository(string dbPath) : base(dbPath)
    {
    }

    public SyncDeletedMessage GetSyncDeletedMessage(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.QueryFirstOrDefault<SyncDeletedMessage>("SELECT * FROM sync_deleted_messages WHERE ROWID = @rowid", new { rowid });
    }

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