using Chainmail.Model.Core;

namespace Chainmail.Model.SQL;

/// <summary>
/// This is a direct mapping of the 'SyncDeletedAttachment' table from the chat.db for Dapper to import into
/// </summary>
public class SyncDeletedAttachment 
{
    public long ROWID { get; set; }
    public string guid { get; set; }
    public string recordID { get; set; }
}