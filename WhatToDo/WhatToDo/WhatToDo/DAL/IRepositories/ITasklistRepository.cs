namespace WhatToDo.DAL.IRepositories
{
    using System.Collections.Generic;
    using Google.Apis.Tasks.v1.Data;

    /// <summary>
    /// The interface for the task list repository class.
    /// </summary>
    public interface ITasklistRepository
    {
        /// <summary>
        /// Gets all task lists.
        /// </summary>
        /// <returns>A list of all task lists.</returns>
        System.Threading.Tasks.Task<List<TaskList>> GetAllTasklists();
    }
}