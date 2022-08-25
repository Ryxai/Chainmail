using Chainmail.Model;
using Chainmail.Model.Core;
using LanguageExt;

namespace Chainmail.Transformer;

public class CoreHandleFactory : IFactory<CoreHandle>
{
    private Handle? _handle;
    
    public bool Load(Handle? handle)
    {
        if (_handle != null)
            return false;
        _handle = handle;
        return true;
    }
    public bool IsReady()
    {
        return _handle != null;
    }

    public Option<CoreHandle> Assemble()
    {
        if (!IsReady())
            return Option<CoreHandle>.None;
        var results = new CoreHandle(_handle);
        _handle = null;
        return results;
    }
}