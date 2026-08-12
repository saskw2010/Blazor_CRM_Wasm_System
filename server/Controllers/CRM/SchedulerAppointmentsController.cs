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

  [Route("odata/CRM/SchedulerAppointments")]
  public partial class SchedulerAppointmentsController : ODataController
  {
    private BlazorCrmWasm.Data.CrmContext context;

    public SchedulerAppointmentsController(BlazorCrmWasm.Data.CrmContext context)
    {
      this.context = context;
    }
    // GET /odata/Crm/SchedulerAppointments
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crm.SchedulerAppointment> GetSchedulerAppointments()
    {
      var items = this.context.SchedulerAppointments.AsQueryable<Models.Crm.SchedulerAppointment>();
      this.OnSchedulerAppointmentsRead(ref items);

      return items;
    }

    partial void OnSchedulerAppointmentsRead(ref IQueryable<Models.Crm.SchedulerAppointment> items);

    partial void OnSchedulerAppointmentGet(ref SingleResult<Models.Crm.SchedulerAppointment> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/CRM/SchedulerAppointments(schedulerid={schedulerid})")]
    public SingleResult<SchedulerAppointment> GetSchedulerAppointment(Int64 key)
    {
        var items = this.context.SchedulerAppointments.Where(i=>i.schedulerid == key);
        var result = SingleResult.Create(items);

        OnSchedulerAppointmentGet(ref result);

        return result;
    }
    partial void OnSchedulerAppointmentDeleted(Models.Crm.SchedulerAppointment item);
    partial void OnAfterSchedulerAppointmentDeleted(Models.Crm.SchedulerAppointment item);

    [HttpDelete("/odata/CRM/SchedulerAppointments(schedulerid={schedulerid})")]
    public IActionResult DeleteSchedulerAppointment(Int64 key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.SchedulerAppointments
                .Where(i => i.schedulerid == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Crm.SchedulerAppointment>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnSchedulerAppointmentDeleted(item);
            this.context.SchedulerAppointments.Remove(item);
            this.context.SaveChanges();
            this.OnAfterSchedulerAppointmentDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSchedulerAppointmentUpdated(Models.Crm.SchedulerAppointment item);
    partial void OnAfterSchedulerAppointmentUpdated(Models.Crm.SchedulerAppointment item);

    [HttpPut("/odata/CRM/SchedulerAppointments(schedulerid={schedulerid})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutSchedulerAppointment(Int64 key, [FromBody]Models.Crm.SchedulerAppointment newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.SchedulerAppointments
                .Where(i => i.schedulerid == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Crm.SchedulerAppointment>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnSchedulerAppointmentUpdated(newItem);
            this.context.SchedulerAppointments.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.SchedulerAppointments.Where(i => i.schedulerid == key);
            this.OnAfterSchedulerAppointmentUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/CRM/SchedulerAppointments(schedulerid={schedulerid})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchSchedulerAppointment(Int64 key, [FromBody]Delta<Models.Crm.SchedulerAppointment> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.SchedulerAppointments.Where(i => i.schedulerid == key);

            items = EntityPatch.ApplyTo<Models.Crm.SchedulerAppointment>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnSchedulerAppointmentUpdated(item);
            this.context.SchedulerAppointments.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.SchedulerAppointments.Where(i => i.schedulerid == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSchedulerAppointmentCreated(Models.Crm.SchedulerAppointment item);
    partial void OnAfterSchedulerAppointmentCreated(Models.Crm.SchedulerAppointment item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Crm.SchedulerAppointment item)
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

            this.OnSchedulerAppointmentCreated(item);
            this.context.SchedulerAppointments.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Crm/SchedulerAppointments/{item.schedulerid}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
