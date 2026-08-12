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
    public partial class TasklistsComponent : ComponentBase
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
        protected RadzenDataGrid<BlazorCrmWasm.Models.Crm.Tasklist> grid0;

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

        IEnumerable<BlazorCrmWasm.Models.Crm.Tasklist> _getTasklistsResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.Tasklist> getTasklistsResult
        {
            get
            {
                return _getTasklistsResult;
            }
            set
            {
                if (!object.Equals(_getTasklistsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getTasklistsResult", NewValue = value, OldValue = _getTasklistsResult };
                    _getTasklistsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getTasklistsCount;
        protected int getTasklistsCount
        {
            get
            {
                return _getTasklistsCount;
            }
            set
            {
                if (!object.Equals(_getTasklistsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getTasklistsCount", NewValue = value, OldValue = _getTasklistsCount };
                    _getTasklistsCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddTasklist>("Add Tasklist", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Crm.ExportTasklistsToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Opportunity,TaskType,TaskStatus", Select = "Id,Title,Opportunity.UserId as OpportunityUserId,DueDate,TaskType.Name as TaskTypeName,TaskStatus.Name as TaskStatusName" }, $"Tasklists");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Crm.ExportTasklistsToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Opportunity,TaskType,TaskStatus", Select = "Id,Title,Opportunity.UserId as OpportunityUserId,DueDate,TaskType.Name as TaskTypeName,TaskStatus.Name as TaskStatusName" }, $"Tasklists");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmGetTasklistsResult = await Crm.GetTasklists(filter:$@"(contains(Title,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", expand:$"Opportunity,TaskType,TaskStatus", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getTasklistsResult = crmGetTasklistsResult.Value.AsODataEnumerable();

                getTasklistsCount = crmGetTasklistsResult.Count;
            }
            catch (System.Exception crmGetTasklistsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Tasklists" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<BlazorCrmWasm.Models.Crm.Tasklist> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditTasklist>("Edit Tasklist", new Dictionary<string, object>() { {"Id", args.Data.Id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var crmDeleteTasklistResult = await Crm.DeleteTasklist(id:data.Id);
                    if (crmDeleteTasklistResult != null && crmDeleteTasklistResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (crmDeleteTasklistResult != null && crmDeleteTasklistResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Tasklist" });
                    }
                }
            }
            catch (System.Exception crmDeleteTasklistException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Tasklist" });
            }
        }
    }
}
