using Chainmail.Model;
using Chainmail.Model.SQL;
using Dapper;
namespace Chainmail.Data;

/// <summary>
/// An implementation of an SQLite database query provider and a Query Layer for ChatHandleJoins
/// </summary>
public class SQLiteChatHandleJoinRepository : SQLiteDB, IChatHandleJoinRepository
{
    
    /// <summary>
    /// Builds a new instance of the SQLiteChatHandleJoinRepository class
    /// </summary>
    /// <param name="dbPath">The path of the SQLite database file</param>
    public SQLiteChatHandleJoinRepository(string dbPath) : base(dbPath)
    {
    }

    
    /// <summary>
    /// Gets all of the ChatHandleJoins from the ChatHandleJoin table
    /// </summary>
    /// <returns>An enumerable containing all the ChatHandleJoins or null if the table is empty</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public IEnumerable<ChatHandleJoin> Get()
    {
        //Check if the dbPath exists if not throw file not found
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("File not found at path: " + _dbPath);
        }
        using var db = CreateConnection();
        return db.Query<ChatHandleJoin>("SELECT * FROM chat_handle_join") ?? new List<ChatHandleJoin>();
    }
}