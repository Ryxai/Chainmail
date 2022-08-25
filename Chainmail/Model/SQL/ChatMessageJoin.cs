namespace Chainmail.Model;

public class ChatMessageJoin
{
    public int chat_id { get; set; }
    public int message_id { get; set; }
    public int message_date { get; set; } = 0;
}