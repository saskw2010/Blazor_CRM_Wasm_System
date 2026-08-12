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
    public partial class EditTasklistComponent : ComponentBase
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

        [Parameter]
        public dynamic Id { get; set; }

        bool _hasChanges;
        protected bool hasChanges
        {
            get
            {
                return _hasChanges;
            }
            set
            {
                if (!object.Equals(_hasChanges, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "hasChanges", NewValue = value, OldValue = _hasChanges };
                    _hasChanges = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        bool _canEdit;
        protected bool canEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if (!object.Equals(_canEdit, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "canEdit", NewValue = value, OldValue = _canEdit };
                    _canEdit = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

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

        BlazorCrmWasm.Models.Crm.Opportunity _getByOpportunitiesForOpportunityIdResult;
        protected BlazorCrmWasm.Models.Crm.Opportunity getByOpportunitiesForOpportunityIdResult
        {
            get
            {
                return _getByOpportunitiesForOpportunityIdResult;
            }
            set
            {
                if (!object.Equals(_getByOpportunitiesForOpportunityIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getByOpportunitiesForOpportunityIdResult", NewValue = value, OldValue = _getByOpportunitiesForOpportunityIdResult };
                    _getByOpportunitiesForOpportunityIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        BlazorCrmWasm.Models.Crm.TaskType _getByTaskTypesForTypeIdResult;
        protected BlazorCrmWasm.Models.Crm.TaskType getByTaskTypesForTypeIdResult
        {
            get
            {
                return _getByTaskTypesForTypeIdResult;
            }
            set
            {
                if (!object.Equals(_getByTaskTypesForTypeIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getByTaskTypesForTypeIdResult", NewValue = value, OldValue = _getByTaskTypesForTypeIdResult };
                    _getByTaskTypesForTypeIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        BlazorCrmWasm.Models.Crm.TaskStatus _getByTaskStatusesForStatusIdResult;
        protected BlazorCrmWasm.Models.Crm.TaskStatus getByTaskStatusesForStatusIdResult
        {
            get
            {
                return _getByTaskStatusesForStatusIdResult;
            }
            set
            {
                if (!object.Equals(_getByTaskStatusesForStatusIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getByTaskStatusesForStatusIdResult", NewValue = value, OldValue = _getByTaskStatusesForStatusIdResult };
                    _getByTaskStatusesForStatusIdResult = value;
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
            hasChanges = false;

            canEdit = true;

            var crmGetTasklistByIdResult = await Crm.GetTasklistById(id:Id);
            tasklist = crmGetTasklistByIdResult;

            canEdit = crmGetTasklistByIdResult != null;

            if (this.tasklist.OpportunityId != null)
            {
                var crmGetOpportunityByIdResult = await Crm.GetOpportunityById(id:this.tasklist.OpportunityId);
                getByOpportunitiesForOpportunityIdResult = crmGetOpportunityByIdResult;
            }

            if (this.tasklist.TypeId != null)
            {
                var crmGetTaskTypeByIdResult = await Crm.GetTaskTypeById(id:this.tasklist.TypeId);
                getByTaskTypesForTypeIdResult = crmGetTaskTypeByIdResult;
            }

            if (this.tasklist.StatusId != null)
            {
                var crmGetTaskStatusByIdResult = await Crm.GetTaskStatusById(id:this.tasklist.StatusId);
                getByTaskStatusesForStatusIdResult = crmGetTaskStatusByIdResult;
            }
        }

        protected async System.Threading.Tasks.Task CloseButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            await this.Load();
        }

        protected async System.Threading.Tasks.Task Form0Submit(BlazorCrmWasm.Models.Crm.Tasklist args)
        {
            try
            {
                var crmUpdateTasklistResult = await Crm.UpdateTasklist(id:Id, tasklist:tasklist);
                if (crmUpdateTasklistResult.StatusCode != System.Net.HttpStatusCode.PreconditionFailed) {
                  DialogService.Close(tasklist);
                }

                hasChanges = crmUpdateTasklistResult.StatusCode == System.Net.HttpStatusCode.PreconditionFailed;
            }
            catch (System.Exception crmUpdateTasklistException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Tasklist" });

            hasChanges = crmUpdateTasklistException.Message.Contains("412");

            if (!crmUpdateTasklistException.Message.Contains("412")) {
                canEdit = false;
            }
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

        protected async System.Threading.Tasks.Task Button4Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
