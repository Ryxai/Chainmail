using Chainmail.Model;
using Chainmail.Model.Core;
using LanguageExt;

namespace Chainmail.Transformer;


public class CoreAttachmentFactory : IFactory<CoreAttachment>
{
    private Attachment? _attachment;
    
    public bool Load(Attachment? attachment)
    {
        if (_attachment != null)
            return false;
        _attachment = attachment;
        return true;
    }
    
    public bool IsReady()
    {
        return _attachment == null;
    }
    
    public Option<CoreAttachment> Assemble()
    {
        if (!IsReady())
            return Option<CoreAttachment>.None;
        var results = new CoreAttachment(_attachment);
        _attachment = null;
        return results;
    }

}