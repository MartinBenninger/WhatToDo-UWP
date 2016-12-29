namespace WhatToDo.ViewModels
{
    using Google.Apis.Tasks.v1.Data;

    /// <summary>
    /// View model for the edit task list page.
    /// </summary>
    /// <seealso cref="WhatToDo.ViewModels.BaseViewModel"/>
    public class EditTaskListViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditTaskListViewModel"/> class.
        /// </summary>
        public EditTaskListViewModel()
        {
            this.Title = "Edit task list";
        }

        /// <summary>
        /// The task list to edit.
        /// </summary>
        public TaskList TaskList { get; set; }
    }
}