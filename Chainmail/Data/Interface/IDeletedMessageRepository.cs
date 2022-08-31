using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;
/// <summary>
/// Interface for querying about DeletedMessages from the database
/// </summary>
public interface IDeletedMessageRepository
{
    /// <summary>
    /// Gets a single message by the rowid from the DeletedMessages table
    /// </summary>
    /// <param name="rowid">The row id/primary key of the deleted message to be retrieved</param>
    /// <returns>Either the deleted message from the table or null if it does not exist</returns>
    public DeletedMessage GetDeletedMessage(int rowid);
    /// <summary>
    /// Gets all of the deleted messages from the DeletedMessage table
    /// </summary>
    /// <returns>An enumerable containing all of the messages or if none are in the table, returns null</returns>
    public IEnumerable<DeletedMessage> GetDeletedMessages();
}