using Chainmail.Model;
using Chainmail.Model.Core;
using Chainmail.Model.SQL;
using LanguageExt;

namespace Chainmail.Transformer;

public class CoreHandleFactory : IFactory<CoreHandle>
{
    private Handle? _handle;
    private LoadStateFlags _loadStateFlags = LoadStateFlags.None;

    [Flags]
    private enum LoadStateFlags
    {
        None = 0,
        Handle = 1,
        All = Handle
    }

    private void Initialize()
    {
        _handle = null;
        _loadStateFlags = LoadStateFlags.None;
    }

    public CoreHandleFactory()
    {
        Initialize();
    }
    
    public bool Load(Handle handle)
    {
        if (_loadStateFlags.HasFlag(LoadStateFlags.Handle))
            return false;
        _loadStateFlags |= LoadStateFlags.Handle;
        _handle = handle;
        return true;
    }
    public bool IsReady()
    {
        return _loadStateFlags == LoadStateFlags.All;
    }

    public void ForceReadyState()
    {
        _loadStateFlags = LoadStateFlags.All;
    }

    public Option<CoreHandle> Assemble()
    {
        if (!IsReady())
            return Option<CoreHandle>.None;
        var results = new CoreHandle(_handle);
        Initialize();
        return results;
    }
}