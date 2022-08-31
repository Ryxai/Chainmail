using System.Data.SQLite;

namespace Chainmail.Data;

/// <summary>
/// An abstract class representing a SQLite database
/// </summary>
public abstract class SQLiteDB
{
    /// <summary>
    /// The database connection string (e.g. the filepath)
    /// </summary>
     protected readonly string _dbPath;
    
     /// <summary>
     /// Builds a SQLite database instance
     /// </summary>
     /// <param name="dbPath">The path of the SQLite database in the filesystem</param>
    public SQLiteDB(string dbPath)
    {
        _dbPath = dbPath;
    }
    
    /// <summary>
    /// Gets the database connection string/filepath
    /// </summary>
    /// <returns>A string containing the filepath for the SQlite database</returns>
    public string GetDBPath()
    {
        return _dbPath;
    }
    
    /// <summary>
    /// Creates a new SQLite database connection
    /// </summary>
    /// <returns>A SQLiteConnection to the database contained in the database path, uses SQLite3 as default</returns>
    public SQLiteConnection CreateConnection()
    {
        return new SQLiteConnection($"Data Source={_dbPath};Version=3;");
    }
    
}