namespace Chainmail.Data;
using Chainmail.Model;
public interface ISyncDeletedMessageRepository
{
    public SyncDeletedMessage Get(int rowid);
    public IEnumerable<SyncDeletedMessage> GetAll();
}