using Chainmail.Model;
using Chainmail.Model.SQL;
using Dapper;
namespace Chainmail.Data;

/// <summary>
/// An implementation of an SQLite database query provider and a Query Layer for Messages
/// </summary>
public class SQLiteMessageRepository : SQLiteDB, IMessageRespository 
{
    /// <summary>
    /// Builds a new instance of the SQLiteMessageRepository class
    /// </summary>
    /// <param name="dbPath">The path of the SQLite database file</param>
    public SQLiteMessageRepository(string dbPath) : base(dbPath)
    {
    }

    /// <summary>
    /// Gets a single Message by its ID
    /// </summary>
    /// <param name="rowid">The primary key of the Message</param>
    /// <returns>A single attachment if its key exists or null</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public Message GetMessage(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var db = CreateConnection();
        return db.QueryFirstOrDefault<Message>("SELECT * FROM message WHERE ROWID = @rowid", new { rowid });
    }

    /// <summary>
    /// Gets all of the Message from the Attachment table
    /// </summary>
    /// <returns>An enumerable containing all the Messages or null if the table is empty</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public IEnumerable<Message> GetMessages()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var db = CreateConnection();
        return db.Query<Message>("SELECT * FROM message") ?? new List<Message>();
    }
}