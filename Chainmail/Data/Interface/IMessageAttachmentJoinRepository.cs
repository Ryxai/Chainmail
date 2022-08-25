namespace Chainmail.Data;
using Chainmail.Model;

public interface IMessageAttachmentJoinRepository
{
    public IEnumerable<MessageAttachmentJoin> GetAll();
}