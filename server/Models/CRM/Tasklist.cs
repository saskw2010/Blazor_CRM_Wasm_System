using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorCrmWasm.Models.Crm
{
  [Table("Tasklists", Schema = "dbo")]
  public partial class Tasklist
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
    public int Id
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Title
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int OpportunityId
    {
      get;
      set;
    }
    public Opportunity Opportunity { get; set; }
    [ConcurrencyCheck]
    public DateTime DueDate
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int TypeId
    {
      get;
      set;
    }
    public TaskType TaskType { get; set; }
    [ConcurrencyCheck]
    public int? StatusId
    {
      get;
      set;
    }
    public TaskStatus TaskStatus { get; set; }
  }
}
