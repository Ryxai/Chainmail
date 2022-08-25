namespace Chainmail.Data;
using Chainmail.Model;

public interface IMessageRespository
{
    //Get a message by id
    Message GetMessage(int id);
    //Get all messages
    IEnumerable<Message> GetAllMessages();
}