namespace WhatToDo.ViewModels
{
    using System.Collections.Generic;
    using Models;

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
            this.Tasklists = new List<Tasklist>();
        }

        /// <summary>
        /// A list of all task lists.
        /// </summary>
        public List<Tasklist> Tasklists { get; set; }
    }
}