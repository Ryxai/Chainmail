using Chainmail.Model;

namespace Chainmail.Data;

public interface IChatHandleJoinRepository
{
    IEnumerable<ChatHandleJoin> Get();
}