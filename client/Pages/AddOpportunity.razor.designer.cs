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
    public partial class AddOpportunityComponent : ComponentBase
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
            opportunity = new BlazorCrmWasm.Models.Crm.Opportunity(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(BlazorCrmWasm.Models.Crm.Opportunity args)
        {
            try
            {
                var crmCreateOpportunityResult = await Crm.CreateOpportunity(opportunity:opportunity);
                DialogService.Close(opportunity);
            }
            catch (System.Exception crmCreateOpportunityException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Opportunity!" });
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

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
