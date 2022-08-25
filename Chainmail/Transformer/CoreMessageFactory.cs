using Chainmail.Model;
using Chainmail.Model.Core;
using LanguageExt;

namespace Chainmail.Transformer;

public class CoreMessageFactory : IFactory<CoreMessage>
{
    private Message? _message;
    private IEnumerable<Attachment>? _attachments;
    private CoreAttachmentFactory _attachmentFactory;

    public CoreMessageFactory()
    {
        _attachmentFactory = new CoreAttachmentFactory();
    }
    
    public bool Load(Message? message)
    {
        if (_message != null)
            return false;
        _message = message;
        return true;
    }
    
    //A load method for attachments that returns true if the attachments were loaded successfully.
    public bool Load(IEnumerable<Attachment>? attachments)
    {
        if (_attachments != null)
            return false;
        _attachments = attachments;
        return true;
    }
    
    
    public bool IsReady()
    {
        return _message != null && _attachments != null;
    }

    public Option<CoreMessage> Assemble()
    {
        if (!IsReady())
            return Option<CoreMessage>.None;
        var processedAttachments = _attachments!.Select(x =>
        {
            _attachmentFactory.Load(x);
            var res = _attachmentFactory.Assemble();
            return res.Match(
                Some: y => y,
                None: () => null!
            );
        }).Where(x => x != null);
        var results = new CoreMessage(_message!, processedAttachments);
        _attachments = null;
        _message = null;
        return results;
    }
}