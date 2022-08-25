using Chainmail.Model;
using Dapper;

namespace Chainmail.Data;

public class SQLiteChatMessageJoinRepository : SQLiteDB, IChatMessageJoinRepository
{
    public SQLiteChatMessageJoinRepository(string dbPath) : base(dbPath)
    {
    }

    public IEnumerable<ChatMessageJoin> Get()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var db = CreateConnection();
        return db.Query<ChatMessageJoin>("SELECT * FROM chat_message_join");
    }
}