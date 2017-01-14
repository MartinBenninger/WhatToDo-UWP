namespace WhatToDo.Views
{
    using System;
    using DAL.IRepositories;
    using Google.Apis.Tasks.v1.Data;
    using ViewModels;

    /// <summary>
    /// The page for editing a task list.
    /// </summary>
    /// <seealso cref="WhatToDo.Views.BaseContentPage"/>
    public partial class EditTaskList : BaseContentPage
    {
        private readonly ITaskListRepository taskListRepository;
        private readonly TaskList taskList;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditTaskList"/> class.
        /// </summary>
        /// <param name="taskListRepository">The DI injected task list repository.</param>
        /// <param name="taskList">The task list.</param>
        public EditTaskList(ITaskListRepository taskListRepository, TaskList taskList)
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

            this.BindingContext = this.GetEditTaskListViewModel();
            this.newTaskListName.Focus();
        }

        /// <summary>
        /// Called when the save button is clicked on the edit task list page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnSaveEditTaskListButtonClicked(object sender, EventArgs e)
        {
            await this.SaveNewTaskListName();
        }

        /// <summary>
        /// Called when the cancel button is clicked on the edit task list page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnCancelEditTaskListButtonClicked(object sender, EventArgs e)
        {
            await this.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Called when the user has ended input by pressing the return key on the keyboard.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnNewTaskListNameCompleted(object sender, EventArgs e)
        {
            await this.SaveNewTaskListName();
        }

        /// <summary>
        /// Saves the new name of the task list and returns to the task list page.
        /// </summary>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        private async System.Threading.Tasks.Task SaveNewTaskListName()
        {
            this.newTaskListName.IsEnabled = false;
            this.taskList.Title = this.newTaskListName.Text;

            await this.taskListRepository.UpdateTaskList(this.taskList);

            await this.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Gets the edit task list view model.
        /// </summary>
        /// <returns>A model containing a task list.</returns>
        private EditTaskListViewModel GetEditTaskListViewModel()
        {
            var viewModel = new EditTaskListViewModel();

            viewModel.TaskList = this.taskList;

            return viewModel;
        }
    }
}