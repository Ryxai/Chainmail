namespace Chainmail.Model.SQL;


/// <summary>
/// This is a direct mapping of the 'ChatHandleJoin' table in the chat.db for Dapper to import into.
/// </summary>
public class ChatHandleJoin
{
    public long chat_id { get; set; }
    public long handle_id { get; set; }
}