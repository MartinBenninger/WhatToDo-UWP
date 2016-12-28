namespace WhatToDo.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// The root credential class for converting the client_id.json file to a GoogleCredentials object.
    /// </summary>
    [JsonObject]
    public class GoogleCredentialsRoot
    {
        /// <summary>
        /// The Google credentials.
        /// </summary>
        [JsonProperty("web")]
        public GoogleCredentials GoogleCredentials { get; set; }
    }
}