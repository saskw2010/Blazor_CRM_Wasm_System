using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorCrmWasm.Models.Crm
{
  [Table("Contacts", Schema = "dbo")]
  public partial class Contact
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

    public IEnumerable<Opportunity> Opportunities { get; set; }
    [ConcurrencyCheck]
    public string Email
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Company
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string LastName
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string FirstName
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Phone
    {
      get;
      set;
    }
  }
}
