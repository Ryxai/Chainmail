using Chainmail.Model.Core;

namespace Chainmail.Model.SQL;

/// <summary>
/// This is a direct mapping of the 'SyncDeletedMessage' table from the chat.db for Dapper to import into
/// </summary>
public class SyncDeletedMessage
{
    public long ROWID { get; set; }
    public string guid { get; set; }
    public string recordID { get; set; }
}