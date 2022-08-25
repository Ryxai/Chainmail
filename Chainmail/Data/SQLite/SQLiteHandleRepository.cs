using Chainmail.Model;

namespace Chainmail.Data;
using Dapper;

public class SQLiteHandleRepository : SQLiteDB, IHandleRepository
{
    public SQLiteHandleRepository(string dbPath) : base(dbPath)
    {
    }

    public Handle GetHandle(int rowid)
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException();
        }
        using var db = CreateConnection();
        return db.QueryFirstOrDefault<Handle>("SELECT * FROM handle WHERE ROWID = @rowid", new { rowid });
    }

    public IEnumerable<Handle> GetAllHandles()
    {
        if (!File.Exists(_dbPath))
        {
            throw new FileNotFoundException();
        }
        using var db = CreateConnection();
        return db.Query<Handle>("SELECT * FROM handle");
    }
}