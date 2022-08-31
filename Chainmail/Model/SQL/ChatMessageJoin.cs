namespace Chainmail.Model.SQL;

public class ChatMessageJoin
{
    public long chat_id { get; set; }
    public long message_id { get; set; }
    public long message_date { get; set; } = 0;
}