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
    public partial class OpportunityStatusesComponent : ComponentBase
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
        protected RadzenDataGrid<BlazorCrmWasm.Models.Crm.OpportunityStatus> grid0;

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

        IEnumerable<BlazorCrmWasm.Models.Crm.OpportunityStatus> _getOpportunityStatusesResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.OpportunityStatus> getOpportunityStatusesResult
        {
            get
            {
                return _getOpportunityStatusesResult;
            }
            set
            {
                if (!object.Equals(_getOpportunityStatusesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOpportunityStatusesResult", NewValue = value, OldValue = _getOpportunityStatusesResult };
                    _getOpportunityStatusesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getOpportunityStatusesCount;
        protected int getOpportunityStatusesCount
        {
            get
            {
                return _getOpportunityStatusesCount;
            }
            set
            {
                if (!object.Equals(_getOpportunityStatusesCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOpportunityStatusesCount", NewValue = value, OldValue = _getOpportunityStatusesCount };
                    _getOpportunityStatusesCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddOpportunityStatus>("Add Opportunity Status", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Crm.ExportOpportunityStatusesToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Id,Name" }, $"Opportunity Statuses");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Crm.ExportOpportunityStatusesToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Id,Name" }, $"Opportunity Statuses");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmGetOpportunityStatusesResult = await Crm.GetOpportunityStatuses(filter:$@"(contains(Name,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getOpportunityStatusesResult = crmGetOpportunityStatusesResult.Value.AsODataEnumerable();

                getOpportunityStatusesCount = crmGetOpportunityStatusesResult.Count;
            }
            catch (System.Exception crmGetOpportunityStatusesException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load OpportunityStatuses" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<BlazorCrmWasm.Models.Crm.OpportunityStatus> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditOpportunityStatus>("Edit Opportunity Status", new Dictionary<string, object>() { {"Id", args.Data.Id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var crmDeleteOpportunityStatusResult = await Crm.DeleteOpportunityStatus(id:data.Id);
                    if (crmDeleteOpportunityStatusResult != null && crmDeleteOpportunityStatusResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (crmDeleteOpportunityStatusResult != null && crmDeleteOpportunityStatusResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete OpportunityStatus" });
                    }
                }
            }
            catch (System.Exception crmDeleteOpportunityStatusException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete OpportunityStatus" });
            }
        }
    }
}
