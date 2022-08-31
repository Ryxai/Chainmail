using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;

public interface IDeletedMessageRepository
{
    public DeletedMessage GetDeletedMessage(int rowid);
    public IEnumerable<DeletedMessage> GetDeletedMessages();
}