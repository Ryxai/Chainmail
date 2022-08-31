using Chainmail.Model;
using Chainmail.Model.SQL;

namespace Chainmail.Data;

/// <summary>
/// Interface for querying about ChatHandleJpins from the database
/// </summary>
public interface IChatHandleJoinRepository
{
    /// <summary>
    /// Gets all of the chathanlejpins in the table
    /// </summary>
    /// <returns>An enumerable containing all the ChatHandleJoins, returns null if the table is empty</returns>
    IEnumerable<ChatHandleJoin> Get();
}