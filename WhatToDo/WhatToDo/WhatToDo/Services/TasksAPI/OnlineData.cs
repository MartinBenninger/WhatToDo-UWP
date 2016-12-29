namespace WhatToDo.Services.TasksAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Services;
    using Google.Apis.Tasks.v1;
    using Google.Apis.Tasks.v1.Data;
    using Google.Apis.Util.Store;
    using Helpers;
    using Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for using the Google tasks API. (Used by repository classes.)
    /// </summary>
    public class OnlineData
    {
        private const int MaxResults = 100;

        private readonly string listsUrl = "https://www.googleapis.com/tasks/v1/users/@me/lists";
        private readonly string tasksUrl = "https://www.googleapis.com/tasks/v1/lists/"; // + taskListId + "/tasks"
        private readonly string revokeTokenUrl = "https://accounts.google.com/o/oauth2/revoke";

        private readonly GoogleCredentials googleCredentials = GoogleCredentialsHelper.GetCredentials;

        /// <summary>
        /// Gets all task lists.
        /// </summary>
        /// <returns>A list of all task lists.</returns>
        public async System.Threading.Tasks.Task<List<TaskList>> GetAllTaskLists()
        {
            return await this.GetAllPages<TaskList, TaskLists>(this.listsUrl);
        }

        /// <summary>
        /// Gets all tasks from a task list.
        /// </summary>
        /// <param name="taskListId">The task list identifier.</param>
        /// <returns>A list of all tasks in a task list.</returns>
        public async System.Threading.Tasks.Task<List<Task>> GetAllTasksFromTaskList(string taskListId)
        {
            return await this.GetAllPages<Task, Tasks>(this.tasksUrl + taskListId + "/tasks");
        }

        /// <summary>
        /// Updates the task list.
        /// </summary>
        /// <param name="taskList">The task list to update.</param>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        public async System.Threading.Tasks.Task UpdateTaskList(TaskList taskList)
        {
            await this.RequestWithAccessTokenAsync((u, c) => new HttpClient().PutAsync(u, c), taskList.SelfLink, string.Empty, new StringContent(JsonConvert.SerializeObject(taskList), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the task list.
        /// </summary>
        /// <param name="taskList">The task list to delete.</param>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        public async System.Threading.Tasks.Task DeleteTaskList(TaskList taskList)
        {
            await this.RequestWithAccessTokenAsync((u, c) => new HttpClient().DeleteAsync(u), taskList.SelfLink);
        }

        /// <summary>
        /// Gets a new access token using the refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>The new access token.</returns>
        public async System.Threading.Tasks.Task<string> RefreshAccessToken(string refreshToken)
        {
            var httpClient = new HttpClient();
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "refresh_token", refreshToken },
                { "client_id", this.googleCredentials.ClientId },
                { "client_secret", this.googleCredentials.ClientSecret },
                { "grant_type", "refresh_token" }
            });
            var response = await httpClient.PostAsync(new Uri(this.googleCredentials.TokenUri), content);
            var responseDictionary = this.GetObjectFromResponse<Dictionary<string, string>>(response);

            return responseDictionary.ContainsKey("access_token") ? responseDictionary["access_token"] : string.Empty;
        }

        /// <summary>
        /// Revokes the refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token to revoke.</param>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        public async System.Threading.Tasks.Task RevokeRefreshToken(string refreshToken)
        {
            await new HttpClient().GetAsync(new Uri(this.revokeTokenUrl + "?token=" + refreshToken));
        }

        /// <summary>
        /// Creates a request using the provided method, url, parameters (optional), and content
        /// (optional). The access token is added to the request. If the request is unauthorized the
        /// access token is refreshed and the request is made again.
        /// </summary>
        /// <param name="requestAsync">The asynchronous request method.</param>
        /// <param name="url">The URL.</param>
        /// <param name="queryParameters">The query parameters.</param>
        /// <param name="content">The content (for requests like POST / PUT).</param>
        /// <returns>The HttpResponseMessage.</returns>
        private async System.Threading.Tasks.Task<HttpResponseMessage> RequestWithAccessTokenAsync(Func<Uri, HttpContent, System.Threading.Tasks.Task<HttpResponseMessage>> requestAsync, string url, string queryParameters = "", HttpContent content = null)
        {
            var response = await requestAsync(new Uri(url + "?access_token=" + Settings.AccessToken + queryParameters), content);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Settings.AccessToken = await this.RefreshAccessToken(Settings.RefreshToken);
                response = await requestAsync(new Uri(url + "?access_token=" + Settings.AccessToken + queryParameters), content);
            }

            return response;
        }

        /// <summary>
        /// Gets all items on all pages.
        /// </summary>
        /// <typeparam name="T">The type of the items to get.</typeparam>
        /// <typeparam name="TS">The type that contains the items to get.</typeparam>
        /// <param name="url">The URL.</param>
        /// <returns>A list of items.</returns>
        private async System.Threading.Tasks.Task<List<T>> GetAllPages<T, TS>(string url)
             where T : class
             where TS : class
        {
            var itemsList = new List<T>();
            TS itemsPage = null;
            string nextPageToken = null;
            HttpResponseMessage response = null;
            var httpClient = new HttpClient();

            do
            {
                response = await this.RequestWithAccessTokenAsync((u, c) => httpClient.GetAsync(u), url, "&maxResults=" + MaxResults + (!string.IsNullOrWhiteSpace(nextPageToken) ? "&pageToken=" + nextPageToken : string.Empty));
                itemsPage = this.GetObjectFromResponse<TS>(response);
                nextPageToken = ((dynamic)itemsPage)?.NextPageToken;

                if (itemsPage != null && ((dynamic)itemsPage).Items != null)
                {
                    itemsList.AddRange(((dynamic)itemsPage).Items);
                }
            }
            while (!string.IsNullOrWhiteSpace(nextPageToken));

            return itemsList;
        }

        /// <summary>
        /// Gets the deserialized object from response.
        /// </summary>
        /// <typeparam name="T">The type of object to get.</typeparam>
        /// <param name="response">The response.</param>
        /// <returns>The deserialized object.</returns>
        private T GetObjectFromResponse<T>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }
    }
}