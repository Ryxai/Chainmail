using Chainmail.Model;
using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Dapper;
/// <summary>
/// An implementation of an SQLite database query provider and a Query Layer for DeletedMessages
/// </summary>
public class SQLiteDeletedMessageRepository : SQLiteDB, IDeletedMessageRepository
{
    /// <summary>
    /// Builds a new instance of the SQLiteDeletedMessageRepository class
    /// </summary>
    /// <param name="dbPath">The path of the SQLite database file</param>
    public SQLiteDeletedMessageRepository(string dbPath) : base(dbPath)
    {
    }

    /// <summary>
    /// Gets a single DeletedMessage by its ID
    /// </summary>
    /// <param name="rowid">The primary key of the DeletedMessage</param>
    /// <returns>A single attachment if its key exists or null</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public DeletedMessage GetDeletedMessage(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.QueryFirstOrDefault<DeletedMessage>("SELECT * FROM deleted_messages WHERE ROWID = @rowid", new { rowid });
    }

    /// <summary>
    /// Gets all of the DeletedMessage from the DeletedMessage table
    /// </summary>
    /// <returns>An enumerable containing all the DeletedMessages or null if the table is empty</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public IEnumerable<DeletedMessage> GetDeletedMessages()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.Query<DeletedMessage>("SELECT * FROM deleted_messages") ?? new List<DeletedMessage>();
    }
}