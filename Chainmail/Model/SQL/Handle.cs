namespace Chainmail.Model.SQL;

public class Handle
{
    public long ROWID { get; set; }
    public string id { get; set; }
    public string country { get; set; }
    public string service { get; set; }
    public string uncanonicalized_id { get; set; }
    public string person_centric_id { get; set; }
}