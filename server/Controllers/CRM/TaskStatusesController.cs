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

  [Route("odata/CRM/TaskStatuses")]
  public partial class TaskStatusesController : ODataController
  {
    private BlazorCrmWasm.Data.CrmContext context;

    public TaskStatusesController(BlazorCrmWasm.Data.CrmContext context)
    {
      this.context = context;
    }
    // GET /odata/Crm/TaskStatuses
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crm.TaskStatus> GetTaskStatuses()
    {
      var items = this.context.TaskStatuses.AsQueryable<Models.Crm.TaskStatus>();
      this.OnTaskStatusesRead(ref items);

      return items;
    }

    partial void OnTaskStatusesRead(ref IQueryable<Models.Crm.TaskStatus> items);

    partial void OnTaskStatusGet(ref SingleResult<Models.Crm.TaskStatus> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/CRM/TaskStatuses(Id={Id})")]
    public SingleResult<TaskStatus> GetTaskStatus(int key)
    {
        var items = this.context.TaskStatuses.Where(i=>i.Id == key);
        var result = SingleResult.Create(items);

        OnTaskStatusGet(ref result);

        return result;
    }
    partial void OnTaskStatusDeleted(Models.Crm.TaskStatus item);
    partial void OnAfterTaskStatusDeleted(Models.Crm.TaskStatus item);

    [HttpDelete("/odata/CRM/TaskStatuses(Id={Id})")]
    public IActionResult DeleteTaskStatus(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.TaskStatuses
                .Where(i => i.Id == key)
                .Include(i => i.Tasklists)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Crm.TaskStatus>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnTaskStatusDeleted(item);
            this.context.TaskStatuses.Remove(item);
            this.context.SaveChanges();
            this.OnAfterTaskStatusDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTaskStatusUpdated(Models.Crm.TaskStatus item);
    partial void OnAfterTaskStatusUpdated(Models.Crm.TaskStatus item);

    [HttpPut("/odata/CRM/TaskStatuses(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutTaskStatus(int key, [FromBody]Models.Crm.TaskStatus newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.TaskStatuses
                .Where(i => i.Id == key)
                .Include(i => i.Tasklists)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Crm.TaskStatus>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnTaskStatusUpdated(newItem);
            this.context.TaskStatuses.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.TaskStatuses.Where(i => i.Id == key);
            this.OnAfterTaskStatusUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/CRM/TaskStatuses(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchTaskStatus(int key, [FromBody]Delta<Models.Crm.TaskStatus> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.TaskStatuses.Where(i => i.Id == key);

            items = EntityPatch.ApplyTo<Models.Crm.TaskStatus>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnTaskStatusUpdated(item);
            this.context.TaskStatuses.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.TaskStatuses.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTaskStatusCreated(Models.Crm.TaskStatus item);
    partial void OnAfterTaskStatusCreated(Models.Crm.TaskStatus item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Crm.TaskStatus item)
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

            this.OnTaskStatusCreated(item);
            this.context.TaskStatuses.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Crm/TaskStatuses/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
