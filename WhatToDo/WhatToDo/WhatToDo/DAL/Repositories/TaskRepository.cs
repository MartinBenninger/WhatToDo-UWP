namespace WhatToDo.DAL.Repositories
{
    using System.Collections.Generic;
    using Google.Apis.Tasks.v1.Data;
    using IRepositories;
    using Services.TasksAPI;

    /// <summary>
    /// The task repository class.
    /// </summary>
    /// <seealso cref="WhatToDo.DAL.IRepositories.ITaskRepository"/>
    public class TaskRepository : ITaskRepository
    {
        private readonly OnlineData onlineData = new OnlineData();

        /// <summary>
        /// Gets all task lists.
        /// </summary>
        /// <param name="taskListId">The task list identifier.</param>
        /// <returns>A list of all task lists.</returns>
        public async System.Threading.Tasks.Task<List<Task>> GetAllTasksFromTaskList(string taskListId)
        {
            return await this.onlineData.GetAllTasksFromTaskList(taskListId);
        }

        /// <summary>
        /// Inserts the task.
        /// </summary>
        /// <param name="taskList">The task list to insert the task into.</param>
        /// <param name="task">The task to insert.</param>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        public async System.Threading.Tasks.Task InsertTask(TaskList taskList, Task task)
        {
            await this.onlineData.InsertTask(task, taskList?.Id);
        }

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="taskList">The task list containing the task.</param>
        /// <param name="task">The task to update.</param>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        public async System.Threading.Tasks.Task UpdateTask(TaskList taskList, Task task)
        {
            await this.onlineData.UpdateTask(task);
        }

        /// <summary>
        /// Deletes the task.
        /// </summary>
        /// <param name="taskList">The task list containing the task.</param>
        /// <param name="task">The task to delete.</param>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        public async System.Threading.Tasks.Task DeleteTask(TaskList taskList, Task task)
        {
            await this.onlineData.DeleteTask(task);
        }
    }
}