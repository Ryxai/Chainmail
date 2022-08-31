using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;

public interface IMessageAttachmentJoinRepository
{
    public IEnumerable<MessageAttachmentJoin> Get();
}