using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;




namespace BlazorCrmWasm.Controllers.Crm
{
  using Models;
  using Data;
  using Models.Crm;

  [Route("odata/CRM/OpportunityStatuses")]
  public partial class OpportunityStatusesController : ODataController
  {
    private BlazorCrmWasm.Data.CrmContext context;

    public OpportunityStatusesController(BlazorCrmWasm.Data.CrmContext context)
    {
      this.context = context;
    }
    // GET /odata/Crm/OpportunityStatuses
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crm.OpportunityStatus> GetOpportunityStatuses()
    {
      var items = this.context.OpportunityStatuses.AsQueryable<Models.Crm.OpportunityStatus>();
      this.OnOpportunityStatusesRead(ref items);

      return items;
    }

    partial void OnOpportunityStatusesRead(ref IQueryable<Models.Crm.OpportunityStatus> items);

    partial void OnOpportunityStatusGet(ref SingleResult<Models.Crm.OpportunityStatus> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/CRM/OpportunityStatuses(Id={Id})")]
    public SingleResult<OpportunityStatus> GetOpportunityStatus(int key)
    {
        var items = this.context.OpportunityStatuses.Where(i=>i.Id == key);
        var result = SingleResult.Create(items);

        OnOpportunityStatusGet(ref result);

        return result;
    }
    partial void OnOpportunityStatusDeleted(Models.Crm.OpportunityStatus item);
    partial void OnAfterOpportunityStatusDeleted(Models.Crm.OpportunityStatus item);

    [HttpDelete("/odata/CRM/OpportunityStatuses(Id={Id})")]
    public IActionResult DeleteOpportunityStatus(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.OpportunityStatuses
                .Where(i => i.Id == key)
                .Include(i => i.Opportunities)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Crm.OpportunityStatus>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnOpportunityStatusDeleted(item);
            this.context.OpportunityStatuses.Remove(item);
            this.context.SaveChanges();
            this.OnAfterOpportunityStatusDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOpportunityStatusUpdated(Models.Crm.OpportunityStatus item);
    partial void OnAfterOpportunityStatusUpdated(Models.Crm.OpportunityStatus item);

    [HttpPut("/odata/CRM/OpportunityStatuses(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutOpportunityStatus(int key, [FromBody]Models.Crm.OpportunityStatus newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.OpportunityStatuses
                .Where(i => i.Id == key)
                .Include(i => i.Opportunities)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Crm.OpportunityStatus>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnOpportunityStatusUpdated(newItem);
            this.context.OpportunityStatuses.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.OpportunityStatuses.Where(i => i.Id == key);
            this.OnAfterOpportunityStatusUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/CRM/OpportunityStatuses(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchOpportunityStatus(int key, [FromBody]Delta<Models.Crm.OpportunityStatus> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.OpportunityStatuses.Where(i => i.Id == key);

            items = EntityPatch.ApplyTo<Models.Crm.OpportunityStatus>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnOpportunityStatusUpdated(item);
            this.context.OpportunityStatuses.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.OpportunityStatuses.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOpportunityStatusCreated(Models.Crm.OpportunityStatus item);
    partial void OnAfterOpportunityStatusCreated(Models.Crm.OpportunityStatus item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Crm.OpportunityStatus item)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null)
            {
                return BadRequest();
            }

            this.OnOpportunityStatusCreated(item);
            this.context.OpportunityStatuses.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Crm/OpportunityStatuses/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
