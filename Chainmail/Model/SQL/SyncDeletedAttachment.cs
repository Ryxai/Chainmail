using Chainmail.Model.Core;

namespace Chainmail.Model.SQL;

public class SyncDeletedAttachment : IGUIDVerifiable
{
    public long ROWID { get; set; }
    public string guid { get; set; }
    public string recordID { get; set; }
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