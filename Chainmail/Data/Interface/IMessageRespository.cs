using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;

/// <summary>
/// Interface for querying about Messages from the database
/// </summary>
public interface IMessageRespository
{
    
    /// <summary>
    /// Gets a Message by the rowid in the database
    /// </summary>
    /// <param name="rowid">The primary key of the Message</param>
    /// <returns>Either a Message if the rowid occurs or null</returns>
    Message GetMessage(int rowid);
    //Get all messages
    
    /// <summary>
    /// Gets all of the Messages from the Message table
    /// </summary>
    /// <returns>An enumerable containing all of the Messages or returns null if none in the table</returns>
    IEnumerable<Message> GetMessages();
}