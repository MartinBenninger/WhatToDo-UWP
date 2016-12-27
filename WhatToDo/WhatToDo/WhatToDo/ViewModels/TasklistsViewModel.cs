namespace WhatToDo.ViewModels
{
    using System.Collections.Generic;
    using Google.Apis.Tasks.v1.Data;

    /// <summary>
    /// The view model for the task lists page.
    /// </summary>
    /// <seealso cref="WhatToDo.ViewModels.BaseViewModel"/>
    public class TasklistsViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TasklistsViewModel"/> class.
        /// </summary>
        public TasklistsViewModel()
        {
            this.Title = "Lists";
            this.Tasklists = new List<TaskList>();
        }

        /// <summary>
        /// A list of all task lists.
        /// </summary>
        public List<TaskList> Tasklists { get; set; }
    }
}