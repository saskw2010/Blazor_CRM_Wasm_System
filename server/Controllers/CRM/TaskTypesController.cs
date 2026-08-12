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

  [Route("odata/CRM/TaskTypes")]
  public partial class TaskTypesController : ODataController
  {
    private BlazorCrmWasm.Data.CrmContext context;

    public TaskTypesController(BlazorCrmWasm.Data.CrmContext context)
    {
      this.context = context;
    }
    // GET /odata/Crm/TaskTypes
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crm.TaskType> GetTaskTypes()
    {
      var items = this.context.TaskTypes.AsQueryable<Models.Crm.TaskType>();
      this.OnTaskTypesRead(ref items);

      return items;
    }

    partial void OnTaskTypesRead(ref IQueryable<Models.Crm.TaskType> items);

    partial void OnTaskTypeGet(ref SingleResult<Models.Crm.TaskType> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/CRM/TaskTypes(Id={Id})")]
    public SingleResult<TaskType> GetTaskType(int key)
    {
        var items = this.context.TaskTypes.Where(i=>i.Id == key);
        var result = SingleResult.Create(items);

        OnTaskTypeGet(ref result);

        return result;
    }
    partial void OnTaskTypeDeleted(Models.Crm.TaskType item);
    partial void OnAfterTaskTypeDeleted(Models.Crm.TaskType item);

    [HttpDelete("/odata/CRM/TaskTypes(Id={Id})")]
    public IActionResult DeleteTaskType(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.TaskTypes
                .Where(i => i.Id == key)
                .Include(i => i.Tasklists)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Crm.TaskType>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnTaskTypeDeleted(item);
            this.context.TaskTypes.Remove(item);
            this.context.SaveChanges();
            this.OnAfterTaskTypeDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTaskTypeUpdated(Models.Crm.TaskType item);
    partial void OnAfterTaskTypeUpdated(Models.Crm.TaskType item);

    [HttpPut("/odata/CRM/TaskTypes(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutTaskType(int key, [FromBody]Models.Crm.TaskType newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.TaskTypes
                .Where(i => i.Id == key)
                .Include(i => i.Tasklists)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Crm.TaskType>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnTaskTypeUpdated(newItem);
            this.context.TaskTypes.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.TaskTypes.Where(i => i.Id == key);
            this.OnAfterTaskTypeUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/CRM/TaskTypes(Id={Id})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchTaskType(int key, [FromBody]Delta<Models.Crm.TaskType> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.TaskTypes.Where(i => i.Id == key);

            items = EntityPatch.ApplyTo<Models.Crm.TaskType>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnTaskTypeUpdated(item);
            this.context.TaskTypes.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.TaskTypes.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTaskTypeCreated(Models.Crm.TaskType item);
    partial void OnAfterTaskTypeCreated(Models.Crm.TaskType item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Crm.TaskType item)
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

            this.OnTaskTypeCreated(item);
            this.context.TaskTypes.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Crm/TaskTypes/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
