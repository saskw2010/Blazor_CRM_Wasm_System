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
    public partial class TaskTypesComponent : ComponentBase
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
        protected RadzenDataGrid<BlazorCrmWasm.Models.Crm.TaskType> grid0;

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

        IEnumerable<BlazorCrmWasm.Models.Crm.TaskType> _getTaskTypesResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.TaskType> getTaskTypesResult
        {
            get
            {
                return _getTaskTypesResult;
            }
            set
            {
                if (!object.Equals(_getTaskTypesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getTaskTypesResult", NewValue = value, OldValue = _getTaskTypesResult };
                    _getTaskTypesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getTaskTypesCount;
        protected int getTaskTypesCount
        {
            get
            {
                return _getTaskTypesCount;
            }
            set
            {
                if (!object.Equals(_getTaskTypesCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getTaskTypesCount", NewValue = value, OldValue = _getTaskTypesCount };
                    _getTaskTypesCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddTaskType>("Add Task Type", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Crm.ExportTaskTypesToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Id,Name" }, $"Task Types");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Crm.ExportTaskTypesToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Id,Name" }, $"Task Types");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmGetTaskTypesResult = await Crm.GetTaskTypes(filter:$@"(contains(Name,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getTaskTypesResult = crmGetTaskTypesResult.Value.AsODataEnumerable();

                getTaskTypesCount = crmGetTaskTypesResult.Count;
            }
            catch (System.Exception crmGetTaskTypesException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load TaskTypes" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<BlazorCrmWasm.Models.Crm.TaskType> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditTaskType>("Edit Task Type", new Dictionary<string, object>() { {"Id", args.Data.Id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var crmDeleteTaskTypeResult = await Crm.DeleteTaskType(id:data.Id);
                    if (crmDeleteTaskTypeResult != null && crmDeleteTaskTypeResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (crmDeleteTaskTypeResult != null && crmDeleteTaskTypeResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete TaskType" });
                    }
                }
            }
            catch (System.Exception crmDeleteTaskTypeException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete TaskType" });
            }
        }
    }
}
