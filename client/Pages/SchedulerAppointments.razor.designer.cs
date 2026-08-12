using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using BlazorCrmWasm.Models.Crm;
using BlazorCrmWasm.Client.Pages;

namespace BlazorCrmWasm.Pages
{
    public partial class SchedulerAppointmentsComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected CrmService Crm { get; set; }
        protected RadzenDataGrid<BlazorCrmWasm.Models.Crm.SchedulerAppointment> grid0;

        string _search;
        protected string search
        {
            get
            {
                return _search;
            }
            set
            {
                if (!object.Equals(_search, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "search", NewValue = value, OldValue = _search };
                    _search = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<BlazorCrmWasm.Models.Crm.SchedulerAppointment> _getSchedulerAppointmentsResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.SchedulerAppointment> getSchedulerAppointmentsResult
        {
            get
            {
                return _getSchedulerAppointmentsResult;
            }
            set
            {
                if (!object.Equals(_getSchedulerAppointmentsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getSchedulerAppointmentsResult", NewValue = value, OldValue = _getSchedulerAppointmentsResult };
                    _getSchedulerAppointmentsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getSchedulerAppointmentsCount;
        protected int getSchedulerAppointmentsCount
        {
            get
            {
                return _getSchedulerAppointmentsCount;
            }
            set
            {
                if (!object.Equals(_getSchedulerAppointmentsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getSchedulerAppointmentsCount", NewValue = value, OldValue = _getSchedulerAppointmentsCount };
                    _getSchedulerAppointmentsCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            if (string.IsNullOrEmpty(search)) {
                search = "";
            }
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddSchedulerAppointment>("Add Scheduler Appointment", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Crm.ExportSchedulerAppointmentsToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "schedulerid,StartDate,EndDate,TextDesc" }, $"Scheduler Appointments");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Crm.ExportSchedulerAppointmentsToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "schedulerid,StartDate,EndDate,TextDesc" }, $"Scheduler Appointments");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmGetSchedulerAppointmentsResult = await Crm.GetSchedulerAppointments(filter:$@"(contains(TextDesc,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getSchedulerAppointmentsResult = crmGetSchedulerAppointmentsResult.Value.AsODataEnumerable();

                getSchedulerAppointmentsCount = crmGetSchedulerAppointmentsResult.Count;
            }
            catch (System.Exception crmGetSchedulerAppointmentsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load SchedulerAppointments" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<BlazorCrmWasm.Models.Crm.SchedulerAppointment> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditSchedulerAppointment>("Edit Scheduler Appointment", new Dictionary<string, object>() { {"schedulerid", args.Data.schedulerid} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var crmDeleteSchedulerAppointmentResult = await Crm.DeleteSchedulerAppointment(schedulerid:data.schedulerid);
                    if (crmDeleteSchedulerAppointmentResult != null && crmDeleteSchedulerAppointmentResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (crmDeleteSchedulerAppointmentResult != null && crmDeleteSchedulerAppointmentResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete SchedulerAppointment" });
                    }
                }
            }
            catch (System.Exception crmDeleteSchedulerAppointmentException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete SchedulerAppointment" });
            }
        }
    }
}
