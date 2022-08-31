namespace Chainmail.Model.Core;

public interface ISerializable
{
    public string[] Serialize();
    public string SerializeAsJson();
}