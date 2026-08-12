using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorCrmWasm.Models.Crm
{
  [Table("Opportunities", Schema = "dbo")]
  public partial class Opportunity
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

    public IEnumerable<Tasklist> Tasklists { get; set; }
    [ConcurrencyCheck]
    public decimal Amount
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string UserId
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int ContactId
    {
      get;
      set;
    }
    public Contact Contact { get; set; }
    [ConcurrencyCheck]
    public int StatusId
    {
      get;
      set;
    }
    public OpportunityStatus OpportunityStatus { get; set; }
    [ConcurrencyCheck]
    public DateTime CloseDate
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Name
    {
      get;
      set;
    }
  }
}
