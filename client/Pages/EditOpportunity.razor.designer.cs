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
    public partial class EditOpportunityComponent : ComponentBase
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

        BlazorCrmWasm.Models.Crm.Opportunity _opportunity;
        protected BlazorCrmWasm.Models.Crm.Opportunity opportunity
        {
            get
            {
                return _opportunity;
            }
            set
            {
                if (!object.Equals(_opportunity, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "opportunity", NewValue = value, OldValue = _opportunity };
                    _opportunity = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        BlazorCrmWasm.Models.Crm.Contact _getByContactsForContactIdResult;
        protected BlazorCrmWasm.Models.Crm.Contact getByContactsForContactIdResult
        {
            get
            {
                return _getByContactsForContactIdResult;
            }
            set
            {
                if (!object.Equals(_getByContactsForContactIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getByContactsForContactIdResult", NewValue = value, OldValue = _getByContactsForContactIdResult };
                    _getByContactsForContactIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        BlazorCrmWasm.Models.Crm.OpportunityStatus _getByOpportunityStatusesForStatusIdResult;
        protected BlazorCrmWasm.Models.Crm.OpportunityStatus getByOpportunityStatusesForStatusIdResult
        {
            get
            {
                return _getByOpportunityStatusesForStatusIdResult;
            }
            set
            {
                if (!object.Equals(_getByOpportunityStatusesForStatusIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getByOpportunityStatusesForStatusIdResult", NewValue = value, OldValue = _getByOpportunityStatusesForStatusIdResult };
                    _getByOpportunityStatusesForStatusIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<BlazorCrmWasm.Models.Crm.Contact> _getContactsForContactIdResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.Contact> getContactsForContactIdResult
        {
            get
            {
                return _getContactsForContactIdResult;
            }
            set
            {
                if (!object.Equals(_getContactsForContactIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getContactsForContactIdResult", NewValue = value, OldValue = _getContactsForContactIdResult };
                    _getContactsForContactIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getContactsForContactIdCount;
        protected int getContactsForContactIdCount
        {
            get
            {
                return _getContactsForContactIdCount;
            }
            set
            {
                if (!object.Equals(_getContactsForContactIdCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getContactsForContactIdCount", NewValue = value, OldValue = _getContactsForContactIdCount };
                    _getContactsForContactIdCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<BlazorCrmWasm.Models.Crm.OpportunityStatus> _getOpportunityStatusesForStatusIdResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.OpportunityStatus> getOpportunityStatusesForStatusIdResult
        {
            get
            {
                return _getOpportunityStatusesForStatusIdResult;
            }
            set
            {
                if (!object.Equals(_getOpportunityStatusesForStatusIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOpportunityStatusesForStatusIdResult", NewValue = value, OldValue = _getOpportunityStatusesForStatusIdResult };
                    _getOpportunityStatusesForStatusIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getOpportunityStatusesForStatusIdCount;
        protected int getOpportunityStatusesForStatusIdCount
        {
            get
            {
                return _getOpportunityStatusesForStatusIdCount;
            }
            set
            {
                if (!object.Equals(_getOpportunityStatusesForStatusIdCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOpportunityStatusesForStatusIdCount", NewValue = value, OldValue = _getOpportunityStatusesForStatusIdCount };
                    _getOpportunityStatusesForStatusIdCount = value;
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

            var crmGetOpportunityByIdResult = await Crm.GetOpportunityById(id:Id);
            opportunity = crmGetOpportunityByIdResult;

            canEdit = crmGetOpportunityByIdResult != null;

            if (this.opportunity.ContactId != null)
            {
                var crmGetContactByIdResult = await Crm.GetContactById(id:this.opportunity.ContactId);
                getByContactsForContactIdResult = crmGetContactByIdResult;
            }

            if (this.opportunity.StatusId != null)
            {
                var crmGetOpportunityStatusByIdResult = await Crm.GetOpportunityStatusById(id:this.opportunity.StatusId);
                getByOpportunityStatusesForStatusIdResult = crmGetOpportunityStatusByIdResult;
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

        protected async System.Threading.Tasks.Task Form0Submit(BlazorCrmWasm.Models.Crm.Opportunity args)
        {
            try
            {
                var crmUpdateOpportunityResult = await Crm.UpdateOpportunity(id:Id, opportunity:opportunity);
                if (crmUpdateOpportunityResult.StatusCode != System.Net.HttpStatusCode.PreconditionFailed) {
                  DialogService.Close(opportunity);
                }

                hasChanges = crmUpdateOpportunityResult.StatusCode == System.Net.HttpStatusCode.PreconditionFailed;
            }
            catch (System.Exception crmUpdateOpportunityException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Opportunity" });

            hasChanges = crmUpdateOpportunityException.Message.Contains("412");

            if (!crmUpdateOpportunityException.Message.Contains("412")) {
                canEdit = false;
            }
            }
        }

        protected async System.Threading.Tasks.Task ContactIdLoadData(LoadDataArgs args)
        {
            var crmGetContactsResult = await Crm.GetContacts(filter:$"contains(Email, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getContactsForContactIdResult = crmGetContactsResult.Value.AsODataEnumerable();

            getContactsForContactIdCount = crmGetContactsResult.Count;
        }

        protected async System.Threading.Tasks.Task StatusIdLoadData(LoadDataArgs args)
        {
            var crmGetOpportunityStatusesResult = await Crm.GetOpportunityStatuses(filter:$"contains(Name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getOpportunityStatusesForStatusIdResult = crmGetOpportunityStatusesResult.Value.AsODataEnumerable();

            getOpportunityStatusesForStatusIdCount = crmGetOpportunityStatusesResult.Count;
        }

        protected async System.Threading.Tasks.Task Button4Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
