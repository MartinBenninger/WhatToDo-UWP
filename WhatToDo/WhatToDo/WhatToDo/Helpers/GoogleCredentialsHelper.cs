namespace WhatToDo.Helpers
{
    using System.IO;
    using Models;
    using Newtonsoft.Json;

    /// <summary>
    /// A helper for getting the constants for creating an OAuth connection to Google.
    /// </summary>
    public static class GoogleCredentialsHelper
    {
        /// <summary>
        /// Gets an object containing the Google access constants.
        /// </summary>
        public static GoogleCredentials GetCredentials
        {
            get
            {
                if (Credentials == null)
                {
                    var stream = ResourceLoader.GetEmbeddedResourceStream("client_id.json");

                    using (var reader = new StreamReader(stream))
                    {
                        var json = reader.ReadToEnd();
                        var rootObject = JsonConvert.DeserializeObject<GoogleCredentialsRoot>(json);
                        Credentials = rootObject.GoogleCredentials;
                    }
                }

                return Credentials;
            }
        }

        /// <summary>
        /// Gets the credentials.
        /// </summary>
        private static GoogleCredentials Credentials { get; set; }
    }
}