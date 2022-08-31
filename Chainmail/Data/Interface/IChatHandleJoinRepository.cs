using Chainmail.Model;
using Chainmail.Model.SQL;

namespace Chainmail.Data;

public interface IChatHandleJoinRepository
{
    IEnumerable<ChatHandleJoin> Get();
}