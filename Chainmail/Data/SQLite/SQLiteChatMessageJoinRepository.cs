using Chainmail.Model;
using Chainmail.Model.SQL;
using Dapper;

namespace Chainmail.Data;

/// <summary>
/// An implementation of an SQLite database query provider and a Query Layer for ChatMessageJoins
/// </summary>
public class SQLiteChatMessageJoinRepository : SQLiteDB, IChatMessageJoinRepository
{
    /// <summary>
    /// Builds a new instance of the SQLiteChatMessageJoinRepository class
    /// </summary>
    /// <param name="dbPath">The path of the SQLite database file</param>
    public SQLiteChatMessageJoinRepository(string dbPath) : base(dbPath)
    {
    }

    /// <summary>
    /// Gets all of the ChatMessageJoin from the ChatMessageJoin table
    /// </summary>
    /// <returns>An enumerable containing all the ChatMessageJoins or null if the table is empty</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public IEnumerable<ChatMessageJoin> Get()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var db = CreateConnection();
        return db.Query<ChatMessageJoin>("SELECT * FROM chat_message_join") ?? new List<ChatMessageJoin>();
    }
}