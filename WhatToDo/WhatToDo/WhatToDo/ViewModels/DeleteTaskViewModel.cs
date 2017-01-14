namespace WhatToDo.ViewModels
{
    using Google.Apis.Tasks.v1.Data;

    /// <summary>
    /// View model for the delete task page.
    /// </summary>
    /// <seealso cref="WhatToDo.ViewModels.BaseViewModel"/>
    public class DeleteTaskViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTaskViewModel"/> class.
        /// </summary>
        public DeleteTaskViewModel()
        {
            this.Title = "Delete task";
        }

        /// <summary>
        /// The task to delete.
        /// </summary>
        public Task Task { get; set; }
    }
}