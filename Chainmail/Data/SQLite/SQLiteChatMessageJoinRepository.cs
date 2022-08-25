using Chainmail.Model;
using Dapper;

namespace Chainmail.Data;

public class SQLiteChatMessageJoinRepository : SQLiteDB, IChatMessageJoinRepository
{
    public SQLiteChatMessageJoinRepository(string dbPath) : base(dbPath)
    {
    }

    public ChatMessageJoin Get(int id)
    {
        //Check if the _dbpath is found if not throw a filenotfounde exception
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException();
        }
        //CreateConnection then get a chatmessagejoin by id
        using var db = CreateConnection();
        return db.QueryFirstOrDefault<ChatMessageJoin>("SELECT * FROM chat_message_join WHERE Id = @id", new { id });
    }

    public IEnumerable<ChatMessageJoin> GetAll()
    {
        throw new NotImplementedException();
    }
}