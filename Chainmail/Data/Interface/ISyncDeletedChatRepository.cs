using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;
public interface ISyncDeletedChatRepository
{
    public SyncDeletedChat GetSyncDeletedChat(int rowid);
    public IEnumerable<SyncDeletedChat> GetSyncDeletedChats();
}