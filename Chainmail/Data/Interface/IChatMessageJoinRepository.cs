namespace Chainmail.Data;
using Chainmail.Model;

public interface IChatMessageJoinRepository
{
    //Get a chat message join by id
    ChatMessageJoin Get(int id);
    //Get all chat message joins
    IEnumerable<ChatMessageJoin> GetAll();
}