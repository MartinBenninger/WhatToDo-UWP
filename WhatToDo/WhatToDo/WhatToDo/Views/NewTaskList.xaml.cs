namespace WhatToDo.Views
{
    using System;
    using DAL.IRepositories;
    using Google.Apis.Tasks.v1.Data;
    using ViewModels;

    /// <summary>
    /// The page for creating a new task list.
    /// </summary>
    /// <seealso cref="WhatToDo.Views.BaseContentPage"/>
    public partial class NewTaskList : BaseContentPage
    {
        private readonly ITaskListRepository taskListRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewTaskList"/> class.
        /// </summary>
        /// <param name="taskListRepository">The task list repository.</param>
        public NewTaskList(ITaskListRepository taskListRepository)
        {
            this.InitializeComponent();

            this.taskListRepository = taskListRepository;
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to
        /// the <see cref="T:Xamarin.Forms.Page"/> becoming visible.
        /// </summary>
        /// <remarks>Sets the view model in the binding context.</remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = this.GetNewTaskListViewModel();
            this.newTaskListName.Focus();
        }

        /// <summary>
        /// Called when the save button is clicked on the new task list page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnSaveNewTaskListButtonClicked(object sender, EventArgs e)
        {
            await this.SaveNewTaskList();
        }

        /// <summary>
        /// Called when the cancel button is clicked on the new task list page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void OnCancelNewTaskListButtonClicked(object sender, EventArgs e)
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
            await this.SaveNewTaskList();
        }

        /// <summary>
        /// Saves the new name of the task list and returns to the task list page.
        /// </summary>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        private async System.Threading.Tasks.Task SaveNewTaskList()
        {
            this.newTaskListName.IsEnabled = false;
            var taskList = new TaskList();
            taskList.Title = this.newTaskListName.Text;

            await this.taskListRepository.InsertTaskList(taskList);

            await this.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Gets the new task list view model.
        /// </summary>
        /// <returns>A model for creating a new task list.</returns>
        private BaseViewModel GetNewTaskListViewModel()
        {
            var viewModel = new BaseViewModel();

            viewModel.Title = "New task list";

            return viewModel;
        }
    }
}