using Chainmail.Model;
using Chainmail.Model.SQL;
using Dapper;

namespace Chainmail.Data;

public class SqLiteChatRepository: SQLiteDB, IChatRepository
{
    public SqLiteChatRepository(string dbPath) : base(dbPath)
    {
    }

    public Chat GetChat(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var db = CreateConnection();
        return db.QueryFirstOrDefault<Chat>("SELECT * FROM chat WHERE ROWID = @rowid", new { rowid });
    }

    public IEnumerable<Chat> GetChats()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var db = CreateConnection();
        return db.Query<Chat>("SELECT * FROM chat") ?? new List<Chat>();
    }
}