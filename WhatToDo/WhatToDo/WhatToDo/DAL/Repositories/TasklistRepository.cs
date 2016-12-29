namespace WhatToDo.DAL.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Google.Apis.Tasks.v1.Data;
    using IRepositories;
    using Services.TasksAPI;

    /// <summary>
    /// The task list repository class.
    /// </summary>
    /// <seealso cref="WhatToDo.DAL.IRepositories.ITaskListRepository"/>
    public class TaskListRepository : ITaskListRepository
    {
        private readonly OnlineData onlineData = new OnlineData();

        /// <summary>
        /// Gets all task lists.
        /// </summary>
        /// <returns>A list of all task lists.</returns>
        public async System.Threading.Tasks.Task<List<TaskList>> GetAllTaskLists()
        {
            return await System.Threading.Tasks.Task.Run(() => this.onlineData.GetAllTaskLists().Result.OrderBy(t => t.Title).ToList());
        }

        /// <summary>
        /// Updates the task list.
        /// </summary>
        /// <param name="taskList">The task list to update.</param>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        public async System.Threading.Tasks.Task UpdateTaskList(TaskList taskList)
        {
            await this.onlineData.UpdateTaskList(taskList);
        }
    }
}