namespace WhatToDo.ViewModels
{
    using System.Collections.Generic;
    using Google.Apis.Tasks.v1.Data;

    /// <summary>
    /// The view model for the task lists page.
    /// </summary>
    /// <seealso cref="WhatToDo.ViewModels.BaseViewModel"/>
    public class TaskListsViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskListsViewModel"/> class.
        /// </summary>
        public TaskListsViewModel()
        {
            this.Title = "Task Lists";
            this.TaskLists = new List<TaskList>();
        }

        /// <summary>
        /// A list of all task lists.
        /// </summary>
        public List<TaskList> TaskLists { get; set; }
    }
}