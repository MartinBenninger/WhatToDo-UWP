using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WhatToDo.Helpers;
using WhatToDo.UWP.ViewRenderers;
using WhatToDo.Views;
using Windows.Security.Authentication.Web;
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

        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (isShown)
            {
                return;
            }

            isShown = true;

            var code = await AuthenticateUsingWebAuthenticationBrocker();

            if (code != null)
            {
                Settings.RefreshToken = code;
                await NavigationHelper.Current.NavigateLoginSuccess();
            }
            else
            {
                await NavigationHelper.Current.NavigateLoginFailure();
            }
        }

        private async Task<string> AuthenticateUsingWebAuthenticationBrocker()
        {
            var googleCredentials = Helpers.GoogleCredentialsHelper.GetCredentials;
            var googleUrl = googleCredentials.AuthUri
                + "?client_id=" + googleCredentials.ClientId
                + "&redirect_uri=" + (googleCredentials.RedirectUris.FirstOrDefault() ?? string.Empty)
                + "&response_type=code"
                + "&scope=email";

            var startUri = new Uri(googleUrl);
            var endUri = new Uri(googleCredentials.RedirectUris.FirstOrDefault() ?? string.Empty);

            var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);

            return webAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success ?
                webAuthenticationResult.ResponseData.Substring(webAuthenticationResult.ResponseData.IndexOf('=') + 1) :
                null;
        }
    }
}