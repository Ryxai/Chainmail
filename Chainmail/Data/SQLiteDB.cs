namespace Chainmail;
using System.Data.SQLite;

public class SQLiteDB
{
    private readonly string _dbPath;
    
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