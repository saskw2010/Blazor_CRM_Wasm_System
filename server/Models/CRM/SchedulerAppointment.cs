using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorCrmWasm.Models.Crm
{
  [Table("SchedulerAppointment", Schema = "dbo")]
  public partial class SchedulerAppointment
  {
    [NotMapped]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("@odata.etag")]
    public string ETag
    {
        get;
        set;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Int64 schedulerid
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? StartDate
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? EndDate
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string TextDesc
    {
      get;
      set;
    }
  }
}
