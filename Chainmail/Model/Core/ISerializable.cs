namespace Chainmail.Model.Core;

/// <summary>
/// An interface representing a serializable object.
/// </summary>
public interface ISerializable
{
    /// <summary>
    /// Serializes the object into a nonstandard string format
    /// </summary>
    /// <returns>A string array containing the serialized format</returns>
    public string[] Serialize();
    /// <summary>
    /// Serializes the object into JSON
    /// </summary>
    /// <returns>A JSON string containing the object</returns>
    public string SerializeAsJson();
}