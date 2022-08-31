using Chainmail.Model.Core;

namespace Chainmail.Model.SQL;

/// <summary>
/// This is a direct mapping of the 'DeletedMessage' table from the chat.db for Dapper to import into
/// </summary>
public class DeletedMessage 
{
    public int ROWID { get; set; }
    public string guid { get; set; }
}