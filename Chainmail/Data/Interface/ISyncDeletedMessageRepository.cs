using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;
public interface ISyncDeletedMessageRepository
{
    public SyncDeletedMessage GetSyncDeletedMessage(int rowid);
    public IEnumerable<SyncDeletedMessage> GetSyncDeletedMessages();
}