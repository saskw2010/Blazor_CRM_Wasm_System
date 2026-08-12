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

  [Route("odata/CRM/Tasklists")]
  public partial class TasklistsController : ODataController
  {
    private BlazorCrmWasm.Data.CrmContext context;

    public TasklistsController(BlazorCrmWasm.Data.CrmContext context)
    {
      this.context = context;
    }
    // GET /odata/Crm/Tasklists
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crm.Tasklist> GetTasklists()
    {
      var items = this.context.Tasklists.AsQueryable<Models.Crm.Tasklist>();
      this.OnTasklistsRead(ref items);

      return items;
    }

    partial void OnTasklistsRead(ref IQueryable<Models.Crm.Tasklist> items);

    partial void OnTasklistGet(ref SingleResult<Models.Crm.Tasklist> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/CRM/Tasklists(Id={Id})")]
    public SingleResult<Tasklist> GetTasklist(int key)
    {
        var items = this.context.Tasklists.Where(i=>i.Id == key);
        var result = SingleResult.Create(items);

        OnTasklistGet(ref result);

        return result;
    }
    partial void OnTasklistDeleted(Models.Crm.Tasklist item);
    partial void OnAfterTasklistDeleted(Models.Crm.Tasklist item);

    [HttpDelete("/odata/CRM/Tasklists(Id={Id})")]
    public IActionResult DeleteTasklist(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.Tasklists
                .Where(i => i.Id == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Crm.Tasklist>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnTasklistDeleted(item);
            this.context.Tasklists.Remove(item);
            this.context.SaveChanges();
            this.OnAfterTasklistDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTasklistUpdated(Models.Crm.Tasklist item);
    partial void OnAfterTasklistUpdated(Models.Crm.Tasklist item);

    [HttpPut("/odata/CRM/Tasklists(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutTasklist(int key, [FromBody]Models.Crm.Tasklist newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Tasklists
                .Where(i => i.Id == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Crm.Tasklist>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnTasklistUpdated(newItem);
            this.context.Tasklists.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Tasklists.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Opportunity,TaskType,TaskStatus");
            this.OnAfterTasklistUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/CRM/Tasklists(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchTasklist(int key, [FromBody]Delta<Models.Crm.Tasklist> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Tasklists.Where(i => i.Id == key);

            items = EntityPatch.ApplyTo<Models.Crm.Tasklist>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnTasklistUpdated(item);
            this.context.Tasklists.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Tasklists.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Opportunity,TaskType,TaskStatus");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTasklistCreated(Models.Crm.Tasklist item);
    partial void OnAfterTasklistCreated(Models.Crm.Tasklist item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Crm.Tasklist item)
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

            this.OnTasklistCreated(item);
            this.context.Tasklists.Add(item);
            this.context.SaveChanges();

            var key = item.Id;

            var itemToReturn = this.context.Tasklists.Where(i => i.Id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Opportunity,TaskType,TaskStatus");

            this.OnAfterTasklistCreated(item);

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
