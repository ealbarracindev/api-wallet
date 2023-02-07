using System.ComponentModel.DataAnnotations.Schema;

namespace wallet.core.Entities;
[Table("ApiLogs")]
public class ApiLog
{
    public int Id { get; set; }
    public string Message { get; set; }
    public string MessageTemplate { get; set; }
    public string Level { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
    public string Exception { get; set; }
    public string Properties { get; set; }
    public string LogEvent { get; set; }
}
