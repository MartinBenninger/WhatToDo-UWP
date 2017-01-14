namespace WhatToDo.ViewModels
{
    using Google.Apis.Tasks.v1.Data;

    /// <summary>
    /// View model for the edit task list page.
    /// </summary>
    /// <seealso cref="WhatToDo.ViewModels.BaseViewModel"/>
    public class EditTaskViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditTaskViewModel"/> class.
        /// </summary>
        public EditTaskViewModel()
        {
            this.Title = "Edit task";
        }

        /// <summary>
        /// The task list to edit.
        /// </summary>
        public Task Task { get; set; }
    }
}