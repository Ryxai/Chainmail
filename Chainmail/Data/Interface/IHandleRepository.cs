namespace Chainmail.Data;
using Chainmail.Model;

public interface IHandleRepository
{
    Handle GetHandle(int rowid);
    IEnumerable<Handle> GetHandles();
}