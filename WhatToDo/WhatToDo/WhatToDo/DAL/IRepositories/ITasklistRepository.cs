namespace WhatToDo.DAL.IRepositories
{
    using System.Collections.Generic;
    using Google.Apis.Tasks.v1.Data;

    /// <summary>
    /// The interface for the task list repository class.
    /// </summary>
    public interface ITaskListRepository
    {
        /// <summary>
        /// Gets all task lists.
        /// </summary>
        /// <returns>A list of all task lists.</returns>
        System.Threading.Tasks.Task<List<TaskList>> GetAllTaskLists();

        /// <summary>
        /// Inserts the task list.
        /// </summary>
        /// <param name="taskList">The task list to insert.</param>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        System.Threading.Tasks.Task InsertTaskList(TaskList taskList);

        /// <summary>
        /// Updates the task list.
        /// </summary>
        /// <param name="taskList">The task list to update.</param>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        System.Threading.Tasks.Task UpdateTaskList(TaskList taskList);

        /// <summary>
        /// Deletes the task list.
        /// </summary>
        /// <param name="taskList">The task list to delete.</param>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        System.Threading.Tasks.Task DeleteTaskList(TaskList taskList);
    }
}