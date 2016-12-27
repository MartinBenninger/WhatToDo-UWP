using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WhatToDo.Helpers;
using WhatToDo.Models;
using WhatToDo.UWP.ViewRenderers;
using WhatToDo.Views;
using Windows.Security.Authentication.Web;
using Windows.Web.Http;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Login), typeof(LoginRenderer))]

namespace WhatToDo.UWP.ViewRenderers
{
    /// <summary>
    /// The login page for the UWP platform.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage"/>
    public class LoginRenderer : PageRenderer
    {
        private bool isShown = false;
        private GoogleCredentials googleCredentials = GoogleCredentialsHelper.GetCredentials;

        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (isShown)
            {
                return;
            }

            isShown = true;

            var code = await GetAuthorizationCode();

            if (code != null)
            {
                await GetTokenFromCode(code);
                await NavigationHelper.Current.NavigateLoginSuccess();
            }
            else
            {
                await NavigationHelper.Current.NavigateLoginFailure();
            }
        }

        private async System.Threading.Tasks.Task<string> GetAuthorizationCode()
        {
            var googleUrl = this.googleCredentials.AuthUri
                + "?client_id=" + this.googleCredentials.ClientId
                + "&redirect_uri=" + (this.googleCredentials.RedirectUris.FirstOrDefault() ?? string.Empty)
                + "&response_type=code"
                + "&scope=https://www.googleapis.com/auth/tasks"
                + "&access_type=offline";

            var startUri = new Uri(googleUrl);
            var endUri = new Uri(this.googleCredentials.RedirectUris.FirstOrDefault() ?? string.Empty);

            var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);

            return webAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success ?
                webAuthenticationResult.ResponseData.Substring(webAuthenticationResult.ResponseData.IndexOf('=') + 1) :
                null;
        }

        private async System.Threading.Tasks.Task GetTokenFromCode(string code)
        {
            var httpClient = new HttpClient();
            var content = new HttpFormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", this.googleCredentials.ClientId },
                { "client_secret", this.googleCredentials.ClientSecret },
                { "redirect_uri", this.googleCredentials.RedirectUris.FirstOrDefault() ?? string.Empty },
                { "grant_type", "authorization_code" }
            });
            var tokenResponse = await httpClient.PostAsync(new Uri(this.googleCredentials.TokenUri), content);
            var responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenResponse.Content.ToString());

            Settings.RefreshToken = responseDictionary.ContainsKey("refresh_token") ? responseDictionary["refresh_token"] : string.Empty;
            Settings.AccessToken = responseDictionary.ContainsKey("access_token") ? responseDictionary["access_token"] : string.Empty;
        }
    }
}