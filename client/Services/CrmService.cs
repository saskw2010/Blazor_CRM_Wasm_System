
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using BlazorCrmWasm.Models.Crm;

namespace BlazorCrmWasm
{
    public partial class CrmService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;
        public CrmService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/CRM/");
        }

        public async System.Threading.Tasks.Task ExportContactsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportContactsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetContacts(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.Contact>> GetContacts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Contacts");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContacts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.Contact>>(response);
        }
        partial void OnCreateContact(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.Contact> CreateContact(BlazorCrmWasm.Models.Crm.Contact contact = default(BlazorCrmWasm.Models.Crm.Contact))
        {
            var uri = new Uri(baseUri, $"Contacts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contact), Encoding.UTF8, "application/json");

            OnCreateContact(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.Contact>(response);
        }

        public async System.Threading.Tasks.Task ExportOpportunitiesToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/opportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/opportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportOpportunitiesToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/opportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/opportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetOpportunities(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.Opportunity>> GetOpportunities(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Opportunities");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunities(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.Opportunity>>(response);
        }
        partial void OnCreateOpportunity(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.Opportunity> CreateOpportunity(BlazorCrmWasm.Models.Crm.Opportunity opportunity = default(BlazorCrmWasm.Models.Crm.Opportunity))
        {
            var uri = new Uri(baseUri, $"Opportunities");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(opportunity), Encoding.UTF8, "application/json");

            OnCreateOpportunity(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.Opportunity>(response);
        }

        public async System.Threading.Tasks.Task ExportOpportunityStatusesToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/opportunitystatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/opportunitystatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportOpportunityStatusesToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/opportunitystatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/opportunitystatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetOpportunityStatuses(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.OpportunityStatus>> GetOpportunityStatuses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunityStatuses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.OpportunityStatus>>(response);
        }
        partial void OnCreateOpportunityStatus(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.OpportunityStatus> CreateOpportunityStatus(BlazorCrmWasm.Models.Crm.OpportunityStatus opportunityStatus = default(BlazorCrmWasm.Models.Crm.OpportunityStatus))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(opportunityStatus), Encoding.UTF8, "application/json");

            OnCreateOpportunityStatus(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.OpportunityStatus>(response);
        }

        public async System.Threading.Tasks.Task ExportSchedulerAppointmentsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/schedulerappointments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/schedulerappointments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportSchedulerAppointmentsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/schedulerappointments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/schedulerappointments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetSchedulerAppointments(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.SchedulerAppointment>> GetSchedulerAppointments(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"SchedulerAppointments");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSchedulerAppointments(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.SchedulerAppointment>>(response);
        }
        partial void OnCreateSchedulerAppointment(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.SchedulerAppointment> CreateSchedulerAppointment(BlazorCrmWasm.Models.Crm.SchedulerAppointment schedulerAppointment = default(BlazorCrmWasm.Models.Crm.SchedulerAppointment))
        {
            var uri = new Uri(baseUri, $"SchedulerAppointments");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(schedulerAppointment), Encoding.UTF8, "application/json");

            OnCreateSchedulerAppointment(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.SchedulerAppointment>(response);
        }

        public async System.Threading.Tasks.Task ExportTaskStatusesToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/taskstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/taskstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTaskStatusesToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/taskstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/taskstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetTaskStatuses(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.TaskStatus>> GetTaskStatuses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TaskStatuses");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskStatuses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.TaskStatus>>(response);
        }
        partial void OnCreateTaskStatus(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.TaskStatus> CreateTaskStatus(BlazorCrmWasm.Models.Crm.TaskStatus taskStatus = default(BlazorCrmWasm.Models.Crm.TaskStatus))
        {
            var uri = new Uri(baseUri, $"TaskStatuses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(taskStatus), Encoding.UTF8, "application/json");

            OnCreateTaskStatus(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.TaskStatus>(response);
        }

        public async System.Threading.Tasks.Task ExportTaskTypesToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/tasktypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/tasktypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTaskTypesToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/tasktypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/tasktypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetTaskTypes(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.TaskType>> GetTaskTypes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TaskTypes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskTypes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.TaskType>>(response);
        }
        partial void OnCreateTaskType(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.TaskType> CreateTaskType(BlazorCrmWasm.Models.Crm.TaskType taskType = default(BlazorCrmWasm.Models.Crm.TaskType))
        {
            var uri = new Uri(baseUri, $"TaskTypes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(taskType), Encoding.UTF8, "application/json");

            OnCreateTaskType(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.TaskType>(response);
        }

        public async System.Threading.Tasks.Task ExportTasklistsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/tasklists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/tasklists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTasklistsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/tasklists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/tasklists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetTasklists(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.Tasklist>> GetTasklists(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Tasklists");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTasklists(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<BlazorCrmWasm.Models.Crm.Tasklist>>(response);
        }
        partial void OnCreateTasklist(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.Tasklist> CreateTasklist(BlazorCrmWasm.Models.Crm.Tasklist tasklist = default(BlazorCrmWasm.Models.Crm.Tasklist))
        {
            var uri = new Uri(baseUri, $"Tasklists");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tasklist), Encoding.UTF8, "application/json");

            OnCreateTasklist(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.Tasklist>(response);
        }
        partial void OnDeleteContact(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteContact(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Contacts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetContactById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.Contact> GetContactById(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Contacts({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.Contact>(response);
        }
        partial void OnUpdateContact(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateContact(int? id = default(int?), BlazorCrmWasm.Models.Crm.Contact contact = default(BlazorCrmWasm.Models.Crm.Contact))
        {
            var uri = new Uri(baseUri, $"Contacts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", contact.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contact), Encoding.UTF8, "application/json");

            OnUpdateContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteOpportunity(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteOpportunity(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Opportunities({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteOpportunity(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetOpportunityById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.Opportunity> GetOpportunityById(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Opportunities({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunityById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.Opportunity>(response);
        }
        partial void OnUpdateOpportunity(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateOpportunity(int? id = default(int?), BlazorCrmWasm.Models.Crm.Opportunity opportunity = default(BlazorCrmWasm.Models.Crm.Opportunity))
        {
            var uri = new Uri(baseUri, $"Opportunities({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", opportunity.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(opportunity), Encoding.UTF8, "application/json");

            OnUpdateOpportunity(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteOpportunityStatus(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteOpportunityStatus(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteOpportunityStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetOpportunityStatusById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.OpportunityStatus> GetOpportunityStatusById(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOpportunityStatusById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.OpportunityStatus>(response);
        }
        partial void OnUpdateOpportunityStatus(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateOpportunityStatus(int? id = default(int?), BlazorCrmWasm.Models.Crm.OpportunityStatus opportunityStatus = default(BlazorCrmWasm.Models.Crm.OpportunityStatus))
        {
            var uri = new Uri(baseUri, $"OpportunityStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", opportunityStatus.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(opportunityStatus), Encoding.UTF8, "application/json");

            OnUpdateOpportunityStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteSchedulerAppointment(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteSchedulerAppointment(Int64? schedulerid = default(Int64?))
        {
            var uri = new Uri(baseUri, $"SchedulerAppointments({schedulerid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteSchedulerAppointment(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetSchedulerAppointmentByschedulerid(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.SchedulerAppointment> GetSchedulerAppointmentByschedulerid(string expand = default(string), Int64? schedulerid = default(Int64?))
        {
            var uri = new Uri(baseUri, $"SchedulerAppointments({schedulerid})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetSchedulerAppointmentByschedulerid(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.SchedulerAppointment>(response);
        }
        partial void OnUpdateSchedulerAppointment(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateSchedulerAppointment(Int64? schedulerid = default(Int64?), BlazorCrmWasm.Models.Crm.SchedulerAppointment schedulerAppointment = default(BlazorCrmWasm.Models.Crm.SchedulerAppointment))
        {
            var uri = new Uri(baseUri, $"SchedulerAppointments({schedulerid})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", schedulerAppointment.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(schedulerAppointment), Encoding.UTF8, "application/json");

            OnUpdateSchedulerAppointment(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteTaskStatus(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteTaskStatus(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"TaskStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTaskStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetTaskStatusById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.TaskStatus> GetTaskStatusById(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"TaskStatuses({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskStatusById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.TaskStatus>(response);
        }
        partial void OnUpdateTaskStatus(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateTaskStatus(int? id = default(int?), BlazorCrmWasm.Models.Crm.TaskStatus taskStatus = default(BlazorCrmWasm.Models.Crm.TaskStatus))
        {
            var uri = new Uri(baseUri, $"TaskStatuses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", taskStatus.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(taskStatus), Encoding.UTF8, "application/json");

            OnUpdateTaskStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteTaskType(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteTaskType(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"TaskTypes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTaskType(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetTaskTypeById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.TaskType> GetTaskTypeById(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"TaskTypes({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaskTypeById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.TaskType>(response);
        }
        partial void OnUpdateTaskType(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateTaskType(int? id = default(int?), BlazorCrmWasm.Models.Crm.TaskType taskType = default(BlazorCrmWasm.Models.Crm.TaskType))
        {
            var uri = new Uri(baseUri, $"TaskTypes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", taskType.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(taskType), Encoding.UTF8, "application/json");

            OnUpdateTaskType(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteTasklist(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteTasklist(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Tasklists({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTasklist(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetTasklistById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<BlazorCrmWasm.Models.Crm.Tasklist> GetTasklistById(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Tasklists({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTasklistById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<BlazorCrmWasm.Models.Crm.Tasklist>(response);
        }
        partial void OnUpdateTasklist(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateTasklist(int? id = default(int?), BlazorCrmWasm.Models.Crm.Tasklist tasklist = default(BlazorCrmWasm.Models.Crm.Tasklist))
        {
            var uri = new Uri(baseUri, $"Tasklists({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", tasklist.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tasklist), Encoding.UTF8, "application/json");

            OnUpdateTasklist(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}
