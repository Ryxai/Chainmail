using Chainmail.Model;
using Dapper;
namespace Chainmail.Data;

public class SQLiteMessageRepository : SQLiteDB, IMessageRespository 
{
    public SQLiteMessageRepository(string dbPath) : base(dbPath)
    {
    }

    public Message GetMessage(int id)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        //Use the CreateConnection method and get a message by id
        using var db = CreateConnection();
        return db.QueryFirstOrDefault<Message>("SELECT * FROM message WHERE ROWID = @id", new { id });
    }

    public IEnumerable<Message> GetAllMessages()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var db = CreateConnection();
        return db.Query<Message>("SELECT * FROM message");
    }
}