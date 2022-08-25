using Chainmail.Model;
using Chainmail.Model.Core;
using LanguageExt;

namespace Chainmail.Transformer;


public class CoreAttachmentFactory : IFactory<CoreAttachment>
{
    private Attachment? _attachment;
    
    public bool RecieveAttachment(Attachment? attachment)
    {
        if (_attachment != null)
            return false;
        _attachment = attachment;
        return true;
    }
    
    public Option<CoreAttachment> Assemble()
    {
        if (_attachment == null)
            return Option<CoreAttachment>.None;
        var results = new CoreAttachment(_attachment);
        _attachment = null;
        return results;
    }

    public bool isReady()
    {
        return _attachment == null;
    }
}