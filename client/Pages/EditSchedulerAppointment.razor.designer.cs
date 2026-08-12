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
    public partial class EditSchedulerAppointmentComponent : ComponentBase
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
        public dynamic schedulerid { get; set; }

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

        BlazorCrmWasm.Models.Crm.SchedulerAppointment _schedulerappointment;
        protected BlazorCrmWasm.Models.Crm.SchedulerAppointment schedulerappointment
        {
            get
            {
                return _schedulerappointment;
            }
            set
            {
                if (!object.Equals(_schedulerappointment, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "schedulerappointment", NewValue = value, OldValue = _schedulerappointment };
                    _schedulerappointment = value;
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

            var crmGetSchedulerAppointmentByscheduleridResult = await Crm.GetSchedulerAppointmentByschedulerid(schedulerid:schedulerid);
            schedulerappointment = crmGetSchedulerAppointmentByscheduleridResult;

            canEdit = crmGetSchedulerAppointmentByscheduleridResult != null;
        }

        protected async System.Threading.Tasks.Task CloseButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            await this.Load();
        }

        protected async System.Threading.Tasks.Task Form0Submit(BlazorCrmWasm.Models.Crm.SchedulerAppointment args)
        {
            try
            {
                var crmUpdateSchedulerAppointmentResult = await Crm.UpdateSchedulerAppointment(schedulerid:schedulerid, schedulerAppointment:schedulerappointment);
                if (crmUpdateSchedulerAppointmentResult.StatusCode != System.Net.HttpStatusCode.PreconditionFailed) {
                  DialogService.Close(schedulerappointment);
                }

                hasChanges = crmUpdateSchedulerAppointmentResult.StatusCode == System.Net.HttpStatusCode.PreconditionFailed;
            }
            catch (System.Exception crmUpdateSchedulerAppointmentException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update SchedulerAppointment" });

            hasChanges = crmUpdateSchedulerAppointmentException.Message.Contains("412");

            if (!crmUpdateSchedulerAppointmentException.Message.Contains("412")) {
                canEdit = false;
            }
            }
        }

        protected async System.Threading.Tasks.Task Button4Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
