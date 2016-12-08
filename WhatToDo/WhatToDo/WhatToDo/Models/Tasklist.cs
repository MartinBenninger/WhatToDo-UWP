namespace WhatToDo.Models
{
    using System;

    /// <summary>
    /// Represents a task list from Google tasks.
    /// </summary>
    /// <see href="https://developers.google.com/google-apps/tasks/v1/reference/tasklists"/>
    public class Tasklist
    {
        /// <summary>
        /// Task list identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ETag of the resource.
        /// </summary>
        public string Etag { get; set; }

        /// <summary>
        /// Title of the task list.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// URL pointing to this task list. Used to retrieve, update, or delete this task list.
        /// </summary>
        public string SelfLink { get; set; }

        /// <summary>
        /// Last modification time of the task list (as System.DateTime from RFC 3339 timestamp).
        /// </summary>
        public DateTime? Updated { get; set; }
    }
}