using LanguageExt;
namespace Chainmail.Transformer;
/// <summary>
/// An interface for a factory utilizing the factory pattern
/// </summary>
/// <typeparam name="T">The type to be assembled</typeparam>
public interface IFactory<T>
{
    /// <summary>
    /// Is the factory been loaded successfully to create a new object
    /// </summary>
    /// <returns>A boolean representing if the factory is ready</returns>
    public bool IsReady();
    /// <summary>
    /// Assembles the object, if the object cannot be assembled then returns Option.None
    /// </summary>
    /// <returns>An option either containing the assembled factory or None</returns>
    public Option<T> Assemble();
}