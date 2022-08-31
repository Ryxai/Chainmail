using Chainmail.Model;
using Chainmail.Model.SQL;
using Dapper;

namespace Chainmail.Data;


/// <summary>
/// An implementation of an SQLite database query provider and a Query Layer for Chats
/// </summary>
public class SqLiteChatRepository: SQLiteDB, IChatRepository
{
    
    /// <summary>
    /// Builds a new instance of the SQLiteChatRepository class
    /// </summary>
    /// <param name="dbPath">The path of the SQLite database file</param>
    public SqLiteChatRepository(string dbPath) : base(dbPath)
    {
    }

    /// <summary>
    /// Gets a single Chat by its ID
    /// </summary>
    /// <param name="rowid">The primary key of the Chat</param>
    /// <returns>A single attachment if its key exists or null</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public Chat GetChat(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var db = CreateConnection();
        return db.QueryFirstOrDefault<Chat>("SELECT * FROM chat WHERE ROWID = @rowid", new { rowid });
    }

    /// <summary>
    /// Gets all of the Chat from the Chat table
    /// </summary>
    /// <returns>An enumerable containing all the Chats or null if the table is empty</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
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