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
    public partial class AddSchedulerAppointmentComponent : ComponentBase
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
            schedulerappointment = new BlazorCrmWasm.Models.Crm.SchedulerAppointment(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(BlazorCrmWasm.Models.Crm.SchedulerAppointment args)
        {
            try
            {
                var crmCreateSchedulerAppointmentResult = await Crm.CreateSchedulerAppointment(schedulerAppointment:schedulerappointment);
                DialogService.Close(schedulerappointment);
            }
            catch (System.Exception crmCreateSchedulerAppointmentException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new SchedulerAppointment!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
