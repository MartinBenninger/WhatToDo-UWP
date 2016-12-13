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
        private const string AccessTokenKey = "AccessToken";
        private const string RefreshTokenKey = "RefreshToken";
        private static readonly string AccessTokenDefault = string.Empty;
        private static readonly string RefreshTokenDefault = string.Empty;

        /// <summary>
        /// Gets or sets the Google tasks API access token.
        /// </summary>
        /// <value>The Google tasks API access token.</value>
        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(AccessTokenKey, AccessTokenDefault);
            }

            set
            {
                AppSettings.AddOrUpdateValue<string>(AccessTokenKey, value);
            }
        }

        /// <summary>
        /// Gets or sets the Google tasks API refresh token.
        /// </summary>
        /// <value>The Google tasks API refresh token.</value>
        public static string RefreshToken
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(RefreshTokenKey, RefreshTokenDefault);
            }

            set
            {
                AppSettings.AddOrUpdateValue<string>(RefreshTokenKey, value);
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
                return !string.IsNullOrWhiteSpace(RefreshToken);
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