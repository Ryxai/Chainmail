using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;
/// <summary>
/// Interface for querying about AttachmentRepositoryJoins from the database
/// </summary>
public interface IMessageAttachmentJoinRepository
{
    
    /// <summary>
    /// Gets all of the MessageAttachmentJoins from the MessageAttachmentJoin table
    /// </summary>
    /// <returns>An enumerable containing all of the MessageAttachmentJoins or returns null if none in the table</returns>
    public IEnumerable<MessageAttachmentJoin> Get();
}