using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;

/// <summary>
/// Interface for querying about ChatMessageJoin from the database
/// </summary>
public interface IChatMessageJoinRepository
{
    /// <summary>
    /// Gets all of the ChatMessageJoin from the database
    /// </summary>
    /// <returns>An an enumerable containing all of the joins, if there are no joins, returns null</returns>
    IEnumerable<ChatMessageJoin> Get();
}