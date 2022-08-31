using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;

/// <summary>
/// Interface for querying about handles from the database
/// </summary>
public interface IHandleRepository
{
    
    /// <summary>
    /// Gets a handle by the rowid in the database
    /// </summary>
    /// <param name="rowid">The primary key of the handle</param>
    /// <returns>Either a handle if the rowid occurs or null</returns>
    Handle GetHandle(int rowid);
    /// <summary>
    /// Gets all of the handles from the Handle table
    /// </summary>
    /// <returns>An enumerable containing all of the Handles or returns null if none in the table</returns>
    IEnumerable<Handle> GetHandles();
}