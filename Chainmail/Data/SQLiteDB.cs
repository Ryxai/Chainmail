using System.Data.SQLite;

namespace Chainmail.Data;

public class SQLiteDB
{
     protected readonly string _dbPath;
    
    public SQLiteDB(string dbPath)
    {
        _dbPath = dbPath;
    }
    
    public string GetDBPath()
    {
        return _dbPath;
    }
    
    public SQLiteConnection CreateConnection()
    {
        return new SQLiteConnection($"Data Source={_dbPath};Version=3;");
    }
    
}