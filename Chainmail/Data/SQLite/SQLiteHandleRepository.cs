using Chainmail.Model;
using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Dapper;

/// <summary>
/// An implementation of an SQLite database query provider and a Query Layer for Handles
/// </summary>
public class SQLiteHandleRepository : SQLiteDB, IHandleRepository
{
    /// <summary>
    /// Builds a new instance of the SQLiteHandleRepository class
    /// </summary>
    /// <param name="dbPath">The path of the SQLite database file</param>
    public SQLiteHandleRepository(string dbPath) : base(dbPath)
    {
    }

    /// <summary>
    /// Gets a single Handle by its ID
    /// </summary>
    /// <param name="rowid">The primary key of the Handle</param>
    /// <returns>A single attachment if its key exists or null</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public Handle GetHandle(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException();
        }
        using var db = CreateConnection();
        return db.QueryFirstOrDefault<Handle>("SELECT * FROM handle WHERE ROWID = @rowid", new { rowid });
    }

    /// <summary>
    /// Gets all of the Handle from the Handle table
    /// </summary>
    /// <returns>An enumerable containing all the Handles or null if the table is empty</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public IEnumerable<Handle> GetHandles()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException();
        }
        using var db = CreateConnection();
        return db.Query<Handle>("SELECT * FROM handle") ?? new List<Handle>();
    }
}