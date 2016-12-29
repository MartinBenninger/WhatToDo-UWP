namespace WhatToDo.ViewModels
{
    using Google.Apis.Tasks.v1.Data;

    /// <summary>
    /// View model for the delete task list page.
    /// </summary>
    /// <seealso cref="WhatToDo.ViewModels.BaseViewModel"/>
    public class DeleteTaskListViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTaskListViewModel"/> class.
        /// </summary>
        public DeleteTaskListViewModel()
        {
            this.Title = "Delete task list";
        }

        /// <summary>
        /// The task list to edit.
        /// </summary>
        public TaskList TaskList { get; set; }
    }
}