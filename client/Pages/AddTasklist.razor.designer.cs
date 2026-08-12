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
    public partial class AddTasklistComponent : ComponentBase
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

        BlazorCrmWasm.Models.Crm.Tasklist _tasklist;
        protected BlazorCrmWasm.Models.Crm.Tasklist tasklist
        {
            get
            {
                return _tasklist;
            }
            set
            {
                if (!object.Equals(_tasklist, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "tasklist", NewValue = value, OldValue = _tasklist };
                    _tasklist = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<BlazorCrmWasm.Models.Crm.Opportunity> _getOpportunitiesForOpportunityIdResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.Opportunity> getOpportunitiesForOpportunityIdResult
        {
            get
            {
                return _getOpportunitiesForOpportunityIdResult;
            }
            set
            {
                if (!object.Equals(_getOpportunitiesForOpportunityIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOpportunitiesForOpportunityIdResult", NewValue = value, OldValue = _getOpportunitiesForOpportunityIdResult };
                    _getOpportunitiesForOpportunityIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getOpportunitiesForOpportunityIdCount;
        protected int getOpportunitiesForOpportunityIdCount
        {
            get
            {
                return _getOpportunitiesForOpportunityIdCount;
            }
            set
            {
                if (!object.Equals(_getOpportunitiesForOpportunityIdCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOpportunitiesForOpportunityIdCount", NewValue = value, OldValue = _getOpportunitiesForOpportunityIdCount };
                    _getOpportunitiesForOpportunityIdCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<BlazorCrmWasm.Models.Crm.TaskType> _getTaskTypesForTypeIdResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.TaskType> getTaskTypesForTypeIdResult
        {
            get
            {
                return _getTaskTypesForTypeIdResult;
            }
            set
            {
                if (!object.Equals(_getTaskTypesForTypeIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getTaskTypesForTypeIdResult", NewValue = value, OldValue = _getTaskTypesForTypeIdResult };
                    _getTaskTypesForTypeIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getTaskTypesForTypeIdCount;
        protected int getTaskTypesForTypeIdCount
        {
            get
            {
                return _getTaskTypesForTypeIdCount;
            }
            set
            {
                if (!object.Equals(_getTaskTypesForTypeIdCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getTaskTypesForTypeIdCount", NewValue = value, OldValue = _getTaskTypesForTypeIdCount };
                    _getTaskTypesForTypeIdCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<BlazorCrmWasm.Models.Crm.TaskStatus> _getTaskStatusesForStatusIdResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.TaskStatus> getTaskStatusesForStatusIdResult
        {
            get
            {
                return _getTaskStatusesForStatusIdResult;
            }
            set
            {
                if (!object.Equals(_getTaskStatusesForStatusIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getTaskStatusesForStatusIdResult", NewValue = value, OldValue = _getTaskStatusesForStatusIdResult };
                    _getTaskStatusesForStatusIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getTaskStatusesForStatusIdCount;
        protected int getTaskStatusesForStatusIdCount
        {
            get
            {
                return _getTaskStatusesForStatusIdCount;
            }
            set
            {
                if (!object.Equals(_getTaskStatusesForStatusIdCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getTaskStatusesForStatusIdCount", NewValue = value, OldValue = _getTaskStatusesForStatusIdCount };
                    _getTaskStatusesForStatusIdCount = value;
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
            tasklist = new BlazorCrmWasm.Models.Crm.Tasklist(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(BlazorCrmWasm.Models.Crm.Tasklist args)
        {
            try
            {
                var crmCreateTasklistResult = await Crm.CreateTasklist(tasklist:tasklist);
                DialogService.Close(tasklist);
            }
            catch (System.Exception crmCreateTasklistException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Tasklist!" });
            }
        }

        protected async System.Threading.Tasks.Task OpportunityIdLoadData(LoadDataArgs args)
        {
            var crmGetOpportunitiesResult = await Crm.GetOpportunities(filter:$"contains(UserId, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getOpportunitiesForOpportunityIdResult = crmGetOpportunitiesResult.Value.AsODataEnumerable();

            getOpportunitiesForOpportunityIdCount = crmGetOpportunitiesResult.Count;
        }

        protected async System.Threading.Tasks.Task TypeIdLoadData(LoadDataArgs args)
        {
            var crmGetTaskTypesResult = await Crm.GetTaskTypes(filter:$"contains(Name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getTaskTypesForTypeIdResult = crmGetTaskTypesResult.Value.AsODataEnumerable();

            getTaskTypesForTypeIdCount = crmGetTaskTypesResult.Count;
        }

        protected async System.Threading.Tasks.Task StatusIdLoadData(LoadDataArgs args)
        {
            var crmGetTaskStatusesResult = await Crm.GetTaskStatuses(filter:$"contains(Name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getTaskStatusesForStatusIdResult = crmGetTaskStatusesResult.Value.AsODataEnumerable();

            getTaskStatusesForStatusIdCount = crmGetTaskStatusesResult.Count;
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
