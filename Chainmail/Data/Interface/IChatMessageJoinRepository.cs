namespace Chainmail.Data;
using Chainmail.Model;

public interface IChatMessageJoinRepository
{
    //Get all chat message joins
    IEnumerable<ChatMessageJoin> Get();
}