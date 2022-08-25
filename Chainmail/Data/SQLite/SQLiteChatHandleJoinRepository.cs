using Chainmail.Model;
using Dapper;
namespace Chainmail.Data;

public class SQLiteChatHandleJoinRepository : SQLiteDB, IChatHandleJoinRepository
{
    public SQLiteChatHandleJoinRepository(string dbPath) : base(dbPath)
    {
    }

    public IEnumerable<ChatHandleJoin> Get()
    {
        //Check if the dbPath exists if not throw file not found
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("File not found at path: " + _dbPath);
        }
        using var db = CreateConnection();
        return db.Query<ChatHandleJoin>("SELECT * FROM chat_handle_join");
    }
}