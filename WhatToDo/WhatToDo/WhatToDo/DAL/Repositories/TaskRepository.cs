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
    }
}