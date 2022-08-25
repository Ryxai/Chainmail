using Chainmail.Model;

namespace Chainmail.Data;
using Dapper;
public class SQLiteMessageAttachmentJoinRepository: SQLiteDB, IMessageAttachmentJoinRepository
{
    public SQLiteMessageAttachmentJoinRepository(string dbPath) : base(dbPath)
    {
    }

    public IEnumerable<MessageAttachmentJoin> Get()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.Query<MessageAttachmentJoin>("SELECT * FROM message_attachment_join");
    }
}