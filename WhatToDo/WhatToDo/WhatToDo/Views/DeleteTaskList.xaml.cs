namespace WhatToDo.Views
{
    using System;
    using DAL.IRepositories;
    using Google.Apis.Tasks.v1.Data;
    using ViewModels;

    /// <summary>
    /// The page for deleting a task list.
    /// </summary>
    /// <seealso cref="WhatToDo.Views.BaseContentPage"/>
    public partial class DeleteTaskList : BaseContentPage
    {
        private readonly ITaskListRepository taskListRepository;
        private readonly TaskList taskList;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTaskList"/> class.
        /// </summary>
        /// <param name="taskListRepository">The task list repository.</param>
        /// <param name="taskList">The task list.</param>
        public DeleteTaskList(ITaskListRepository taskListRepository, TaskList taskList)
        {
            this.InitializeComponent();

            this.taskListRepository = taskListRepository;
            this.taskList = taskList;
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to
        /// the <see cref="T:Xamarin.Forms.Page"/> becoming visible.
        /// </summary>
        /// <remarks>Sets the view model in the binding context.</remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = this.GetDeleteTaskListViewModel();
        }

        /// <summary>
        /// Called when the delete button is clicked on the delete task list page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnDeleteDeleteTaskListButtonClicked(object sender, EventArgs e)
        {
            await this.taskListRepository.DeleteTaskList(this.taskList);

            await this.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Called when the cancel button is clicked on the delete task list page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnCancelDeleteTaskListButtonClicked(object sender, EventArgs e)
        {
            await this.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Gets the delete task list view model.
        /// </summary>
        /// <returns>A model containing a task list.</returns>
        private DeleteTaskListViewModel GetDeleteTaskListViewModel()
        {
            var viewModel = new DeleteTaskListViewModel();

            viewModel.TaskList = this.taskList;

            return viewModel;
        }
    }
}