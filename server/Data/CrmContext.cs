using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using BlazorCrmWasm.Models.Crm;

namespace BlazorCrmWasm.Data
{
  public partial class CrmContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public CrmContext(DbContextOptions<CrmContext> options):base(options)
    {
    }

    public CrmContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BlazorCrmWasm.Models.Crm.Opportunity>()
              .HasOne(i => i.Contact)
              .WithMany(i => i.Opportunities)
              .HasForeignKey(i => i.ContactId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<BlazorCrmWasm.Models.Crm.Opportunity>()
              .HasOne(i => i.OpportunityStatus)
              .WithMany(i => i.Opportunities)
              .HasForeignKey(i => i.StatusId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<BlazorCrmWasm.Models.Crm.Tasklist>()
              .HasOne(i => i.Opportunity)
              .WithMany(i => i.Tasklists)
              .HasForeignKey(i => i.OpportunityId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<BlazorCrmWasm.Models.Crm.Tasklist>()
              .HasOne(i => i.TaskType)
              .WithMany(i => i.Tasklists)
              .HasForeignKey(i => i.TypeId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<BlazorCrmWasm.Models.Crm.Tasklist>()
              .HasOne(i => i.TaskStatus)
              .WithMany(i => i.Tasklists)
              .HasForeignKey(i => i.StatusId)
              .HasPrincipalKey(i => i.Id);


        builder.Entity<BlazorCrmWasm.Models.Crm.Opportunity>()
              .Property(p => p.CloseDate)
              .HasColumnType("datetime");

        builder.Entity<BlazorCrmWasm.Models.Crm.SchedulerAppointment>()
              .Property(p => p.StartDate)
              .HasColumnType("datetime");

        builder.Entity<BlazorCrmWasm.Models.Crm.SchedulerAppointment>()
              .Property(p => p.EndDate)
              .HasColumnType("datetime");

        builder.Entity<BlazorCrmWasm.Models.Crm.Tasklist>()
              .Property(p => p.DueDate)
              .HasColumnType("datetime");

        builder.Entity<BlazorCrmWasm.Models.Crm.Contact>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<BlazorCrmWasm.Models.Crm.Opportunity>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<BlazorCrmWasm.Models.Crm.Opportunity>()
              .Property(p => p.Amount)
              .HasPrecision(19, 4);

        builder.Entity<BlazorCrmWasm.Models.Crm.Opportunity>()
              .Property(p => p.ContactId)
              .HasPrecision(10, 0);

        builder.Entity<BlazorCrmWasm.Models.Crm.Opportunity>()
              .Property(p => p.StatusId)
              .HasPrecision(10, 0);

        builder.Entity<BlazorCrmWasm.Models.Crm.OpportunityStatus>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<BlazorCrmWasm.Models.Crm.SchedulerAppointment>()
              .Property(p => p.schedulerid)
              .HasPrecision(19, 0);

        builder.Entity<BlazorCrmWasm.Models.Crm.TaskStatus>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<BlazorCrmWasm.Models.Crm.TaskType>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<BlazorCrmWasm.Models.Crm.Tasklist>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<BlazorCrmWasm.Models.Crm.Tasklist>()
              .Property(p => p.OpportunityId)
              .HasPrecision(10, 0);

        builder.Entity<BlazorCrmWasm.Models.Crm.Tasklist>()
              .Property(p => p.TypeId)
              .HasPrecision(10, 0);

        builder.Entity<BlazorCrmWasm.Models.Crm.Tasklist>()
              .Property(p => p.StatusId)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<BlazorCrmWasm.Models.Crm.Contact> Contacts
    {
      get;
      set;
    }

    public DbSet<BlazorCrmWasm.Models.Crm.Opportunity> Opportunities
    {
      get;
      set;
    }

    public DbSet<BlazorCrmWasm.Models.Crm.OpportunityStatus> OpportunityStatuses
    {
      get;
      set;
    }

    public DbSet<BlazorCrmWasm.Models.Crm.SchedulerAppointment> SchedulerAppointments
    {
      get;
      set;
    }

    public DbSet<BlazorCrmWasm.Models.Crm.TaskStatus> TaskStatuses
    {
      get;
      set;
    }

    public DbSet<BlazorCrmWasm.Models.Crm.TaskType> TaskTypes
    {
      get;
      set;
    }

    public DbSet<BlazorCrmWasm.Models.Crm.Tasklist> Tasklists
    {
      get;
      set;
    }
  }
}
