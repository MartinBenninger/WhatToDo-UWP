namespace WhatToDo.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the Google credentials stored in the client_id.json file.
    /// </summary>
    [JsonObject]
    public class GoogleCredentials
    {
        /// <summary>
        /// The client identifier
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// The project identifier
        /// </summary>
        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        /// <summary>
        /// The authentication URI
        /// </summary>
        [JsonProperty("auth_uri")]
        public string AuthUri { get; set; }

        /// <summary>
        /// The token URI
        /// </summary>
        [JsonProperty("token_uri")]
        public string TokenUri { get; set; }

        /// <summary>
        /// The authentication provider cert URL
        /// </summary>
        [JsonProperty("auth_provider_x509_cert_url")]
        public string AuthProviderCertUrl { get; set; }

        /// <summary>
        /// The client secret
        /// </summary>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>
        /// The redirect uris
        /// </summary>
        [JsonProperty("redirect_uris")]
        public List<string> RedirectUris { get; set; }
    }
}