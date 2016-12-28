namespace WhatToDo.ViewModels
{
    using System.Collections.Generic;
    using Google.Apis.Tasks.v1.Data;

    /// <summary>
    /// The view model for the tasks page.
    /// </summary>
    /// <seealso cref="WhatToDo.ViewModels.BaseViewModel"/>
    public class TasksViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TasksViewModel"/> class.
        /// </summary>
        public TasksViewModel()
        {
            this.Title = "Tasks";
            this.Tasks = new List<Task>();
        }

        /// <summary>
        /// A list of all tasks in a task list.
        /// </summary>
        public List<Task> Tasks { get; set; }
    }
}