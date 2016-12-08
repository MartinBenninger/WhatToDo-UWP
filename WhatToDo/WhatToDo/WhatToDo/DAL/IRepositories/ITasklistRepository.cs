namespace WhatToDo.DAL.IRepositories
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// The interface for the task list repository class.
    /// </summary>
    public interface ITasklistRepository
    {
        /// <summary>
        /// Gets all task lists.
        /// </summary>
        /// <returns>A list of all task lists.</returns>
        List<Tasklist> GetAllTasklists();
    }
}