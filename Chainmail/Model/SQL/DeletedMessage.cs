using Chainmail.Model.Core;

namespace Chainmail.Model.SQL;

public class DeletedMessage : IGUIDVerifiable
{
    public int ROWID { get; set; }
    public string guid { get; set; }
    public bool HasValidGuid()
    {
        try
        {
            var temp = new Guid(guid);
        }
        catch (FormatException)
        {
            return false;
        }

        return true;
    }
}