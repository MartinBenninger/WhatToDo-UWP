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
            var responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);

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
            var response = await this.GetUrlWithAccessTokenAsync(this.listsUrl);
            var taskLists = JsonConvert.DeserializeObject<TaskLists>(response.Content.ReadAsStringAsync().Result);

            return (taskLists?.Items ?? Enumerable.Empty<TaskList>()).ToList();
        }

        /// <summary>
        /// Gets the URL with access token asynchronously. Refreshes the access token if needed.
        /// </summary>
        /// <param name="url">The URL to get.</param>
        /// <returns>The HttpResponseMessage.</returns>
        private async System.Threading.Tasks.Task<HttpResponseMessage> GetUrlWithAccessTokenAsync(string url)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(url + "?access_token=" + Settings.AccessToken));

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Settings.AccessToken = await this.RefreshAccessToken(Settings.RefreshToken);
                response = await httpClient.GetAsync(new Uri(url + "?access_token=" + Settings.AccessToken));
            }

            return response;
        }
    }
}