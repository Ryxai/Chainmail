using Chainmail.Model;
using Chainmail.Model.SQL;

namespace Chainmail.Data;
using Chainmail.Model;

public interface IChatRepository
{
    //Returns a chat from a id for the chat
    Chat GetChat(int id);
    //Returns a list of all the chats
    IEnumerable<Chat> GetChats();
}