namespace WhatToDo.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a task from Google tasks.
    /// </summary>
    /// <see href="https://developers.google.com/google-apps/tasks/v1/reference/tasks"/>
    public class Task
    {
        /// <summary>
        /// Task identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ETag of the resource.
        /// </summary>
        public string Etag { get; set; }

        /// <summary>
        /// Title of the task.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Last modification time of the task (as System.DateTime from RFC 3339 timestamp).
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// URL pointing to this task. Used to retrieve, update, or delete this task.
        /// </summary>
        public string SelfLink { get; set; }

        /// <summary>
        /// Parent task identifier. This field is omitted if it is a top-level task. This field is
        /// read-only. Use the "move" method to move the task under a different parent or to the top level.
        /// </summary>
        public string Parent { get; set; }

        /// <summary>
        /// String indicating the position of the task among its sibling tasks under the same parent
        /// task or at the top level. If this string is greater than another task's corresponding
        /// position string according to lexicographical ordering, the task is positioned after the
        /// other task under the same parent task (or at the top level). This field is read-only. Use
        /// the "move" method to move the task to another position.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Notes describing the task. Optional.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Status of the task. This is either "needsAction" or "completed".
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Due date of the task (as System.DateTime from RFC 3339 timestamp). Optional.
        /// </summary>
        public DateTime? Due { get; set; }

        /// <summary>
        /// Completion date of the task (as System.DateTime from RFC 3339 timestamp). This field is
        /// omitted if the task has not been completed.
        /// </summary>
        public DateTime? Completed { get; set; }

        /// <summary>
        /// Flag indicating whether the task has been deleted. The default is False.
        /// </summary>
        public bool? Deleted { get; set; }

        /// <summary>
        /// Flag indicating whether the task is hidden. This is the case if the task had been marked
        /// completed when the task list was last cleared. The default is False. This field is read-only.
        /// </summary>
        public bool? Hidden { get; set; }

        /// <summary>
        /// Collection of links. This collection is read-only.
        /// </summary>
        public IList<LinksData> Links { get; set; }

        /// <summary>
        /// Helper class for storing links.
        /// </summary>
        public class LinksData
        {
            /// <summary>
            /// Type of the link, e.g. "email".
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// The description. In HTML speak: Everything between <a>and</a>.
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// The URL.
            /// </summary>
            public string Link { get; set; }
        }
    }
}