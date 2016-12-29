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
    /// <seealso cref="WhatToDo.DAL.IRepositories.ITasklistRepository"/>
    public class TasklistRepository : ITasklistRepository
    {
        private readonly OnlineData onlineData = new OnlineData();

        /// <summary>
        /// Gets all task lists.
        /// </summary>
        /// <returns>A list of all task lists.</returns>
        public async System.Threading.Tasks.Task<List<TaskList>> GetAllTasklists()
        {
            return await System.Threading.Tasks.Task.Run(() => this.onlineData.GetAllTaskLists().Result.OrderBy(t => t.Title).ToList());
        }
    }
}