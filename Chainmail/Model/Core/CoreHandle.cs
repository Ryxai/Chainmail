namespace Chainmail.Model.Core;

public class CoreHandle
{
    public string Id { get; set; }

    public CoreHandle(Handle handle)
    {
        Id = handle.id;
    }
}