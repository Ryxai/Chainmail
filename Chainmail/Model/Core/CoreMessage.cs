namespace Chainmail.Model.Core;

public class CoreMessage
{
    public string Text { get; set; }
    public int Date { get; set; }
    public List<CoreAttachment> Attachments { get; set; }
    public bool IsSentFromMe { get; set; }

    public CoreMessage(Message message, IEnumerable<CoreAttachment> attachments)
    {
        Text = message.text;
        Date = message.date;
        Attachments = attachments.ToList();
        IsSentFromMe = message.is_from_me == 1;
    }
}