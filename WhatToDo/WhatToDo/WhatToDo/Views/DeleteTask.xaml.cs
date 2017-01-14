namespace WhatToDo.Views
{
    using System;
    using DAL.IRepositories;
    using Google.Apis.Tasks.v1.Data;
    using ViewModels;

    /// <summary>
    /// The page for deleting a task.
    /// </summary>
    /// <seealso cref="WhatToDo.Views.BaseContentPage"/>
    public partial class DeleteTask : BaseContentPage
    {
        private readonly ITaskRepository taskRepository;
        private readonly TaskList taskList;
        private readonly Task task;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTask"/> class.
        /// </summary>
        /// <param name="taskRepository">The task list repository.</param>
        /// <param name="taskList">The task list.</param>
        /// <param name="task">The task.</param>
        public DeleteTask(ITaskRepository taskRepository, TaskList taskList, Task task)
        {
            this.InitializeComponent();

            this.taskRepository = taskRepository;
            this.taskList = taskList;
            this.task = task;
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to
        /// the <see cref="T:Xamarin.Forms.Page"/> becoming visible.
        /// </summary>
        /// <remarks>Sets the view model in the binding context.</remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = this.GetDeleteTaskViewModel();
        }

        /// <summary>
        /// Called when the delete button is clicked on the delete task page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnDeleteDeleteTaskButtonClicked(object sender, EventArgs e)
        {
            await this.taskRepository.DeleteTask(this.taskList, this.task);

            await this.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Called when the cancel button is clicked on the delete task page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnCancelDeleteTaskButtonClicked(object sender, EventArgs e)
        {
            await this.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Gets the delete task view model.
        /// </summary>
        /// <returns>A model containing a task.</returns>
        private DeleteTaskViewModel GetDeleteTaskViewModel()
        {
            var viewModel = new DeleteTaskViewModel();

            viewModel.Task = this.task;

            return viewModel;
        }
    }
}