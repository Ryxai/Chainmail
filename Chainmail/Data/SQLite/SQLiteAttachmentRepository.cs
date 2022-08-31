using Chainmail.Model;
using Chainmail.Model.SQL;
using Dapper;
namespace Chainmail.Data;

public class SQLiteAttachmentRepository : SQLiteDB, IAttachmentRepository
{
    public SQLiteAttachmentRepository(string dbPath) : base(dbPath)
    {
    }

    public Attachment GetAttachment(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException("Database not found at " + _dbPath);
        }
        using var conn = CreateConnection();
        return conn.QueryFirstOrDefault<Attachment>("SELECT * FROM attachment WHERE ROWID = @rowid", new { rowid });
    }

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