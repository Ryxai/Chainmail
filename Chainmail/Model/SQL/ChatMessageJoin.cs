namespace Chainmail.Model.SQL;

/// <summary>
/// This is a direct mapping of the 'ChatMessageJoin' table from the chat.db for Dapper to import into
/// </summary>
public class ChatMessageJoin
{
    public long chat_id { get; set; }
    public long message_id { get; set; }
    public long message_date { get; set; } = 0;
}