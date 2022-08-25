using Chainmail.Model;
using Dapper;
namespace Chainmail.Data;

public class SQLiteSyncDeletedChatRepository : SQLiteDB, ISyncDeletedChatRepository
{
    public SQLiteSyncDeletedChatRepository(string dbPath) : base(dbPath)
    {
    }

    public SyncDeletedChat GetSyncDeletedChat(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.QueryFirstOrDefault<SyncDeletedChat>("SELECT * FROM sync_deleted_chats ROWID = @rowid", new { rowid });
    }

    public IEnumerable<SyncDeletedChat> GetSyncDeletedChats()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException();
        }
        using var db = CreateConnection();
        return db.Query<SyncDeletedChat>("SELECT * FROM sync_deleted_chats");
    }
}