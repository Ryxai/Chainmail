using Chainmail.Model;
using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;

/// <summary>
/// Interface for querying about Chats from the database
/// </summary>
public interface IChatRepository
{
    /// <summary>
    /// Gets a single chat by the rowid
    /// </summary>
    /// <param name="rowid">An integer which is the primary key for a row from the Chat table</param>
    /// <returns>A single chat if it is available from the table, otherwise null</returns>
    Chat GetChat(int rowid);
    /// <summary>
    /// Gets all of the chats from the Chat table
    /// </summary>
    /// <returns>An enumerable containing all the of the Chats, if the table is empty it returns null</returns>
    IEnumerable<Chat> GetChats();
}