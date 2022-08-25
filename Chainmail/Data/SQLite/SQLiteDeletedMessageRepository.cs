using Chainmail.Model;

namespace Chainmail.Data;
using Dapper;
public class SQLiteDeletedMessageRepository : SQLiteDB, IDeletedMessageRepository
{
    public SQLiteDeletedMessageRepository(string dbPath) : base(dbPath)
    {
    }

    public DeletedMessage GetDeletedMessage(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.QueryFirstOrDefault<DeletedMessage>("SELECT * FROM deleted_messages WHERE ROWID = @rowid", new { rowid });
    }

    public IEnumerable<DeletedMessage> GetDeletedMessages()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.Query<DeletedMessage>("SELECT * FROM deleted_messages");
    }
}