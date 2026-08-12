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

  [Route("odata/CRM/Opportunities")]
  public partial class OpportunitiesController : ODataController
  {
    private BlazorCrmWasm.Data.CrmContext context;

    public OpportunitiesController(BlazorCrmWasm.Data.CrmContext context)
    {
      this.context = context;
    }
    // GET /odata/Crm/Opportunities
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crm.Opportunity> GetOpportunities()
    {
      var items = this.context.Opportunities.AsQueryable<Models.Crm.Opportunity>();
      this.OnOpportunitiesRead(ref items);

      return items;
    }

    partial void OnOpportunitiesRead(ref IQueryable<Models.Crm.Opportunity> items);

    partial void OnOpportunityGet(ref SingleResult<Models.Crm.Opportunity> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/CRM/Opportunities(Id={Id})")]
    public SingleResult<Opportunity> GetOpportunity(int key)
    {
        var items = this.context.Opportunities.Where(i=>i.Id == key);
        var result = SingleResult.Create(items);

        OnOpportunityGet(ref result);

        return result;
    }
    partial void OnOpportunityDeleted(Models.Crm.Opportunity item);
    partial void OnAfterOpportunityDeleted(Models.Crm.Opportunity item);

    [HttpDelete("/odata/CRM/Opportunities(Id={Id})")]
    public IActionResult DeleteOpportunity(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.Opportunities
                .Where(i => i.Id == key)
                .Include(i => i.Tasklists)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Crm.Opportunity>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnOpportunityDeleted(item);
            this.context.Opportunities.Remove(item);
            this.context.SaveChanges();
            this.OnAfterOpportunityDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOpportunityUpdated(Models.Crm.Opportunity item);
    partial void OnAfterOpportunityUpdated(Models.Crm.Opportunity item);

    [HttpPut("/odata/CRM/Opportunities(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutOpportunity(int key, [FromBody]Models.Crm.Opportunity newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Opportunities
                .Where(i => i.Id == key)
                .Include(i => i.Tasklists)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Crm.Opportunity>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnOpportunityUpdated(newItem);
            this.context.Opportunities.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Opportunities.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Contact,OpportunityStatus");
            this.OnAfterOpportunityUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/CRM/Opportunities(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchOpportunity(int key, [FromBody]Delta<Models.Crm.Opportunity> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Opportunities.Where(i => i.Id == key);

            items = EntityPatch.ApplyTo<Models.Crm.Opportunity>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnOpportunityUpdated(item);
            this.context.Opportunities.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Opportunities.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Contact,OpportunityStatus");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOpportunityCreated(Models.Crm.Opportunity item);
    partial void OnAfterOpportunityCreated(Models.Crm.Opportunity item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Crm.Opportunity item)
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

            this.OnOpportunityCreated(item);
            this.context.Opportunities.Add(item);
            this.context.SaveChanges();

            var key = item.Id;

            var itemToReturn = this.context.Opportunities.Where(i => i.Id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Contact,OpportunityStatus");

            this.OnAfterOpportunityCreated(item);

            return new ObjectResult(SingleResult.Create(itemToReturn))
            {
                StatusCode = 201
            };
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
