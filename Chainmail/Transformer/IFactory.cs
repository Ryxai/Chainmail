using LanguageExt;
namespace Chainmail.Transformer;
public interface IFactory<T>
{
    public bool IsReady();
    public Option<T> Assemble();
}