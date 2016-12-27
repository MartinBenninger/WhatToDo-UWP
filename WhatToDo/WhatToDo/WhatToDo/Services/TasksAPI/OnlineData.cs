namespace WhatToDo.Services.TasksAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
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
        private readonly string listsUrl = "https://www.googleapis.com/tasks/v1/users/@me/lists";

        private readonly GoogleCredentials googleCredentials = GoogleCredentialsHelper.GetCredentials;

        /// <summary>
        /// Refreshes the access token.
        /// </summary>
        /// <returns>Gets a new access token using the refresh token.</returns>
        public async System.Threading.Tasks.Task RefreshAccessToken()
        {
            var httpClient = new HttpClient();
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "refresh_token", Settings.RefreshToken },
                { "client_id", this.googleCredentials.ClientId },
                { "client_secret", this.googleCredentials.ClientSecret },
                { "grant_type", "refresh_token" }
            });
            var response = await httpClient.PostAsync(new Uri(this.googleCredentials.TokenUri), content);
            var responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);

            Settings.AccessToken = responseDictionary.ContainsKey("access_token") ? responseDictionary["access_token"] : string.Empty;
        }

        /// <summary>
        /// Gets all task lists.
        /// </summary>
        /// <returns>A list of all task lists.</returns>
        public async System.Threading.Tasks.Task<List<TaskList>> GetAllTaskLists()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(this.listsUrl + "?access_token=" + Settings.AccessToken));

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await this.RefreshAccessToken();
                response = await httpClient.GetAsync(new Uri(this.listsUrl + "?access_token=" + Settings.AccessToken));
            }

            var taskLists = JsonConvert.DeserializeObject<TaskLists>(response.Content.ReadAsStringAsync().Result);

            return taskLists.Items.ToList();
        }
    }
}