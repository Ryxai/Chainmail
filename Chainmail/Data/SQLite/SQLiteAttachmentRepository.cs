using Chainmail.Model;
using Chainmail.Model.SQL;
using Dapper;
namespace Chainmail.Data;

/// <summary>
/// An implementation of an SQLite database query provider and a Query Layer for Attachments
/// </summary>
public class SQLiteAttachmentRepository : SQLiteDB, IAttachmentRepository
{
    /// <summary>
    /// Builds a new instance of the SQLiteAttachmentRepository class
    /// </summary>
    /// <param name="dbPath">The path of the SQLite database file</param>
    public SQLiteAttachmentRepository(string dbPath) : base(dbPath)
    {
    }

    /// <summary>
    /// Gets a single Attachment by its ID
    /// </summary>
    /// <param name="rowid">The primary key of the Attachment</param>
    /// <returns>A single attachment if its key exists or null</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public Attachment GetAttachment(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.QueryFirstOrDefault<Attachment>("SELECT * FROM attachment WHERE ROWID = @rowid", new { rowid });
    }

    /// <summary>
    /// Gets all of the Attachments from the Attachment table
    /// </summary>
    /// <returns>An enumerable containing all the attachments or null if the table is empty</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public IEnumerable<Attachment> GetAttachments()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.Query<Attachment>("SELECT * FROM attachment") ?? new List<Attachment>();
    }
}