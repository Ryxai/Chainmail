using Chainmail.Model;
using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Dapper;
/// <summary>
/// An implementation of an SQLite database query provider and a Query Layer for MessageAttachmentJoins 
/// </summary>
public class SQLiteMessageAttachmentJoinRepository: SQLiteDB, IMessageAttachmentJoinRepository
{
    /// <summary>
    /// Builds a new instance of the SQLiteMessageAttachmentJoinclass
    /// </summary>
    /// <param name="dbPath">The path of the SQLite database file</param>
    public SQLiteMessageAttachmentJoinRepository(string dbPath) : base(dbPath)
    {
    }

    /// <summary>
    /// Gets all of the MessageAttachmentJoin from the MessageAttachmentJoin table
    /// </summary>
    /// <returns>An enumerable containing all the Attachments or null if the table is empty</returns>
    /// <exception cref="FileNotFoundException">If the database cannot be queried if missing this exception is thrown</exception>
    public IEnumerable<MessageAttachmentJoin> Get()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.Query<MessageAttachmentJoin>("SELECT * FROM message_attachment_join") ?? new List<MessageAttachmentJoin>();
    }
}