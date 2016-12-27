namespace WhatToDo.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using Google.Apis.Tasks.v1.Data;
    using IRepositories;
    using Models;
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
            return await this.onlineData.GetAllTaskLists();
        }
    }
}