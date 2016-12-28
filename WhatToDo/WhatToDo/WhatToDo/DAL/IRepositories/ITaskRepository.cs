namespace WhatToDo.DAL.IRepositories
{
    using System.Collections.Generic;
    using Google.Apis.Tasks.v1.Data;

    /// <summary>
    /// The interface for the task repository class.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Gets all task lists.
        /// </summary>
        /// <param name="taskListId">The task list identifier.</param>
        /// <returns>A list of all task lists.</returns>
        System.Threading.Tasks.Task<List<Task>> GetAllTasksFromTaskList(string taskListId);
    }
}