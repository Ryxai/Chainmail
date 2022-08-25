namespace Chainmail.Data;
using Chainmail.Model;
public interface ISyncDeletedChatRepository
{
    public SyncDeletedChat Get(int rowId);
    public IEnumerable<SyncDeletedChat> GetAll();
}