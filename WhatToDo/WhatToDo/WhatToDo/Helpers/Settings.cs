namespace WhatToDo.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any of your
    /// client applications. All settings are laid out the same exact way with getters and setters.
    /// </summary>
    public static class Settings
    {
        private const string TokenKey = "token";
        private static readonly string TokenDefault = string.Empty;

        /// <summary>
        /// Gets or sets the Google tasks API access token.
        /// </summary>
        /// <value>The Google tasks API access token.</value>
        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(TokenKey, TokenDefault);
            }

            set
            {
                AppSettings.AddOrUpdateValue<string>(TokenKey, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether a user is logged in.
        /// </summary>
        /// <value><c>true</c> if a user is logged in; otherwise, <c>false</c>.</value>
        public static bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrWhiteSpace(AccessToken);
            }
        }

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
    }
}