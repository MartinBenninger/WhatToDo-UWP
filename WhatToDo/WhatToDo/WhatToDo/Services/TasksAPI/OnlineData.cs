namespace WhatToDo.Services.TasksAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
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
        /// <returns>Null</returns>
        public async System.Threading.Tasks.Task RevokeRefreshToken(string refreshToken)
        {
            await new HttpClient().GetAsync(new Uri(this.revokeTokenUrl + "?token=" + refreshToken));
        }

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
        /// Gets the URL with access token asynchronously. Refreshes the access token if needed.
        /// </summary>
        /// <param name="url">The URL to get.</param>
        /// <param name="queryParameters">Optional query parameters.</param>
        /// <returns>The HttpResponseMessage.</returns>
        private async System.Threading.Tasks.Task<HttpResponseMessage> GetUrlWithAccessTokenAsync(string url, string queryParameters = "")
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(url + "?access_token=" + Settings.AccessToken + queryParameters));

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Settings.AccessToken = await this.RefreshAccessToken(Settings.RefreshToken);
                response = await httpClient.GetAsync(new Uri(url + "?access_token=" + Settings.AccessToken + queryParameters));
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

            do
            {
                response = await this.GetUrlWithAccessTokenAsync(url, "&maxResults=" + MaxResults + (!string.IsNullOrWhiteSpace(nextPageToken) ? "&pageToken=" + nextPageToken : string.Empty));
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